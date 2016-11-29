//-----------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2013 , Hairihan TECH, Ltd. 
//-----------------------------------------------------------------

using System.Data;
using System.Linq;
using System.Collections.Generic;

namespace DotNet.Business
{
    using DotNet.Utilities;

    /// <summary>
    /// BaseOrganizeScopeManager
    /// 组织机构权限
    /// 
    /// 修改纪录
    ///
    ///     2012.03.22 版本：1.0 JiRiGaLa 创建主键。
    ///
    /// <author>
    ///		<name>JiRiGaLa</name>
    ///		<date>2012.03.22</date>
    /// </author>
    /// </summary>
    public partial class BaseOrganizeScopeManager : BaseManager, IBaseManager
    {
        ////
        ////
        //// 授权范围管理部分
        ////
        ////

        #region public string[] GetModuleIds(string organizeId, string permissionCode) 获取员工的权限主键数组
        /// <summary>
        /// 获取员工的权限主键数组
        /// </summary>
        /// <param name="organizeId">员工代吗</param>
        /// <param name="permissionCode">权限代码</param>
        /// <returns>主键数组</returns>
        public string[] GetModuleIds(string organizeId, string permissionCode)
        {
            string[] result = null;

            List<KeyValuePair<string, object>> parameters = new List<KeyValuePair<string, object>>();
            parameters.Add(new KeyValuePair<string, object>(BasePermissionScopeEntity.FieldResourceCategory, BaseOrganizeEntity.TableName));
            parameters.Add(new KeyValuePair<string, object>(BasePermissionScopeEntity.FieldResourceId, organizeId));
            parameters.Add(new KeyValuePair<string, object>(BasePermissionScopeEntity.FieldTargetCategory, BaseModuleEntity.TableName));
            parameters.Add(new KeyValuePair<string, object>(BasePermissionScopeEntity.FieldPermissionId, this.GetIdByCode(permissionCode)));

            var dt = this.GetDataTable(parameters);
            result = BaseBusinessLogic.FieldToArray(dt, BasePermissionScopeEntity.FieldTargetId).Distinct<string>().Where(t => !string.IsNullOrEmpty(t)).ToArray();
            return result;
        }
        #endregion

        //
        // 授予授权范围的实现部分
        //

        #region private string GrantModule(BasePermissionScopeManager permissionScopeManager, string id, string organizeId, string grantModuleId) 为了提高授权的运行速度
        /// <summary>
        /// 为了提高授权的运行速度
        /// </summary>
        /// <param name="permissionScopeManager">权限域读写器</param>
        /// <param name="Id">主键</param>
        /// <param name="organizeId">员工主键</param>
        /// <param name="grantModuleId">权限主键</param>
        /// <returns>主键</returns>
        private string GrantModule(BasePermissionScopeManager permissionScopeManager, string organizeId, string permissionCode, string grantModuleId)
        {
            string result = string.Empty;
            BasePermissionScopeEntity resourcePermissionScopeEntity = new BasePermissionScopeEntity();
            resourcePermissionScopeEntity.PermissionId = int.Parse(this.GetIdByCode(permissionCode));
            resourcePermissionScopeEntity.ResourceCategory = BaseOrganizeEntity.TableName;
            resourcePermissionScopeEntity.ResourceId = organizeId;
            resourcePermissionScopeEntity.TargetCategory = BaseModuleEntity.TableName;
            resourcePermissionScopeEntity.TargetId = grantModuleId;
            resourcePermissionScopeEntity.Enabled = 1;
            resourcePermissionScopeEntity.DeletionStateCode = 0;
            return permissionScopeManager.Add(resourcePermissionScopeEntity);
        }
        #endregion

        #region public string GrantModule(string organizeId, string permissionId) 员工授予权限
        /// <summary>
        /// 员工授予权限
        /// </summary>
        /// <param name="organizeId">员工主键</param>
        /// <param name="permissionId">权限主键</param>
        /// <param name="organizeId">权组织机构限主键</param>
        /// <returns>主键</returns>
        public string GrantModule(string organizeId, string permissionCode, string grantModuleId)
        {
            BasePermissionScopeManager permissionScopeManager = new BasePermissionScopeManager(DbHelper, UserInfo, this.CurrentTableName);
            return this.GrantModule(permissionScopeManager, organizeId, permissionCode, grantModuleId);
        }
        #endregion

        public int GrantModules(string organizeId, string permissionCode, string[] grantModuleIds)
        {
            int result = 0;
            BasePermissionScopeManager permissionScopeManager = new BasePermissionScopeManager(DbHelper, UserInfo, this.CurrentTableName);
            for (int i = 0; i < grantModuleIds.Length; i++)
            {
                this.GrantModule(permissionScopeManager, organizeId, permissionCode, grantModuleIds[i]);
                result++;
            }
            return result;
        }

        public int GrantModules(string[] organizeIds, string permissionCode, string grantModuleId)
        {
            int result = 0;
            BasePermissionScopeManager permissionScopeManager = new BasePermissionScopeManager(DbHelper, UserInfo, this.CurrentTableName);
            for (int i = 0; i < organizeIds.Length; i++)
            {
                this.GrantModule(permissionScopeManager, organizeIds[i], permissionCode, grantModuleId);
                result++;
            }
            return result;
        }

        public int GrantModules(string[] organizeIds, string permissionCode, string[] grantModuleIds)
        {
            int result = 0;
            BasePermissionScopeManager permissionScopeManager = new BasePermissionScopeManager(DbHelper, UserInfo, this.CurrentTableName);
            for (int i = 0; i < organizeIds.Length; i++)
            {
                for (int j = 0; j < grantModuleIds.Length; j++)
                {
                    this.GrantModule(permissionScopeManager, organizeIds[i], permissionCode, grantModuleIds[j]);
                    result++;
                }
            }
            return result;
        }


        //
        //  撤销授权范围的实现部分
        //

        #region private int RevokeModule(BasePermissionScopeManager permissionScopeManager, string organizeId, string permissionCode, string revokeModuleId) 为了提高授权的运行速度
        /// <summary>
        /// 为了提高授权的运行速度
        /// </summary>
        /// <param name="permissionScopeManager">权限域读写器</param>
        /// <param name="organizeId">员工主键</param>
        /// <param name="revokeModuleId">权限主键</param>
        /// <returns>主键</returns>
        private int RevokeModule(BasePermissionScopeManager permissionScopeManager, string organizeId, string permissionCode, string revokeModuleId)
        {
            List<KeyValuePair<string, object>> parameters = new List<KeyValuePair<string, object>>();
            parameters.Add(new KeyValuePair<string, object>(BasePermissionScopeEntity.FieldResourceCategory, BaseOrganizeEntity.TableName));
            parameters.Add(new KeyValuePair<string, object>(BasePermissionScopeEntity.FieldResourceId, organizeId));
            parameters.Add(new KeyValuePair<string, object>(BasePermissionScopeEntity.FieldTargetCategory, BaseModuleEntity.TableName));
            parameters.Add(new KeyValuePair<string, object>(BasePermissionScopeEntity.FieldTargetId, revokeModuleId));
            parameters.Add(new KeyValuePair<string, object>(BasePermissionScopeEntity.FieldPermissionId, this.GetIdByCode(permissionCode)));
            return permissionScopeManager.Delete(parameters);
        }
        #endregion

        #region public int RevokeModule(string organizeId, string permissionId) 员工撤销授权
        /// <summary>
        /// 员工撤销授权
        /// </summary>
        /// <param name="organizeId">员工主键</param>
        /// <param name="permissionId">权限主键</param>
        /// <returns>主键</returns>
        public int RevokeModule(string organizeId, string permissionCode, string revokeModuleId)
        {
            BasePermissionScopeManager permissionScopeManager = new BasePermissionScopeManager(DbHelper, UserInfo, this.CurrentTableName);
            return this.RevokeModule(permissionScopeManager, organizeId, permissionCode, revokeModuleId);
        }
        #endregion

        public int RevokeModules(string organizeId, string permissionCode, string[] revokeModuleIds)
        {
            int result = 0;
            BasePermissionScopeManager permissionScopeManager = new BasePermissionScopeManager(DbHelper, UserInfo, this.CurrentTableName);
            for (int i = 0; i < revokeModuleIds.Length; i++)
            {
                this.RevokeModule(permissionScopeManager, organizeId, permissionCode, revokeModuleIds[i]);
                result++;
            }
            return result;
        }

        public int RevokeModules(string[] organizeIds, string permissionCode, string revokeModuleId)
        {
            int result = 0;
            BasePermissionScopeManager permissionScopeManager = new BasePermissionScopeManager(DbHelper, UserInfo, this.CurrentTableName);
            for (int i = 0; i < organizeIds.Length; i++)
            {
                this.RevokeModule(permissionScopeManager, organizeIds[i], permissionCode, revokeModuleId);
                result++;
            }
            return result;
        }

        public int RevokeModules(string[] organizeIds, string permissionCode, string[] revokeModuleIds)
        {
            int result = 0;
            BasePermissionScopeManager permissionScopeManager = new BasePermissionScopeManager(DbHelper, UserInfo, this.CurrentTableName);
            for (int i = 0; i < organizeIds.Length; i++)
            {
                for (int j = 0; j < revokeModuleIds.Length; j++)
                {
                    this.RevokeModule(permissionScopeManager, organizeIds[i], permissionCode, revokeModuleIds[j]);
                    result++;
                }
            }
            return result;
        }
    }
}