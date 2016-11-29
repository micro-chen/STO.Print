using System.IO;
using System.Linq;

namespace Xilium.CefGlue.WindowsForms
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    internal sealed class CefWebLifeSpanHandler : CefLifeSpanHandler
    {
        private readonly CefWebBrowser _core;

        public CefWebLifeSpanHandler(CefWebBrowser core)
        {
            _core = core;
        }

        protected override void OnAfterCreated(CefBrowser browser)
        {
            base.OnAfterCreated(browser);

        	_core.InvokeIfRequired(() => _core.OnBrowserAfterCreated(browser));
        }

        protected override bool DoClose(CefBrowser browser)
        {
            // TODO: ... dispose core
            return false;
        }

		protected override void OnBeforeClose(CefBrowser browser)
		{
			if (_core.InvokeRequired)
				_core.BeginInvoke((Action)_core.OnBeforeClose);
			else
				_core.OnBeforeClose();
		}

		protected override bool OnBeforePopup(CefBrowser browser, CefFrame frame, string targetUrl, string targetFrameName, CefPopupFeatures popupFeatures, CefWindowInfo windowInfo, ref CefClient client, CefBrowserSettings settings, ref bool noJavascriptAccess)
		{
            if (Path.HasExtension(targetUrl))
            {
                var ext = Path.GetExtension(targetUrl).Remove(0, 1);
                var extarr = new string[] { "exe", "pdf", "doc", "xls", "rar","zip" }.ToList();
                if (extarr.Contains(ext))
                {
                    browser.GetMainFrame().Browser.GetHost().StartDownload(targetUrl);
                    return true;
                }
            }
            return base.OnBeforePopup(browser, frame, targetUrl, targetFrameName, popupFeatures, windowInfo, ref client, settings, ref noJavascriptAccess);
		}
    }
}
