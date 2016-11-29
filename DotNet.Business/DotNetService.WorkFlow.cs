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
    public partial class DotNetService : AbstractServiceFactory		//, IDotNetService
    {   
        /// <summary>
        /// 创建当前工作流服务
        /// </summary>
        /// <returns>服务接口</returns>
        public virtual IWorkFlowCurrentService WorkFlowCurrentService
        {
            get
            {
                return serviceFactory.CreateWorkFlowCurrentService();
            }
        }

        /// <summary>
        /// 创建工作流审核步骤管理服务
        /// </summary>
        /// <returns>服务接口</returns>
        public virtual IWorkFlowActivityAdminService WorkFlowActivityAdminService
        {
            get
            {
                return serviceFactory.CreateWorkFlowActivityAdminService();
            }
        }

        /// <summary>
        /// 创建工作流管理服务
        /// </summary>
        /// <returns>服务接口</returns>
        public virtual IWorkFlowProcessAdminService WorkFlowProcessAdminService
        {
            get
            {
                return serviceFactory.CreateWorkFlowProcessAdminService();
            }
        }
    }
}