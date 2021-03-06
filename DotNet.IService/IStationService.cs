﻿//-----------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2016 , Hairihan TECH, Ltd.  
//-----------------------------------------------------------------

using System.Data;
using System.ServiceModel;

namespace DotNet.IService
{
    using DotNet.Utilities;

    /// <summary>
    /// IComputerService
    /// 计算机接口
    /// 
    /// 修改记录
    /// 
    ///		2015.02.24 版本：1.0 JiRiGaLa 创建主键。
    ///		
    /// <author>
    ///		<name>JiRiGaLa</name>
    ///		<date>2015.02.24</date>
    /// </author> 
    /// </summary>
    [ServiceContract]
    public interface IStationService
    {
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="userInfo">用户</param>
        /// <returns>数据表</returns>
        [OperationContract]
        DataTable GetDataTable(BaseUserInfo userInfo);
	}
}