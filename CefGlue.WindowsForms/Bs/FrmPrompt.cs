using System;
using System.Windows.Forms;

namespace Xilium.CefGlue.Client.Chrome.Bs
{
    public partial class FrmPrompt : Form
    {
        //public FrmPrompt()
        //{
            
        //}

        public string Content { get; set; }

        public FrmPrompt(string prompt, string title, string defaultEntry)
        {
            InitializeComponent();
            this.Text = title;
            labelControl1.Text = prompt;
            textEditMsg.Text = defaultEntry;
        }

        private void simpleButtonOK_Click(object sender, EventArgs e)
        {
            Content = textEditMsg.Text;
            base.DialogResult = DialogResult.OK;
        }

        private void simpleButtonCancel_Click(object sender, EventArgs e)
        {
            base.DialogResult = DialogResult.Cancel;
        }

        private void textEditMsg_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                simpleButtonCancel_Click(null, null);
            }
        }
    }
}