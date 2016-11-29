//-----------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2016 , Hairihan TECH, Ltd.  
//-----------------------------------------------------------------

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
        public virtual IWorkFlowCurrentService CreateWorkFlowCurrentService()
        {
            return new WorkFlowCurrentService();
        }

        public virtual IWorkFlowActivityAdminService CreateWorkFlowActivityAdminService()
        {
            return new WorkFlowActivityAdminService();
        }

        public virtual IWorkFlowProcessAdminService CreateWorkFlowProcessAdminService()
        {
            return new WorkFlowProcessAdminService();
        }
    }
}