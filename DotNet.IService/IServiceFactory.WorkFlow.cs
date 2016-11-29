//-----------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2016 , Hairihan TECH, Ltd.  
//-----------------------------------------------------------------

namespace DotNet.IService
{
    /// <summary>
    /// IServiceFactory
    /// 服务工厂接口定义
    /// 
    /// 修改记录
    /// 
    ///	    2013.06.07 版本：3.1 JiRiGaLa 整理函数顺序，这里用到了设计模式的闭包。
    ///	    2011.05.07 版本：3.0 JiRiGaLa 整理目录结构。
    ///	    2011.04.30 版本：2.0 JiRiGaLa 修改注释。
    ///	    2007.12.30 版本：1.0 JiRiGaLa 创建。
    ///	    
    /// <author>
    ///     <name>JiRiGaLa</name>
    ///     <date>2013.06.07</date>
    /// </author> 
    /// </summary>
    public partial interface IServiceFactory
    {
        /// <summary>
        /// 创建工作流审核步骤管理服务
        /// </summary>
        /// <returns>服务接口</returns>
        IWorkFlowActivityAdminService CreateWorkFlowActivityAdminService();

        /// <summary>
        /// 创建当前工作流服务
        /// </summary>
        /// <returns>服务接口</returns>
        IWorkFlowCurrentService CreateWorkFlowCurrentService();

        /// <summary>
        /// 创建工作流管理服务
        /// </summary>
        /// <returns>服务接口</returns>
		IWorkFlowProcessAdminService CreateWorkFlowProcessAdminService();
    }
}