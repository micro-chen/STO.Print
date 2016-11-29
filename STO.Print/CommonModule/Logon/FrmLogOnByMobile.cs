//-----------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2016 , STO EXPRESS TECH, Ltd.  
//-----------------------------------------------------------------

using System;
using System.Windows.Forms;

namespace STO.Print
{
    using STO.Print.AddBillForm;
    using DotNet.Utilities;

    /// <summary>
    /// FrmLogOnByCompany
    /// 用户登录系统
    /// 
    /// 修改记录
    /// 
    ///		2016-11-20 版本：1.0 YangHengLian 整理文件。
    ///		
    /// <author>
    ///		<name>YangHengLian</name>
    ///		<date>2016-11-20</date>
    /// </author> 
    /// </summary>
    public partial class FrmLogOnByMobile : BaseForm
    {

        public FrmLogOnByMobile()
        {
            InitializeComponent();
           
        }

        private void txtPassword_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                // 检查输入的有效性
            }
        }

        private void FrmLogOnByCompany_DoubleClick(object sender, EventArgs e)
        {
        }

       

        private void FrmLogOnByCompany_Load(object sender, EventArgs e)
        {
            // 这里判断自动登录
            if (!BaseSystemInfo.UserIsLogOn && BaseSystemInfo.AutoLogOn)
            {
                if (BaseSystemInfo.UserIsLogOn)
                {
                    Close();
                }
            }
        }
        

        private void FrmLogOnByCompany_FormClosed(object sender, FormClosedEventArgs e)
        {
        }
    }
}