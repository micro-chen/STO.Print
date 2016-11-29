
using System.Collections;
using System.Reflection;

namespace STO.Print.Utilities
{
    public class Reflect<T> where T : class 
    {
        private static Hashtable m_objCache = null;
        public static Hashtable ObjCache
        {
            get
            {
                if (m_objCache == null)
                {
                    m_objCache = new Hashtable();
                }
                return m_objCache;
            }
        }

        public static T Create(string sName, string sFilePath)
        {
            return Create(sName, sFilePath, true);
        }
        public static T Create(string sName, string sFilePath, bool bCache)
        {
            string CacheKey = sFilePath + "." + sName;
            T objType = null;
            if (bCache)
            {
                objType = (T)ObjCache[CacheKey];    //从缓存读取 
                if (!ObjCache.ContainsKey(CacheKey))
                {
                    Assembly assObj = CreateAssembly(sFilePath);
                    object obj = assObj.CreateInstance(CacheKey);
                    objType = (T)obj;

                    ObjCache.Add(CacheKey, objType);// 写入缓存 将DAL内某个对象装入缓存
                }
            }
            else
            {
                objType = (T)CreateAssembly(sFilePath).CreateInstance(CacheKey); //反射创建 
            }

            return objType;
        }

        public static Assembly CreateAssembly(string sFilePath)
        {
            Assembly assObj = (Assembly)ObjCache[sFilePath];
            if (assObj == null)
            {
                assObj = Assembly.Load(sFilePath);
                if (!ObjCache.ContainsKey(sFilePath))
                {
                    ObjCache.Add(sFilePath, assObj);//将整个ITDB。DAL。DLL装入缓存
                }
            }
            return assObj;
        }
    }
}
