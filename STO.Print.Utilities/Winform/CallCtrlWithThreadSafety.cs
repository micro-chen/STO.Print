using System.Windows.Forms;

namespace STO.Print.Utilities
{
    public class CallCtrlWithThreadSafety
    {
        //跨线程的控件安全访问方式
        #region 跨线程的控件安全访问方式
        delegate void SetTextCallback2(ToolStripStatusLabel objCtrl, string text, Form winf);
        delegate void SetTextCallback(System.Windows.Forms.Control objCtrl, string text, Form winf);
        delegate void SetEnableCallback(System.Windows.Forms.Control objCtrl, bool enable, Form winf);
        delegate void SetFocusCallback(System.Windows.Forms.Control objCtrl, Form winf);
        delegate void SetCheckedCallback(System.Windows.Forms.CheckBox objCtrl, bool isCheck,Form winf);

        public static void SetText<TObject>(TObject objCtrl, string text, Form winf) where TObject : System.Windows.Forms.Control
        {
            if (objCtrl.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(SetText);
                if (winf.IsDisposed)
                {
                    return;
                }
                winf.Invoke(d, new object[] { objCtrl, text, winf });
            }
            else
            {
                objCtrl.Text = text;
            }
        }
        public static void SetEnable<TObject>(TObject objCtrl, bool enable, Form winf) where TObject : System.Windows.Forms.Control
        {
            if (objCtrl.InvokeRequired)
            {
                SetEnableCallback d = new SetEnableCallback(SetEnable);
                if (winf.IsDisposed)
                {
                    return;
                }
                winf.Invoke(d, new object[] { objCtrl, enable,winf });
            }
            else
            {
                objCtrl.Enabled = enable;
            }
        }
        public static void SetFocus<TObject>(TObject objCtrl, Form winf) where TObject : System.Windows.Forms.Control
        {
            if (objCtrl.InvokeRequired)
            {
                SetFocusCallback d = new SetFocusCallback(SetFocus);
                if (winf.IsDisposed)
                {
                    return;
                }
                winf.Invoke(d, new object[] { objCtrl, winf });
            }
            else
            {
                objCtrl.Focus();
            }
        }
        public static void SetChecked<TObject>(TObject objCtrl, bool isChecked,Form winf) where TObject : System.Windows.Forms.CheckBox
        {
            if (objCtrl.InvokeRequired)
            {
                SetCheckedCallback d = new SetCheckedCallback(SetChecked);
                if (winf.IsDisposed)
                {
                    return;
                }
                winf.Invoke(d, new object[] { objCtrl, isChecked, winf });
            }
            else
            {
                objCtrl.Checked = isChecked;
            }
        }
        public static void SetVisible<TObject>(TObject objCtrl, bool isVisible, Form winf) where TObject : System.Windows.Forms.Control
        {
            if (objCtrl.InvokeRequired)
            {
                SetCheckedCallback d = new SetCheckedCallback(SetChecked);
                if (winf.IsDisposed)
                {
                    return;
                }
                winf.Invoke(d, new object[] { objCtrl, isVisible, winf });
            }
            else
            {
                objCtrl.Visible = isVisible;
            }
        }

        public static void SetText2<TObject>(TObject objCtrl, string text, Form winf) where TObject : ToolStripStatusLabel
        {
            if (objCtrl.Owner.InvokeRequired)
            {
                SetTextCallback2 d = new SetTextCallback2(SetText2);
                if (winf.IsDisposed)
                {
                    return;
                }
                winf.Invoke(d, new object[] { objCtrl, text, winf });
            }
            else
            {
                objCtrl.Text = text;
            }
        }
        #endregion
    }
}
