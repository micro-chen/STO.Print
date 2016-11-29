using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace STO.Print.UserControl
{
    public partial class MyLock : XtraUserControl
    {
        public MyLock()
        {
            InitializeComponent();
        }

        private void MyLock_Load(object sender, EventArgs e)
        {
            txtPassword.Focus();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            txtPassword.Text = "";
            this.Visible = false;
        }
    }
}
