//-----------------------------------------------------------------------
// <copyright file="TAB_EMPLOYEEManager.Auto.cs" company="Hairihan">
//     Copyright (c) 2013 , All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;

namespace DotNet.Business
{
    using DotNet.Utilities;
    using DotNet.Model;

    /// <summary>
    /// TAB_EMPLOYEEManager
    /// 员工
    /// 
    /// 修改纪录
    /// 
    /// 2013-11-23 版本：1.0 JiRiGaLa 创建文件。
    /// 
    /// <author>
    ///     <name>JiRiGaLa</name>
    ///     <date>2013-11-23</date>
    /// </author>
    /// </summary>
    public partial class TAB_EMPLOYEEManager : BaseManager, IBaseManager
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TAB_EMPLOYEEManager()
        {
            if (base.dbHelper == null)
            {
                base.dbHelper = DbHelperFactory.GetHelper(BaseSystemInfo.BusinessDbType, BaseSystemInfo.BusinessDbConnection);
            }
            if (string.IsNullOrEmpty(base.CurrentTableName))
            {
                base.CurrentTableName = TAB_EMPLOYEEEntity.TableName;
            }
            base.PrimaryKey = "ID";
        }

        /// <summary>
        /// 构造函数
        /// <param name="tableName">指定表名</param>
        /// </summary>
        public TAB_EMPLOYEEManager(string tableName)
        {
            base.CurrentTableName = tableName;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="dbHelper">数据库连接</param>
        public TAB_EMPLOYEEManager(IDbHelper dbHelper): this()
        {
            DbHelper = dbHelper;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="userInfo">用户信息</param>
        public TAB_EMPLOYEEManager(BaseUserInfo userInfo) : this()
        {
            UserInfo = userInfo;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="userInfo">用户信息</param>
        /// <param name="tableName">指定表名</param>
        public TAB_EMPLOYEEManager(BaseUserInfo userInfo, string tableName) : this(userInfo)
        {
            base.CurrentTableName = tableName;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="dbHelper">数据库连接</param>
        /// <param name="userInfo">用户信息</param>
        public TAB_EMPLOYEEManager(IDbHelper dbHelper, BaseUserInfo userInfo) : this(dbHelper)
        {
            UserInfo = userInfo;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="dbHelper">数据库连接</param>
        /// <param name="userInfo">用户信息</param>
        /// <param name="tableName">指定表名</param>
        public TAB_EMPLOYEEManager(IDbHelper dbHelper, BaseUserInfo userInfo, string tableName) : this(dbHelper, userInfo)
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
        public string Add(TAB_EMPLOYEEEntity entity, bool identity = false, bool returnId = false)
        {
            this.Identity = identity;
            this.ReturnId = returnId;
            return this.AddObject(entity);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entity">实体</param>
        public int Update(TAB_EMPLOYEEEntity entity)
        {
            return this.UpdateObject(entity);
        }

        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="id">主键</param>
        public TAB_EMPLOYEEEntity GetObject(string id)
        {
            return BaseEntity.Create<TAB_EMPLOYEEEntity>(this.GetDataTable(new KeyValuePair<string, object>(this.PrimaryKey, id)));
        }

        /// <summary>
        /// 添加实体
        /// </summary>
        /// <param name="entity">实体</param>
        public string AddObject(TAB_EMPLOYEEEntity entity)
        {
            string key = string.Empty;
            key = entity.ID.ToString();
            SQLBuilder sqlBuilder = new SQLBuilder(DbHelper, this.Identity, this.ReturnId);
            sqlBuilder.BeginInsert(this.CurrentTableName, this.PrimaryKey);
            if (!this.Identity) 
            {
                // 这里已经是指定了主键了，所以不需要返回主键了
                sqlBuilder.ReturnId = false;
                sqlBuilder.SetValue(this.PrimaryKey, entity.ID);
            }
            this.SetObject(sqlBuilder, entity);
            sqlBuilder.EndInsert();
            return key;
        }

        /// <summary>
        /// 更新实体
        /// </summary>
        /// <param name="entity">实体</param>
        public int UpdateObject(TAB_EMPLOYEEEntity entity)
        {
            SQLBuilder sqlBuilder = new SQLBuilder(DbHelper);
            sqlBuilder.BeginUpdate(this.CurrentTableName);
            this.SetObject(sqlBuilder, entity);
            sqlBuilder.SetWhere(this.PrimaryKey, entity.ID);
            return sqlBuilder.EndUpdate();
        }

        // 这个是声明扩展方法
        partial void SetObjectExpand(SQLBuilder sqlBuilder, TAB_EMPLOYEEEntity entity);
        
        /// <summary>
        /// 设置实体
        /// </summary>
        /// <param name="entity">实体</param>
        private void SetObject(SQLBuilder sqlBuilder, TAB_EMPLOYEEEntity entity)
        {
            SetObjectExpand(sqlBuilder, entity);
            sqlBuilder.SetValue(TAB_EMPLOYEEEntity.Field_OWNER_SITE, entity.OWNER_SITE);
            sqlBuilder.SetValue(TAB_EMPLOYEEEntity.Field_TRANSFER_ADD_FEE, entity.TRANSFER_ADD_FEE);
            sqlBuilder.SetValue(TAB_EMPLOYEEEntity.Field_EMPLOYEE_CODE, entity.EMPLOYEE_CODE);
            sqlBuilder.SetValue(TAB_EMPLOYEEEntity.Field_OWNER_RANGE, entity.OWNER_RANGE);
            sqlBuilder.SetValue(TAB_EMPLOYEEEntity.Field_CARDNUM, entity.CARDNUM);
            sqlBuilder.SetValue(TAB_EMPLOYEEEntity.Field_BL_DISPATCH_GAIN, entity.BL_DISPATCH_GAIN);
            sqlBuilder.SetValue(TAB_EMPLOYEEEntity.Field_BL_SEND_GAIN, entity.BL_SEND_GAIN);
            sqlBuilder.SetValue(TAB_EMPLOYEEEntity.Field_ID_CARD, entity.ID_CARD);
            sqlBuilder.SetValue(TAB_EMPLOYEEEntity.Field_UPDATETIME, entity.UPDATETIME);
            sqlBuilder.SetValue(TAB_EMPLOYEEEntity.Field_BAR_PASSWORD, entity.BAR_PASSWORD);
            sqlBuilder.SetValue(TAB_EMPLOYEEEntity.Field_EMPLOYEE_TYPE, entity.EMPLOYEE_TYPE);
            sqlBuilder.SetValue(TAB_EMPLOYEEEntity.Field_DISPATCH__ADD_FEE, entity.DISPATCH__ADD_FEE);
            sqlBuilder.SetValue(TAB_EMPLOYEEEntity.Field_DEPT_NAME, entity.DEPT_NAME);
            sqlBuilder.SetValue(TAB_EMPLOYEEEntity.Field_ADDRESS, entity.ADDRESS);
            sqlBuilder.SetValue(TAB_EMPLOYEEEntity.Field_PHONE, entity.PHONE);
            sqlBuilder.SetValue(TAB_EMPLOYEEEntity.Field_EMPLOYEE_NAME, entity.EMPLOYEE_NAME);
            sqlBuilder.SetValue(TAB_EMPLOYEEEntity.Field_DELETIONSTATECODE, entity.DELETIONSTATECODE);
            sqlBuilder.SetValue(TAB_EMPLOYEEEntity.Field_GROUP_NAME, entity.GROUP_NAME);
            sqlBuilder.SetValue(TAB_EMPLOYEEEntity.Field_DISPATCH_ADD_FEE_OPERA, entity.DISPATCH_ADD_FEE_OPERA);
            sqlBuilder.SetValue(TAB_EMPLOYEEEntity.Field_TRANSFER_ADD_FEE_OPERA, entity.TRANSFER_ADD_FEE_OPERA);
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
