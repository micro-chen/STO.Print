using Xilium.CefGlue;

namespace Xilium.CefGlue.Client
{
    /// <summary>
    /// 属性访问器
    /// </summary>
    public class PropertyAccessor : CefV8Accessor
    {

        public object myval_;
        protected override bool Get(string name, CefV8Value obj, out CefV8Value returnValue, out string exception)
        {
            returnValue = null;
            exception = null;
            V8ValueClrMap.ConvertToV8Value(myval_);
            return true;
        }

        protected override bool Set(string name, CefV8Value obj, CefV8Value value, out string exception)
        { 
            exception = null;
            myval_ = V8ValueClrMap.GetClrValue(value);
            return true;
        }
         
    }
}
