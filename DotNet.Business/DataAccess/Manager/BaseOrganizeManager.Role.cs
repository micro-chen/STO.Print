//-----------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2016 , Hairihan TECH, Ltd.  
//-----------------------------------------------------------------

using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace DotNet.Business
{
    using DotNet.Model;
    using DotNet.Utilities;

    /// <summary>
    /// BaseOrganizeManager（程序OK）
    /// 组织机构、部门表
    ///
    /// 修改记录
    ///     
    ///		2015.12.08 版本：1.1 JiRiGaLa	参数顺序更换。
    ///		2014.04.15 版本：1.0 JiRiGaLa	有些思想进行了改进。
    ///
    /// <author>
    ///		<name>JiRiGaLa</name>
    ///		<date>2015.12.08</date>
    /// </author>
    /// </summary>
    public partial class BaseOrganizeManager : BaseManager //, IBaseOrganizeManager
    {
        #region public string[] GetIdsInRole(string systemCode, string roleId) 按角色主键获得组织机构主键数组
        /// <summary>
        /// 按角色主键获得组织机构主键数组
        /// </summary>
        /// <param name="systemCode">系统编号</param>
        /// <param name="roleId">角色主键</param>
        /// <returns>主键数组</returns>
        public string[] GetIdsInRole(string systemCode, string roleId)
        {
            string[] result = null;

            string tableName = systemCode + "RoleOrganize";

            // 需要显示未被删除的用户
            string sqlQuery = "SELECT OrganizeId FROM " + tableName 
                            + " WHERE RoleId = " + DbHelper.GetParameter(BaseRoleOrganizeEntity.FieldRoleId) 
                                  + " AND DeletionStateCode = 0 "
                                  + " AND OrganizeId IN (SELECT Id FROM BaseOrganize WHERE DeletionStateCode = 0)";

            List<IDbDataParameter> dbParameters = new List<IDbDataParameter>();
            dbParameters.Add(DbHelper.MakeParameter(BaseRoleOrganizeEntity.FieldRoleId, roleId));

            List<string> organizeIds = new List<string>();
            using (IDataReader dataReader = DbHelper.ExecuteReader(sqlQuery, dbParameters.ToArray()))
            {
                while (dataReader.Read())
                {
                    organizeIds.Add(dataReader[BaseRoleOrganizeEntity.FieldOrganizeId].ToString());
                }
            }
            result = organizeIds.ToArray();

            // 2015-12-08 吉日嘎拉 提高效率参数化执行
            // var dt = DbHelper.Fill(sqlQuery, dbParameters.ToArray());
            // BaseBusinessLogic.FieldToArray(dt, BaseRoleOrganizeEntity.FieldOrganizeId).Distinct<string>().Where(t => !string.IsNullOrEmpty(t)).ToArray();
            
            return result;
        }
        #endregion

        string GetSqlQueryByRole(string systemCode, string[] roleIds)
        {
            string tableNameRoleOrganize = systemCode + "RoleOrganize";

            string sqlQuery = "SELECT * FROM " + BaseOrganizeEntity.TableName
                            + " WHERE " + BaseOrganizeEntity.FieldEnabled + " = 1 "
                            + "       AND " + BaseOrganizeEntity.FieldDeletionStateCode + "= 0 "
                            + "       AND ( " + BaseOrganizeEntity.FieldId + " IN "
                            + "           (SELECT  " + BaseRoleOrganizeEntity.FieldOrganizeId
                            + "              FROM " + tableNameRoleOrganize
                            + "             WHERE " + BaseRoleOrganizeEntity.FieldRoleId + " IN (" + string.Join(",", roleIds) + ")"
                            + "               AND " + BaseRoleOrganizeEntity.FieldEnabled + " = 1"
                            + "                AND " + BaseRoleOrganizeEntity.FieldDeletionStateCode + " = 0)) "
                            + " ORDER BY  " + BaseOrganizeEntity.FieldSortCode;

            return sqlQuery;
        }

        public DataTable GetDataTableByRole(string systemCode, string[] roleIds)
        {
            string sqlQuery = GetSqlQueryByRole(systemCode, roleIds);
            return this.DbHelper.Fill(sqlQuery);
        }

        /// <summary>
        /// 为了提高授权的运行速度
        /// </summary>
        /// <param name="Id">主键</param>
        /// <param name="systemCode">系统编号</param>
        /// <param name="organizeId">组织机构主键</param>
        /// <param name="roleId">角色主键</param>
        /// <returns>主键</returns>
        public string AddToRole(string systemCode, string organizeId, string roleId)
        {
            string result = string.Empty;

            BaseRoleOrganizeEntity entity = new BaseRoleOrganizeEntity();
            entity.OrganizeId = organizeId;
            entity.RoleId = roleId;
            entity.Enabled = 1;
            entity.DeletionStateCode = 0;
            string tableName = systemCode + "RoleOrganize";
            BaseRoleOrganizeManager manager = new BaseRoleOrganizeManager(this.DbHelper, this.UserInfo, tableName);
            return manager.Add(entity);
        }

        public int AddToRole(string systemCode, string[] organizeIds, string roleId)
        {
            int result = 0;

            for (int i = 0; i < organizeIds.Length; i++)
            {
                this.AddToRole(systemCode, organizeIds[i], roleId);
                result++;
            }

            return result;
        }

        /// <summary>
        /// 移除角色成功
        /// </summary>
        /// <param name="systemCode">系统编号</param>
        /// <param name="organizeId">组织机构主键</param>
        /// <param name="roleId">角色主键</param>
        /// <returns>影响行数</returns>
        public int RemoveFormRole(string systemCode, string organizeId, string roleId)
        {
            List<KeyValuePair<string, object>> parameters = new List<KeyValuePair<string, object>>();
            parameters.Add(new KeyValuePair<string, object>(BaseRoleOrganizeEntity.FieldRoleId, roleId));
            parameters.Add(new KeyValuePair<string, object>(BaseRoleOrganizeEntity.FieldOrganizeId, organizeId));
            string tableName = systemCode + "RoleOrganize";
            BaseRoleOrganizeManager manager = new BaseRoleOrganizeManager(this.DbHelper, this.UserInfo, tableName);
            return manager.Delete(parameters);
        }

        public int RemoveFormRole(string systemCode, string[] organizeIds, string roleId)
        {
            int result = 0;

            for (int i = 0; i < organizeIds.Length; i++)
            {
                // 移除用户角色
                result += this.RemoveFormRole(systemCode, organizeIds[i], roleId);
            }

            return result;
        }

        public int ClearOrganize(string systemCode, string roleId)
        {
            int result = 0;

            string tableName = systemCode + "RoleOrganize";
            BaseRoleOrganizeManager manager = new BaseRoleOrganizeManager(this.DbHelper, this.UserInfo, tableName);
            result += manager.Delete(new KeyValuePair<string, object>(BaseRoleOrganizeEntity.FieldRoleId, roleId));

            return result;
        }
    }
}