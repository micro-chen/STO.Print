using Xilium.CefGlue.Client.bs;

namespace Xilium.CefGlue.WindowsForms
{
    using System;
    using System.Collections.Generic;
    using Xilium.CefGlue;

    public class CefWebClient : CefClient
    {
        private readonly CefWebBrowser _core;
        private readonly CefWebLifeSpanHandler _lifeSpanHandler;
        private readonly CefWebDisplayHandler _displayHandler;
        private readonly CefWebLoadHandler _loadHandler;
        private readonly CefWebRequestHandler _requestHandler;

        private readonly CefDownloadHandler _downloadHandler;
        private readonly CefJSDialogHandler _jsDialogHandler;
        private readonly CefContextMenuHandler _contextMenuHandler;
        private readonly CefKeyboardHandler _keyboardHandler;

        public CefWebClient(CefWebBrowser core)
        {
            _core = core;
            _lifeSpanHandler = new CefWebLifeSpanHandler(_core);
            _displayHandler = new CefWebDisplayHandler(_core);
            _loadHandler = new CefWebLoadHandler(_core);
            _requestHandler = new CefWebRequestHandler(_core);

            _downloadHandler = new BsDownloadHandler();
            _jsDialogHandler = new BsJsDialogHandler();
            _contextMenuHandler = new BsContextMenuHandler();
            _keyboardHandler = new BsCefKeyboardHandler();
        }

        protected CefWebBrowser Core { get { return _core; } }

        protected override CefLifeSpanHandler GetLifeSpanHandler()
        {
            return _lifeSpanHandler;
        }

        protected override CefDisplayHandler GetDisplayHandler()
        {
            return _displayHandler;
        }

        protected override CefLoadHandler GetLoadHandler()
        {
            return _loadHandler;
        }

        protected override CefRequestHandler GetRequestHandler()
        {
            return _requestHandler;
        }

        protected override CefDownloadHandler GetDownloadHandler()
        {
            return _downloadHandler;
        }
        protected override CefJSDialogHandler GetJSDialogHandler()
        {
            return _jsDialogHandler;
        }
        protected override CefContextMenuHandler GetContextMenuHandler()
        {
            return _contextMenuHandler;
        }
        protected override CefKeyboardHandler GetKeyboardHandler()
        {
            return _keyboardHandler;
        }
    }
}
