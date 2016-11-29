using System.Collections.Generic;
using Xilium.CefGlue;

namespace Xilium.CefGlue.Client
{
    public class UnmanagedWrapper : CefUserData
    {
        /// <summary>
        /// 非托管对象包
        /// </summary>
        /// <param name="obj">需要绑定的C#对象</param>
        public UnmanagedWrapper(object obj)
        {
            this.ClrObject = obj;
        }

        public IList<string> Properties { get; set; }
        public object ClrObject { get; set; }

        //public IDictionary<string, string> MethodMapping { get; set; }
        //public IDictionary<string, string> PropertyMapping { get; set; }

        //public void AddMethodMapping(string methodName, string jsMethodName)
        //{
        //    MethodMapping.Add(methodName, jsMethodName);
        //}

        //public void AddPropertyMapping(string property, string jsPropertyName)
        //{
        //    PropertyMapping.Add(property, jsPropertyName);
        //}

    }
}
