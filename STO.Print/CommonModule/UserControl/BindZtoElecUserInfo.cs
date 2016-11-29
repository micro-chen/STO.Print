//-------------------------------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2016 , STO , Ltd .
//-------------------------------------------------------------------------------------

using Newtonsoft.Json;
using System;
using System.Windows.Forms;

namespace STO.Print.UserControl
{
    using DevExpress.XtraEditors;
    using DotNet.Utilities;
    using STO.Print.Model;
    using STO.Print.Utilities;

    /// <summary>
    /// 申通电子面单账号绑定用户控件
    /// 
    /// 修改记录
    /// 
    ///     2015-07-10  版本：1.0 YangHengLian 创建
    /// 
    /// 版本：1.0
    ///
    /// <author>
    ///		<name>YangHengLian</name>
    ///		<date>2015-07-18</date>
    /// </author>
    /// </summary>
    public partial class BindZtoElecUserInfo : XtraUserControl
    {
        /// <summary>
        /// 
        /// </summary>
        public BindZtoElecUserInfo()
        {
            InitializeComponent();
            ZtoElecUserInfoEntity elecUserInfoEntity = BillPrintHelper.GetElecUserInfoEntity();
            if (elecUserInfoEntity != null)
            {
                txtCustomerID.Text = elecUserInfoEntity.Kehuid;
                txtCustomerPassword.Text = elecUserInfoEntity.Pwd;
            }
            var elecUserInfoExtendEntity = BillPrintHelper.GetElecUserInfoExtendEntity();
            if (elecUserInfoExtendEntity != null)
            {
                txtSiteCode.Text = elecUserInfoExtendEntity.siteCode;
                txtSiteName.Text = elecUserInfoExtendEntity.siteName;
            }
        }

        private void btnGetElecCount_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtCustomerID.Text.Trim()))
            {
                txtCustomerID.Focus();
                txtCustomerID.Select();
                txtCustomerID.ShowTip("商家ID必填");
                return;
            }
            if (string.IsNullOrEmpty(txtCustomerPassword.Text.Trim()))
            {
                txtCustomerPassword.Focus();
                txtCustomerPassword.Select();
                txtCustomerPassword.ShowTip("商家ID密码必填");
                return;
            }
            var elecUserInfoEntity = new ZtoElecUserInfoEntity();
            elecUserInfoEntity.Kehuid = txtCustomerID.Text.Replace(" ", "");
            elecUserInfoEntity.Pwd = txtCustomerPassword.Text.Replace(" ", "");
            var result = ZtoElecBillHelper.GetElecBillCount(elecUserInfoEntity);
            int elecBillCount = 0;
            if (int.TryParse(result, out elecBillCount))
            {
                XtraMessageBox.Show(string.Format("商家ID为：{1}可用线下电子面单数量为：{0}条", result, elecUserInfoEntity.Kehuid), AppMessage.MSG0000, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                XtraMessageBox.Show(result, AppMessage.MSG0000, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtCustomerID.Text.Trim()))
            {
                txtCustomerID.Focus();
                txtCustomerID.Select();
                txtCustomerID.ShowTip("商家ID必填");
                return;
            }
            if (string.IsNullOrEmpty(txtCustomerPassword.Text.Trim()))
            {
                txtCustomerPassword.Focus();
                txtCustomerPassword.Select();
                txtCustomerPassword.ShowTip("商家ID密码必填");
                return;
            }
            if (string.IsNullOrEmpty(txtSiteName.Text.Trim()))
            {
                txtSiteName.Focus();
                txtSiteName.Select();
                txtSiteName.ShowTip("所属网点名称必填");
                return;
            }
            if (string.IsNullOrEmpty(txtSiteCode.Text.Trim()))
            {
                txtSiteCode.Focus();
                txtSiteCode.Select();
                txtSiteCode.ShowTip("所属网点编号必填");
                return;
            }
            if (!string.IsNullOrEmpty(txtCustomerID.Text) && !string.IsNullOrEmpty(txtCustomerPassword.Text))
            {
                var elecUserInfoEntity = new ZtoElecUserInfoEntity();
                elecUserInfoEntity.Kehuid = txtCustomerID.Text.Replace(" ", "");
                elecUserInfoEntity.Pwd = txtCustomerPassword.Text.Replace(" ", "");
                elecUserInfoEntity.Result = "false";
                elecUserInfoEntity.InterfaceType = "0";
                string encryInfo = JsonConvert.SerializeObject(elecUserInfoEntity);
                // 扩展类保存一下
                ZtoElecUserInfoExtendEntity elecUserInfoExtendEntity = new ZtoElecUserInfoExtendEntity();
                elecUserInfoExtendEntity.Kehuid = txtCustomerID.Text.Replace(" ", "");
                elecUserInfoExtendEntity.Pwd = txtCustomerPassword.Text.Replace(" ", "");
                elecUserInfoExtendEntity.Result = "false";
                elecUserInfoExtendEntity.InterfaceType = "0";
                elecUserInfoExtendEntity.siteCode = txtSiteCode.Text;
                elecUserInfoExtendEntity.siteName = txtSiteName.Text;
                string encryExtendInfo = JsonConvert.SerializeObject(elecUserInfoExtendEntity);
                BillPrintHelper.SetZtoCustomerInfo(SecretUtil.Encrypt(encryInfo, BaseSystemInfo.SecurityKey));
                BillPrintHelper.SetZtoCustomerExtendInfo(SecretUtil.Encrypt(encryExtendInfo, BaseSystemInfo.SecurityKey));
                XtraMessageBox.Show("绑定成功", AppMessage.MSG0000, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                BillPrintHelper.DeleteElecUserInfoEntity();
            }
        }

        private void lblBindElecUserInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ToolHelper.OpenBrowserUrl("http://note.youdao.com/share/web/file.html?id=ff498eac3764c42d7c4c6090f201a620&type=note");
        }

        private void ckProclaimText_CheckedChanged(object sender, EventArgs e)
        {
            txtCustomerPassword.Properties.UseSystemPasswordChar = !ckProclaimText.Checked;
        }


    }
}
