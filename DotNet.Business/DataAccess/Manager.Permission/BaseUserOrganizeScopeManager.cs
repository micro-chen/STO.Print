//-----------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2016 , Hairihan TECH, Ltd.  
//-----------------------------------------------------------------

using System.Collections.Generic;
using System.Data;

namespace DotNet.Business
{
    using DotNet.Model;
    using DotNet.Utilities;

    /// <summary>
    /// BaseUserScopeManager
    /// 用户组织机构权限域
    /// 
    /// 修改记录
    ///
    ///     2011.03.13 版本：2.0 JiRiGaLa 重新整理代码。
    ///     2008.05.24 版本：1.0 JiRiGaLa 创建主键。
    ///
    /// <author>
    ///		<name>JiRiGaLa</name>
    ///		<date>2011.03.13</date>
    /// </author>
    /// </summary>
    public partial class BaseUserScopeManager : BaseManager, IBaseManager
    {
        BasePermissionScopeManager ScopeManager
        {
            get
            {
                string tableName = this.UserInfo.SystemCode + "PermissionScope";
                return new BasePermissionScopeManager(DbHelper, UserInfo, tableName);
            }
        }

        /// <summary>
        /// 组织机构上级节点
        /// </summary>
        /// <param name="organizeParentId">组织机构上级主键</param>
        /// <param name="userId">用户主键</param>
        /// <param name="permissionCode">权限编号</param>
        /// <returns>数据表</returns>
        public DataTable GetUserOrganizeScopes(string systemCode, string organizeParentId, string userId, string permissionCode = "Resource.AccessPermission")
        {
            DataTable result = null;
            string permissionId = BaseModuleManager.GetIdByCodeByCache(systemCode, permissionCode);
            if (!string.IsNullOrEmpty(permissionId))
            {
                string tableName = this.UserInfo.SystemCode + "PermissionScope";
                string sqlQuery = @"SELECT  BaseOrganize.Id AS OrganizeId
                                          , BaseOrganize.FullName
                                          , BaseOrganize.ContainChildNodes
                                          , BasePermissionScope.TargetId
                                          , BasePermissionScope.ContainChild
                                     FROM BaseOrganize
                          LEFT OUTER JOIN BasePermissionScope ON BaseOrganize.Id = BasePermissionScope.TargetId
                                          AND BasePermissionScope.TargetCategory = 'BaseOrganize'
                                          AND BasePermissionScope.ResourceCategory = 'BaseUser'
                                          AND BasePermissionScope.ResourceId = '" + userId + @"'
                                          AND BasePermissionScope.PermissionId = " + permissionId;
                if (!string.IsNullOrEmpty(organizeParentId))
                {
                    sqlQuery += "  WHERE BaseOrganize.ParentId = " + organizeParentId;
                }
                else
                {
                    sqlQuery += "  WHERE BaseOrganize.ParentId IS NULL ";
                }
                sqlQuery = sqlQuery.Replace("BasePermissionScope", tableName);
                result = this.DbHelper.Fill(sqlQuery);
            }
            return result;
        }

        /*
        public List<BaseOrganizeScopeEntity> GetUserOrganizeScopes(string userId, string permissionCode = "Resource.AccessPermission")
        {
            List<BaseOrganizeScopeEntity> result = null;
            string result = this.GetPermissionIdByCode(permissionCode);
            if (!string.IsNullOrEmpty(result))
            {
                BaseOrganizeScopeManager organizeScopeManager = new BaseOrganizeScopeManager(this.DbHelper, this.UserInfo);
                List<KeyValuePair<string, object>> parameters = new List<KeyValuePair<string, object>>();
                parameters.Add(new KeyValuePair<string, object>(BaseOrganizeScopeEntity.FieldResourceCategory, BaseUserEntity.TableName));
                parameters.Add(new KeyValuePair<string, object>(BaseOrganizeScopeEntity.FieldResourceId, userId));
                parameters.Add(new KeyValuePair<string, object>(BaseOrganizeScopeEntity.FieldPermissionId, result));
                result = organizeScopeManager.GetList<BaseOrganizeScopeEntity>(parameters);
            }
            return result;
        }
        */

        public PermissionOrganizeScope GetUserOrganizeScope(string systemCode, string userId, out bool containChild, string permissionCode = "Resource.AccessPermission")
        {
            containChild = false;
            PermissionOrganizeScope permissionScope = PermissionOrganizeScope.UserCompany;

            BaseOrganizeScopeEntity organizeScopeEntity = null;
            string permissionId = BaseModuleManager.GetIdByCodeByCache(systemCode, permissionCode);
            if (!string.IsNullOrEmpty(permissionId))
            {
                string tableName =  BaseOrganizeScopeEntity.TableName;
                if (!string.IsNullOrEmpty(systemCode))
                {
                    tableName = systemCode + "OrganizeScope";
                }

                BaseOrganizeScopeManager organizeScopeManager = new BaseOrganizeScopeManager(this.DbHelper, this.UserInfo, tableName);
                List<KeyValuePair<string, object>> parameters = new List<KeyValuePair<string, object>>();
                parameters.Add(new KeyValuePair<string, object>(BaseOrganizeScopeEntity.FieldResourceCategory, BaseUserEntity.TableName));
                parameters.Add(new KeyValuePair<string, object>(BaseOrganizeScopeEntity.FieldResourceId, userId));
                parameters.Add(new KeyValuePair<string, object>(BaseOrganizeScopeEntity.FieldPermissionId, permissionId));
                DataTable dt = organizeScopeManager.GetDataTable(parameters);
                if (dt != null && dt.Rows.Count > 0)
                {
                    organizeScopeEntity = BaseOrganizeScopeEntity.Create<BaseOrganizeScopeEntity>(dt);
                }
            }

            if (organizeScopeEntity != null)
            {
                if (organizeScopeEntity.ContainChild == 1)
                {
                    containChild = true;
                }
                if (organizeScopeEntity.AllData == 1)
                {
                    permissionScope = PermissionOrganizeScope.AllData;
                }
                if (organizeScopeEntity.Province == 1)
                {
                    permissionScope = PermissionOrganizeScope.Province;
                }
                if (organizeScopeEntity.City == 1)
                {
                    permissionScope = PermissionOrganizeScope.City;
                }
                if (organizeScopeEntity.District == 1)
                {
                    permissionScope = PermissionOrganizeScope.District;
                }
                if (organizeScopeEntity.ByDetails == 1)
                {
                    permissionScope = PermissionOrganizeScope.ByDetails;
                }
                if (organizeScopeEntity.NotAllowed == 1)
                {
                    permissionScope = PermissionOrganizeScope.NotAllowed;
                }
                if (organizeScopeEntity.OnlyOwnData == 1)
                {
                    permissionScope = PermissionOrganizeScope.OnlyOwnData;
                }
                if (organizeScopeEntity.UserCompany == 1)
                {
                    permissionScope = PermissionOrganizeScope.UserCompany;
                }
                if (organizeScopeEntity.UserSubCompany == 1)
                {
                    permissionScope = PermissionOrganizeScope.UserSubCompany;
                }
                if (organizeScopeEntity.UserDepartment == 1)
                {
                    permissionScope = PermissionOrganizeScope.UserDepartment;
                }
                if (organizeScopeEntity.UserSubDepartment == 1)
                {
                    permissionScope = PermissionOrganizeScope.UserSubDepartment;
                }
                if (organizeScopeEntity.UserWorkgroup == 1)
                {
                    permissionScope = PermissionOrganizeScope.UserWorkgroup;
                }
            }
            return permissionScope;
        }


        public string SetUserOrganizeScope(string systemCode, string userId, PermissionOrganizeScope permissionScope, string permissionCode = "Resource.AccessPermission", bool containChild = false)
        {
            string result = string.Empty;

            string permissionId = BaseModuleManager.GetIdByCodeByCache(systemCode, permissionCode);
            if (!string.IsNullOrEmpty(permissionId))
            {
                string tableName = BaseOrganizeScopeEntity.TableName;
                if (!string.IsNullOrEmpty(systemCode))
                {
                    tableName = systemCode + "OrganizeScope";
                }

                BaseOrganizeScopeManager organizeScopeManager = new BaseOrganizeScopeManager(this.DbHelper, this.UserInfo, tableName);
                List<KeyValuePair<string, object>> parameters = new List<KeyValuePair<string, object>>();
                parameters.Add(new KeyValuePair<string, object>(BaseOrganizeScopeEntity.FieldResourceCategory, BaseUserEntity.TableName));
                parameters.Add(new KeyValuePair<string, object>(BaseOrganizeScopeEntity.FieldResourceId, userId));
                parameters.Add(new KeyValuePair<string, object>(BaseOrganizeScopeEntity.FieldPermissionId, permissionId));
                result = organizeScopeManager.GetId(parameters);
                BaseOrganizeScopeEntity organizeScopeEntity = null;
                if (string.IsNullOrEmpty(result))
                {
                    organizeScopeEntity = new BaseOrganizeScopeEntity();
                }
                else
                {
                    organizeScopeEntity = organizeScopeManager.GetObject(result);
                }
                organizeScopeEntity.AllData = (permissionScope == PermissionOrganizeScope.AllData ? 1 : 0);
                organizeScopeEntity.Province = (permissionScope == PermissionOrganizeScope.Province ? 1 : 0);
                organizeScopeEntity.City = (permissionScope == PermissionOrganizeScope.City ? 1 : 0);
                organizeScopeEntity.District = (permissionScope == PermissionOrganizeScope.District ? 1 : 0);
                organizeScopeEntity.UserCompany = (permissionScope == PermissionOrganizeScope.UserCompany ? 1 : 0);
                organizeScopeEntity.UserSubCompany = (permissionScope == PermissionOrganizeScope.UserSubCompany ? 1 : 0);
                organizeScopeEntity.UserDepartment = (permissionScope == PermissionOrganizeScope.UserDepartment ? 1 : 0);
                organizeScopeEntity.UserSubDepartment = (permissionScope == PermissionOrganizeScope.UserSubDepartment ? 1 : 0);
                organizeScopeEntity.UserWorkgroup = (permissionScope == PermissionOrganizeScope.UserWorkgroup ? 1 : 0);
                organizeScopeEntity.OnlyOwnData = (permissionScope == PermissionOrganizeScope.OnlyOwnData ? 1 : 0);
                organizeScopeEntity.ByDetails = (permissionScope == PermissionOrganizeScope.ByDetails ? 1 : 0);
                organizeScopeEntity.NotAllowed = (permissionScope == PermissionOrganizeScope.NotAllowed ? 1 : 0);
                organizeScopeEntity.Enabled = 1;
                organizeScopeEntity.DeletionStateCode = 0;
                organizeScopeEntity.ContainChild = containChild ? 1 : 0;
                organizeScopeEntity.PermissionId = int.Parse(permissionId);
                organizeScopeEntity.ResourceCategory = BaseUserEntity.TableName;
                organizeScopeEntity.ResourceId = userId;
                if (string.IsNullOrEmpty(result))
                {
                    result = organizeScopeManager.Add(organizeScopeEntity);
                }
                else
                {
                    organizeScopeManager.Update(organizeScopeEntity);
                }
            }
            return result;
        }


        ////
        ////
        //// 授权范围管理部分
        ////
        ////

        #region public string[] GetOrganizeIds(string userId, string permissionCode) 获取用户的权限主键数组
        /// <summary>
        /// 获取用户的权限主键数组
        /// </summary>
        /// <param name="userId">用户主键</param>
        /// <param name="permissionCode">权限编号</param>
        /// <returns>主键数组</returns>
        public string[] GetOrganizeIds(string systemCode, string userId, string permissionCode)
        {
            string[] result = null;

            string permissionId = BaseModuleManager.GetIdByCodeByCache(systemCode, permissionCode);
            if (!string.IsNullOrEmpty(permissionId))
            {
                List<KeyValuePair<string, object>> parameters = new List<KeyValuePair<string, object>>();
                parameters.Add(new KeyValuePair<string, object>(BasePermissionScopeEntity.FieldResourceCategory, BaseUserEntity.TableName));
                parameters.Add(new KeyValuePair<string, object>(BasePermissionScopeEntity.FieldResourceId, userId));
                parameters.Add(new KeyValuePair<string, object>(BasePermissionScopeEntity.FieldTargetCategory, BaseOrganizeEntity.TableName));
                parameters.Add(new KeyValuePair<string, object>(BasePermissionScopeEntity.FieldPermissionId, permissionId));

                // 20130605 JiRiGaLa 这个运行效率更高一些
                result = this.GetProperties(parameters, BasePermissionScopeEntity.FieldTargetId);
                // var result = this.GetDataTable(parameters);
                // result = BaseBusinessLogic.FieldToArray(result, BasePermissionScopeEntity.FieldTargetId).Distinct<string>().Where(t => !string.IsNullOrEmpty(t)).ToArray();
            }
            return result;
        }
        #endregion

        //
        // 授予授权范围的实现部分
        //

        #region public string GrantOrganize(string userId, string grantOrganizeId, string permissionCode = "Resource.AccessPermission", bool containChild = false) 给用户授予组织机构的某个范围权限
        /// <summary>
        /// 给用户授予组织机构的某个范围权限
        /// 哪个用户对哪个部门有什么权限
        /// </summary>
        /// <param name="userId">用户主键</param>
        /// <param name="grantOrganizeId">权组织机构限主键</param>
        /// <param name="permissionCode">权限编号</param>
        /// <param name="containChild">包含子节点，递归</param>
        /// <returns>主键</returns>
        public string GrantOrganize(string systemCode, string userId, string grantOrganizeId, string permissionCode = "Resource.AccessPermission", bool containChild = false)
        {
            return this.GrantOrganize(this.ScopeManager, systemCode, userId, grantOrganizeId, permissionCode, containChild);
        }
        #endregion

        #region private string GrantOrganize(BasePermissionScopeManager manager, string userId, string grantOrganizeId, string permissionCode = "Resource.AccessPermission", bool containChild = false) 为了提高授权的运行速度
        /// <summary>
        /// 为了提高授权的运行速度
        /// </summary>
        /// <param name="manager">权限域读写器</param>
        /// <param name="userId">用户主键</param>
        /// <param name="grantOrganizeId">权组织机构限主键</param>
        /// <param name="permissionCode">权限编号</param>
        /// <returns>主键</returns>
        private string GrantOrganize(BasePermissionScopeManager manager, string systemCode, string userId, string grantOrganizeId, string permissionCode = "Resource.AccessPermission", bool containChild = false)
        {
            string result = string.Empty;
            string permissionId = BaseModuleManager.GetIdByCodeByCache(systemCode, permissionCode);
            if (!string.IsNullOrEmpty(permissionId))
            {
                List<KeyValuePair<string, object>> parameters = new List<KeyValuePair<string, object>>();
                parameters.Add(new KeyValuePair<string, object>(BasePermissionScopeEntity.FieldResourceCategory, BaseUserEntity.TableName));
                parameters.Add(new KeyValuePair<string, object>(BasePermissionScopeEntity.FieldResourceId, userId));
                parameters.Add(new KeyValuePair<string, object>(BasePermissionScopeEntity.FieldTargetCategory, BaseOrganizeEntity.TableName));
                parameters.Add(new KeyValuePair<string, object>(BasePermissionScopeEntity.FieldTargetId, grantOrganizeId));
                parameters.Add(new KeyValuePair<string, object>(BasePermissionScopeEntity.FieldPermissionId, permissionId));
                // Nick Deng 优化数据权限设置，没有权限和其他任意一种权限互斥
                // 即当没有权限时，该用户对应该数据权限的其他权限都应删除
                // 当该用户拥有对应该数据权限的其他权限时，删除该用户的没有权限的权限
                result = manager.GetId(parameters);
                if (!string.IsNullOrEmpty(result))
                {
                    manager.SetProperty(new KeyValuePair<string, object>(BasePermissionScopeEntity.FieldId, result), new KeyValuePair<string, object>(BasePermissionScopeEntity.FieldContainChild, containChild ? 1 : 0));
                }
                else
                {
                    BasePermissionScopeEntity entity = new BasePermissionScopeEntity();
                    entity.PermissionId = permissionId;
                    entity.ResourceCategory = BaseUserEntity.TableName;
                    entity.ResourceId = userId;
                    entity.TargetCategory = BaseOrganizeEntity.TableName;
                    entity.TargetId = grantOrganizeId;
                    entity.ContainChild = containChild ? 1 : 0;
                    entity.Enabled = 1;
                    entity.DeletionStateCode = 0;
                    result = manager.Add(entity);
                }
            }
            return result;
        }
        #endregion

        public int GrantOrganizes(string systemCode, string userId, string[] grantOrganizeIds, string permissionCode, bool containChild = false)
        {
            int result = 0;
            for (int i = 0; i < grantOrganizeIds.Length; i++)
            {
                this.GrantOrganize(this.ScopeManager, systemCode, userId, grantOrganizeIds[i], permissionCode, containChild);
                result++;
            }
            return result;
        }

        public int GrantOrganizes(string systemCode, string[] userIds, string grantOrganizeId, string permissionCode, bool containChild = false)
        {
            int result = 0;
            for (int i = 0; i < userIds.Length; i++)
            {
                this.GrantOrganize(this.ScopeManager, systemCode, userIds[i], grantOrganizeId, permissionCode, containChild);
                result++;
            }
            return result;
        }

        public int GrantOrganizes(string systemCode, string[] userIds, string[] grantOrganizeIds, string permissionCode, bool containChild = false)
        {
            int result = 0;
            for (int i = 0; i < userIds.Length; i++)
            {
                for (int j = 0; j < grantOrganizeIds.Length; j++)
                {
                    this.GrantOrganize(this.ScopeManager, systemCode, userIds[i], grantOrganizeIds[j], permissionCode, containChild);
                    result++;
                }
            }
            return result;
        }


        //
        //  撤销授权范围的实现部分
        //

        #region public int RevokeOrganize(string userId, string revokeOrganizeId, string permissionCode) 用户撤销授权
        /// <summary>
        /// 用户撤销授权
        /// </summary>
        /// <param name="userId">用户主键</param>
        /// <param name="permissionCode">权限编号</param>
        /// <returns>主键</returns>
        public int RevokeOrganize(string systemCode, string userId, string revokeOrganizeId, string permissionCode = "Resource.AccessPermission")
        {
            return this.RevokeOrganize(this.ScopeManager, systemCode, userId, revokeOrganizeId, permissionCode);
        }
        #endregion

        #region private int RevokeOrganize(BasePermissionScopeManager manager, string userId, string revokeOrganizeId, string permissionCode) 为了提高授权的运行速度
        /// <summary>
        /// 为了提高授权的运行速度
        /// </summary>
        /// <param name="manager">权限域读写器</param>
        /// <param name="userId">用户主键</param>
        /// <param name="revokeOrganizeId">权限主键</param>
        /// <param name="permissionCode">权限编号</param>
        /// <returns>主键</returns>
        private int RevokeOrganize(BasePermissionScopeManager manager, string systemCode, string userId, string revokeOrganizeId, string permissionCode)
        {
            List<KeyValuePair<string, object>> parameters = new List<KeyValuePair<string, object>>();
            parameters.Add(new KeyValuePair<string, object>(BasePermissionScopeEntity.FieldResourceCategory, BaseUserEntity.TableName));
            parameters.Add(new KeyValuePair<string, object>(BasePermissionScopeEntity.FieldResourceId, userId));
            parameters.Add(new KeyValuePair<string, object>(BasePermissionScopeEntity.FieldTargetCategory, BaseOrganizeEntity.TableName));
            if (!string.IsNullOrEmpty(revokeOrganizeId))
            {
                parameters.Add(new KeyValuePair<string, object>(BasePermissionScopeEntity.FieldTargetId, revokeOrganizeId));
            }
            parameters.Add(new KeyValuePair<string, object>(BasePermissionScopeEntity.FieldPermissionId, BaseModuleManager.GetIdByCodeByCache(systemCode, permissionCode)));
            return manager.Delete(parameters);
        }
        #endregion

        #region public int RevokeOrganize(string userId, string permissionCode) 用户撤销授权
        /// <summary>
        /// 用户撤销授权
        /// </summary>
        /// <param name="userId">用户主键</param>
        /// <param name="permissionCode">权限编号</param>
        /// <returns>主键</returns>
        public int RevokeOrganize(string systemCode, string userId, string permissionCode)
        {
            return this.RevokeOrganize(this.ScopeManager, systemCode, userId, permissionCode, null);
        }
        #endregion

        

        public int RevokeOrganizes(string systemCode, string userId, string[] revokeOrganizeIds, string permissionCode)
        {
            int result = 0;
            for (int i = 0; i < revokeOrganizeIds.Length; i++)
            {
                this.RevokeOrganize(this.ScopeManager, systemCode, userId, revokeOrganizeIds[i], permissionCode);
                result++;
            }
            return result;
        }

        public int RevokeOrganizes(string systemCode, string[] userIds, string revokeOrganizeId, string permissionCode)
        {
            int result = 0;
            for (int i = 0; i < userIds.Length; i++)
            {
                this.RevokeOrganize(this.ScopeManager, systemCode, userIds[i], revokeOrganizeId, permissionCode);
                result++;
            }
            return result;
        }

        public int RevokeOrganizes(string systemCode, string[] userIds, string[] revokeOrganizeIds, string permissionCode)
        {
            int result = 0;
            for (int i = 0; i < userIds.Length; i++)
            {
                for (int j = 0; j < revokeOrganizeIds.Length; j++)
                {
                    this.RevokeOrganize(this.ScopeManager, systemCode, userIds[i], revokeOrganizeIds[j], permissionCode);
                    result++;
                }
            }
            return result;
        }
    }
}