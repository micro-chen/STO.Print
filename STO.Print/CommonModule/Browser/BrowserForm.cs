using DotNet.Utilities;
using System.Windows.Forms;
using Xilium.CefGlue;
using Xilium.CefGlue.WindowsForms;
using STO.Print.AddBillForm;

namespace STO.Print
{
    public partial class BrowserForm : BaseForm, IBaseBrowser
    {

        public BrowserForm()
        {
            InitializeComponent();
        }

        private void NewTab(string startUrl)
        {
            var page = new TabPage();
            page.Padding = new Padding(0, 0, 0, 0);

            var browser = new CefWebBrowser();
            browser.BrowserSettings = new CefBrowserSettings
                                          {
                                              //控制文件的URL是否将有机会获得其他的文件的URL
                                              FileAccessFromFileUrls = CefState.Enabled,
                                              //控制的JavaScript是否可以访问剪贴板
                                              JavaScriptAccessClipboard = CefState.Enabled,
                                              //控制是否网络安全限制，是否禁止跨站脚本
                                              WebSecurity = CefState.Disabled,
                                              //控制文件的URL是否可以访问所有的URL
                                              UniversalAccessFromFileUrls = CefState.Enabled,
                                              //应用程序的缓存是否可以使用
                                              ApplicationCache = CefState.Enabled,
                                              //控制光标的位置是否将被绘制
                                              CaretBrowsing = CefState.Enabled,
                                              //是否可以使用数据库
                                              Databases = CefState.Enabled,
                                              //硬件加速
                                              AcceleratedCompositing = CefState.Enabled,
                                              //控制本地存储是否可以使用
                                              LocalStorage = CefState.Enabled,
                                              //3D计算机图形的技术
                                              WebGL = CefState.Enabled,
                                          };
            browser.StartUrl = startUrl;
            browser.Dock = DockStyle.Fill;
            //browser.TitleChanged += (s, e) =>
            //                            {
            //                                BeginInvoke(new Action(() =>
            //                                                           {
            //                                                               string title = browser.Title;
            //                                                               if (tabControl.SelectedTab == page)
            //                                                               {
            //                                                                   Text = browser.Title + " - " + _mainTitle;
            //                                                               }
            //                                                               page.ToolTipText = title;
            //                                                               if (title.Length > 18)
            //                                                               {
            //                                                                   title = title.Substring(0, 18) + "...";
            //                                                               }
            //                                                               page.Text = title;
            //                                                           }));
            //                            };
            //browser.AddressChanged +=
            //    (s, e) => { BeginInvoke(new Action(() => { addressTextBox.Text = browser.Address; })); };
            // browser.StatusMessage += (s, e) => { BeginInvoke(new Action(() => { statusLabel.Text = e.Value; })); };
            this.Controls.Add(browser);
            //page.Controls.Add(browser);

            //tabControl.TabPages.Add(page);

            //tabControl.SelectedTab = page;
        }

        public string NavigateUrl { get; set; }
        public string NavigateText { get; set; }
        public void Navigate(string url)
        {
            NavigateUrl = url;
            NewTab(NavigateUrl);
        }
    }
}