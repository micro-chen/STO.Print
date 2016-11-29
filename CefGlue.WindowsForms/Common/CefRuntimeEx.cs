using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Xilium.CefGlue.Client.Renderer;
using Xilium.CefGlue;

namespace Xilium.CefGlue.Client
{
    public class CefRuntimeEx
    {
        
        private static IList<JsObject> _jsObjects=new List<JsObject>();
        public static IList<JsObject> GetObjects()
        {
            return _jsObjects;
        }

        public static void RegisterJsObject(string objectNmae, object Object)
        {
            _jsObjects.Add(new JsObject() { Key = objectNmae ,Value = Object});
        }
        
        public static void RegisterExtension()
        {

        }

        public static string GetExtensionsResource()
        {
            System.Reflection.Assembly asm = System.Reflection.Assembly.GetExecutingAssembly();
            var str = new StringBuilder();
            foreach (var rs in asm.GetManifestResourceNames().Where(n => n.Contains(".Chrome.Extensions.jquery")))
            {
                str.AppendLine(new System.IO.StreamReader(asm.GetManifestResourceStream(rs), EncodingType.GetType(asm.GetManifestResourceStream(rs))).ReadToEnd());
            }

            return str.ToString();
        
        }

    }

    public class JsObject
    {
        

        public string Key { get; set; }
        public object Value { get; set; }
    }
}
