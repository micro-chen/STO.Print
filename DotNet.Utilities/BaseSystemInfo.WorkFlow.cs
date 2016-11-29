//-----------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2016 , Hairihan TECH, Ltd.  
//-----------------------------------------------------------------

namespace DotNet.Utilities
{
    /// <summary>
    /// BaseSystemInfo
    /// 这是系统的核心基础信息部分
    /// 
    /// 修改记录
    ///		2012.04.14 版本：1.0 JiRiGaLa	主键创建。
    ///		
    /// <author>
    ///		<name>JiRiGaLa</name>
    ///		<date>2012.04.14</date>
    /// </author>
    /// </summary>
    public partial class BaseSystemInfo
    {
        /// <summary>
        /// 启用行政审批流程管理
        /// </summary>
        public static bool UseWorkFlow = false;

        /// <summary>
        /// 发送简易提醒消息
        /// </summary>
        public static bool SimpleReminders = false;

        /// <summary>
        /// 待审核包含被驳回单据
        /// </summary>
        public static bool ContainsTheRejectedDocuments = true;
    }
}