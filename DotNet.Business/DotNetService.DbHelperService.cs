//-----------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2015 , Hairihan TECH, Ltd. 
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
    /// 修改纪录
    ///
    ///		2015.04.30 版本：3.0 JiRiGaLa 分离方法，提高安全性。
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
        public virtual IDbHelperService BusinessDbHelperService
        {
            get
            {
                return serviceFactory.CreateBusinessDbHelperService();
            }
        }

        public virtual IDbHelperService UserCenterDbHelperService
        {
            get
            {
                return serviceFactory.CreateUserCenterDbHelperService();
            }
        }

        public virtual IDbHelperService LoginLogDbHelperService
        {
            get
            {
                return serviceFactory.CreateLoginLogDbHelperService();
            }
        }
    }
}