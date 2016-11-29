//-------------------------------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2015 , STO , Ltd .
//-------------------------------------------------------------------------------------

using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using STO.Print.AddBillForm;

namespace STO.Print
{
    using DevExpress.XtraEditors;
    using Microsoft.AspNet.SignalR.Client.Hubs;

    /// <summary>
    /// 右下角弹框
    ///
    /// 修改纪录
    ///
    ///		  2015-07-13  版本：1.0 YangHengLian 创建主键，注意命名空间的排序。
    /// 
    /// 版本：1.0
    ///
    /// <author>
    ///		<name>YangHengLian</name>
    ///		<date>2015-07-13</date>
    /// </author>
    /// </summary>
    public partial class FrmAlert : BaseForm
    {
        public FrmAlert()
        {
            InitializeComponent();
        }

        public delegate void ShowHideCallback(int top, double opacity);
        Thread _t;
        Point _p;
        HubConnection hubConnection;
        IHubProxy hubProxy;
        private delegate void AddTxt(string msg);

        private void Form1_Load(object sender, EventArgs e)
        {
            CheckForIllegalCrossThreadCalls = false;
            txtSend.Text = "";
            txtSend.SelectionStart = 0; // 定位光标
            txtSend.Select();
            try
            {
                hubConnection = new HubConnection("http://122.225.117.230:89/signalr/hubs");
                hubProxy = hubConnection.CreateHubProxy("myChatHub");
                hubProxy.On<string>("addMessage", (message) => this.Invoke(new AddTxt(Show), message));
                hubConnection.Start().Wait();
            }
            catch (Exception)
            {
                btnSend.Enabled = false;
            }
            int xpos = Screen.PrimaryScreen.WorkingArea.Width - Width;
            int ypos = Screen.PrimaryScreen.WorkingArea.Height;
            _p = new Point(xpos, ypos);
            Location = _p;
            Opacity = 0;
            _t = new Thread(ShowThread);
            _t.Start();
        }

        public void ShowHide(int top, double opacity)
        {
            try
            {
                Opacity = opacity;
                Top -= top;
                if (opacity == 0)
                {
                    _t.Abort();
                    _t = new Thread(ShowThread);
                    _t.Start();
                }
                if (_p.Y - Top == Height)
                {
                    _t.Abort();
                }
            }
            catch (Exception ex)
            {
                ProcessException(ex);
            }
        }

        /// <summary>
        /// 显示窗口
        /// </summary>
        public void ShowThread()
        {
            try
            {
                Rectangle rect = Screen.PrimaryScreen.WorkingArea;
                int endtop = Screen.PrimaryScreen.WorkingArea.Height - Height;
                while (true)
                {
                    Thread.Sleep(100);
                    if (Top > endtop)
                    {
                        Opacity = 1 - 1.0 * (Height - endtop) / (rect.Height - Height - endtop);
                        // 860-560
                        if ((Top - endtop) < 5)
                        {
                            //Top -= (Top - endtop);
                            Invoke(new ShowHideCallback(ShowHide), -(endtop - Top), 1.0 * (endtop) / (Top));
                        }
                        else
                        {
                            Invoke(new ShowHideCallback(ShowHide), -(endtop - Top) / 5, 1.0 * (endtop) / (Top));
                            //Top -= ((Top - endtop) / 5);
                        }
                        continue;
                    }
                    //Opacity = 1;
                    break;
                }
            }
            catch (Exception ex)
            {
                // ignored
            }
        }

        public void HideThread()
        {
            try
            {
                int h = Screen.PrimaryScreen.WorkingArea.Height;
                int endtop = h;
                while (true)
                {
                    Thread.Sleep(100);
                    if (Top < (endtop - 4))
                    {
                        Invoke(new ShowHideCallback(ShowHide), -(endtop - Top) / 5, 1.0 * (endtop - Top) / (Height));
                        continue;
                    }
                    else
                    {
                        Invoke(new ShowHideCallback(ShowHide), 0, 0);
                    }
                    break;
                }
            }
            catch (Exception ex)
            {
                // ignored
            }
        }

        private void FrmAlert_FormClosed(object sender, FormClosingEventArgs e)
        {
            this.Hide();
            e.Cancel = true;
        }

        /// <summary>
        /// 刷新聊天纪录
        /// </summary>
        /// <param name="msg"></param>
        private void Show(string msg)
        {
            this.memoEdit1.Text += STO.Print.Utilities.Computer.GetComputerName() + "  " + DateTime.Now.ToString(DotNet.Utilities.BaseSystemInfo.DateTimeFormat) + Environment.NewLine + msg + Environment.NewLine;
            memoEdit1.SelectionStart = memoEdit1.Text.Length;
            memoEdit1.ScrollToCaret();
        }

        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSend_Click(object sender, EventArgs e)
        {
            try
            {
                hubProxy.Invoke("send", this.txtSend.Text).Wait();
                hubProxy.GetValue<string>("ddd");
                txtSend.Text = "";
                txtSend.SelectionStart = 0;
                txtSend.Select();
            }
            catch (Exception exception)
            {
                XtraMessageBox.Show(exception.Message);
            }
        }

        /// <summary>
        /// 回车发送
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtSend_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (string.IsNullOrEmpty(txtSend.Text) && txtSend.Text != "\r\n") { return; }
                // btnSend_Click(this, null);
            }
        }
    }
}
