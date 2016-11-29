using System;
using Xilium.CefGlue;

namespace Xilium.CefGlue.Client.Renderer
{

    class DemoRenderProcessHandler : CefRenderProcessHandler
    {
        protected override bool OnProcessMessageReceived(CefBrowser browser, CefProcessId sourceProcess, CefProcessMessage message)
        {
            //Console.WriteLine("Render::OnProcessMessageReceived: SourceProcess={0}", sourceProcess);
            //Console.WriteLine("Message Name={0} IsValid={1} IsReadOnly={2}", message.Name, message.IsValid, message.IsReadOnly);
            //var arguments = message.Arguments;
            //for (var i = 0; i < arguments.Count; i++)
            //{
            //    var type = arguments.GetValueType(i);
            //    object value;
            //    switch (type)
            //    {
            //        case CefValueType.Null: value = null; break;
            //        case CefValueType.String: value = arguments.GetString(i); break;
            //        case CefValueType.Int: value = arguments.GetInt(i); break;
            //        case CefValueType.Double: value = arguments.GetDouble(i); break;
            //        case CefValueType.Bool: value = arguments.GetBool(i); break;
            //        default: value = null; break;
            //    }

            //    Console.WriteLine("  [{0}] ({1}) = {2}", i, type, value);
            //}
            //if (message.Name == "myMessage2") return true;

            //var message2 = CefProcessMessage.Create("myMessage2");
            //var success = browser.SendProcessMessage(CefProcessId.Renderer, message2);
            //Console.WriteLine("Sending myMessage2 to renderer process = {0}", success);

            //var message3 = CefProcessMessage.Create("myMessage3");
            //var success2 = browser.SendProcessMessage(CefProcessId.Browser, message3);
            //Console.WriteLine("Sending myMessage3 to browser process = {0}", success);

            if (message.Name == "ExecuteJavaScript")
            {
                string code = message.Arguments.GetString(0);
                var context = browser.GetMainFrame().V8Context;
                context.Enter();
                try
                {
                    var global = context.GetGlobal();
                    var evalFunc = global.GetValue("eval");
                    CefV8Value arg0 = CefV8Value.CreateString(code);

                    var rtn = evalFunc.ExecuteFunctionWithContext(context, evalFunc,
                        new CefV8Value[] { arg0 });
                    if (evalFunc.HasException)
                    {
                        var exception = evalFunc.GetException();
                        var message3 = CefProcessMessage.Create("Exception");
                        var arguments = message3.Arguments;
                        arguments.SetString(0, exception.Message + " At Line" + exception.LineNumber);
                        var success2 = browser.SendProcessMessage(CefProcessId.Browser, message3);

                    }
                    else
                    {
                        var message3 = CefProcessMessage.Create("JavascriptExecutedResult");
                        var arguments = message3.Arguments;
                        arguments.SetString(0, rtn.GetStringValue());
                        var success2 = browser.SendProcessMessage(CefProcessId.Browser, message3);
                    }
                }

                finally
                {
                    context.Exit();
                }
            }

            return false;
        }

        protected override void OnWebKitInitialized()
        {
            #region 注册自己.net类库供JS调用
            CefRuntimeEx.RegisterJsObject("exampleObject", new ExampleObject());
            #endregion

            #region 调用自己封装的JS类库
            //CefRuntimeEx.RegisterExtension();
            #endregion

            base.OnWebKitInitialized();
        }

        protected override void OnContextCreated(CefBrowser browser, CefFrame frame, CefV8Context context)
        {
            foreach (var kvp in CefRuntimeEx.GetObjects())
            {
                BindingHandler.Bind(kvp.Key, kvp.Value, context.GetGlobal()); 
            }

            base.OnContextCreated(browser, frame, context);
        }
    }
}
