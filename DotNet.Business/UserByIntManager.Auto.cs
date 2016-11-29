//-----------------------------------------------------------------------
// <copyright file="UserByIntManager.Auto.cs" company="Hairihan">
//     Copyright (c) 2013 , All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;

namespace DotNet.Business
{
    using DotNet.Business;
    using DotNet.Utilities;

    /// <summary>
    /// UserByIntManager
    /// 用户表按数值主键测试
    /// 
    /// 修改纪录
    /// 
    /// 2013-10-02 版本：1.0 JiRiGaLa 创建主键。
    /// 
    /// <author>
    ///     <name>JiRiGaLa</name>
    ///     <date>2013-10-02</date>
    /// </author>
    /// </summary>
    public partial class UserByIntManager : BaseManager, IBaseManager
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public UserByIntManager()
        {
            if (base.dbHelper == null)
            {
                base.dbHelper = DbHelperFactory.GetHelper(BaseSystemInfo.BusinessDbType, BaseSystemInfo.BusinessDbConnection);
            }
            if (string.IsNullOrEmpty(base.CurrentTableName))
            {
                base.CurrentTableName = UserByIntEntity.TableName;
            }
            base.PrimaryKey = "UserId";
        }

        /// <summary>
        /// 构造函数
        /// <param name="tableName">指定表名</param>
        /// </summary>
        public UserByIntManager(string tableName)
        {
            base.CurrentTableName = tableName;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="dbHelper">数据库连接</param>
        public UserByIntManager(IDbHelper dbHelper): this()
        {
            DbHelper = dbHelper;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="userInfo">用户信息</param>
        public UserByIntManager(BaseUserInfo userInfo) : this()
        {
            UserInfo = userInfo;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="userInfo">用户信息</param>
        /// <param name="tableName">指定表名</param>
        public UserByIntManager(BaseUserInfo userInfo, string tableName) : this(userInfo)
        {
            base.CurrentTableName = tableName;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="dbHelper">数据库连接</param>
        /// <param name="userInfo">用户信息</param>
        public UserByIntManager(IDbHelper dbHelper, BaseUserInfo userInfo) : this(dbHelper)
        {
            UserInfo = userInfo;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="dbHelper">数据库连接</param>
        /// <param name="userInfo">用户信息</param>
        /// <param name="tableName">指定表名</param>
        public UserByIntManager(IDbHelper dbHelper, BaseUserInfo userInfo, string tableName) : this(dbHelper, userInfo)
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
        public string Add(UserByIntEntity entity, bool identity = false, bool returnId = false)
        {
            this.Identity = identity;
            this.ReturnId = returnId;
            return this.AddObject(entity);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entity">实体</param>
        public int Update(UserByIntEntity entity)
        {
            return this.UpdateObject(entity);
        }

        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="id">主键</param>
        public UserByIntEntity GetObject(string id)
        {
            return BaseEntity.Create<UserByIntEntity>(this.GetDataTable(new KeyValuePair<string, object>(this.PrimaryKey, id)));
        }

        /// <summary>
        /// 添加实体
        /// </summary>
        /// <param name="entity">实体</param>
        public string AddObject(UserByIntEntity entity)
        {
            string key = string.Empty;
            if (entity.SortCode == 0)
            {
                BaseSequenceManager sequenceManager = new BaseSequenceManager(DbHelper, this.Identity);
                key = sequenceManager.Increment(this.CurrentTableName);
                entity.SortCode = int.Parse(key);
            }
            key = entity.UserId.ToString();
            SQLBuilder sqlBuilder = new SQLBuilder(DbHelper, this.Identity, this.ReturnId);
            sqlBuilder.BeginInsert(this.CurrentTableName, this.PrimaryKey);
            if (!this.Identity) 
            {
                // 这里已经是指定了主键了，所以不需要返回主键了
                sqlBuilder.ReturnId = false;
                sqlBuilder.SetValue(this.PrimaryKey, entity.UserId);
            }
            else
            {
                if (!this.ReturnId && (DbHelper.CurrentDbType == CurrentDbType.Oracle || DbHelper.CurrentDbType == CurrentDbType.DB2))
                {
                    if (DbHelper.CurrentDbType == CurrentDbType.Oracle)
                    {
                        sqlBuilder.SetFormula(this.PrimaryKey, "SEQ_" + this.CurrentTableName.ToUpper() + ".NEXTVAL ");
                    }
                    if (DbHelper.CurrentDbType == CurrentDbType.DB2)
                    {
                        sqlBuilder.SetFormula(this.PrimaryKey, "NEXT VALUE FOR SEQ_" + this.CurrentTableName.ToUpper());
                    }
                }
                else
                {
                    if (this.Identity && (DbHelper.CurrentDbType == CurrentDbType.Oracle || DbHelper.CurrentDbType == CurrentDbType.DB2))
                    {
                        sqlBuilder.SetValue(this.PrimaryKey, entity.UserId);
                    }
                }
            }
            this.SetObject(sqlBuilder, entity);
            if (UserInfo != null) 
            { 
                sqlBuilder.SetValue(UserByIntEntity.FieldCreateUserId, UserInfo.Id);
                sqlBuilder.SetValue(UserByIntEntity.FieldCreateBy, UserInfo.Realname);
            } 
            sqlBuilder.SetDBNow(UserByIntEntity.FieldCreateOn);
            if (UserInfo != null) 
            { 
                sqlBuilder.SetValue(UserByIntEntity.FieldModifiedUserId, UserInfo.Id);
                sqlBuilder.SetValue(UserByIntEntity.FieldModifiedBy, UserInfo.Realname);
            } 
            sqlBuilder.SetDBNow(UserByIntEntity.FieldModifiedOn);
            if (this.Identity && (DbHelper.CurrentDbType == CurrentDbType.SqlServer || DbHelper.CurrentDbType == CurrentDbType.Access))
            {
                key = sqlBuilder.EndInsert().ToString();
            }
            else
            {
                sqlBuilder.EndInsert();
            }
            return key;
        }

        /// <summary>
        /// 更新实体
        /// </summary>
        /// <param name="entity">实体</param>
        public int UpdateObject(UserByIntEntity entity)
        {
            SQLBuilder sqlBuilder = new SQLBuilder(DbHelper);
            sqlBuilder.BeginUpdate(this.CurrentTableName);
            this.SetObject(sqlBuilder, entity);
            if (UserInfo != null) 
            { 
                sqlBuilder.SetValue(UserByIntEntity.FieldModifiedUserId, UserInfo.Id);
                sqlBuilder.SetValue(UserByIntEntity.FieldModifiedBy, UserInfo.Realname);
            } 
            sqlBuilder.SetDBNow(UserByIntEntity.FieldModifiedOn);
            sqlBuilder.SetWhere(this.PrimaryKey, entity.UserId);
            return sqlBuilder.EndUpdate();
        }

        // 这个是声明扩展方法
        partial void SetObjectExpand(SQLBuilder sqlBuilder, UserByIntEntity entity);
        
        /// <summary>
        /// 设置实体
        /// </summary>
        /// <param name="entity">实体</param>
        private void SetObject(SQLBuilder sqlBuilder, UserByIntEntity entity)
        {
            SetObjectExpand(sqlBuilder, entity);
            sqlBuilder.SetValue(UserByIntEntity.FieldFullName, entity.FullName);
            sqlBuilder.SetValue(UserByIntEntity.FieldSalary, entity.Salary);
            sqlBuilder.SetValue(UserByIntEntity.FieldAge, entity.Age);
            sqlBuilder.SetValue(UserByIntEntity.FieldBirthday, entity.Birthday);
            sqlBuilder.SetValue(UserByIntEntity.FieldPhoto, entity.Photo);
            sqlBuilder.SetValue(UserByIntEntity.FieldAllowEdit, entity.AllowEdit);
            sqlBuilder.SetValue(UserByIntEntity.FieldAllowDelete, entity.AllowDelete);
            sqlBuilder.SetValue(UserByIntEntity.FieldEnabled, entity.Enabled);
            sqlBuilder.SetValue(UserByIntEntity.FieldDeletionStateCode, entity.DeletionStateCode);
            sqlBuilder.SetValue(UserByIntEntity.FieldSortCode, entity.SortCode);
            sqlBuilder.SetValue(UserByIntEntity.FieldDescription, entity.Description);
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

		protected override List<KeyValuePair<string, object>> GetDeleteExtParam(List<KeyValuePair<string, object>> parameters)
		{
			var result = base.GetDeleteExtParam(parameters);
			result.Add(new KeyValuePair<string, object>(UserByIntEntity.FieldAllowDelete, 1));
			return result;
		}
    }
}
