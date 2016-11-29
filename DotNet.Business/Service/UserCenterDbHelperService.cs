//-----------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2015 , Hairihan TECH, Ltd. 
//-----------------------------------------------------------------

using System.ServiceModel;

namespace DotNet.Business
{
    using DotNet.Utilities;

    /// <summary>
    /// UserCenterDbHelperService
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
    public class UserCenterDbHelperService : DbHelperService
    {
        public UserCenterDbHelperService()
        {
            ServiceDbConnection = BaseSystemInfo.UserCenterDbConnection;
            ServiceDbType = BaseSystemInfo.UserCenterDbType;
        }

        public UserCenterDbHelperService(string dbConnection)
        {
            ServiceDbConnection = dbConnection;
        }
    }
}