using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace STO.Print.UserControl
{
    partial class MyAutoDocker
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && this.components != null)
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.components = new Container();
            this._checkPosTimer = new Timer(this.components);
            this._checkPosTimer.Enabled = true;
            this._checkPosTimer.Interval = 300;
            this._checkPosTimer.Tick += new EventHandler(this.CheckPosTimer_Tick);
        }
    }
}
