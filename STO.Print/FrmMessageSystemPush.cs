//--------------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2015 , ZTO TECH, Ltd. 
//--------------------------------------------------------------------

using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace ZTO.Print
{
    using ZTO.Print.AddBillForm;
    using DotNet.Model;
    using DotNet.Utilities;

    /// <summary>
    /// FrmMessageSystemPush.cs
    /// 
    /// 消息提醒
    /// 
    ///     1：首先所有的提醒信息都在这里显示比较好。
    ///     2：弹出的窗口位置最好是比较理想的位置，不能是屏幕中间。
    ///     3：消息一直在这个窗体里，能累加显示，比较省事，不要打开很多窗口比较好。
    ///     4：每次来消息的时候，应该有声音提示比较好。
    ///     5：还应该有个查看内容的按钮比较好。
    ///     6：消息一次取多个，好像有问题，现在只显示一个。 
    ///		
    /// 修改记录
    /// 
    ///     2015.10.12 版本：2.1 YangHengLian 显示发出者的单位、部门。
    ///     2012.06.04 版本：2.0 YangHengLian 改进功能。
    ///     2007.10.31 版本：1.0 YangHengLian 创建。
    ///		
    ///		<name>YangHengLian</name>
    ///		<date>2015.10.12</date>
    /// </summary>
    public partial class FrmMessageSystemPush : BaseForm
    {
        public FrmMessageSystemPush()
        {
            InitializeComponent();
        }

        // 声明FlashWindow()
        [DllImport("user32.dll")]
        public static extern bool FlashWindow(
         IntPtr hWnd,           //   handle   to   window   
         bool bInvert       //   flash   status   
         );

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            // this.TopMost = true;
            this.StartPosition = FormStartPosition.Manual;

            Rectangle bounds = Screen.FromControl(this).WorkingArea;
            // this.Location = new Point(bounds.Width - this.Width, bounds.Height - this.Height);

            // 初始化窗体时加载 GGYY， // 屏蔽右键
            this.webMessage.Navigate("about:blank");
            this.webMessage.DocumentText = @"<body style=""margin: 3px;font-family:宋体"" onload=""scrollBy(0,document.body.scrollHeight)"">" +
               @"<script language=""javascript"" type=""text/javascript"">document.oncontextmenu=new Function(""event.result=false;""); </script>";
        }

        protected override bool ShowWithoutActivation
        {
            get
            {
                return true;
            }
        }

        // 播放音乐
        [DllImport("winmm.dll")]
        // 播放windows音乐，重载
        public static extern bool PlaySound(string pszSound, int hmod, int fdwSound);

        public const int SND_FILENAME = 0x00020000;

        public const int SND_ASYNC = 0x0001;

        readonly string _filePath = AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "\\Resources\\Media\\msg.wav";

        private void PlaySound()
        {
            PlaySound(_filePath, 0, SND_ASYNC | SND_FILENAME);
        }

        // 防止线程阻塞的写法
        private delegate bool SetMessage(BaseMessageEntity message);

        // 检查日期是否今天 
        public static bool IsToday(DateTime dt)
        {
            DateTime today = DateTime.Today;
            DateTime tempToday = new DateTime(dt.Year, dt.Month, dt.Day);
            return (today == tempToday);
        }

        // 弹出显示加载不成功时重新加载一次 GGYY
        private void webBMsg_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            if (this.webMessage.DocumentText == "<HTML></HTML>\0")
            {
                // 屏蔽右键
                this.webMessage.DocumentText = @"<body style=""margin: 3px;font-family:宋体"" onload=""scrollBy(0,document.body.scrollHeight)"">" +
                   @"<script language=""javascript"" type=""text/javascript"">document.oncontextmenu=new Function(""event.result=false;""); </script>" +
                   OneShow.ToString();
            }
        }

        // 聊天内容（带样式）字符串
        public StringBuilder OneShow = new StringBuilder();

        public bool ShowMessage(string message)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append(message);
            OneShow.Append(stringBuilder.ToString());
            this.webMessage.DocumentText = this.webMessage.DocumentText.Insert(this.webMessage.DocumentText.Length, stringBuilder.ToString());

            this.PlaySound();

            FlashWindow(this.Handle, true);

            // this.txtContents.AppendText(createOn + " : " + "\r\n");
            // this.txtContents.AppendText(message + "\r\n");
            // this.txtContents.AppendText("- - - - - - - - - - - - - - -" + "\r\n");
            // this.txtContents.ScrollToCaret();

            return true;
        }

        public bool ShowMessage(BaseMessageEntity entity)
        {
            string createBy = string.Empty;

            if (!string.IsNullOrEmpty(entity.CreateCompanyName))
            {
                createBy += entity.CreateCompanyName;
            }
            if (!string.IsNullOrEmpty(entity.CreateDepartmentName))
            {
                createBy += " - " + entity.CreateDepartmentName;
            }
            if (!string.IsNullOrEmpty(entity.CreateBy))
            {
                createBy += " - " + entity.CreateBy;
            }
            if (!string.IsNullOrEmpty(entity.Telephone))
            {
                createBy += " - " + entity.Telephone;
            }

            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("<div style='color:#00f;font-size:12px;'>" + createBy + " [");
            if (IsToday((DateTime)entity.CreateOn))
            {
                stringBuilder.Append(((DateTime)entity.CreateOn).ToLongTimeString() + "]：</div>");
            }
            else
            {
                stringBuilder.Append(((DateTime)entity.CreateOn).ToString(BaseSystemInfo.DateTimeFormat) + "]：</div>");
            }
            stringBuilder.Append(entity.Contents);
            OneShow.Append(stringBuilder.ToString());
            this.webMessage.DocumentText = this.webMessage.DocumentText.Insert(this.webMessage.DocumentText.Length, stringBuilder.ToString());

            this.PlaySound();

            FlashWindow(this.Handle, true);

            //this.txtContents.AppendText(createOn + " : " + "\r\n");
            //this.txtContents.AppendText(message + "\r\n");
            //this.txtContents.AppendText("- - - - - - - - - - - - - - -" + "\r\n");
            //this.txtContents.ScrollToCaret();

            return true;
        }

        public bool OnReceiveMessage(string message)
        {
            bool result = false;
            //if (this.InvokeRequired)
            //{
            //    SetMessage setMessage = new SetMessage(OnReceiveMessage);
            //    this.Invoke(setMessage, new object[] { message });
            //}
            //else
            //{
            ShowMessage(message);
            result = true;
            // }
            return result;
        }

        public bool OnReceiveMessage(BaseMessageEntity entity)
        {
            bool result = false;
            if (this.InvokeRequired)
            {
                SetMessage setMessage = new SetMessage(OnReceiveMessage);
                this.Invoke(setMessage, new object[] { entity });
            }
            else
            {
                ShowMessage(entity);
                result = true;
            }
            return result;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}