using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using Xilium.CefGlue;

namespace Xilium.CefGlue.Client.bs
{
    public class BsDownloadHandler : CefDownloadHandler
    {
        protected override void OnBeforeDownload(CefBrowser browser, CefDownloadItem downloadItem, string suggestedName, CefBeforeDownloadCallback callback)
        {
            callback.Continue(string.Empty, true);
        }

        protected override void OnDownloadUpdated(CefBrowser browser, CefDownloadItem downloadItem,
            CefDownloadItemCallback callback)
        {
            //downloadItem.CurrentSpeed 下载速度
            //downloadItem.TotalBytes 总字节数
            //downloadItem.ReceivedBytes 已完成字节数
            //downloadItem.PercentComplete 完成百分比
            //downloadItem.StartTime 下载开始时间
            //downloadItem.EndTime 下载完成时间
            if (downloadItem.IsComplete)
            {
                MessageBox.Show("文件下载成功！");
                if (browser.IsPopup && !browser.HasDocument)
                {
                    browser.GetHost().ParentWindowWillClose();
                    browser.GetHost().CloseBrowser();
                }
            }
        }
    }
}
