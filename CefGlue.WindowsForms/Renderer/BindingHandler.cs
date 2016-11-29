using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using Xilium.CefGlue;

namespace Xilium.CefGlue.Client.Renderer
{
    public class BindingHandler : CefV8Handler
    {
        /// <summary>
        /// 绑定数据并注册对象
        /// 说明：已经过滤特殊名称，即不含系统自动生成的属性、方法
        /// </summary>
        /// <param name="name">对象名称</param>
        /// <param name="obj">需要绑定的对象</param>
        /// <param name="window">用于注册的V8 JS引擎对象，类似于整个程序的窗口句柄</param>
        public static void Bind(string name, object obj, CefV8Value window)
        {
            var unmanagedWrapper = new UnmanagedWrapper(obj);

            var propertyAccessor = new PropertyAccessor();
            //javascript对象包
            CefV8Value javascriptWrapper = CefV8Value.CreateObject(propertyAccessor);
            //将非托管对象放到javascript对象包
            javascriptWrapper.SetUserData(unmanagedWrapper);

            var handler = new BindingHandler();
            unmanagedWrapper.Properties = GetProperties(obj.GetType());
            CreateJavascriptProperties(javascriptWrapper, unmanagedWrapper.Properties);

            IList<string> methodNames = GetMethodNames(obj.GetType(), unmanagedWrapper.Properties);
            CreateJavascriptMethods(handler, javascriptWrapper, methodNames);

            window.SetValue(name, javascriptWrapper, CefV8PropertyAttribute.None);
        }

        /// <summary>
        /// 获取属性集合
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        private static IList<string> GetProperties(Type type)
        {
            return type.GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly).Select(n => n.Name).ToList();
        }

        /// <summary>
        /// 获取方法集合
        /// </summary>
        /// <param name="type">要操作的对象类型</param>
        /// <param name="propertiesNames">需要过滤的属性集合</param>
        /// <returns></returns>
        private static IList<string> GetMethodNames(Type type, IList<string> propertiesNames)
        {

            return type.GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly).Where((n => n.Name != type.Name)).Where(
                n =>
                {
                    return  (!n.Name.StartsWith("set_") && !n.Name.StartsWith("get_")) || propertiesNames.FirstOrDefault(p => "set_" + p == n.Name || "get_" + p == n.Name) == null;

                }).Select(n => n.Name).ToList();
        }

        /// <summary>
        /// 创建JavaScript属性
        /// </summary>
        /// <param name="javascriptWrapper">经过V8 JS引擎处理后的对象</param>
        /// <param name="properties">属性键值对集合</param>
        private static void CreateJavascriptProperties(CefV8Value javascriptWrapper, IList<string> properties)
        {
            var unmanagedWrapper = (UnmanagedWrapper)(javascriptWrapper.GetUserData());

            foreach (string property in properties)
            {
                string jsPropertyName = LowercaseFirst(property);
                var PropertyInfo = unmanagedWrapper.ClrObject.GetType().GetProperty(property, BindingFlags.Instance | BindingFlags.Public | BindingFlags.InvokeMethod | BindingFlags.DeclaredOnly);
                var v8Value = V8ValueClrMap.ConvertToV8Value(PropertyInfo.GetValue(unmanagedWrapper.ClrObject, null));
                javascriptWrapper.SetValue(jsPropertyName, v8Value, CefV8PropertyAttribute.None);
            }
        }

        /// <summary>
        /// 创建JavaScript方法
        /// </summary>
        /// <param name="handler">处理程序</param>
        /// <param name="javascriptObject">经过V8 JS引擎处理后的对象</param>
        /// <param name="methodNames">方法键值对集合</param>
        public static void CreateJavascriptMethods(CefV8Handler handler, CefV8Value javascriptWrapper, IList<String> methodNames)
        {
            var unmanagedWrapper = (UnmanagedWrapper)(javascriptWrapper.GetUserData());

            foreach (string methodName in methodNames)
            {
                string jsMethodName = LowercaseFirst(methodName);
                //unmanagedWrapper.AddMethodMapping(methodName, jsMethodName);
                javascriptWrapper.SetValue(jsMethodName, CefV8Value.CreateFunction(jsMethodName, handler), CefV8PropertyAttribute.None);
            }
        }

        private static string LowercaseFirst(string methodName)
        {
            return methodName[0].ToString().ToLower() + methodName.Substring(1);
        }

        /// <summary>
        /// 执行JS方法
        /// </summary>
        /// <param name="name">方法名</param>
        /// <param name="obj"></param>
        /// <param name="arguments"></param>
        /// <param name="returnValue"></param>
        /// <param name="exception"></param>
        /// <returns></returns>
        protected override bool Execute(string name, CefV8Value obj, CefV8Value[] arguments, out CefV8Value returnValue, out string exception)
        {
            exception = null;
            try
            {
                List<object> paras = V8ValueClrMap.ConvertToClrParameters(arguments);
                var clrObj = V8ValueClrMap.GetClrValue(obj);
                Type t =null;
                if (obj.IsObject)
                {
                    t = clrObj.GetType();
                }
                
                var function = t.GetMethod(name, BindingFlags.Instance | BindingFlags.Public | BindingFlags.InvokeMethod | BindingFlags.IgnoreCase | BindingFlags.DeclaredOnly);
                if (function != null)
                {
                    var objRst = function.Invoke(clrObj, paras.Count == 0 ? null : paras.ToArray());
                    if (objRst is CefV8Handler)
                    {
                        returnValue = CefV8Value.CreateObject(null);
                        Bind(name, objRst as CefV8Handler, returnValue);
                    }
                    else
                    {
                        returnValue = V8ValueClrMap.ConvertToV8Value(objRst);
                    }
                }
                else
                {
                    exception = "方法没有找到:" + name;
                    returnValue = CefV8Value.CreateString("");
                    var message3 = CefProcessMessage.Create("Exception");
                    message3.Arguments.SetString(0, exception);
                    //var success2 = Browser.SendProcessMessage(CefProcessId.Browser, message3);
                    
                    return true;
                }
                return true;
            }
            catch (Exception ee)
            {
                returnValue = CefV8Value.CreateString("");
                exception = ee.Message;

                returnValue = CefV8Value.CreateString("");
                var message3 = CefProcessMessage.Create("Exception");
                message3.Arguments.SetString(0, ee.ToString());
                //var success2 = Browser.SendProcessMessage(CefProcessId.Browser, message3);

                return true;
            }

        }

    }
}
