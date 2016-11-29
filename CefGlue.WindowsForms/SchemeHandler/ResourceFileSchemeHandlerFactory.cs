using Xilium.CefGlue;

namespace Xilium.CefGlue.Client
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    internal sealed class ResourceFileSchemeHandlerFactory : CefSchemeHandlerFactory
    {
        protected override CefResourceHandler Create(CefBrowser browser, CefFrame frame, string schemeName, CefRequest request)
        {
            return new ResourceFileRequestResourceHandler();
        }
    }
}
