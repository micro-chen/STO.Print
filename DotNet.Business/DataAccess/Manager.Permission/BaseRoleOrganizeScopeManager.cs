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
    /// BaseRoleScopeManager
    /// 角色权限域
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
    public partial class BaseRoleScopeManager : BaseManager, IBaseManager
    {
        ////
        ////
        //// 授权范围管理部分
        ////
        ////

        public List<BaseOrganizeScopeEntity> GetRoleOrganizeScopes(string roleId, string permissionCode = "Resource.AccessPermission")
        {
            List<BaseOrganizeScopeEntity> result = null;
            string permissionId = this.GetPermissionIdByCode(permissionCode);
            if (!string.IsNullOrEmpty(permissionId))
            {
                BaseOrganizeScopeManager organizeScopeManager = new BaseOrganizeScopeManager(this.DbHelper, this.UserInfo);
                string tableName = UserInfo.SystemCode + "Role";
                List<KeyValuePair<string, object>> parameters = new List<KeyValuePair<string, object>>();
                parameters.Add(new KeyValuePair<string, object>(BaseOrganizeScopeEntity.FieldResourceCategory, tableName));
                parameters.Add(new KeyValuePair<string, object>(BaseOrganizeScopeEntity.FieldResourceId, roleId));
                parameters.Add(new KeyValuePair<string, object>(BaseOrganizeScopeEntity.FieldPermissionId, permissionId));
                result = organizeScopeManager.GetList<BaseOrganizeScopeEntity>(parameters);
            }
            return result;
        }

        public PermissionOrganizeScope GetRoleOrganizeScope(string roleId, out bool containChild, string permissionCode = "Resource.AccessPermission")
        {
            containChild = false;
            PermissionOrganizeScope permissionScope = PermissionOrganizeScope.OnlyOwnData;

            BaseOrganizeScopeEntity organizeScopeEntity = null;
            string permissionId = this.GetPermissionIdByCode(permissionCode);
            if (!string.IsNullOrEmpty(permissionId))
            {
                BaseOrganizeScopeManager organizeScopeManager = new BaseOrganizeScopeManager(this.DbHelper, this.UserInfo);
                string tableName = UserInfo.SystemCode + "Role";
                List<KeyValuePair<string, object>> parameters = new List<KeyValuePair<string, object>>();
                parameters.Add(new KeyValuePair<string, object>(BaseOrganizeScopeEntity.FieldResourceCategory, tableName));
                parameters.Add(new KeyValuePair<string, object>(BaseOrganizeScopeEntity.FieldResourceId, roleId));
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
                if (organizeScopeEntity.Street == 1)
                {
                    permissionScope = PermissionOrganizeScope.Street;
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

        public string SetRoleOrganizeScope(string roleId, PermissionOrganizeScope permissionScope, string permissionCode = "Resource.AccessPermission", bool containChild = false)
        {
            string result = string.Empty;

            string permissionId = this.GetPermissionIdByCode(permissionCode);
            if (!string.IsNullOrEmpty(permissionId))
            {
                BaseOrganizeScopeManager organizeScopeManager = new BaseOrganizeScopeManager(this.DbHelper, this.UserInfo);
                string tableName = UserInfo.SystemCode + "Role";
                List<KeyValuePair<string, object>> parameters = new List<KeyValuePair<string, object>>();
                parameters.Add(new KeyValuePair<string, object>(BaseOrganizeScopeEntity.FieldResourceCategory, tableName));
                parameters.Add(new KeyValuePair<string, object>(BaseOrganizeScopeEntity.FieldResourceId, roleId));
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
                organizeScopeEntity.Street = (permissionScope == PermissionOrganizeScope.Street ? 1 : 0);
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
                organizeScopeEntity.ResourceCategory = tableName;
                organizeScopeEntity.ResourceId = roleId;
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

        #region public string[] GetOrganizeIds(string roleId, string permissionCode) 获取员工的权限主键数组
        /// <summary>
        /// 获取员工的权限主键数组
        /// </summary>
        /// <param name="roleId">员工主键</param>
        /// <param name="permissionCode">权限编号</param>
        /// <returns>主键数组</returns>
        public string[] GetOrganizeIds(string roleId, string permissionCode)
        {
            string[] result = null;
            string permissionId = this.GetPermissionIdByCode(permissionCode);
            if (!string.IsNullOrEmpty(permissionId))
            {
                string roleTableName = UserInfo.SystemCode + "Role";
                List<KeyValuePair<string, object>> parameters = new List<KeyValuePair<string, object>>();
                parameters.Add(new KeyValuePair<string, object>(BasePermissionScopeEntity.FieldResourceCategory, roleTableName));
                parameters.Add(new KeyValuePair<string, object>(BasePermissionScopeEntity.FieldResourceId, roleId));
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

        #region private string GrantOrganize(BasePermissionScopeManager manager, string id, string roleId, string grantOrganizeId) 为了提高授权的运行速度
        /// <summary>
        /// 为了提高授权的运行速度
        /// </summary>
        /// <param name="manager">权限域读写器</param>
        /// <param name="roleId">员工主键</param>
        /// <param name="grantOrganizeId">权限主键</param>
        /// <param name="permissionCode">权限编号</param>
        /// <returns>主键</returns>
        private string GrantOrganize(BasePermissionScopeManager permissionScopeManager, string roleId, string grantOrganizeId, string permissionCode)
        {
            string result = string.Empty;

            string roleTableName = UserInfo.SystemCode + "Role";

            List<KeyValuePair<string, object>> parameters = new List<KeyValuePair<string, object>>();
            parameters.Add(new KeyValuePair<string, object>(BasePermissionScopeEntity.FieldResourceCategory, roleTableName));
            parameters.Add(new KeyValuePair<string, object>(BasePermissionScopeEntity.FieldResourceId, roleId));
            parameters.Add(new KeyValuePair<string, object>(BasePermissionScopeEntity.FieldTargetCategory, BaseOrganizeEntity.TableName));
            parameters.Add(new KeyValuePair<string, object>(BasePermissionScopeEntity.FieldTargetId, grantOrganizeId));
            parameters.Add(new KeyValuePair<string, object>(BasePermissionScopeEntity.FieldPermissionId, this.GetPermissionIdByCode(permissionCode)));

            // Nick Deng 优化数据权限设置，没有权限和其他任意一种权限互斥
            // 即当没有权限时，该角色对应该数据权限的其他权限都应删除
            // 当该角色拥有对应该数据权限的其他权限时，删除该角色的没有权限的权限
            BasePermissionScopeEntity resourcePermissionScopeEntity = new BasePermissionScopeEntity();
            var dt = new DataTable();
            if (!this.Exists(parameters))
            {
                resourcePermissionScopeEntity.PermissionId = this.GetPermissionIdByCode(permissionCode);
                resourcePermissionScopeEntity.ResourceCategory = BaseRoleEntity.TableName;
                resourcePermissionScopeEntity.ResourceId = roleId;
                resourcePermissionScopeEntity.TargetCategory = BaseOrganizeEntity.TableName;
                resourcePermissionScopeEntity.TargetId = grantOrganizeId;
                resourcePermissionScopeEntity.Enabled = 1;
                resourcePermissionScopeEntity.DeletionStateCode = 0;
                result = permissionScopeManager.Add(resourcePermissionScopeEntity);
                if (grantOrganizeId != ((int)PermissionOrganizeScope.NotAllowed).ToString())
                {
                    parameters = new List<KeyValuePair<string, object>>();
                    parameters.Add(new KeyValuePair<string, object>(BasePermissionScopeEntity.FieldResourceCategory, roleTableName));
                    parameters.Add(new KeyValuePair<string, object>(BasePermissionScopeEntity.FieldResourceId, roleId));
                    parameters.Add(new KeyValuePair<string, object>(BasePermissionScopeEntity.FieldTargetCategory, BaseOrganizeEntity.TableName));
                    parameters.Add(new KeyValuePair<string, object>(BasePermissionScopeEntity.FieldTargetId, ((int)PermissionOrganizeScope.NotAllowed).ToString()));
                    parameters.Add(new KeyValuePair<string, object>(BasePermissionScopeEntity.FieldPermissionId, this.GetPermissionIdByCode(permissionCode)));

                    if (this.Exists(parameters))
                    {
                        dt = permissionScopeManager.GetDataTable(parameters);
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            permissionScopeManager.DeleteObject(dt.Rows[0][BasePermissionScopeEntity.FieldId].ToString());
                        }
                    }
                }
                else
                {
                    parameters = new List<KeyValuePair<string, object>>();
                    parameters.Add(new KeyValuePair<string, object>(BasePermissionScopeEntity.FieldResourceCategory, roleTableName));
                    parameters.Add(new KeyValuePair<string, object>(BasePermissionScopeEntity.FieldResourceId, roleId));
                    parameters.Add(new KeyValuePair<string, object>(BasePermissionScopeEntity.FieldTargetCategory, BaseOrganizeEntity.TableName));
                    parameters.Add(new KeyValuePair<string, object>(BasePermissionScopeEntity.FieldPermissionId, this.GetPermissionIdByCode(permissionCode)));

                    dt = permissionScopeManager.GetDataTable(parameters);
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (dt.Rows[i]["TargetId"].ToString() != ((int)PermissionOrganizeScope.NotAllowed).ToString())
                            permissionScopeManager.DeleteObject(dt.Rows[0][BasePermissionScopeEntity.FieldId].ToString());
                    }
                }
            }

            return result;
        }
        #endregion

        #region public string GrantOrganize(string roleId, string result) 员工授予权限
        /// <summary>
        /// 员工授予权限
        /// </summary>
        /// <param name="roleId">员工主键</param>
        /// <param name="grantOrganizeId">权组织机构限主键</param>
        /// <param name="permissionCode">权限编号</param>
        /// <returns>主键</returns>
        public string GrantOrganize(string roleId, string grantOrganizeId, string permissionCode)
        {
            BasePermissionScopeManager permissionScopeManager = new BasePermissionScopeManager(DbHelper, UserInfo, this.CurrentTableName);
            return this.GrantOrganize(permissionScopeManager, roleId, grantOrganizeId, permissionCode);
        }
        #endregion

        public int GrantOrganizes(string roleId, string[] grantOrganizeIds, string permissionCode)
        {
            int result = 0;
            BasePermissionScopeManager permissionScopeManager = new BasePermissionScopeManager(DbHelper, UserInfo, this.CurrentTableName);
            for (int i = 0; i < grantOrganizeIds.Length; i++)
            {
                this.GrantOrganize(permissionScopeManager, roleId, grantOrganizeIds[i], permissionCode);
                result++;
            }
            return result;
        }

        public int GrantOrganizes(string[] roleIds, string grantOrganizeId, string permissionCode)
        {
            int result = 0;
            BasePermissionScopeManager permissionScopeManager = new BasePermissionScopeManager(DbHelper, UserInfo, this.CurrentTableName);
            for (int i = 0; i < roleIds.Length; i++)
            {
                this.GrantOrganize(permissionScopeManager, roleIds[i], grantOrganizeId, permissionCode);
                result++;
            }
            return result;
        }

        public int GrantOrganizes(string[] roleIds, string[] grantOrganizeIds, string permissionCode)
        {
            int result = 0;
            BasePermissionScopeManager permissionScopeManager = new BasePermissionScopeManager(DbHelper, UserInfo, this.CurrentTableName);
            for (int i = 0; i < roleIds.Length; i++)
            {
                for (int j = 0; j < grantOrganizeIds.Length; j++)
                {
                    this.GrantOrganize(permissionScopeManager, roleIds[i], grantOrganizeIds[j], permissionCode);
                    result++;
                }
            }
            return result;
        }


        //
        //  撤销授权范围的实现部分
        //

        #region private int RevokeOrganize(BasePermissionScopeManager manager, string roleId, string revokeOrganizeId, string permissionCode) 为了提高授权的运行速度
        /// <summary>
        /// 为了提高授权的运行速度
        /// </summary>
        /// <param name="manager">权限域读写器</param>
        /// <param name="roleId">角色主键</param>
        /// <param name="revokeOrganizeId">权限主键</param>
        /// <param name="permissionCode">权限编号</param>
        /// <returns>主键</returns>
        private int RevokeOrganize(BasePermissionScopeManager permissionScopeManager, string roleId, string revokeOrganizeId, string permissionCode)
        {
            string roleTableName = UserInfo.SystemCode + "Role";
            List<KeyValuePair<string, object>> parameters = new List<KeyValuePair<string, object>>();
            parameters.Add(new KeyValuePair<string, object>(BasePermissionScopeEntity.FieldResourceCategory, roleTableName));
            parameters.Add(new KeyValuePair<string, object>(BasePermissionScopeEntity.FieldResourceId, roleId));
            parameters.Add(new KeyValuePair<string, object>(BasePermissionScopeEntity.FieldTargetCategory, BaseOrganizeEntity.TableName));
            parameters.Add(new KeyValuePair<string, object>(BasePermissionScopeEntity.FieldTargetId, revokeOrganizeId));
            parameters.Add(new KeyValuePair<string, object>(BasePermissionScopeEntity.FieldPermissionId, this.GetPermissionIdByCode(permissionCode)));

            return permissionScopeManager.Delete(parameters);
        }
        #endregion

        #region public int RevokeOrganize(string roleId, string result) 员工撤销授权
        /// <summary>
        /// 员工撤销授权
        /// </summary>
        /// <param name="roleId">角色主键</param>
        /// <param name="permissionCode">权限编号</param>
        /// <returns>主键</returns>
        public int RevokeOrganize(string roleId, string revokeOrganizeId, string permissionCode)
        {
            BasePermissionScopeManager permissionScopeManager = new BasePermissionScopeManager(DbHelper, UserInfo, this.CurrentTableName);
            return this.RevokeOrganize(permissionScopeManager, roleId, revokeOrganizeId, permissionCode);
        }
        #endregion

        public int RevokeOrganizes(string roleId, string[] revokeOrganizeIds, string permissionCode)
        {
            int result = 0;
            BasePermissionScopeManager permissionScopeManager = new BasePermissionScopeManager(DbHelper, UserInfo, this.CurrentTableName);
            for (int i = 0; i < revokeOrganizeIds.Length; i++)
            {
                this.RevokeOrganize(permissionScopeManager, roleId, revokeOrganizeIds[i], permissionCode);
                result++;
            }
            return result;
        }

        public int RevokeOrganizes(string[] roleIds, string revokeOrganizeId, string permissionCode)
        {
            int result = 0;
            BasePermissionScopeManager permissionScopeManager = new BasePermissionScopeManager(DbHelper, UserInfo, this.CurrentTableName);
            for (int i = 0; i < roleIds.Length; i++)
            {
                this.RevokeOrganize(permissionScopeManager, roleIds[i], revokeOrganizeId, permissionCode);
                result++;
            }
            return result;
        }

        public int RevokeOrganizes(string[] roleIds, string[] revokeOrganizeIds, string permissionCode)
        {
            int result = 0;
            BasePermissionScopeManager permissionScopeManager = new BasePermissionScopeManager(DbHelper, UserInfo, this.CurrentTableName);
            for (int i = 0; i < roleIds.Length; i++)
            {
                for (int j = 0; j < revokeOrganizeIds.Length; j++)
                {
                    this.RevokeOrganize(permissionScopeManager, roleIds[i], revokeOrganizeIds[j], permissionCode);
                    result++;
                }
            }
            return result;
        }
    }
}