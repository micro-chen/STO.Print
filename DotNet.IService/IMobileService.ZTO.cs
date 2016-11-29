//-----------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2016 , Hairihan TECH, Ltd.  
//-----------------------------------------------------------------

using System.ServiceModel;

namespace DotNet.IService
{
    using DotNet.Utilities;

    /// <summary>
    /// IMobileService
    /// 手机短信接口
    /// 
    /// 修改记录
    /// 
    ///		2014.03.18 版本：1.0 JiRiGaLa 创建主键。
    ///		
    /// <author>
    ///		<name>JiRiGaLa</name>
    ///		<date>2014.03.18</date>
    /// </author> 
    /// </summary>
    public partial interface IMobileService
    {
        /// <summary>
        /// 发送手机消息
        /// </summary>
        /// <param name="userInfo">用户</param>
        /// <param name="billCode">运单编号</param>
        /// <param name="message">内容</param>
        /// <returns>主键</returns>
        [OperationContract]
        int SendBillMessage(BaseUserInfo userInfo, string billCode, string message, string channel);

        /// <summary>
        /// 获取手机号码
        /// </summary>
        /// <param name="userInfo">用户</param>
        /// <param name="billCode">运单编号</param>
        /// <returns>手机号码</returns>
        [OperationContract]
        string GetMobileByBill(BaseUserInfo userInfo, string billCode);
    }
}