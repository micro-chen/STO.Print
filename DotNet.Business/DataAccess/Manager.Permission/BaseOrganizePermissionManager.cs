//-----------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2016 , Hairihan TECH, Ltd.  
//-----------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;

namespace DotNet.Business
{
    using DotNet.Model;
    using DotNet.Utilities;

    /// <summary>
    /// BaseOrganizePermissionManager
    /// 组织机构权限
    /// 
    /// 修改记录
    ///
    ///     2012.03.22 版本：1.0 JiRiGaLa 创建主键。
    ///
    /// <author>
    ///		<name>JiRiGaLa</name>
    ///		<date>2012.03.22</date>
    /// </author>
    /// </summary>
    public partial class BaseOrganizePermissionManager : BaseManager, IBaseManager
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public BaseOrganizePermissionManager()
        {
            base.CurrentTableName = BasePermissionEntity.TableName;
        }

        /// <summary>
        /// 构造函数
        /// <param name="tableName">指定表名</param>
        /// </summary>
        public BaseOrganizePermissionManager(string tableName)
        {
            base.CurrentTableName = tableName;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="dbHelper">数据库连接</param>
        public BaseOrganizePermissionManager(IDbHelper dbHelper)
            : this()
        {
            DbHelper = dbHelper;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="userInfo">用户信息</param>
        public BaseOrganizePermissionManager(BaseUserInfo userInfo)
            : this()
        {
            UserInfo = userInfo;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="dbHelper">数据库连接</param>
        /// <param name="userInfo">用户信息</param>
        public BaseOrganizePermissionManager(IDbHelper dbHelper, BaseUserInfo userInfo)
            : this(dbHelper)
        {
            UserInfo = userInfo;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="dbHelper">数据库连接</param>
        /// <param name="userInfo">用户信息</param>
        /// <param name="tableName">指定表名</param>
        public BaseOrganizePermissionManager(IDbHelper dbHelper, BaseUserInfo userInfo, string tableName)
            : this(dbHelper, userInfo)
        {
            base.CurrentTableName = tableName;
        }

        public static string[] ResetPermissionByCache(string systemCode, string organizeId)
        {
            string key = "Permission:" + systemCode + ":Organize:" + organizeId;
            using (var redisClient = PooledRedisHelper.GetPermissionClient())
            {
                redisClient.Remove(key);
            }
            return GetPermissionIdsByCache(systemCode, organizeId);
        }

        public static string[] GetPermissionIdsByCache(string systemCode, string organizeId)
        {
            string[] result = null;

            string key = string.Empty;
            key = "Permission:" + systemCode + ":Organize:" + organizeId;

            List<string> items = null;
            using (var readOnlyRedisClient = PooledRedisHelper.GetPermissionReadOnlyClient())
            {
                HashSet<string> setItems = readOnlyRedisClient.GetAllItemsFromSet(key);
                if (setItems.Count == 0)
                {
                    BaseOrganizePermissionManager organizePermissionManager = new Business.BaseOrganizePermissionManager();
                    result = organizePermissionManager.GetPermissionIds(organizeId);

                    if (result != null)
                    {
                        if (result.Length > 0)
                        {
                            items = new List<string>(result);
                        }
                        else
                        {
                            // 2016-02-26 吉日嘎拉 这里是为了防止重复读取，数据库被拖、效率低
                            items = new List<string>(new string[] { string.Empty });
                        }
#if ReadOnlyRedis
                        using (var redisClient = PooledRedisHelper.GetPermissionClient())
                        {
                            redisClient.AddRangeToSet(key, items);
                            redisClient.ExpireEntryAt(key, DateTime.Now.AddMinutes(20));
                        }
#else
                        readOnlyRedisClient.AddRangeToSet(key, items);
                        readOnlyRedisClient.ExpireEntryAt(key, DateTime.Now.AddMinutes(20));
#endif
                    }
                }
                else
                {
                    result = setItems.ToArray();
                }
            }

            return result;
        }

        #region public string[] GetPermissionIds(string organizeId) 获取组织机构的权限主键数组
        /// <summary>
        /// 获取组织机构的权限主键数组
        /// </summary>
        /// <param name="organizeId">组织机构主键</param>
        /// <returns>主键数组</returns>
        public string[] GetPermissionIds(string organizeId)
        {
            string[] result = null;

            List<KeyValuePair<string, object>> parameters = new List<KeyValuePair<string, object>>();
            parameters.Add(new KeyValuePair<string, object>(BasePermissionEntity.FieldResourceCategory, BaseOrganizeEntity.TableName));
            parameters.Add(new KeyValuePair<string, object>(BasePermissionEntity.FieldResourceId, organizeId));
            parameters.Add(new KeyValuePair<string, object>(BasePermissionEntity.FieldEnabled, 1));
            parameters.Add(new KeyValuePair<string, object>(BasePermissionEntity.FieldDeletionStateCode, 0));
            result = this.GetProperties(parameters, BasePermissionEntity.FieldPermissionId);

            return result;
        }
        #endregion

        #region public string[] GetOrganizeIds(string result) 获取组织机构主键数组
        /// <summary>
        /// 获取组织机构主键数组
        /// </summary>
        /// <param name="result">操作权限</param>
        /// <returns>主键数组</returns>
        public string[] GetOrganizeIds(string permissionId)
        {
            string[] result = null;

            List<KeyValuePair<string, object>> parameters = new List<KeyValuePair<string, object>>();
            parameters.Add(new KeyValuePair<string, object>(BasePermissionEntity.FieldResourceCategory, BaseOrganizeEntity.TableName));
            parameters.Add(new KeyValuePair<string, object>(BasePermissionEntity.FieldPermissionId, permissionId));
            parameters.Add(new KeyValuePair<string, object>(BasePermissionEntity.FieldEnabled, 1));
            parameters.Add(new KeyValuePair<string, object>(BasePermissionEntity.FieldDeletionStateCode, 0));

            result = this.GetProperties(parameters, BasePermissionEntity.FieldResourceId);
            return result;
        }
        #endregion

        //
        // 授予权限的实现部分
        //

        #region private string Grant(BasePermissionManager permissionManager, string systemCode, string organizeId, string result, bool chekExists = true) 为了提高授权的运行速度
        /// <summary>
        /// 为了提高授权的运行速度
        /// </summary>
        /// <param name="permissionManager">资源权限读写器</param>
        /// <param name="Id">主键</param>
        /// <param name="organizeId">组织机构主键</param>
        /// <param name="result">权限主键</param>
        /// <param name="chekExists">判断是否存在</param>
        /// <returns>主键</returns>
        private string Grant(BasePermissionManager permissionManager, string systemCode, string organizeId, string permissionId, bool chekExists = true)
        {
            string result = string.Empty;

            string currentId = string.Empty;
            // 判断是否已经存在这个权限，若已经存在就不重复增加了
            if (chekExists)
            {
                List<KeyValuePair<string, object>> whereParameters = new List<KeyValuePair<string, object>>();
                whereParameters.Add(new KeyValuePair<string, object>(BasePermissionEntity.FieldResourceCategory, BaseOrganizeEntity.TableName));
                whereParameters.Add(new KeyValuePair<string, object>(BasePermissionEntity.FieldResourceId, organizeId));
                whereParameters.Add(new KeyValuePair<string, object>(BasePermissionEntity.FieldPermissionId, permissionId));
                currentId = this.GetId(whereParameters);

                if (!string.IsNullOrEmpty(currentId))
                {
                    List<KeyValuePair<string, object>> parameters = new List<KeyValuePair<string, object>>();
                    parameters.Add(new KeyValuePair<string, object>(BasePermissionEntity.FieldEnabled, 1));
                    parameters.Add(new KeyValuePair<string, object>(BasePermissionEntity.FieldDeletionStateCode, 0));
                    parameters.Add(new KeyValuePair<string, object>(BasePermissionEntity.FieldModifiedUserId, this.UserInfo.Id));
                    parameters.Add(new KeyValuePair<string, object>(BasePermissionEntity.FieldModifiedBy, this.UserInfo.RealName));
                    parameters.Add(new KeyValuePair<string, object>(BasePermissionEntity.FieldModifiedOn, DateTime.Now));
                    // 更新状态，设置为有效、并取消删除标志，权限也不是天天变动的，所以可以更新一下
                    this.SetProperty(currentId, parameters);
                    result = currentId;
                }
            }

            if (string.IsNullOrEmpty(currentId))
            {
                BasePermissionEntity resourcePermission = new BasePermissionEntity();
                resourcePermission.ResourceCategory = BaseOrganizeEntity.TableName;
                resourcePermission.ResourceId = organizeId;
                resourcePermission.PermissionId = permissionId;
                // 防止不允许为NULL的错误发生
                resourcePermission.Enabled = 1;
                resourcePermission.DeletionStateCode = 0;
                result = permissionManager.Add(resourcePermission);
            }

            if (string.IsNullOrEmpty(systemCode))
            {
                if (UserInfo != null && !string.IsNullOrWhiteSpace(this.UserInfo.SystemCode))
                {
                    systemCode = this.UserInfo.SystemCode;
                }
            }
            if (string.IsNullOrEmpty(systemCode))
            {
                systemCode = "Base";
            }

            // 2015-09-21 吉日嘎拉 这里增加变更日志
            string tableName = systemCode + ".Permission.Organize";
            SQLBuilder sqlBuilder = new SQLBuilder(this.DbHelper);
            sqlBuilder.BeginInsert(BaseModifyRecordEntity.TableName);
            sqlBuilder.SetValue(BaseModifyRecordEntity.FieldTableCode, tableName);
            if (DbHelper.CurrentDbType == CurrentDbType.Oracle)
            {
                sqlBuilder.SetFormula(BaseModifyRecordEntity.FieldId, "SEQ_" + BaseModifyRecordEntity.TableName + ".NEXTVAL");
            }
            sqlBuilder.SetValue(BaseModifyRecordEntity.FieldRecordKey, organizeId);
            sqlBuilder.SetValue(BaseModifyRecordEntity.FieldColumnCode, permissionId);
            sqlBuilder.SetValue(BaseModifyRecordEntity.FieldColumnDescription, BaseModuleManager.GetNameByCache(systemCode, permissionId));
            sqlBuilder.SetValue(BaseModifyRecordEntity.FieldOldValue, null);
            sqlBuilder.SetValue(BaseModifyRecordEntity.FieldNewValue, "授权");
            sqlBuilder.SetValue(BaseModifyRecordEntity.FieldCreateUserId, this.UserInfo.Id);
            sqlBuilder.SetValue(BaseModifyRecordEntity.FieldCreateBy, this.UserInfo.RealName);
            sqlBuilder.SetDBNow(BaseModifyRecordEntity.FieldCreateOn);
            sqlBuilder.SetValue(BaseModifyRecordEntity.FieldIPAddress, this.UserInfo.IPAddress);
            sqlBuilder.EndInsert();

            return result;
        }
        #endregion

        #region public string Grant(string organizeId, string result) 组织机构授予权限
        /// <summary>
        /// 组织机构授予权限
        /// </summary>
        /// <param name="organizeId">组织机构主键</param>
        /// <param name="result">权限主键</param>
        public string Grant(string systemCode, string organizeId, string permissionId)
        {
            BasePermissionManager permissionManager = new BasePermissionManager(DbHelper, UserInfo, this.CurrentTableName);
            return this.Grant(permissionManager, systemCode, organizeId, permissionId);
        }
        #endregion

        public int Grant(string systemCode, string organizeId, string[] permissionIds)
        {
            int result = 0;
            BasePermissionManager permissionManager = new BasePermissionManager(DbHelper, UserInfo, this.CurrentTableName);
            for (int i = 0; i < permissionIds.Length; i++)
            {
                this.Grant(permissionManager, systemCode, organizeId, permissionIds[i]);
                result++;
            }
            return result;
        }

        public int Grant(string systemCode, string[] organizeIds, string permissionId)
        {
            int result = 0;

            BasePermissionManager permissionManager = new BasePermissionManager(DbHelper, UserInfo, this.CurrentTableName);
            for (int i = 0; i < organizeIds.Length; i++)
            {
                this.Grant(permissionManager, systemCode, organizeIds[i], permissionId);
                result++;
            }

            return result;
        }

        public int Grant(string systemCode, string[] organizeIds, string[] permissionIds)
        {
            int result = 0;

            BasePermissionManager permissionManager = new BasePermissionManager(DbHelper, UserInfo, this.CurrentTableName);
            for (int i = 0; i < organizeIds.Length; i++)
            {
                for (int j = 0; j < permissionIds.Length; j++)
                {
                    this.Grant(permissionManager, systemCode, organizeIds[i], permissionIds[j]);
                    result++;
                }
            }

            return result;
        }


        //
        //  撤销权限的实现部分
        //

        #region private int Revoke(BasePermissionManager permissionManager, string systemCode, string organizeId, string result) 为了提高撤销的运行速度
        /// <summary>
        /// 为了提高撤销的运行速度
        /// </summary>
        /// <param name="permissionManager">资源权限读写器</param>
        /// <param name="organizeId">组织机构主键</param>
        /// <param name="result">权限主键</param>
        /// <returns>影响行数</returns>
        private int Revoke(BasePermissionManager permissionManager, string systemCode, string organizeId, string permissionId)
        {
            int result = 0;

            List<KeyValuePair<string, object>> parameters = new List<KeyValuePair<string, object>>();
            parameters.Add(new KeyValuePair<string, object>(BasePermissionEntity.FieldResourceCategory, BaseOrganizeEntity.TableName));
            parameters.Add(new KeyValuePair<string, object>(BasePermissionEntity.FieldResourceId, organizeId));
            parameters.Add(new KeyValuePair<string, object>(BasePermissionEntity.FieldPermissionId, permissionId));
            result = permissionManager.Delete(parameters);

            // 2015-09-21 吉日嘎拉 这里增加变更日志
            string tableName = systemCode + ".Permission.Organize";
            SQLBuilder sqlBuilder = new SQLBuilder(this.DbHelper);
            sqlBuilder.BeginInsert(BaseModifyRecordEntity.TableName);
            sqlBuilder.SetValue(BaseModifyRecordEntity.FieldTableCode, tableName);
            sqlBuilder.SetFormula(BaseModifyRecordEntity.FieldId, "SEQ_" + BaseModifyRecordEntity.TableName + ".NEXTVAL");
            sqlBuilder.SetValue(BaseModifyRecordEntity.FieldRecordKey, organizeId);
            sqlBuilder.SetValue(BaseModifyRecordEntity.FieldColumnCode, permissionId);
            sqlBuilder.SetValue(BaseModifyRecordEntity.FieldColumnDescription, BaseModuleManager.GetNameByCache(systemCode, permissionId));
            sqlBuilder.SetValue(BaseModifyRecordEntity.FieldOldValue, "1");
            sqlBuilder.SetValue(BaseModifyRecordEntity.FieldNewValue, "撤销授权");
            sqlBuilder.SetValue(BaseModifyRecordEntity.FieldCreateUserId, this.UserInfo.Id);
            sqlBuilder.SetValue(BaseModifyRecordEntity.FieldCreateBy, this.UserInfo.RealName);
            sqlBuilder.SetDBNow(BaseModifyRecordEntity.FieldCreateOn);
            sqlBuilder.SetValue(BaseModifyRecordEntity.FieldIPAddress, this.UserInfo.IPAddress);
            sqlBuilder.EndInsert();

            return result;
        }
        #endregion

        #region public int Revoke(string systemCode, string organizeId, string result) 撤销组织机构权限
        /// <summary>
        /// 撤销组织机构权限
        /// </summary>
        /// <param name="organizeId">组织机构主键</param>
        /// <param name="result">权限主键</param>
        /// <returns>影响行数</returns>
        public int Revoke(string systemCode, string organizeId, string permissionId)
        {
            BasePermissionManager permissionManager = new BasePermissionManager(DbHelper, UserInfo, this.CurrentTableName);
            return this.Revoke(permissionManager, systemCode, organizeId, permissionId);
        }
        #endregion

        public int Revoke(string systemCode, string organizeId, string[] permissionIds)
        {
            int result = 0;
            BasePermissionManager permissionManager = new BasePermissionManager(DbHelper, UserInfo, this.CurrentTableName);
            for (int i = 0; i < permissionIds.Length; i++)
            {
                result += this.Revoke(permissionManager, systemCode, organizeId, permissionIds[i]);
            }
            return result;
        }

        public int Revoke(string systemCode, string[] organizeIds, string permissionId)
        {
            int result = 0;
            BasePermissionManager permissionManager = new BasePermissionManager(DbHelper, UserInfo, this.CurrentTableName);
            for (int i = 0; i < organizeIds.Length; i++)
            {
                result += this.Revoke(permissionManager, systemCode, organizeIds[i], permissionId);
            }
            return result;
        }

        public int Revoke(string systemCode, string[] organizeIds, string[] permissionIds)
        {
            int result = 0;

            BasePermissionManager permissionManager = new BasePermissionManager(DbHelper, UserInfo, this.CurrentTableName);
            for (int i = 0; i < organizeIds.Length; i++)
            {
                for (int j = 0; j < permissionIds.Length; j++)
                {
                    result += this.Revoke(permissionManager, systemCode, organizeIds[i], permissionIds[j]);
                }
            }

            return result;
        }

        #region public int RevokeAll(string organizeId) 撤销组织机构全部权限
        /// <summary>
        /// 撤销组织机构全部权限
        /// </summary>
        /// <param name="organizeId">组织机构主键</param>
        /// <returns>影响行数</returns>
        public int RevokeAll(string organizeId)
        {
            BasePermissionManager permissionManager = new BasePermissionManager(DbHelper, UserInfo, this.CurrentTableName);
            List<KeyValuePair<string, object>> parameters = new List<KeyValuePair<string, object>>();
            parameters.Add(new KeyValuePair<string, object>(BasePermissionEntity.FieldResourceCategory, BaseOrganizeEntity.TableName));
            parameters.Add(new KeyValuePair<string, object>(BasePermissionEntity.FieldResourceId, organizeId));
            return permissionManager.Delete(parameters);
        }
        #endregion
    }
}