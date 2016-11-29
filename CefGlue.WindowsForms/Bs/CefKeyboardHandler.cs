using System;
using System.Windows.Forms;
using Xilium.CefGlue;

namespace Xilium.CefGlue.Client.bs
{
    public class BsCefKeyboardHandler : CefKeyboardHandler
    {
        protected override bool OnKeyEvent(CefBrowser browser, CefKeyEvent keyEvent, IntPtr osEvent)
        {
            switch (keyEvent.WindowsKeyCode)
            {
                case (int)Keys.F12:
                {
                    var host = browser.GetHost();
                    var wi = CefWindowInfo.Create();
                    wi.SetAsPopup(IntPtr.Zero, "医汇通 - 开发调试工具");
                    host.ShowDevTools(wi, new DevToolsWebClient(), new CefBrowserSettings());
                    //var devToolsUrl = webBrowser.Browser.GetHost().GetDevToolsUrl(true);
                    //var isShow = false;
                    //foreach (Form field in Application.OpenForms)
                    //{
                    //    if (field.Text.EndsWith("开发调试工具"))
                    //    {
                    //        ((DevTools)field).LoadUrl(devToolsUrl);
                    //        field.Activate();
                    //        isShow = true;
                    //    }
                    //}
                    //if (!isShow)
                    //{
                    //    var devtool = new DevTools(devToolsUrl);
                    //    devtool.Size = new Size(this.Size.Width, devtool.Size.Height);
                    //    var point = this.PointToScreen(new Point(0, 0));
                    //    devtool.Location = new Point(point.X, point.Y + this.Size.Height - devtool.Size.Height);
                    //    devtool.Show();
                    //}
                    break;
                }
                case (int)Keys.F5:
                {
                    browser.ReloadIgnoreCache();
                    break;
                }
                default:
                    break;
            }
            return base.OnKeyEvent(browser, keyEvent, osEvent);
        }

        private class DevToolsWebClient : CefClient
        {
        }
    }
}
