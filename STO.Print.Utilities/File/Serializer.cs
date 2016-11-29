using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization.Formatters.Soap;
using System.Text;
using System.Xml.Serialization;

namespace STO.Print.Utilities
{
    /// <summary>
    /// 序列号操作辅助类
    /// </summary>
    /// 
    public class Serializer
    {
        private Serializer()
        {
        }

        #region 各种格式的序列化操作
        /// <summary>
        /// 序列化对象到二进制字节数组
        /// </summary>
        /// <param name="obj">待序列化的对象</param>
        /// <returns></returns>
        public static byte[] SerializeToBinary(object obj)
        {
            byte[] b = new byte[2500];
            MemoryStream ms = new MemoryStream();

            try
            {
                BinaryFormatter bformatter = new BinaryFormatter();
                bformatter.Serialize(ms, obj);
                ms.Seek(0, 0);
                if (ms.Length > b.Length) b = new byte[ms.Length];
                b = ms.ToArray();
            }
            catch (SerializationException e)
            {
                Console.WriteLine("Failed to serialize. Reason: " + e.Message);
                throw;
            }
            finally
            {
                ms.Close();
            }
            return b;
        }

        /// <summary>
        /// 序列化对象到指定的文件中
        /// </summary>
        /// <param name="obj">待序列化的对象</param>
        /// <param name="path">文件路径</param>
        /// <param name="mode">文件打开方式</param>
        public static void SerializeToBinary(object obj, string path, FileMode mode)
        {
            FileStream fs = new FileStream(path, mode);

            // Construct a BinaryFormatter and use it to serialize the data to the stream.
            BinaryFormatter formatter = new BinaryFormatter();
            try
            {
                formatter.Serialize(fs, obj);
            }
            catch (SerializationException e)
            {
                Console.WriteLine("序列化对象失败: " + e.Message);
                throw;
            }
            finally
            {
                fs.Close();
            }
        }

        /// <summary>
        /// 序列号对象到文件中，创建一个新文件
        /// </summary>
        /// <param name="obj">待序列化的对象</param>
        /// <param name="path">文件路径</param>
        public static void SerializeToBinary(object obj, string path)
        {
            SerializeToBinary(obj, path, FileMode.Create);
        }

        /// <summary>
        /// 序列化对象到Soap字符串中
        /// </summary>
        /// <param name="obj">待序列化的对象</param>
        /// <returns></returns>
        public static string SerializeToSoap(object obj)
        {
            string s = "";
            MemoryStream ms = new MemoryStream();

            try
            {
                SoapFormatter sformatter = new SoapFormatter();
                sformatter.Serialize(ms, obj);
                ms.Seek(0, 0);
                s = Encoding.ASCII.GetString(ms.ToArray());
            }
            catch (SerializationException e)
            {
                Console.WriteLine("Failed to serialize. Reason: " + e.Message);
                throw;
            }
            finally
            {
                ms.Close();
            }

            return s;
        }

        /// <summary>
        /// 序列化对象到Soap字符串中，并保存到文件
        /// </summary>
        /// <param name="obj">待序列化的对象</param>
        /// <param name="path">文件路径</param>
        /// <param name="mode">文件打开方式</param>
        public static void SerializeToSoap(object obj, string path, FileMode mode)
        {
            FileStream fs = new FileStream(path, mode);

            // Construct a BinaryFormatter and use it to serialize the data to the stream.
            SoapFormatter formatter = new SoapFormatter();
            try
            {
                formatter.Serialize(fs, obj);
            }
            catch (SerializationException e)
            {
                Console.WriteLine("Failed to serialize. Reason: " + e.Message);
                throw;
            }
            finally
            {
                fs.Close();
            }
        }

        /// <summary>
        /// 序列化对象到Soap字符串中，并保存到文件
        /// </summary>
        /// <param name="obj">待序列化的对象</param>
        /// <param name="path">文件路径</param>
        public static void SerializeToSoap(object obj, string path)
        {
            SerializeToSoap(obj, path, FileMode.Create);
        }

        /// <summary>
        /// 序列化对象到XML字符串中
        /// </summary>
        /// <param name="obj">待序列化的对象</param>
        /// <returns></returns>
        public static string SerializeToXml(object obj)
        {
            string s = "";
            MemoryStream ms = new MemoryStream();

            try
            {
                XmlSerializer serializer = new XmlSerializer(obj.GetType());
                serializer.Serialize(ms, obj);
                ms.Seek(0, 0);
                s = Encoding.ASCII.GetString(ms.ToArray());
            }
            catch (SerializationException e)
            {
                Console.WriteLine("Failed to serialize. Reason: " + e.Message);
                throw;
            }
            finally
            {
                ms.Close();
            }
            return s;
        }

        /// <summary>
        /// 序列化对象到XML字符串,并保存到文件中
        /// </summary>
        /// <param name="obj">待序列化的对象</param>
        /// <param name="path">文件路径</param>
        /// <param name="mode">文件打开方式</param>
        public static void SerializeToXmlFile(object obj, string path, FileMode mode)
        {
            FileStream fs = new FileStream(path, mode);

            //构建一个BinaryFormatter和使用它序列化的数据流。
            XmlSerializer serializer = new XmlSerializer(obj.GetType());
            try
            {
                serializer.Serialize(fs, obj);
            }
            catch (SerializationException e)
            {
                Console.WriteLine("Failed to serialize. Reason: " + e.Message);
                throw;
            }
            finally
            {
                fs.Close();
            }
        }

        /// <summary>
        /// 序列化对象到XML字符串,并保存到文件中
        /// </summary>
        /// <param name="obj">待序列化的对象</param>
        /// <param name="path">文件路径</param>
        public static void SerializeToXmlFile(object obj, string path)
        {
            SerializeToXmlFile(obj, path, FileMode.Create);
        } 
        #endregion


        /// <summary>
        /// 从指定的文件中反序列化到具体的对象
        /// </summary>
        /// <param name="type">对象的类型</param>
        /// <param name="path">文件路径</param>
        /// <returns></returns>
        public static object DeserializeFromXmlFile(Type type, string path)
        {
            object o = new object();
            FileStream fs = new FileStream(path, FileMode.Open);

            try
            {
                XmlSerializer serializer = new XmlSerializer(type);
                o = serializer.Deserialize(fs);
            }
            catch (SerializationException e)
            {
                Console.WriteLine("Failed to deserialize. Reason: " + e.Message);
                throw;
            }
            finally
            {
                fs.Close();
            }
            return o;
        }

        /// <summary>
        /// 从指定的XML字符串中反序列化到具体的对象
        /// </summary>
        /// <param name="type">对象的类型</param>
        /// <param name="s">XML字符串</param>
        /// <returns></returns>
        public static object DeserializeFromXml(Type type, string s)
        {
            object o = new object();

            try
            {
                XmlSerializer serializer = new XmlSerializer(type);
                o = serializer.Deserialize(new StringReader(s));
            }
            catch (SerializationException e)
            {
                Console.WriteLine("Failed to deserialize. Reason: " + e.Message);
                throw;
            }
            finally
            {
            }
            return o;
        }

        /// <summary>
        /// 从指定的Soap协议字符串中反序列化到具体的对象
        /// </summary>
        /// <param name="type">对象的类型</param>
        /// <param name="s">Soap协议字符串</param>
        /// <returns></returns>
        public static object DeserializeFromSoap(Type type, string s)
        {
            object o = new object();
            MemoryStream ms = new MemoryStream(new UTF8Encoding().GetBytes(s));

            try
            {
                SoapFormatter serializer = new SoapFormatter();
                o = serializer.Deserialize(ms);
            }
            catch (SerializationException e)
            {
                Console.WriteLine("Failed to deserialize. Reason: " + e.Message);
                throw;
            }
            finally
            {
            }
            return o;
        }

        /// <summary>
        /// 从指定的二进制字节数组中反序列化到具体的对象
        /// </summary>
        /// <param name="type">对象的类型</param>
        /// <param name="bytes">二进制字节数组</param>
        /// <returns></returns>
        public static object DeserializeFromBinary(Type type, byte[] bytes)
        {
            object o = new object();
            MemoryStream ms = new MemoryStream(bytes);

            try
            {
                BinaryFormatter serializer = new BinaryFormatter();
                o = serializer.Deserialize(ms);
            }
            catch (SerializationException e)
            {
                Console.WriteLine("Failed to deserialize. Reason: " + e.Message);
                throw;
            }
            finally
            {
            }
            return o;
        }

        /// <summary>
        /// 从指定的文件总，以二进制字节数组中反序列化到具体的对象
        /// </summary>
        /// <param name="type">对象的类型</param>
        /// <param name="bytes">二进制字节数组存储的文件</param>
        /// <returns></returns>
        public static object DeserializeFromBinary(Type type, string path)
        {
            object o = new object();
            FileStream fs = new FileStream(path, FileMode.Open);

            try
            {
                BinaryFormatter serializer = new BinaryFormatter();
                o = serializer.Deserialize(fs);
            }
            catch (SerializationException e)
            {
                Console.WriteLine("Failed to deserialize. Reason: " + e.Message);
                throw;
            }
            finally
            {
                fs.Close();
            }
            return o;
        }

        /// <summary>
        /// 获取对象的字节数组大小
        /// </summary>
        /// <param name="o">对象</param>
        /// <returns></returns>
        public static long GetByteSize(object o)
        {
            BinaryFormatter bFormatter = new BinaryFormatter();
            MemoryStream stream = new MemoryStream();
            bFormatter.Serialize(stream, o);
            return stream.Length;
        }

        /// <summary>
        /// 克隆一个对象
        /// </summary>
        /// <param name="o">对象</param>
        /// <returns></returns>
        public static object Clone(object o)
        {
            BinaryFormatter bFormatter = new BinaryFormatter();
            MemoryStream stream = new MemoryStream();
            object cloned = null;

            try
            {
                bFormatter.Serialize(stream, o);
                stream.Seek(0, SeekOrigin.Begin);
                cloned = bFormatter.Deserialize(stream);
            }
            catch //(Exception e)
            {
            }
            finally
            {
                stream.Close();
            }

            return cloned;
        }
    }
}