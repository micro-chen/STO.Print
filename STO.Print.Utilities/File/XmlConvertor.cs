using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace STO.Print.Utilities
{
    /// <summary>
    /// 这个类提供了一些实用的方法来转换XML和对象。
    /// </summary>
    public sealed class XmlConvertor
    {
        private XmlConvertor()
        {
        }

        /// <summary>
        /// 将XML字符串转换到指定的对象。
        /// </summary>
        /// <param name="xml">XML字符串。</param>
        /// <param name="type">对象的类型。</param>
        /// <returns>从XML字符串反序列化的对象。</returns>
        public static object XmlToObject(string xml, Type type)
        {
            if (null == xml)
            {
                throw new ArgumentNullException("xml");
            }
            if (null == type)
            {
                throw new ArgumentNullException("type");
            }

            object obj = null;
            XmlSerializer serializer = new XmlSerializer(type);
            StringReader strReader = new StringReader(xml);
            XmlReader reader = new XmlTextReader(strReader);

            try
            {
                obj = serializer.Deserialize(reader);
            }
            catch (InvalidOperationException ie)
            {
                throw new InvalidOperationException("Can not convert xml to object", ie);
            }
            finally
            {
                reader.Close();
            }
            return obj;
        }

        /// <summary>
        /// 转换为XML字符串的对象。
        /// </summary>
        /// <param name="obj">要序列化的对象</param>
        /// <param name="toBeIndented"><c>true</c> 如果想要的XML字符串的目的，否则 <c>false</c>.</param>
        /// <returns>XML字符串。</returns>
        public static string ObjectToXml(object obj, bool toBeIndented)
        {
            if (null == obj)
            {
                throw new ArgumentNullException("obj");
            }
            UTF8Encoding encoding = new UTF8Encoding(false);
            XmlSerializer serializer = new XmlSerializer(obj.GetType());
            MemoryStream stream = new MemoryStream();
            XmlTextWriter writer = new XmlTextWriter(stream, encoding);
            writer.Formatting = (toBeIndented ? Formatting.Indented : Formatting.None);

            try
            {
                serializer.Serialize(writer, obj);
            }
            catch (InvalidOperationException)
            {
                throw new InvalidOperationException("Can not convert object to xml.");
            }
            finally
            {
                writer.Close();
            }

            string xml = encoding.GetString(stream.ToArray());
            return xml;
        }
    }
}