//-----------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2016 , Hairihan TECH, Ltd.  
//-----------------------------------------------------------------

using System;
using System.ServiceModel;

namespace DotNet.Business
{
    using DotNet.Utilities;
    using DotNet.IService;

    /// <summary>
    /// DotNetService
    /// 
    /// 修改记录
    /// 
    ///		2011.08.21 版本：2.0 JiRiGaLa 方便在系统组件化用,命名进行了修改。
    ///		2007.12.27 版本：1.0 JiRiGaLa 创建。
    ///		
    /// <author>
    ///		<name>JiRiGaLa</name>
    ///		<date>2011.08.21</date>
    /// </author> 
    /// </summary>
    [ServiceBehavior(IncludeExceptionDetailInFaults = true)]
    public partial class DotNetService : AbstractServiceFactory		//, IDotNetService
    {
        // private static readonly string servicePath = BaseSystemInfo.Service;
        // private static string servicePath = BaseSystemInfo.Service;
        private static readonly string serviceFactoryClass = BaseSystemInfo.ServiceFactory;
        
        public DotNetService()
        {
            serviceFactory = GetServiceFactory(BaseSystemInfo.Service, serviceFactoryClass);
        }

        /// <summary>
        /// 可以从外部指定调用哪个服务
        /// </summary>
        /// <param name="service">实现的服务</param>
        public DotNetService(string service)
        {
            BaseSystemInfo.Service = service;
            serviceFactory = GetServiceFactory(BaseSystemInfo.Service, serviceFactoryClass);
        }

        private IServiceFactory serviceFactory = null;
		//public IService CreateService<Service, IService>() where Service : IService, new()
		//{
		//    return  serviceFactory.CreateService<Service, IService>();
		//}

		//public Service CreateService<Service>() where Service : new()
		//{
		//    return serviceFactory.CreateService<Service>();
		//}

        public void InitService()
        {
            serviceFactory.InitService();
        }


        // 这里不能继续用单实例了，会遇到WCF回收资源的问题

        private static DotNetService instance = null;
        private static object locker = new Object();

        public static DotNetService Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (locker)
                    {
                        if (instance == null)
                        {
                            instance = new DotNetService();
                        }
                    }
                }
                return instance;
            }
        }

        public virtual IAreaService AreaService
        {
            get
            {
                return serviceFactory.CreateAreaService();
            }
        }

        public virtual ILogOnService LogOnService
        {
            get
            {
                return serviceFactory.CreateLogOnService();
            }
        }

        public virtual ISequenceService SequenceService
        {
            get
            {
                return serviceFactory.CreateSequenceService();
            }
        }

        public virtual IUserService UserService
        {
            get
            {
                return serviceFactory.CreateUserService();
            }
        }

        public virtual ILogService LogService
        {
            get
            {
                return serviceFactory.CreateLogService();
            }
        }

        public virtual IStationService StationService
        {
            get
            {
                return serviceFactory.CreateStationService();
            }
        }

        public virtual IExceptionService ExceptionService
        {
            get
            {
                return serviceFactory.CreateExceptionService();
            }
        }

        public virtual IPermissionService PermissionService
        {
            get
            {
                return serviceFactory.CreatePermissionService();
            }
        }

        public virtual IServicesLicenseService ServicesLicenseService
        {
            get
            {
                return serviceFactory.CreateServicesLicenseService();
            }
        }

        public virtual IOrganizeService OrganizeService
        {
            get
            {
                return serviceFactory.CreateOrganizeService();
            }
        }

        public virtual IDepartmentService DepartmentService
        {
            get
            {
                return serviceFactory.CreateDepartmentService();
            }
        }

        public virtual IBaseItemsService BaseItemsService
        {
            get
            {
                return serviceFactory.CreateBaseItemsService();
            }
        }

        public virtual IBaseItemDetailsService BaseItemDetailsService
        {
            get
            {
                return serviceFactory.CreateBaseItemDetailsService();
            }
        }

        public virtual IBaseItemsService ItemsService
        {
            get
            {
                return serviceFactory.CreateItemsService();
            }
        }

        public virtual IBaseItemDetailsService ItemDetailsService
        {
            get
            {
                return serviceFactory.CreateItemDetailsService();
            }
        }

        public virtual IModuleService ModuleService
        {
            get
            {
                return serviceFactory.CreateModuleService();
            }
        }

        public virtual IMobileService MobileService
        {
            get
            {
                return serviceFactory.CreateMobileService();
            }
        }

        public virtual IModifyRecordService ModifyRecordService
        {
            get
            {
                return serviceFactory.CreateModifyRecordService();
            }
        }

        public virtual IStaffService StaffService
        {
            get
            {
                return serviceFactory.CreateStaffService();
            }
        }

        public virtual IRoleService RoleService
        {
            get
            {
                return serviceFactory.CreateRoleService();
            }
        }

        public virtual ILanguageService LanguageService
        {
            get
            {
                return serviceFactory.CreateLanguageService();
            }
        }

        public virtual IMessageService MessageService
        {
            get
            {
                return serviceFactory.CreateMessageService();
            }
        }

        public virtual IFileService FileService
        {
            get
            {
                return serviceFactory.CreateFileService();
            }
        }

        public virtual IFolderService FolderService
        {
            get
            {
                return serviceFactory.CreateFolderService();
            }
        }

        public virtual IParameterService ParameterService
        {
            get
            {
                return serviceFactory.CreateParameterService();
            }
        }

        /// <summary>
        /// 表字段结构
        /// </summary>
        /// <returns>服务接口</returns>
        public virtual ITableColumnsService TableColumnsService
        {
            get
            {
                return serviceFactory.CreateTableColumnsService();
            }
        }
    }
}