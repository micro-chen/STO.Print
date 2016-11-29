using Xilium.CefGlue.Client.Renderer;
using Xilium.CefGlue;

namespace Xilium.CefGlue.Client
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using System.Text;
    using System.IO;

    public sealed class CefApplication : CefApp
    {
        private CefRenderProcessHandler _renderProcessHandler = new DemoRenderProcessHandler();

        protected override CefRenderProcessHandler GetRenderProcessHandler()
        {
            return _renderProcessHandler;
        }
    }

}
