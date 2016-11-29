//--------------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2015 , STO TECH, Ltd. 
//--------------------------------------------------------------------

using System;
using System.Drawing.Printing;
using System.Net.Mail;
using System.Reflection;
using System.Windows.Forms;

namespace STO.Print
{
    using AddBillForm;
    using DotNet.Model;
    using DotNet.Utilities;
    using Utilities;

    /// <summary>
    /// FrmException
    /// 
    /// 修改纪录
    /// 
    ///		2016-1-20 版本：1.0 YangHengLian 创建。
    ///		
    /// <author>
    ///		<name>YangHengLian</name>
    ///		<date>2016-1-20</date>
    /// </author> 
    /// </summary>
    public partial class FrmException : BaseForm
    {
        /// <summary>
        /// 异常信息
        /// </summary>
        public BaseExceptionEntity ExceptionEntity = null;

        public FrmException()
        {
            InitializeComponent();
        }

        private BaseExceptionEntity ConvertException(Exception ex)
        {
            var exceptionEntity = new BaseExceptionEntity();
            exceptionEntity.Message = ex.Message;
            exceptionEntity.FormattedMessage = ex.Source;
            exceptionEntity.StackTrace = ex.StackTrace;
            exceptionEntity.CreateOn = DateTime.Now.ToString(BaseSystemInfo.DateTimeFormat);
            exceptionEntity.CreateUserId = BaseSystemInfo.UserInfo.Id;
            exceptionEntity.CreateBy = BaseSystemInfo.UserInfo.RealName;
            return exceptionEntity;
        }

        public FrmException(BaseExceptionEntity exceptionEntity)
            : this()
        {
            this.ExceptionEntity = exceptionEntity;
        }

        public FrmException(Exception ex)
            : this()
        {
            this.ExceptionEntity = this.ConvertException(ex);
        }

        public void FormOnLoad(object sender, EventArgs e)
        {

        }

        public void SetControlState()
        {
            this.btnMessage.Visible = BaseSystemInfo.UserIsLogOn;
        }

        #region public override void ShowEntity() 显示异常
        /// <summary>
        /// 显示异常
        /// </summary>
        public void ShowEntity()
        {
            this.txtCompany.Text = BaseSystemInfo.CustomerCompanyName;
            this.txtSoft.Text = BaseSystemInfo.SoftFullName;
            if (this.ExceptionEntity != null)
            {
                this.txtUser.Text = this.ExceptionEntity.CreateBy;
                this.txtUser.Tag = this.ExceptionEntity.CreateUserId;
                this.txtOccurTime.Text = this.ExceptionEntity.CreateOn;
                this.txtException.Text = this.ExceptionEntity.Message
                    + Environment.NewLine + this.ExceptionEntity.StackTrace
                    + Environment.NewLine + this.ExceptionEntity.FormattedMessage;
            }
        }
        #endregion

        private void docToPrint_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            string text = this.txtException.Text;
            var printFont = new System.Drawing.Font("宋体", 30, System.Drawing.FontStyle.Regular);
            e.Graphics.DrawString(text, printFont, System.Drawing.Brushes.Black, e.MarginBounds.X, e.MarginBounds.Y);
        }

        private void Print()
        {
            PrintDialog printDialogException = new PrintDialog();
            DialogResult result = printDialogException.ShowDialog();
            PrintDocument docToPrint = new PrintDocument();
            docToPrint.PrintPage += new PrintPageEventHandler(docToPrint_PrintPage);
            printDialogException.AllowSomePages = true;
            printDialogException.ShowHelp = true;
            printDialogException.Document = docToPrint;
            if (result == DialogResult.OK)
            {
                docToPrint.Print();
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            this.Print();
        }

        #region private void SendErrorReport() 发送错误报告
        /// <summary>
        /// 发送错误报告
        /// </summary>
        private void SendErrorReport()
        {
            if (string.IsNullOrEmpty(BaseSystemInfo.ErrorReportTo))
            {
                return;
            }
            string[] errorReportTo = BaseSystemInfo.ErrorReportTo.Split(',', ';', ' ');
            // 邮件的内容部分
            MailMessage mailMessage = new MailMessage();
            // 收件箱
            for (int i = 0; i < errorReportTo.Length; i++)
            {
                if (errorReportTo[i].Trim().Length > 0)
                {
                    mailMessage.To.Add(errorReportTo[i].Trim());
                }
            }
            if (mailMessage.To.Count > 0)
            {
                Assembly assembly = Assembly.Load(BaseSystemInfo.MainAssembly);
                // 邮件主题
                mailMessage.Subject = "Error " + BaseSystemInfo.UserInfo.CompanyName + " - " + this.txtCompany.Text + "(" + BaseSystemInfo.UserInfo.IPAddress + ")" + this.Text;
                // 邮件内容
                mailMessage.Body = "User:" + this.txtUser.Text + "\n"
                    + "UserId:" + BaseSystemInfo.UserInfo.Id + "\n"
                    + "UserCode:" + BaseSystemInfo.UserInfo.Code + "\n"
                    + "UserName:" + BaseSystemInfo.UserInfo.UserName + "\n"
                    + "NickName:" + BaseSystemInfo.UserInfo.NickName + "\n"
                    + "CompanyId:" + BaseSystemInfo.UserInfo.CompanyId + "\n"
                    + "CompanyName:" + BaseSystemInfo.UserInfo.CompanyName + "\n"
                    + "CompanyCode:" + BaseSystemInfo.UserInfo.CompanyCode + "\n"
                    + "OccurTime:" + this.txtOccurTime.Text + "\n"
                    + "Customer:" + this.txtCompany.Text + "\n"
                    + "Soft:" + this.txtSoft.Text + "\n"
                    + "Exception:" + this.txtException.Text;
                mailMessage.IsBodyHtml = false;
                mailMessage.Priority = MailPriority.High;
                mailMessage.BodyEncoding = System.Text.Encoding.UTF8;
                mailMessage.From = new MailAddress(BaseSystemInfo.MailUserName, BaseSystemInfo.CustomerCompanyName + AppMessage.MSG0243);

                SmtpClient smtpClient = new SmtpClient(BaseSystemInfo.MailServer);
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = new System.Net.NetworkCredential(BaseSystemInfo.MailUserName, BaseSystemInfo.MailPassword);
                // 指定如何处理待发的邮件
                smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtpClient.Send(mailMessage);
            }
        }
        #endregion

        private void BtnReportClick(object sender, EventArgs e)
        {
            // 设置鼠标繁忙状态，并保留原先的状态
            Cursor holdCursor = this.Cursor;
            this.Cursor = Cursors.WaitCursor;
            try
            {
                this.SendErrorReport();
                // 是否需要有提示信息？
                if (BaseSystemInfo.ShowInformation)
                {
                    // 批量保存，进行提示
                    DevExpress.XtraEditors.XtraMessageBox.Show(AppMessage.MSG0237, AppMessage.MSG0000, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(AppMessage.MSG3020, AppMessage.MSG0000, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {
                // 设置鼠标默认状态，原来的光标状态
                this.Cursor = holdCursor;
            }
        }

        private void btnMessage_Click(object sender, EventArgs e)
        {
            QQHelper.JoinQQGroup();
        }

        /// <summary>
        /// 联系qq
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnQqClick(object sender, EventArgs e)
        {
            QQHelper.ChatQQ(txtQQ.Text);
        }

        /// <summary>
        /// 关闭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 窗体加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrmException_Load(object sender, EventArgs e)
        {
            // 程序集名
            this.Text = BaseSystemInfo.SoftFullName + " V" + Assembly.GetExecutingAssembly().GetName().Version; ;
            this.ShowEntity();
        }
    }
}