//-----------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2016 , Hairihan TECH, Ltd. 
//-----------------------------------------------------------------

using System;
using System.ServiceModel;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Caching;

namespace DotNet.Business
{
    using DotNet.Utilities;

    public partial class Utilities
    {
        /// <summary>
        /// 业务数据库
        /// </summary>
        public static IDbHelper BusinessDbHelper
        {
            get
            {
                return DbHelperFactory.GetHelper(BaseSystemInfo.BusinessDbType, BaseSystemInfo.BusinessDbConnection);
            }
        }

        /// <summary>
        /// 用户中心库
        /// </summary>
        public static IDbHelper UserCenterDbHelper
        {
            get
            {
                return DbHelperFactory.GetHelper(BaseSystemInfo.UserCenterDbType, BaseSystemInfo.UserCenterDbConnection);
            }
        }
    }
}