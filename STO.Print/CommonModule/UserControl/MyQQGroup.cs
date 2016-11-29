using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using STO.Print.Utilities;

namespace STO.Print.UserControl
{
    public partial class MyQQGroup : XtraUserControl
    {
        public MyQQGroup()
        {
            InitializeComponent();
        }

        private void btnQQGroup1_Click(object sender, EventArgs e)
        {
            QQHelper.JoinQQGroup();
            Clipboard.SetText("600952565");
        }

        private void btnGroup2_Click(object sender, EventArgs e)
        {
            QQHelper.JoinQQGroup3();
            Clipboard.SetText("207444366");
        }

        private void btnGroup3_Click(object sender, EventArgs e)
        {
            QQHelper.JoinQQGroup2();
            Clipboard.SetText("342190881");
        }
    }
}
