//-----------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2016 , Hairihan TECH, Ltd.  
//-----------------------------------------------------------------

using System;

namespace DotNet.Business
{
    using DotNet.IService;

    /// <summary>
    /// ServiceFactory
    /// 本地服务的具体实现接口
    /// 
    /// 修改记录
    /// 
    ///		2011.08.21 版本：2.0 JiRiGaLa 方便在系统组件化用,命名进行了修改。
    ///		2007.12.30 版本：1.0 JiRiGaLa 创建。
    ///		
    /// <author>
    ///		<name>JiRiGaLa</name>
    ///		<date>2011.08.21</date>
    /// </author> 
    /// </summary>
    public partial class ServiceFactory : IServiceFactory
    {
        public void InitService()
        {
        }

        public virtual IAreaService CreateAreaService()
        {
            return new AreaService();
        }

        public virtual ILogOnService CreateLogOnService()
        {
            return new LogOnService();
        }

        public virtual ISequenceService CreateSequenceService()
        {
            return new SequenceService();
        }

        public virtual IUserService CreateUserService()
        {
            return new UserService();
        }

        public virtual ILogService CreateLogService()
        {
            return new LogService();
        }

        public virtual IStationService CreateStationService()
        {
            return new StationService();
        }

        public virtual IExceptionService CreateExceptionService()
        {
            return new ExceptionService();
        }

        public virtual IPermissionService CreatePermissionService()
        {
            return new PermissionService();
        }

        public virtual IOrganizeService CreateOrganizeService()
        {
            return new OrganizeService();
        }

        public virtual IDepartmentService CreateDepartmentService()
        {
            return new DepartmentService();
        }

        public virtual IBaseItemsService CreateBaseItemsService()
        {
            return new BaseItemsService();
        }

        public virtual IBaseItemDetailsService CreateBaseItemDetailsService()
        {
            return new BaseItemDetailsService();
        }

        public virtual IBaseItemsService CreateItemsService()
        {
            return new ItemsService();
        }

        public virtual IBaseItemDetailsService CreateItemDetailsService()
        {
            return new ItemDetailsService();
        }

        public virtual IModuleService CreateModuleService()
        {
            return new ModuleService();
        }

        public virtual IMobileService CreateMobileService()
        {
            return new MobileService();
        }

        public virtual IModifyRecordService CreateModifyRecordService()
        {
            return new ModifyRecordService();
        }

        public virtual IStaffService CreateStaffService()
        {
            return new StaffService();
        }

        public virtual IRoleService CreateRoleService()
        {
            return new RoleService();
        }

        public virtual ILanguageService CreateLanguageService()
        {
            return new LanguageService();
        }

        public virtual IMessageService CreateMessageService()
        {
            return new MessageService();
        }

        public virtual IFileService CreateFileService()
        {
            return new FileService();
        }

        public virtual IFolderService CreateFolderService()
        {
            return new FolderService();
        }

        public virtual IParameterService CreateParameterService()
        {
            return new ParameterService();
        }

        public virtual IServicesLicenseService CreateServicesLicenseService()
        {
            return new ServicesLicenseService();
        }

        public virtual ITableColumnsService CreateTableColumnsService()
        {
            return new TableColumnsService();
        }

		public IService CreateService<Service, IService>() where Service : IService, new()
		{
			return new Service();
		}

		public Service CreateService<Service>() where Service : new()
		{
			return new Service();
		}
    }
}