//-------------------------------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2015 , STO , Ltd .
//-------------------------------------------------------------------------------------

using System;
using System.Threading;
using System.Windows.Forms;

namespace STO.Print
{
    using DevExpress.XtraEditors;
    using DotNet.Utilities;
    using Newtonsoft.Json;
    using STO.Print.AddBillForm;
    using STO.Print.Model;
    using STO.Print.Utilities;

    /// <summary>
    /// 绑定线下申通商家ID
    /// 
    /// 修改记录
    ///  
    ///     2015-11-04  版本：1.0 YangHengLian 创建
    /// 
    /// 版本：1.0
    ///
    /// <author>
    ///		<name>YangHengLian</name>
    ///		<date>2015-11-04</date>
    /// </author>
    /// </summary>
    public partial class FrmBindZtoElecUserInfo : BaseForm
    {
        public FrmBindZtoElecUserInfo()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 窗体加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrmBindZtoElecUserInfo_Load(object sender, EventArgs e)
        {
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

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
                txtCustomerPassword.ShowTip("密码必填");
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
                MessageUtil.ShowTips("绑定成功，账号密码不要告诉别人");
                //if (!string.IsNullOrEmpty(txtQQNumber.Text))
                //{
                //    ThreadPool.QueueUserWorkItem(delegate
                //    {

                //        string content = "商家id：" + elecUserInfoEntity.Kehuid + "<br />密码是：" + elecUserInfoEntity.Pwd + "<br />一定不要告诉别人，涉及到安全问题<br />如果手机坏了找不到账号密码可以从qq邮箱找到。";
                //        // 账号密码发送到邮箱里面做一个备份，2016-6-10 19:29:52
                //        MailHelper.SendMail(BaseSystemInfo.MailUserName, BaseSystemInfo.MailPassword, BaseSystemInfo.SoftFullName, new[] { txtQQNumber.Text + "@qq.com" }, "申通线下电子面单商家ID和密码备份", content, null, BaseSystemInfo.MailServer);

                //    });
                //}
                // 关闭窗口
                Close();
            }
            else
            {
                BillPrintHelper.DeleteElecUserInfoEntity();
            }
        }

        private void ckProclaimText_CheckedChanged(object sender, EventArgs e)
        {
            txtCustomerPassword.Properties.UseSystemPasswordChar = !ckProclaimText.Checked;
        }

        private void lblBindElecUserInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ToolHelper.OpenBrowserUrl("http://note.youdao.com/share/web/file.html?id=ff498eac3764c42d7c4c6090f201a620&type=note");
        }

        /// <summary>
        /// 查询剩余电子面单数量
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
    }
}