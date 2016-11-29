//-----------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2015 , STO TECH, Ltd. 
//-----------------------------------------------------------------

using System;
using System.Collections;
using System.Reflection;
using System.Windows.Forms;

namespace STO.Print.Utilities
{
    /// <summary>
    /// CacheManagerHelper
    /// Assembly 缓存服务
    /// 
    /// 修改纪录
    /// 
    ///		2008.06.05 版本：1.0 YangHengLian 创建。
    ///		
    /// <author>
    ///		<name>YangHengLian</name>
    ///		<date>2008.06.05</date>
    /// </author> 
    /// </summary>
    public class CacheManagerHelper
    {
        private CacheManagerHelper()
        {
        }

        private readonly Hashtable _objectCacheStore = new Hashtable();

        private static CacheManagerHelper _instance;
        private static readonly object Locker = new Object();

        public static CacheManagerHelper Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (Locker)
                    {
                        if (_instance == null)
                        {
                            return _instance = new CacheManagerHelper();
                        }
                    }
                }
                return _instance;
            }
        }

        /// <summary>
        /// 获得一个窗体
        /// </summary>
        /// <param name="assemblyName"></param>
        /// <param name="formName">窗体名</param>
        /// <returns>窗体</returns>
        public Form GetForm(string formName, string assemblyName = "DotNet.WinForm")
        {
            Type type = GetType(formName, assemblyName);
            return (Form)Activator.CreateInstance(type);
        }

        public Type GetType(string name, string assemblyName = "DotNet.WinForm")
        {
            Assembly assembly = this.Load(assemblyName);
            Type type = assembly.GetType(assemblyName + "." + name, true, false);
            return type;
        }

        /// <summary>
        /// 加载 Assembly
        /// </summary>
        /// <param name="assemblyName">命名空间</param>
        /// <returns>Assembly</returns>
        public Assembly Load(string assemblyName)
        {
            Assembly assembly = null;
            if (this._objectCacheStore.ContainsKey(assemblyName))
            {
                assembly = (Assembly)this._objectCacheStore[assemblyName];
            }
            else
            {
                assembly = Assembly.Load(assemblyName);
                // assembly = Assembly.LoadFrom(assemblyName);
                // assembly = Assembly.LoadFrom(assemblyName + ".exe");
                this._objectCacheStore.Add(assemblyName, assembly);
            }
            return assembly;
        }

        public void Add(string key, object storeObject)
        {
            if (!this._objectCacheStore.ContainsKey(key))
            {
                this._objectCacheStore.Add(key, storeObject);
            }
        }

        public object Retrieve(string key)
        {
            return this._objectCacheStore[key];
        }

        /// <summary>
        /// 删除缓存对象
        /// </summary>
        /// <param name="key">缓存对象名</param>
        /// <returns></returns>
        public void Remove(string key)
        {
            this._objectCacheStore.Remove(key);
        }
    }
}