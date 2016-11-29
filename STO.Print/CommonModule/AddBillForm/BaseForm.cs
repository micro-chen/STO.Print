//-------------------------------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2016 , STO , Ltd .
//-------------------------------------------------------------------------------------

using DotNet.Utilities;
using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using STO.Print.Model;
using STO.Print.Utilities;

namespace STO.Print.AddBillForm
{
    using DevExpress.Utils;
    using DevExpress.XtraEditors;
    using DevExpress.XtraGrid.Views.Grid;

    /// <summary>
    /// 窗体基类
    ///  
    /// 修改记录
    /// 
    ///     2015-07-21  版本：1.0 YangHengLian 创建
    /// 
    /// 版本：1.0
    ///
    /// <author>
    ///		<name>YangHengLian</name>
    ///		<date>2015-07-21</date>
    /// </author>
    /// </summary>
    public partial class BaseForm : XtraForm
    {
        /// <summary>
        /// 选中打印记录ID（用来进行修改和新增动作标识符）
        /// </summary>
        public string SelectedId;

        /// <summary>
        /// 发件人实体
        /// </summary>
        public ZtoUserEntity SendManEntity;

        /// <summary>
        /// 收件人实体
        /// </summary>
        public ZtoUserEntity ReceiveManEntity;

        ///// <summary>
        ///// 绑定收件人信息的委托
        ///// </summary>
        //public Action BindReceiveMan;

        ///// <summary>
        ///// 绑定发件人信息的委托
        ///// </summary>
        //public Action BindSendMan;

        public BaseForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 设置grid的字体
        /// </summary>
        /// <param name="view"></param>
        /// <param name="font"></param>
        public void SetGridFont(GridView view, Font font)
        {
            foreach (AppearanceObject ap in view.Appearance)
            {
                ap.Font = font;
            }
        }

        protected void Navigate(string url, string tableFormName = "导航页面")
        {
            Navigate(string.Empty, url, tableFormName);
        }

        private void Navigate(string assemblyName, string url, string tableFormName = "导航页面")
        {
            bool found = false;
            Form tableForm = null;
            foreach (Form form in this.MdiChildren)
            {
                if (form.Name == tableFormName)
                {
                    tableForm = form;
                    ((IBaseBrowser)tableForm).Navigate(url);
                    found = true;
                    break;
                }
            }
            if (!found)
            {
                if (string.IsNullOrEmpty(assemblyName))
                {
                    assemblyName = "STO.Print";
                    string path = BaseSystemInfo.StartupPath + @"\cefclient.exe";
                    if (System.IO.File.Exists(path))
                    {
                        assemblyName = "STO.Print";
                    }
                }
                tableForm = ChildFormManagementHelper.LoadMdiForm(this, "BrowserForm", assemblyName);
                tableForm.Name = tableFormName;
                tableForm.Text = tableFormName;
                tableForm.MdiParent = this;
                ((IBaseBrowser)tableForm).Navigate(url);
                tableForm.Show();
            }
            tableForm.BringToFront();
            tableForm.Activate();
        }

        /// <summary>
        /// 处理异常信息
        /// </summary>
        /// <param name="ex">异常</param>
        public void ProcessException(Exception ex)
        {
            // 201400711 页面假死问题解决
            new Thread(() =>
            {
                Thread.Sleep(500);
                this.Invoke((EventHandler)delegate
                {
                    // 显示异常页面
                    var frmException = new FrmException(ex);
                    frmException.ShowDialog();
                });
            }).Start();
        }

        /// <summary>
        /// 按ESC触发关闭事件，2016-1-20 17:07:04
        /// 上海-小杰  693684292 提供代码
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="keyData"></param>
        /// <returns></returns>
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            int WM_KEYDOWN = 256;

            int WM_SYSKEYDOWN = 260;

            if (msg.Msg == WM_KEYDOWN | msg.Msg == WM_SYSKEYDOWN)
            {
                switch (keyData)
                {
                    case Keys.Escape:
                        Close();
                        break;
                }
            }
            return false;
        }
    }
}