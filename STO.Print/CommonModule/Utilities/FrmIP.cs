//-----------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2016 , STO EXPRESS TECH, Ltd.  
//-----------------------------------------------------------------

using DotNet.Utilities;
using System;
using System.Windows.Forms;
using STO.Print.AddBillForm;

namespace STO.Print
{
    public partial class FrmIP : BaseForm
    {
        public FrmIP()
        {
            InitializeComponent();

            GetIp();
        }

        private void GetIp()
        {
            this.txtIP.Text = MachineInfo.GetIPAddress();
            this.txtMacAddress.Text = MachineInfo.GetMacAddress();
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            string url = @"http://www.ip138.com/";
            STO.Print.Utilities.ToolHelper.OpenBrowserUrl(url);
        }

        private void btnGet_Click(object sender, EventArgs e)
        {
            string userInfo = string.Empty;

            userInfo = this.txtIP.Text + System.Environment.NewLine
                    + this.txtMacAddress.Text + System.Environment.NewLine;

            Clipboard.SetText(userInfo);
        }
    }
}
