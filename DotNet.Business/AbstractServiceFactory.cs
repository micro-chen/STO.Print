//-----------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2016 , Hairihan TECH, Ltd.  
//-----------------------------------------------------------------

using System.Configuration;
using System.Reflection;

namespace DotNet.Business
{
    using DotNet.Utilities;
    using DotNet.IService;

    /// <summary>
    /// AbstractServiceFactory
    /// 
    /// 修改记录
    /// 
    ///		2007.12.27 版本：1.0 JiRiGaLa 创建。
    ///		
    /// <author>
    ///		<name>JiRiGaLa</name>
    ///		<date>2007.12.27</date>
    /// </author> 
    /// </summary>
    public abstract class AbstractServiceFactory:System.MarshalByRefObject
    {
        // Look up the Dao implementation we should be using
        // private static readonly string serviceFactoryClass = ConfigurationManager.AppSettings["ServiceFactory"];
        
        public AbstractServiceFactory() 
        {
        }

        public virtual IServiceFactory GetServiceFactory()
        {
            return GetServiceFactory(BaseSystemInfo.Service);
        }

        public virtual IServiceFactory GetServiceFactory(string servicePath)
        {
            string className = servicePath + "." + BaseSystemInfo.ServiceFactory;
            return (IServiceFactory)Assembly.Load(servicePath).CreateInstance(className);
        }

        public virtual IServiceFactory GetServiceFactory(string servicePath, string serviceFactoryClass)
        {
            string className = servicePath + "." + serviceFactoryClass;
            return (IServiceFactory)Assembly.Load(servicePath).CreateInstance(className);
        }
    }
}