using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
//using Xilium.CefGlue.Client.Chrome.Bs;
using Xilium.CefGlue;

namespace Xilium.CefGlue.Client.bs
{
    public class BsJsDialogHandler : CefJSDialogHandler     
    {
        protected override bool OnJSDialog(CefBrowser browser, string originUrl, string acceptLang, CefJSDialogType dialogType, string message_text, string default_prompt_text, CefJSDialogCallback callback, out bool suppress_message)
        {
            switch (dialogType)
            {
                case CefJSDialogType.Alert:
                    //LoadingUtil.ShowInformationMessage(message_text, "系统提示");
                    suppress_message = true;
                    return false;
                    break;
                case CefJSDialogType.Confirm:
                    var dr = MessageBox.Show(message_text, "系统提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question); 
                    if (dr == DialogResult.Yes)
                    {
                        callback.Continue(true, string.Empty);
                        suppress_message = false;
                        return true;
                    }
                    else
                    {
                        callback.Continue(false, string.Empty);
                        suppress_message = false;
                        return true;
                    }
                    break;
                case CefJSDialogType.Prompt:
                    //var prompt = new FrmPrompt(message_text, "系统提示", default_prompt_text);
                    //if (prompt.ShowDialog() == DialogResult.OK)
                    //{
                    //    callback.Continue(true, prompt.Content);
                    //    suppress_message = false;
                    //    return true;
                    //}
                    //else
                    //{
                    //    callback.Continue(false, string.Empty);
                    //    suppress_message = false;
                    //    return true;
                    //}
                    break;
            }
            suppress_message = true;
            return false;
        }

        protected override bool OnBeforeUnloadDialog(CefBrowser browser, string messageText, bool isReload, CefJSDialogCallback callback)
        {
            return true;
        }

        protected override void OnResetDialogState(CefBrowser browser)
        {
            
        }

        protected override void OnDialogClosed(CefBrowser browser)
        {
            
        }
    }
}
