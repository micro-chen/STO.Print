//-----------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2013 , Hairihan TECH, Ltd. 
//-----------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;

namespace DotNet.Business
{
    using DotNet.Utilities;

    /// <summary>
    /// BaseUserOrganizeManager
    /// 用户-组织结构关系管理
    /// 
    /// 修改纪录
    /// 
    ///     2010.09.25 版本：1.0 JiRiGaLa	创建。
    /// 
    /// <author>
    ///		<name>JiRiGaLa</name>
    ///		<date>2010.09.25</date>
    /// </author> 
    /// </summary>
    public partial class BaseUserOrganizeManager : BaseManager
    {
        #region public string Add(BaseUserOrganizeEntity userOrganizeEntity, out string statusCode) 添加用户
        /// <summary>
        /// 添加用户
        /// </summary>
        /// <param name="userOrganizeEntity">用户组织机构实体</param>
        /// <param name="statusCode">状态码</param>
        /// <returns>主键</returns>
        public string Add(BaseUserOrganizeEntity userOrganizeEntity, out string statusCode)
        {
            string result = string.Empty;
            // 判断数据是否重复了
            List<KeyValuePair<string, object>> parameters = new List<KeyValuePair<string, object>>();
            parameters.Add(new KeyValuePair<string, object>(BaseUserOrganizeEntity.FieldDeletionStateCode, 0));
            parameters.Add(new KeyValuePair<string, object>(BaseUserOrganizeEntity.FieldEnabled, 1));
            parameters.Add(new KeyValuePair<string, object>(BaseUserOrganizeEntity.FieldUserId, userOrganizeEntity.UserId));
            parameters.Add(new KeyValuePair<string, object>(BaseUserOrganizeEntity.FieldCompanyId, userOrganizeEntity.CompanyId));
            parameters.Add(new KeyValuePair<string, object>(BaseUserOrganizeEntity.FieldDepartmentId, userOrganizeEntity.DepartmentId));
            parameters.Add(new KeyValuePair<string, object>(BaseUserOrganizeEntity.FieldWorkgroupId, userOrganizeEntity.WorkgroupId));
            if (this.Exists(parameters))
            {
                // 用户名已重复
                statusCode = Status.Exist.ToString();
            }
            else
            {
                result = this.AddObject(userOrganizeEntity);
                // 运行成功
                statusCode = Status.OKAdd.ToString();
            }
            return result;
        }
        #endregion
        
        /// <summary>
        /// 获得用户的组织机构兼职情况
        /// </summary>
        /// <param name="userId">用户主键</param>
        /// <returns>数据表</returns>
        public DataTable GetUserOrganizeDT(string userId)
        {
            string sqlQuery = string.Empty ;
            switch (BaseSystemInfo.UserCenterDbType)
            {
                // case CurrentDbType.Access:
                default:
                    sqlQuery = "SELECT  BaseUserOrganize.* "
                             + "       , BaseOrganize1.FullName AS CompanyName "
                             + "       , BaseOrganize2.FullName AS SubCompanyName "
                             + "       , BaseOrganize3.FullName AS DepartmentName "
                             + "       , BaseOrganize4.FullName AS WorkgroupName "
                             + "  FROM BaseUserOrganize "
                             + "       LEFT OUTER JOIN BaseOrganize BaseOrganize1 ON BaseUserOrganize.CompanyId = BaseOrganize1.Id"
                             + "       LEFT OUTER JOIN BaseOrganize BaseOrganize2 ON BaseUserOrganize.SubCompanyId = BaseOrganize2.Id "
                             + "       LEFT OUTER JOIN BaseOrganize BaseOrganize3 ON BaseUserOrganize.DepartmentId = BaseOrganize3.Id "
                             + "       LEFT OUTER JOIN BaseOrganize BaseOrganize4 ON BaseUserOrganize.WorkgroupId = BaseOrganize4.Id "
                             + " WHERE UserId = '" + userId + "'"
                             + "       AND BaseUserOrganize.DeletionStateCode = 0 ";
                    break;
            }
            return DbHelper.Fill(sqlQuery);
        }
    }
}