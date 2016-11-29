//-----------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2016 , Hairihan TECH, Ltd. 
//-----------------------------------------------------------------

using System;
using System.Runtime.Caching;
using System.Web.Script.Serialization;

namespace DotNet.Business
{
    using DotNet.Utilities;
    using DotNet.Model;

    /// <remarks>
    /// CurrentUserInfo
    /// 当前用户信息
    /// 
    /// 修改记录
    /// 
    ///	版本：1.0 2014.03.12    JiRiGaLa    创建。
    ///	
    /// <author>  
    ///		<name>JiRiGaLa</name>
    ///		<date>2014.03.12</date>
    /// </author> 
    /// </remarks>
    public class CurrentUserInfo : BaseUserInfo
    {
        /// <summary>
        /// 扩展的信息，从哪里读取？
        /// 数据库？
        /// 远程URL读取？
        /// </summary>
        public bool GetExtensionsFromDb = false;

        public CurrentUserInfo()
        {
        }

        public CurrentUserInfo(BaseUserInfo userInfo)
        {
            DotNet.Utilities.BaseBusinessLogic.CopyObjectProperties(userInfo, this);
        }

        /// <summary>
        /// 当前的组织结构公司名称
        /// </summary>
        public BaseOrganizeEntity Company
        {
            get
            {
                BaseOrganizeEntity company = null;
                // 读取组织机构的信息
                if (!string.IsNullOrEmpty(this.CompanyId))
                {
                    company = OrganizeUtilities.GetObjectByCache(this, this.CompanyId);
                }
                return company;
            }
        }
    }
}