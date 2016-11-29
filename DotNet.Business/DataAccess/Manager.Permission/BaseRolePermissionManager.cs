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
    /// BaseRolePermissionManager
    /// 角色权限
    /// 
    /// 修改记录
    ///
    ///     2016.02.25 版本：2.2 JiRiGaLa 通过缓存获取权限的方法改进。
    ///     2016.01.06 版本：2.1 JiRiGaLa 分 systemCode 进行代码整顿。
    ///     2013.05.27 版本：2.0 JiRiGaLa BasePermissionEntity.FieldResourceCategory, tableName，写入准确的对象表名
    ///     2010.04.23 版本：1.1 JiRiGaLa Enabled 不允许为NULL的错误解决。
    ///     2008.05.23 版本：1.0 JiRiGaLa 创建主键。
    ///
    /// <author>
    ///		<name>JiRiGaLa</name>
    ///		<date>2016.02.25</date>
    /// </author>
    /// </summary>
    public partial class BaseRolePermissionManager : BaseManager, IBaseManager
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public BaseRolePermissionManager()
        {
            base.CurrentTableName = BasePermissionEntity.TableName;
        }

        /// <summary>
        /// 构造函数
        /// <param name="tableName">指定表名</param>
        /// </summary>
        public BaseRolePermissionManager(string tableName)
        {
            base.CurrentTableName = tableName;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="dbHelper">数据库连接</param>
        public BaseRolePermissionManager(IDbHelper dbHelper)
            : this()
        {
            DbHelper = dbHelper;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="userInfo">用户信息</param>
        public BaseRolePermissionManager(BaseUserInfo userInfo)
            : this()
        {
            UserInfo = userInfo;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="dbHelper">数据库连接</param>
        /// <param name="userInfo">用户信息</param>
        public BaseRolePermissionManager(IDbHelper dbHelper, BaseUserInfo userInfo)
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
        public BaseRolePermissionManager(IDbHelper dbHelper, BaseUserInfo userInfo, string tableName)
            : this(dbHelper, userInfo)
        {
            base.CurrentTableName = tableName;
        }

        /// <summary>
        /// 获取角色拥有的权限列表
        /// </summary>
        /// <param name="systemCode">系统编号</param>
        /// <param name="roleId">角色主键</param>
        /// <returns>列表</returns>
        public List<BaseModuleEntity> GetPermissionList(string systemCode, string roleId)
        {
            List<BaseModuleEntity> result = null;

            string[] permissionIds = GetPermissionIds(systemCode, roleId);
            if (permissionIds != null && permissionIds.Length > 0)
            {
                List<KeyValuePair<string, object>> parameters = new List<KeyValuePair<string, object>>();
                parameters.Add(new KeyValuePair<string, object>(BaseModuleEntity.FieldId, permissionIds));
                parameters.Add(new KeyValuePair<string, object>(BaseModuleEntity.FieldEnabled, 1));
                parameters.Add(new KeyValuePair<string, object>(BaseModuleEntity.FieldDeletionStateCode, 0));

                string tableName = systemCode + "Module";
                BaseModuleManager moduleManager = new BaseModuleManager(tableName);
                result = moduleManager.GetList<BaseModuleEntity>(parameters);
            }

            return result;
        }

        public static string[] ResetPermissionByCache(string systemCode, string roleId)
        {
            string key = "Permission:" + systemCode + ":Role:" + roleId;
            using (var redisClient = PooledRedisHelper.GetPermissionClient())
            {
                redisClient.Remove(key);
            }
            return GetPermissionIdsByCache(systemCode, new string[] { roleId });
        }

        /// <summary>
        /// 多个角色，都有啥权限？单个角色都有啥权限的循环获取？
        /// </summary>
        /// <param name="systemCode">系统编号</param>
        /// <param name="roleIds">角色主键数组</param>
        /// <returns>权限数组</returns>
        public static string[] GetPermissionIdsByCache(string systemCode, string[] roleIds)
        {
            string[] result = null;

            string key = string.Empty;
            string roleId = string.Empty;
            BaseRolePermissionManager rolePermissionManager = null;

            string[] permissionIds = null;
            using (var readOnlyRedisClient = PooledRedisHelper.GetPermissionReadOnlyClient())
            {
                for (int i = 0; i < roleIds.Length; i++)
                {
                    // 2016-02-26 吉日嘎拉 若是角色是空的，跳出循环，没必要查了，提高效率
                    roleId = roleIds[i];
                    if (string.IsNullOrEmpty(roleId))
                    {
                        continue;
                    }
                    key = "Permission:" + systemCode + ":Role:" + roleId;
                    List<string> items = null;

                    HashSet<string> setItems = readOnlyRedisClient.GetAllItemsFromSet(key);
                    if (setItems.Count == 0)
                    {
                        if (rolePermissionManager == null)
                        {
                            rolePermissionManager = new Business.BaseRolePermissionManager();
                        }
                        permissionIds = rolePermissionManager.GetPermissionIds(systemCode, roleId);

                        if (permissionIds != null)
                        {
                            if (permissionIds.Length > 0)
                            {
                                items = new List<string>(permissionIds);
                            }
                            else
                            {
                                // 2016-02-26 吉日嘎拉 这里是为了防止重复读取，数据库被拖、效率低
                                items = new List<string>(new string[] { string.Empty });
                            }

#if ReadOnlyRedis
                            using (var redisClient = PooledRedisHelper.GetClient())
                            {
                                // 2016-02-26 吉日嘎拉 角色权限一般发生变更时，需要有一定的即时性、所以不能缓存太长时间，宁可再次读取一下数据库
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
                        permissionIds = setItems.ToArray();
                    }

                    result = StringUtil.Concat(result, permissionIds);
                }
            }

            return result;
        }

        #region public string[] GetPermissionIds(string systemCode, string roleId) 获取角色的权限主键数组
        /// <summary>
        /// 获取角色的权限主键数组
        /// </summary>
        /// <param name="systemCode">系统编号</param>
        /// <param name="roleId">角色主键</param>
        /// <returns>主键数组</returns>
        public string[] GetPermissionIds(string systemCode, string roleId)
        {
            string[] result = null;

            this.CurrentTableName = systemCode + "Permission";

            List<KeyValuePair<string, object>> parameters = new List<KeyValuePair<string, object>>();
            string resourceCategory = systemCode + "Role";

            parameters.Add(new KeyValuePair<string, object>(BasePermissionEntity.FieldResourceCategory, resourceCategory));
            parameters.Add(new KeyValuePair<string, object>(BasePermissionEntity.FieldResourceId, roleId));
            parameters.Add(new KeyValuePair<string, object>(BasePermissionEntity.FieldEnabled, 1));
            parameters.Add(new KeyValuePair<string, object>(BasePermissionEntity.FieldDeletionStateCode, 0));

            result = this.GetProperties(parameters, BasePermissionEntity.FieldPermissionId);

            return result;
        }
        #endregion

        #region public string[] GetRoleIds(string systemCode, string permissionId) 获取角色主键数组
        /// <summary>
        /// 获取角色主键数组
        /// </summary>
        /// <param name="systemCode">系统编号</param>
        /// <param name="permissionId">操作权限(模块主键)</param>
        /// <returns>主键数组</returns>
        public string[] GetRoleIds(string systemCode, string permissionId)
        {
            string[] result = null;

            this.CurrentTableName = systemCode + "Permission";
            List<KeyValuePair<string, object>> parameters = new List<KeyValuePair<string, object>>();
            string resourceCategory = systemCode + "Role";
            parameters.Add(new KeyValuePair<string, object>(BasePermissionEntity.FieldResourceCategory, resourceCategory));
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

        #region public string Grant(string systemCode, string roleId, string permissionId, bool chekExists = true) 为了提高授权的运行速度
        /// <summary>
        /// 为了提高授权的运行速度
        /// </summary>
        /// <param name="systemCode">系统编号</param>
        /// <param name="roleId">角色主键</param>
        /// <param name="permissionId">权限主键</param>
        /// <returns>主键</returns>
        public string Grant(string systemCode, string roleId, string permissionId, bool chekExists = true)
        {
            string result = string.Empty;

            string currentId = string.Empty;

            this.CurrentTableName = systemCode + "Permission";

            string tableName = systemCode + "Role";

            // 判断是否已经存在这个权限，若已经存在就不重复增加了
            if (chekExists)
            {
                List<KeyValuePair<string, object>> whereParameters = new List<KeyValuePair<string, object>>();
                whereParameters.Add(new KeyValuePair<string, object>(BasePermissionEntity.FieldResourceCategory, tableName));
                whereParameters.Add(new KeyValuePair<string, object>(BasePermissionEntity.FieldResourceId, roleId));
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
                }
            }

            if (string.IsNullOrEmpty(currentId))
            {
                BasePermissionEntity permissionEntity = new BasePermissionEntity();
                permissionEntity.ResourceCategory = tableName;
                permissionEntity.ResourceId = roleId;
                permissionEntity.PermissionId = permissionId;
                // 防止不允许为NULL的错误发生
                permissionEntity.Enabled = 1;
                permissionEntity.DeletionStateCode = 0;
                BasePermissionManager permissionManager = new BasePermissionManager(DbHelper, UserInfo, this.CurrentTableName);
                result = permissionManager.Add(permissionEntity);
            }

            // 2015-09-21 吉日嘎拉 这里增加变更日志
            tableName = systemCode + ".Permission.Role";
            SQLBuilder sqlBuilder = new SQLBuilder(this.DbHelper);
            sqlBuilder.BeginInsert(BaseModifyRecordEntity.TableName);
            sqlBuilder.SetValue(BaseModifyRecordEntity.FieldTableCode, tableName);
            if (DbHelper.CurrentDbType == CurrentDbType.Oracle)
            {
                sqlBuilder.SetFormula(BaseModifyRecordEntity.FieldId, "SEQ_" + BaseModifyRecordEntity.TableName + ".NEXTVAL");
            }
            sqlBuilder.SetValue(BaseModifyRecordEntity.FieldRecordKey, roleId);
            sqlBuilder.SetValue(BaseModifyRecordEntity.FieldColumnCode, "授权");
            sqlBuilder.SetValue(BaseModifyRecordEntity.FieldColumnDescription, BaseModuleManager.GetNameByCache(systemCode, permissionId));
            sqlBuilder.SetValue(BaseModifyRecordEntity.FieldOldValue, null);
            sqlBuilder.SetValue(BaseModifyRecordEntity.FieldNewValue, permissionId);
            sqlBuilder.SetValue(BaseModifyRecordEntity.FieldCreateUserId, this.UserInfo.Id);
            sqlBuilder.SetValue(BaseModifyRecordEntity.FieldCreateBy, this.UserInfo.RealName);
            sqlBuilder.SetDBNow(BaseModifyRecordEntity.FieldCreateOn);
            sqlBuilder.SetValue(BaseModifyRecordEntity.FieldIPAddress, this.UserInfo.IPAddress);
            sqlBuilder.EndInsert();

            return result;
        }
        #endregion

        public int Grant(string systemCode, string roleId, string[] permissionIds)
        {
            int result = 0;
            for (int i = 0; i < permissionIds.Length; i++)
            {
                this.Grant(systemCode, roleId, permissionIds[i]);
                result++;
            }
            return result;
        }

        public int Grant(string systemCode, string[] roleIds, string permissionId)
        {
            int result = 0;
            for (int i = 0; i < roleIds.Length; i++)
            {
                this.Grant(systemCode, roleIds[i], permissionId);
                result++;
            }
            return result;
        }

        public int Grant(string systemCode, string[] roleIds, string[] permissionIds)
        {
            int result = 0;

            for (int i = 0; i < roleIds.Length; i++)
            {
                for (int j = 0; j < permissionIds.Length; j++)
                {
                    this.Grant(systemCode, roleIds[i], permissionIds[j]);
                    result++;
                }
            }

            return result;
        }


        //
        //  撤销权限的实现部分
        //

        #region public int Revoke(string systemCode, string roleId, string permissionId) 为了提高撤销的运行速度
        /// <summary>
        /// 为了提高撤销的运行速度
        /// </summary>
        /// <param name="systemCode">资源权限读写器</param>
        /// <param name="roleId">角色主键</param>
        /// <param name="permissionId">权限主键</param>
        /// <returns>影响行数</returns>
        public int Revoke(string systemCode, string roleId, string permissionId)
        {
            int result = 0;

            string tableName = systemCode + "Permission";
            BasePermissionManager permissionManager = new BasePermissionManager(DbHelper, UserInfo, tableName);

            tableName = systemCode + "Role";
            List<KeyValuePair<string, object>> parameters = new List<KeyValuePair<string, object>>();
            parameters.Add(new KeyValuePair<string, object>(BasePermissionEntity.FieldResourceCategory, tableName));
            parameters.Add(new KeyValuePair<string, object>(BasePermissionEntity.FieldResourceId, roleId));
            parameters.Add(new KeyValuePair<string, object>(BasePermissionEntity.FieldPermissionId, permissionId));

            // 2015-09-21 吉日嘎拉 这里增加变更日志
            tableName = systemCode + ".Permission.Role";
            SQLBuilder sqlBuilder = new SQLBuilder(this.DbHelper);
            sqlBuilder.BeginInsert(BaseModifyRecordEntity.TableName);
            sqlBuilder.SetValue(BaseModifyRecordEntity.FieldTableCode, tableName);
            if (DbHelper.CurrentDbType == CurrentDbType.Oracle)
            {
                sqlBuilder.SetFormula(BaseModifyRecordEntity.FieldId, "SEQ_" + BaseModifyRecordEntity.TableName + ".NEXTVAL");
            }
            sqlBuilder.SetValue(BaseModifyRecordEntity.FieldRecordKey, roleId);
            sqlBuilder.SetValue(BaseModifyRecordEntity.FieldColumnCode, "撤销授权");
            sqlBuilder.SetValue(BaseModifyRecordEntity.FieldColumnDescription, BaseModuleManager.GetNameByCache(systemCode, permissionId));
            sqlBuilder.SetValue(BaseModifyRecordEntity.FieldOldValue, "1");
            sqlBuilder.SetValue(BaseModifyRecordEntity.FieldNewValue, permissionId);
            sqlBuilder.SetValue(BaseModifyRecordEntity.FieldCreateUserId, this.UserInfo.Id);
            sqlBuilder.SetValue(BaseModifyRecordEntity.FieldCreateBy, this.UserInfo.RealName);
            sqlBuilder.SetDBNow(BaseModifyRecordEntity.FieldCreateOn);
            sqlBuilder.SetValue(BaseModifyRecordEntity.FieldIPAddress, this.UserInfo.IPAddress);
            sqlBuilder.EndInsert();

            // 伪删除、数据有冗余，但是有历史记录
            // result = permissionManager.SetDeleted(parameters);
            // 真删除、执行效率高、但是没有历史记录
            result = permissionManager.Delete(parameters);

            return result;
        }
        #endregion

        public int Revoke(string systemCode, string roleId, string[] permissionIds)
        {
            int result = 0;
            for (int i = 0; i < permissionIds.Length; i++)
            {
                result += this.Revoke(systemCode, roleId, permissionIds[i]);
            }
            return result;
        }

        public int Revoke(string systemCode, string[] roleIds, string permissionId)
        {
            int result = 0;
            for (int i = 0; i < roleIds.Length; i++)
            {
                result += this.Revoke(systemCode, roleIds[i], permissionId);
            }
            return result;
        }

        public int Revoke(string systemCode, string[] roleIds, string[] permissionIds)
        {
            int result = 0;
            for (int i = 0; i < roleIds.Length; i++)
            {
                for (int j = 0; j < permissionIds.Length; j++)
                {
                    result += this.Revoke(systemCode, roleIds[i], permissionIds[j]);
                }
            }
            return result;
        }

        #region public int RevokeAll(string systemCode, string roleId) 撤销角色全部权限
        /// <summary>
        /// 撤销角色全部权限
        /// </summary>
        /// <param name="systemCode">系统编号</param>
        /// <param name="roleId">角色主键</param>
        /// <returns>影响行数</returns>
        public int RevokeAll(string systemCode, string roleId)
        {
            string tableName = systemCode + "Permission";
            BasePermissionManager permissionManager = new BasePermissionManager(DbHelper, UserInfo, tableName);
            List<KeyValuePair<string, object>> parameters = new List<KeyValuePair<string, object>>();
            tableName = systemCode + "Role";
            parameters.Add(new KeyValuePair<string, object>(BasePermissionEntity.FieldResourceCategory, tableName));
            parameters.Add(new KeyValuePair<string, object>(BasePermissionEntity.FieldResourceId, roleId));
            return permissionManager.Delete(parameters);
        }
        #endregion
    }
}