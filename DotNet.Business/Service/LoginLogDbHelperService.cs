//-----------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2015 , Hairihan TECH, Ltd. 
//-----------------------------------------------------------------

using System.ServiceModel;

namespace DotNet.Business
{
    using DotNet.Utilities;

    /// <summary>
    /// LoginLogDbHelperService
    /// 
    /// 修改纪录
    /// 
    ///		2015.04.29 版本：1.0 JiRiGaLa 创建。
    ///		
    /// <author>
    ///		<name>JiRiGaLa</name>
    ///		<date>2015.04.29</date>
    /// </author> 
    /// </summary>
    [ServiceBehavior(IncludeExceptionDetailInFaults = true)]
    [System.ServiceModel.Activation.AspNetCompatibilityRequirements(RequirementsMode = System.ServiceModel.Activation.AspNetCompatibilityRequirementsMode.Allowed)]
    public class LoginLogDbHelperService : DbHelperService
    {
        public LoginLogDbHelperService()
        {
            ServiceDbConnection = BaseSystemInfo.LoginLogDbConnection;
            ServiceDbType = BaseSystemInfo.LoginLogDbType;
        }

        public LoginLogDbHelperService(string dbConnection)
        {
            ServiceDbConnection = dbConnection;
        }
    }
}