using System;
using System.Collections.Generic;
using Xilium.CefGlue;

namespace Xilium.CefGlue.Client
{
    public class V8ValueClrMap
    {
        public static List<object> ConvertToClrParameters(CefV8Value[] arguments)
        {
            List<object> rtn = new List<object>();
            if (arguments == null || arguments.Length == 0)
            {
                return rtn;
            }
            foreach (var v in arguments)
            {
                rtn.Add(GetClrValue(v));
            }
            return rtn;
        }
        public static object GetClrValue(CefV8Value v)
        {
            if (v.IsArray)
            {
                int length = v.GetArrayLength();
                object[] objs = new object[length];
                for (int i = 0; i < length; i++)
                {
                    var value = v.GetValue(i);
                    objs[i] = GetClrValue(value);
                }
                return objs;
            }

            if (v.IsBool) return v.GetBoolValue();

            if (v.IsDate) return v.GetDateValue();
            if (v.IsInt) return v.GetIntValue();

            if (v.IsDouble) return v.GetDoubleValue();

            if (v.IsFunction)
            {
                throw new NotSupportedException("IsFunction");
            }


            if (v.IsNull) return null;
            if (v.IsObject)
            {
                //throw new NotSupportedException("IsObject");
                //todo:这里可能存在BUG
                var map = v.GetUserData() as UnmanagedWrapper;
                if (map != null)
                    return map.ClrObject;
                return null;
            }
            if (v.IsString) return v.GetStringValue();
            if (v.IsUInt) return v.GetUIntValue();
            if (v.IsUndefined) return null;
            if (v.IsUserCreated)
            {
                throw new NotSupportedException("IsUserCreated");
            }
            throw new NotSupportedException("??");

        }

        public static CefV8Value ConvertToV8Value(object o)
        {
            if (o == null) return CefV8Value.CreateUndefined();
            if (o is bool) return CefV8Value.CreateBool((bool)o);
            if (o is DateTime) return CefV8Value.CreateDate((DateTime)o);
            if (o is double) return CefV8Value.CreateDouble((double)o);
            if (o is int) return CefV8Value.CreateInt((int)o);
            if (o is string) return CefV8Value.CreateString((string)o);
            if (o is uint) return CefV8Value.CreateUInt((uint)o);
            if (o is Array)
            {
                var a = (Array)o;
                var rtn = CefV8Value.CreateArray(a.Length);
                for (int i = 0; i < a.Length; i++)
                {
                    rtn.SetValue(i, ConvertToV8Value(a.GetValue(i)));
                }
                return rtn;
            }
            if (o is System.Collections.IList)
            {
                var a = (System.Collections.IList)o;
                var rtn = CefV8Value.CreateArray(a.Count);
                for (int i = 0; i < a.Count; i++)
                {
                    rtn.SetValue(i, ConvertToV8Value(a[i]));
                }
                return rtn;
            }
            throw new NotSupportedException("??");
        }
    }
}
