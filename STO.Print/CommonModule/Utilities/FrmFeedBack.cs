//-------------------------------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2015 , STO , Ltd .
//-------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Windows.Forms;
using STO.Print.AddBillForm;

namespace STO.Print
{
    using DevExpress.XtraEditors;
    using DevExpress.XtraRichEdit;
    using DotNet.Utilities;
    using Utilities;

    /// <summary>
    /// 反馈建议
    ///
    /// 修改纪录
    ///
    ///		  2015-07-25  版本：1.0 YangHengLian 创建主键，注意命名空间的排序。
    /// 
    /// 版本：1.0
    ///
    /// <author>
    ///		<name>YangHengLian</name>
    ///		<date>2015-07-25</date>
    /// </author>
    /// </summary>
    public partial class FrmFeedBack : BaseForm
    {
        public FrmFeedBack()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSend_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (string.IsNullOrEmpty(txtSubject.Text.Trim()))
            {
                XtraMessageBox.Show("请填写标题", AppMessage.MSG0000, MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtSubject.Focus();
                return;
            }
            string html = string.Empty;
            var linkList = new List<LinkedAttachementInfo>();
            var controls = richTextBox1.Controls;
            foreach (var control in controls)
            {
                var rich = control as RichEditControl;
                if (rich != null)
                {
                    var exporter = new RichMailExporter(rich);
                    exporter.Export(out html, out linkList);
                    break;
                }
            }
            if (string.IsNullOrEmpty(html))
            {
                XtraMessageBox.Show("请填写内容", AppMessage.MSG0000, MessageBoxButtons.OK, MessageBoxIcon.Information);
                richTextBox1.Focus();
                return;
            }
            string mailContent = html;
            if (!string.IsNullOrEmpty(txtLine.Text))
            {
                mailContent += "<br />联系方式：" + txtLine.Text;
            }
            if (MailHelper.SendMail(BaseSystemInfo.MailUserName, BaseSystemInfo.MailPassword, BaseSystemInfo.SoftFullName, new[] { txtToUser.Text }, txtSubject.Text, mailContent, null, BaseSystemInfo.MailServer))
            {
                XtraMessageBox.Show("反馈成功", AppMessage.MSG0000, MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtSubject.Text = string.Empty;
                txtLine.Text = string.Empty;
                foreach (var control in controls)
                {
                    var rich = control as RichEditControl;
                    if (rich != null)
                    {
                        rich.Document.Text = "";
                    }
                }
                txtSubject.Focus();
                txtSubject.Select();
            }
        }

        /// <summary>
        /// 联系系统开发人员QQ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnQQ_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            QQHelper.ChatQQ();
        }

        /// <summary>
        /// 加入运单群
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnJoinQQGroup_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            QQHelper.JoinQQGroup();
        }

        /// <summary>
        /// 窗体加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrmFeedBack_Load(object sender, EventArgs e)
        {
            var qqArray = QQHelper.GetQClientKey();
            if (qqArray != null && qqArray.Length > 0)
            {
                txtLine.Text = qqArray[0].QQ;
            }
        }
    }
}