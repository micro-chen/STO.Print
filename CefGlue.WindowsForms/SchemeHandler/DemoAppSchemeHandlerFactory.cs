using Xilium.CefGlue;

namespace Xilium.CefGlue.Client
{
    internal sealed class DemoAppSchemeHandlerFactory : CefSchemeHandlerFactory
    {
        protected override CefResourceHandler Create(CefBrowser browser, CefFrame frame, string schemeName, CefRequest request)
        {
            return new DumpRequestResourceHandler();
        }
    }
}
