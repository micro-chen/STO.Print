//-------------------------------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2014 , STO , Ltd .
//-------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace STO.Print
{
    using DevExpress.XtraEditors;
    using STO.Print.AddBillForm;
    using STO.Print.Model;
    using STO.Print.Utilities;

    /// <summary>
    /// 根据订单号重新生成新的订单号码
    ///
    /// 修改纪录
    ///
    ///		2014-5-29    版本：1.0 Yanghenglian 创建主键，注意命名空间的排序。
    /// 
    /// 版本：1.0
    ///
    /// <author>
    ///		<name>Yanghenglian</name>
    ///		<date>2014-5-29</date>
    /// </author>
    /// </summary>
    public partial class FrmBuildOrderNumber : BaseForm
    {
        private readonly List<Model.ZtoPrintBillEntity> _list;

        public FrmBuildOrderNumber()
        {
            InitializeComponent();
        }

        public FrmBuildOrderNumber(List<Model.ZtoPrintBillEntity> list)
        {
            InitializeComponent();
            this._list = list;
        }

        /// <summary>
        /// 清空订单号
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnBillClear_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            txtOrderNumbers.Text = "";
        }

        /// <summary>
        /// 复制
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCopy_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            STO.Print.Utilities.ClipboardHepler.SetText(txtOrderNumbers.Text);
        }

        /// <summary>
        /// 粘贴
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPaste_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            txtOrderNumbers.Text = STO.Print.Utilities.ClipboardHepler.GetText();
        }

        /// <summary>
        /// 重新生成新的订单号
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnBuildOrderNumber_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (STO.Print.Utilities.MessageUtil.ShowYesNoAndTips("是否重新生成订单号，一定要谨慎") == DialogResult.Yes)
            {
                var stateList = new List<object>();
                //输入单号字符串集合
                var orderNumberList = DotNet.Utilities.StringUtil.SplitMobile(txtOrderNumbers.Text, false).ToList();
                if (!orderNumberList.Any())
                {
                    XtraMessageBox.Show("没有任何的订单信息，请在左侧填写订单号");
                    return;
                }
                int rowCount = 0;
                foreach (var orderNumber in orderNumberList)
                {
                    string newOrderNumber = Guid.NewGuid().ToString("N");
                    stateList.Add(new
                                      {
                                          订单号 = orderNumber,
                                          新订单号 = newOrderNumber
                                      });
                    string commandText = string.Format("UPDATE ZTO_PRINT_BILL SET ORDER_NUMBER = '" + newOrderNumber + "' WHERE ORDER_NUMBER = '" + orderNumber + "'");
                    var result = BillPrintHelper.DbHelper.ExecuteNonQuery(commandText);
                    if (result > 0)
                    {
                        ++rowCount;
                    }
                }
                if (stateList.Any())
                {
                    gcStatus.DataSource = stateList;
                    STO.Print.Utilities.MessageUtil.ShowTips(string.Format("成功更新了{0}条订单号，订单号一定不能重复", rowCount));
                }
                else
                {
                    STO.Print.Utilities.MessageUtil.ShowTips("生成失败");
                }
            }
        }

        /// <summary>
        /// 窗体加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrmBuildOrderNumber_Load(object sender, EventArgs e)
        {
            if (_list != null && _list.Any())
            {
                lblTotalOrderNumber.Text = _list.Count.ToString();
                foreach (ZtoPrintBillEntity ztoPrintBillEntity in _list)
                {
                    txtOrderNumbers.Text += ztoPrintBillEntity.OrderNumber + Environment.NewLine;
                }
            }
        }

        /// <summary>
        /// 导出表格
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExportExcel_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            STO.Print.Utilities.ExportHelper.Export(ExportEnum.Xlsx, gcStatus, gvStatus);
        }

        private void FrmBuildOrderNumber_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult = DialogResult.OK;
        }
    }
}
