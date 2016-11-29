//-----------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2015 , STO TECH, Ltd. 
//-----------------------------------------------------------------

using DotNet.Utilities;
using System;
using System.Reflection;
using STO.Print.AddBillForm;

namespace STO.Print
{
    using STO.Print.Utilities;

    /// <summary>
    /// 关于软件
    /// 
    /// 修改纪录
    /// 
    ///     2015-07-24 版本：1.0 YangHengLian 创建。
    ///		
    /// <author>
    ///		<name>YangHengLian</name>
    ///		<date>2015-07-24</date>
    /// </author> 
    /// </summary>
    public partial class FrmAboutThis : BaseForm
    {

        public FrmAboutThis()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 点击窗体关闭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrmAboutThis_Click(object sender, EventArgs e)
        {
            Close();
        }

        /// <summary>
        /// 点击Ok关闭窗体
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnConfirm_Click(object sender, EventArgs e)
        {
            Close();
        }

        /// <summary>
        /// 技术支持
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lblTechnicalSupport_Click(object sender, EventArgs e)
        {
            QQHelper.ChatQQ("766096823");
        }

        /// <summary>
        /// 打开申通官网
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void picCompany_Click(object sender, EventArgs e)
        {
            ToolHelper.OpenBrowserUrl("http://www.zto.com");
        }

        /// <summary>
        /// 窗体加载事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrmAboutThis_Load(object sender, EventArgs e)
        {
            lblSoftFullName.Text = BaseSystemInfo.SoftFullName;
            lblVersion.Text = "Version V" + Assembly.GetExecutingAssembly().GetName().Version;
            lblCopyright.Text = string.Format("Copyright 2015-{0} STO EXPRESS", DateTime.Now.Year);
        }

        /// <summary>
        /// 下载链接
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lblDownLoad_Click(object sender, EventArgs e)
        {
            ToolHelper.OpenBrowserUrl("http://www.zto.cn/GuestService/Print");
        }
    }
}