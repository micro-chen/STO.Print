//-------------------------------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2015 , STO , Ltd .
//-------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using STO.Print.AddBillForm;

namespace STO.Print
{
    using DevExpress.Utils;
    using DevExpress.XtraEditors;
    using DevExpress.XtraEditors.Controls;
    using DotNet.Model;
    using DotNet.Utilities;
    using STO.Print.Manager;
    using STO.Print.Model;
    using STO.Print.Utilities;

    /// <summary>
    /// 大头笔查询窗体
    /// 
    /// 修改记录
    /// 
    ///     2015-09-02  版本：1.0 YangHengLian 创建
    /// 
    /// 版本：1.0
    ///
    /// <author>
    ///		<name>YangHengLian</name>
    ///		<date>2015-09-02</date>
    /// </author>
    /// </summary>
    public partial class FrmZtoSearchPrintMark : BaseForm
    {
        public FrmZtoSearchPrintMark()
        {
            InitializeComponent();
        }
        private void btnSearch_Click(object sender, EventArgs e)
        {
            BindPrintMark();
        }

        private void FrmCheckPrintMark_Load(object sender, EventArgs e)
        {
            BindArea();
        }


        #region private void BindArea() 绑定省市区到内存中

        /// <summary>
        /// 绑定省市区到内存中
        /// </summary>
        private void BindArea()
        {
            try
            {
                var areaDt = BillPrintHelper.GetArea();
                if (areaDt.Rows.Count > 0)
                {
                    GridLookUpEditHelper.GridLookUpEditInit(dgvSearchSendArea, areaDt, BaseAreaEntity.FieldFullName, BaseAreaEntity.FieldFullName);
                    GridLookUpEditHelper.GridLookUpEditInit(dgvSearchReceiveArea, areaDt, BaseAreaEntity.FieldFullName, BaseAreaEntity.FieldFullName);
                    dgvSearchSendArea.EditValueChanging += dgvSearchSendArea_EditValueChanging;
                    dgvSearchSendArea.KeyUp += dgvSearchSendArea_KeyUp;
                    dgvSearchSendArea.Enter += dgvSearchSendArea_Enter;
                    dgvSearchReceiveArea.KeyUp += dgvSearchReceiveArea_KeyUp;
                    dgvSearchReceiveArea.EditValueChanging += dgvSearchReceiveArea_EditValueChanging;
                    dgvSearchReceiveArea.Enter += dgvSearchReceiveArea_Enter;
                }
            }
            catch (Exception ex)
            {
                ProcessException(ex);
            }
        }
        void dgvSearchReceiveArea_KeyUp(object sender, KeyEventArgs e)
        {
            if (string.IsNullOrEmpty(dgvSearchReceiveArea.Text))
            {
                dgvSearchReceiveArea.ShowPopup();
                return;
            }
            GridLookUpEditHelper.FilterLookup(sender, BaseAreaEntity.FieldFullName, BaseAreaEntity.FieldSimpleSpelling);
        }

        void dgvSearchSendArea_KeyUp(object sender, KeyEventArgs e)
        {
            if (string.IsNullOrEmpty(dgvSearchSendArea.Text))
            {
                dgvSearchSendArea.ShowPopup();
                return;
            }
            GridLookUpEditHelper.FilterLookup(sender, BaseAreaEntity.FieldFullName, BaseAreaEntity.FieldSimpleSpelling);
        }


        private void dgvSearchReceiveArea_EditValueChanging(object sender, ChangingEventArgs e)
        {
            if (string.IsNullOrEmpty(dgvSearchReceiveArea.Text)) return;
            BeginInvoke(new MethodInvoker(() => GridLookUpEditHelper.FilterLookup(sender, BaseAreaEntity.FieldFullName, BaseAreaEntity.FieldSimpleSpelling)));
        }

        private void dgvSearchSendArea_EditValueChanging(object sender, ChangingEventArgs e)
        {
            if (string.IsNullOrEmpty(dgvSearchSendArea.Text)) return;
            BeginInvoke(new MethodInvoker(() => GridLookUpEditHelper.FilterLookup(sender, BaseAreaEntity.FieldFullName, BaseAreaEntity.FieldSimpleSpelling)));
        }
        void dgvSearchReceiveArea_Enter(object sender, EventArgs e)
        {
            if (dgvSearchReceiveArea.Text == @"格式：江苏省-苏州市-吴中区")
            {
                dgvSearchReceiveArea.ShowPopup();
            }
        }

        void dgvSearchSendArea_Enter(object sender, EventArgs e)
        {
            if (dgvSearchSendArea.Text == @"格式：江苏省-苏州市-吴中区")
            {
                dgvSearchSendArea.ShowPopup();
            }
        }

        #endregion

        private void BindPrintMark()
        {
            if (dgvSearchSendArea.Text.Trim().Length == 0)
            {
                // 获取系统默认发件人的省市区信息绑定就可以了，网点提供解决方案
                ZtoUserManager userManager = new ZtoUserManager(BillPrintHelper.DbHelper);
                var userList = userManager.GetList<ZtoUserEntity>(new KeyValuePair<string, object>(ZtoUserEntity.FieldIsDefault, 1), new KeyValuePair<string, object>(ZtoUserEntity.FieldIssendorreceive, 1));
                if (userList.Any())
                {
                    dgvSearchSendArea.Text = string.Format("{0}-{1}-{2}", userList.First().Province, userList.First().City, userList.First().County);
                }
                else
                {
                    XtraMessageBox.Show(@"发件区域填写不正确，请重新填写", AppMessage.MSG0000, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            if (dgvSearchReceiveArea.Text.Trim().Length == 0) return;
            var sendAreaArray = dgvSearchSendArea.Text.Split('-');
            if (sendAreaArray.Length != 3)
            {
                sendAreaArray = new[] { "", "", "" };
            }
            var receiveAreaArray = dgvSearchReceiveArea.Text.Split('-');
            if (receiveAreaArray.Length != 3)
            {
                XtraMessageBox.Show(@"收件区域填写不正确，请重新填写", AppMessage.MSG0000, MessageBoxButtons.OK, MessageBoxIcon.Error);
                dgvSearchReceiveArea.Focus();
                return;
            }
            var selectedRemark = new List<string>() { sendAreaArray[0], sendAreaArray[1], sendAreaArray[2] };
            var selectedReceiveMark = new List<string>() { receiveAreaArray[0], receiveAreaArray[1], receiveAreaArray[2] };
            var printMarkEntity = BillPrintHelper.GetZtoPrintMark(string.Join(",", selectedRemark), null, string.Join(",", selectedReceiveMark), null);
            if (printMarkEntity != null)
            {
                txtPrint.Text = printMarkEntity.Result.Mark;
                txtPrintMark.Text = printMarkEntity.Result.PrintMark;
            }
            else
            {
                XtraMessageBox.Show(@"未提取到大头笔信息", AppMessage.MSG0000, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        /// <summary>
        /// 赋值手写大头笔
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCopyPrint_Click(object sender, EventArgs e)
        {
            Clipboard.SetDataObject(txtPrint.Text);
            btnCopyPrint.ShowTip(null, "复制成功", ToolTipLocation.RightBottom, ToolTipType.SuperTip);
        }

        /// <summary>
        /// 复制机打大头笔
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCopyPrintMark_Click(object sender, EventArgs e)
        {
            Clipboard.SetDataObject(txtPrintMark.Text);
            btnCopyPrintMark.ShowTip(null, "复制成功", ToolTipLocation.RightBottom, ToolTipType.SuperTip);
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSearch2_Click(object sender, EventArgs e)
        {
            var sendArea = BaiduMapHelper.GetProvCityDistFromBaiduMap(txtSendAddress.Text);
            var receiveArea = BaiduMapHelper.GetProvCityDistFromBaiduMap(txtReceiveAddress.Text);
            if (sendArea != null && receiveArea != null)
            {
                var selectedRemark = new List<string>() { sendArea.Result.AddressComponent.Province, sendArea.Result.AddressComponent.City, sendArea.Result.AddressComponent.District };
                var selectedReceiveMark = new List<string>() { receiveArea.Result.AddressComponent.Province, receiveArea.Result.AddressComponent.City, receiveArea.Result.AddressComponent.District };
                var printMarkEntity = BillPrintHelper.GetZtoPrintMark(string.Join(",", selectedRemark), txtSendAddress.Text, string.Join(",", selectedReceiveMark), txtReceiveAddress.Text);
                if (printMarkEntity != null)
                {
                    txtMark2.Text = printMarkEntity.Result.Mark;
                    txtPrintMark2.Text = printMarkEntity.Result.PrintMark;
                }
                else
                {
                    XtraMessageBox.Show(@"未提取到大头笔信息", AppMessage.MSG0000, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                XtraMessageBox.Show(@"地址填写不详细", AppMessage.MSG0000, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnCopy1_Click(object sender, EventArgs e)
        {
            Clipboard.SetDataObject(txtMark2.Text);
            btnCopy1.ShowTip(null, "复制成功", ToolTipLocation.RightBottom, ToolTipType.SuperTip);
        }
        private void btnCopy2_Click(object sender, EventArgs e)
        {
            Clipboard.SetDataObject(txtPrintMark2.Text);
            btnCopy2.ShowTip(null, "复制成功", ToolTipLocation.RightBottom, ToolTipType.SuperTip);
        }
    }
}