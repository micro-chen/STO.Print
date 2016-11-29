//-----------------------------------------------------------------------
// <copyright file="BaseExportDataManager.Auto.cs" company="Hairihan">
//     Copyright (C) 2016 , All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System;
using System.Data;
using System.Collections.Generic;

namespace DotNet.Business
{
    using DotNet.Model;
    using DotNet.Utilities;

    /// <summary>
    /// BaseExportDataManager
    /// 大数据导出任务表
    /// 
    /// 修改记录
    /// 
    /// 2016-02-23 版本：1.1 JiRiGaLa 增加远程增加功能。
    /// 2013-12-27 版本：1.0 JiRiGaLa 创建文件。
    /// 
    /// <author>
    ///     <name>JiRiGaLa</name>
    ///     <date>2016-02-23</date>
    /// </author>
    /// </summary>
    public partial class BaseExportDataManager : BaseManager, IBaseManager
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public BaseExportDataManager()
        {
            if (base.dbHelper == null)
            {
                base.dbHelper = DbHelperFactory.GetHelper(BaseSystemInfo.UserCenterDbType, BaseSystemInfo.UserCenterDbConnection);
            }
            if (string.IsNullOrEmpty(base.CurrentTableName))
            {
                base.CurrentTableName = BaseExportDataEntity.TableName;
            }
            base.PrimaryKey = "Id";
        }

        /// <summary>
        /// 构造函数
        /// <param name="tableName">指定表名</param>
        /// </summary>
        public BaseExportDataManager(string tableName)
        {
            base.CurrentTableName = tableName;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="dbHelper">数据库连接</param>
        public BaseExportDataManager(IDbHelper dbHelper)
            : this()
        {
            DbHelper = dbHelper;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="userInfo">用户信息</param>
        public BaseExportDataManager(BaseUserInfo userInfo)
            : this()
        {
            UserInfo = userInfo;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="userInfo">用户信息</param>
        /// <param name="tableName">指定表名</param>
        public BaseExportDataManager(BaseUserInfo userInfo, string tableName)
            : this(userInfo)
        {
            base.CurrentTableName = tableName;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="dbHelper">数据库连接</param>
        /// <param name="userInfo">用户信息</param>
        public BaseExportDataManager(IDbHelper dbHelper, BaseUserInfo userInfo)
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
        public BaseExportDataManager(IDbHelper dbHelper, BaseUserInfo userInfo, string tableName)
            : this(dbHelper, userInfo)
        {
            base.CurrentTableName = tableName;
        }

        /// <summary>
        /// 添加, 这里可以人工干预，提高程序的性能
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="identity">自增量方式，表主键是否采用自增的策略</param>
        /// <param name="returnId">返回主键，不返回程序允许速度会快，主要是为了主细表批量插入数据优化用的</param>
        /// <returns>主键</returns>
        public string Add(BaseExportDataEntity entity, bool identity = false, bool returnId = false, bool remoteInterface = false)
        {
            this.Identity = identity;
            this.ReturnId = returnId;
            this.RemoteInterface = remoteInterface;
            return this.AddObject(entity);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entity">实体</param>
        public int Update(BaseExportDataEntity entity)
        {
            return this.UpdateObject(entity);
        }

        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="id">主键</param>
        public BaseExportDataEntity GetObject(string id)
        {
            return BaseEntity.Create<BaseExportDataEntity>(this.ExecuteReader(new KeyValuePair<string, object>(BaseExportDataEntity.FieldId, id)));
            // return BaseEntity.Create<BaseExportDataEntity>(this.GetDataTable(new KeyValuePair<string, object>(this.PrimaryKey, id)));
        }

        /// <summary>
        /// 添加实体
        /// </summary>
        /// <param name="entity">实体</param>
        public string AddObject(BaseExportDataEntity entity)
        {
            string result = string.Empty;

            if (string.IsNullOrEmpty(entity.Id))
            {
                entity.Id = Guid.NewGuid().ToString("N");
            }
            result = entity.Id;

            SQLBuilder sqlBuilder = new SQLBuilder(DbHelper, this.Identity, this.ReturnId);
            sqlBuilder.BeginInsert(this.CurrentTableName, this.PrimaryKey);
            if (!this.Identity)
            {
                // 这里已经是指定了主键了，所以不需要返回主键了
                sqlBuilder.SetValue(this.PrimaryKey, entity.Id);
            }
            this.SetObject(sqlBuilder, entity);
            if (UserInfo != null)
            {
                sqlBuilder.SetValue(BaseExportDataEntity.FieldCreateUserId, UserInfo.Id);
                sqlBuilder.SetValue(BaseExportDataEntity.FieldCreateBy, UserInfo.RealName);
            }
            sqlBuilder.SetDBNow(BaseExportDataEntity.FieldCreateOn);
            // 若是远程调用接口？
            if (this.RemoteInterface)
            {
                sqlBuilder.PrepareCommand();
                DbHelperUtilities.ExecuteNonQuery(UserInfo, sqlBuilder.CommandText, CommandType.Text.ToString(), sqlBuilder.DbParameters);
            }
            else
            {
                sqlBuilder.EndInsert();
            }
            
            return result;
        }

        /// <summary>
        /// 更新实体
        /// </summary>
        /// <param name="entity">实体</param>
        public int UpdateObject(BaseExportDataEntity entity)
        {
            SQLBuilder sqlBuilder = new SQLBuilder(DbHelper);
            sqlBuilder.BeginUpdate(this.CurrentTableName);
            this.SetObject(sqlBuilder, entity);
            sqlBuilder.SetWhere(this.PrimaryKey, entity.Id);
            return sqlBuilder.EndUpdate();
        }

        // 这个是声明扩展方法
        partial void SetObjectExpand(SQLBuilder sqlBuilder, BaseExportDataEntity entity);

        /// <summary>
        /// 设置实体
        /// </summary>
        /// <param name="entity">实体</param>
        private void SetObject(SQLBuilder sqlBuilder, BaseExportDataEntity entity)
        {
            SetObjectExpand(sqlBuilder, entity);
            sqlBuilder.SetValue(BaseExportDataEntity.FieldCompanyId, entity.CompanyId);
            sqlBuilder.SetValue(BaseExportDataEntity.FieldCompanyCode, entity.CompanyCode);
            sqlBuilder.SetValue(BaseExportDataEntity.FieldCompanyName, entity.CompanyName);
            sqlBuilder.SetValue(BaseExportDataEntity.FieldDepartmentName, entity.DepartmentName);
            sqlBuilder.SetValue(BaseExportDataEntity.FieldDbCode, entity.DbCode);
            sqlBuilder.SetValue(BaseExportDataEntity.FieldDataCategory, entity.DataCategory);
            sqlBuilder.SetValue(BaseExportDataEntity.FieldFullName, entity.FullName);
            sqlBuilder.SetValue(BaseExportDataEntity.FieldFilePath, entity.FilePath);
            sqlBuilder.SetValue(BaseExportDataEntity.FieldFileName, entity.FileName);
            sqlBuilder.SetValue(BaseExportDataEntity.FieldProcessingStart, entity.ProcessingStart);
            sqlBuilder.SetValue(BaseExportDataEntity.FieldProcessingEnd, entity.ProcessingEnd);
            sqlBuilder.SetValue(BaseExportDataEntity.FieldExportSql, entity.ExportSql);
            sqlBuilder.SetValue(BaseExportDataEntity.FieldPermission, entity.Permission);
            sqlBuilder.SetValue(BaseExportDataEntity.FieldExportState, entity.ExportState);
            sqlBuilder.SetValue(BaseExportDataEntity.FieldSeverAddress, entity.SeverAddress);
            sqlBuilder.SetValue(BaseExportDataEntity.FieldDescription, entity.Description);
        }

        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns>影响行数</returns>
        public int Delete(string id)
        {
            return this.Delete(new KeyValuePair<string, object>(this.PrimaryKey, id));
        }
    }
}
