//-----------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2016 , Hairihan TECH, Ltd.  
//-----------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Data;

namespace DotNet.Business
{
    using DotNet.Model;
    using DotNet.Utilities;

    /// <summary>
    /// BaseMessageManager（程序OK）
    /// 消息表
    ///
    /// 修改记录
    ///     
    ///     2016.01.27 版本：1.0 JiRiGaLa 创建主键。
    ///
    /// <author>
    ///		<name>JiRiGaLa</name>
    ///		<date>2016.01.27</date>
    /// </author>
    /// </summary>
    public partial class BaseMessageManager : BaseManager
    {
        public static string[] GetUserByOrganizeByCache(string companyId, string departmentId = null)
        {
            string[] result = null;

            if (string.IsNullOrEmpty(companyId))
            {
                return result;
            }

            string key = string.Empty;

            string users = string.Empty;
            if (string.IsNullOrEmpty(departmentId))
            {
                departmentId = string.Empty;
                key = "OU:" + companyId;

            }
            else
            {
                key = "OU:" + companyId + ":" + departmentId;
            }

            using (var redisClient = PooledRedisHelper.GetReadOnlyClient())
            {
                users = redisClient.Get<string>(key);
                if (!string.IsNullOrEmpty(users))
                {
                    result = users.Split(',');
                }
            }

            if (result == null)
            {
                BaseMessageManager manager = new BaseMessageManager();
                result = manager.GetUserByOrganize(companyId, departmentId);
                // 若是空的不用缓存，继续读取实体
                if (result != null)
                {
                    users = string.Join(",", result);
                    using (var redisClient = PooledRedisHelper.GetClient())
                    {
                        redisClient.Set<string>(key, users, DateTime.Now.AddDays(1));
                    }
                }
            }

            return result;
        }

        #region public string[] GetUserByOrganize(string companyId, string departmentId = null) 按组织机构获取用户列表
        /// <summary>
        /// 按组织机构获取用户列表
        /// </summary>
        /// <param name="companyId">组织机构主键</param>
        /// <param name="departmentId">部门主键</param>
        /// <returns>数据表</returns>
        public string[] GetUserByOrganize(string companyId, string departmentId = null)
        {
            string[] result = null;

            List<IDbDataParameter> dbParameters = new List<IDbDataParameter>();
            string commandText = "SELECT " + BaseUserEntity.TableName + "." + BaseUserEntity.FieldId
                                        + "," + BaseUserEntity.TableName + "." + BaseUserEntity.FieldRealName
                                        + "," + BaseUserEntity.TableName + "." + BaseUserEntity.FieldDepartmentName
                             + " FROM " + BaseUserEntity.TableName
                             + " WHERE " + BaseUserEntity.TableName + "." + BaseUserEntity.FieldDeletionStateCode + " = " + DbHelper.GetParameter(BaseUserEntity.FieldDeletionStateCode)
                                    + " AND " + BaseUserEntity.TableName + "." + BaseUserEntity.FieldEnabled + " = " + DbHelper.GetParameter(BaseUserEntity.FieldEnabled)
                                    + " AND " + BaseUserEntity.TableName + "." + BaseUserEntity.FieldIsVisible + " = " + DbHelper.GetParameter(BaseUserEntity.FieldIsVisible)
                                    + " AND " + BaseUserEntity.TableName + "." + BaseUserEntity.FieldUserFrom + " = " + DbHelper.GetParameter(BaseUserEntity.FieldUserFrom);

            dbParameters.Add(DbHelper.MakeParameter(BaseUserEntity.FieldDeletionStateCode, 0));
            dbParameters.Add(DbHelper.MakeParameter(BaseUserEntity.FieldEnabled, 1));
            dbParameters.Add(DbHelper.MakeParameter(BaseUserEntity.FieldIsVisible, 1));
            dbParameters.Add(DbHelper.MakeParameter(BaseUserEntity.FieldUserFrom, "Base"));

            if (!String.IsNullOrEmpty(companyId))
            {
                // 直接按公司进行查询，执行效率高
                commandText += " AND " + BaseUserEntity.TableName + "." + BaseUserEntity.FieldCompanyId + " = " + DbHelper.GetParameter(BaseUserEntity.FieldCompanyId);
                dbParameters.Add(DbHelper.MakeParameter(BaseUserEntity.FieldCompanyId, companyId));
            }
            if (!String.IsNullOrEmpty(departmentId))
            {
                // 直接按公司进行查询，执行效率高
                commandText += " AND " + BaseUserEntity.TableName + "." + BaseUserEntity.FieldDepartmentId + " = " + DbHelper.GetParameter(BaseUserEntity.FieldDepartmentId);
                dbParameters.Add(DbHelper.MakeParameter(BaseUserEntity.FieldDepartmentId, departmentId));
            }
            commandText += " ORDER BY " + BaseUserEntity.TableName + "." + BaseUserEntity.FieldDepartmentName
                                  + "," + BaseUserEntity.TableName + "." + BaseUserEntity.FieldRealName;

            // 2015-11-12 吉日嘎拉 优化获取用户的方法
            List<string> list = new List<string>();
            using (IDataReader dr = dbHelper.ExecuteReader(commandText, dbParameters.ToArray()))
            {
                while (dr.Read())
                {
                    list.Add(dr[BaseUserEntity.FieldId].ToString() + "=[" + dr[BaseUserEntity.FieldDepartmentName].ToString() + "] " + dr[BaseUserEntity.FieldRealName].ToString());
                }
            }

            result = list.ToArray();

            return result;
        }
        #endregion
    }
}