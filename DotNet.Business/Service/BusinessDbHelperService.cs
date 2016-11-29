//-----------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2015 , Hairihan TECH, Ltd. 
//-----------------------------------------------------------------

using System.ServiceModel;

namespace DotNet.Business
{
    using DotNet.Utilities;

    /// <summary>
    /// BusinessDbHelperService
    /// 业务数据库服务
    /// 
    /// 修改纪录
    /// 
    ///		2011.05.07 版本：1.0 JiRiGaLa 创建。
    ///		
    /// <author>
    ///		<name>JiRiGaLa</name>
    ///		<date>2011.05.07</date>
    /// </author> 
    /// </summary>
    [ServiceBehavior(IncludeExceptionDetailInFaults = true)]
    [System.ServiceModel.Activation.AspNetCompatibilityRequirements(RequirementsMode = System.ServiceModel.Activation.AspNetCompatibilityRequirementsMode.Allowed)]
    public class BusinessDbHelperService : DbHelperService
    {
        public BusinessDbHelperService()
        {
            ServiceDbConnection = BaseSystemInfo.BusinessDbConnection;
            ServiceDbType = BaseSystemInfo.BusinessDbType;
        }

        public BusinessDbHelperService(string dbConnection)
        {
            ServiceDbConnection = dbConnection;
        }
    }
}