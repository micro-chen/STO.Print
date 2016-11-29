//-----------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2015 , STO TECH, Ltd. 
//-----------------------------------------------------------------

using System;
using System.Windows.Forms;
using STO.Print.AddBillForm;

namespace STO.Print
{
    public partial class FrmProcessBar : BaseForm
    {
        private Timer timer = new Timer();
        public FrmProcessBar()
        {
            InitializeComponent();
            timer.Interval = 10;
            timer.Tick += new EventHandler(timer_Tick);
            timer.Enabled = true;
        }

        void timer_Tick(object sender, EventArgs e)
        {
            if (this.circularProgressBarEx4.Value < 100) { this.circularProgressBarEx4.Value++; }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.circularProgressBarEx4.Value = 1000;
        }
    }
}
