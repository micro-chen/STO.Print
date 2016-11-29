//-------------------------------------------------------------------------------------
// AllData Rights Reserved , Copyright (C) 2011 , Hairihan TECH, Ltd .
//-------------------------------------------------------------------------------------

using System;
using System.Data;
using System.Collections.Generic;

namespace DotNet.Business
{
    using DotNet.Utilities;

    /// <summary>
    /// BaseContactManager
    /// 联络单主表
    ///
    /// 修改纪录
    ///
    ///		2010-07-15 版本：1.0 JiRiGaLa 创建主键。
    ///
    /// <author>
    ///		<name>JiRiGaLa</name>
    ///		<date>2010-07-15</date>
    /// </author>
    /// </summary>
    public partial class BaseContactManager : BaseManager, IBaseManager
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public BaseContactManager()
        {
            base.CurrentTableName = BaseContactEntity.TableName;
        }

        /// <summary>
        /// 构造函数
        /// <param name="tableName">指定表名</param>
        /// </summary>
        public BaseContactManager(string tableName)
        {
            base.CurrentTableName = tableName;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="dbHelper">数据库连接</param>
        public BaseContactManager(IDbHelper dbHelper)
            : this()
        {
            DbHelper = dbHelper;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="userInfo">操作员信息</param>
        public BaseContactManager(BaseUserInfo userInfo)
            : this()
        {
            UserInfo = userInfo;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="dbHelper">数据库连接</param>
        /// <param name="userInfo">操作员信息</param>
        public BaseContactManager(IDbHelper dbHelper, BaseUserInfo userInfo)
            : this(dbHelper)
        {
            UserInfo = userInfo;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="dbHelper">数据库连接</param>
        /// <param name="userInfo">操作员信息</param>
        /// <param name="tableName">指定表名</param>
        public BaseContactManager(IDbHelper dbHelper, BaseUserInfo userInfo, string tableName)
            : this(dbHelper, userInfo)
        {
            base.CurrentTableName = tableName;
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns>主键</returns>
        public string Add(BaseContactEntity entity)
        {
            return this.AddObject(entity);
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="identity">自增量方式</param>
        /// <returns>主键</returns>
        public string Add(BaseContactEntity entity, bool identity)
        {
            this.Identity = identity;
            return this.AddObject(entity);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entity">实体</param>
        public int Update(BaseContactEntity entity)
        {
            return this.UpdateObject(entity);
        }

        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="id">主键</param>
        public BaseContactEntity GetObject(string id)
        {
            return BaseEntity.Create<BaseContactEntity>(this.GetDataTableById(id));
        }

        /// <summary>
        /// 添加实体
        /// </summary>
        /// <param name="entity">实体</param>
        public string AddObject(BaseContactEntity entity)
        {
            string sequence = string.Empty;
            sequence = entity.Id;
            if (entity.SortCode == 0)
            {
                BaseSequenceManager sequenceManager = new BaseSequenceManager(DbHelper, this.Identity);
                sequence = sequenceManager.Increment(this.CurrentTableName);
                entity.SortCode = int.Parse(sequence);
            }
            SQLBuilder sqlBuilder = new SQLBuilder(DbHelper, this.Identity, this.ReturnId);
            sqlBuilder.BeginInsert(BaseContactEntity.TableName, BaseContactEntity.FieldId);
            if (entity.Id is string)
            {
                this.Identity = false;
            }
            if (!this.Identity)
            {
                sqlBuilder.SetValue(BaseContactEntity.FieldId, entity.Id);
            }
            else
            {
                if (!this.ReturnId && DbHelper.CurrentDbType == CurrentDbType.Oracle)
                {
                    sqlBuilder.SetFormula(BaseContactEntity.FieldId, "SEQ_" + this.CurrentTableName.ToUpper() + ".NEXTVAL ");
                }
                else
                {
                    if (this.Identity && DbHelper.CurrentDbType == CurrentDbType.Oracle)
                    {
                        if (string.IsNullOrEmpty(sequence))
                        {
                            BaseSequenceManager sequenceManager = new BaseSequenceManager(DbHelper, this.Identity);
                            sequence = sequenceManager.Increment(this.CurrentTableName);
                        }
                        entity.Id = sequence;
                        sqlBuilder.SetValue(BaseContactEntity.FieldId, entity.Id);
                    }
                }
            }
            this.SetObject(sqlBuilder, entity);
            if (UserInfo != null)
            {
                sqlBuilder.SetValue(BaseContactEntity.FieldCreateUserId, UserInfo.Id);
                sqlBuilder.SetValue(BaseContactEntity.FieldCreateBy, UserInfo.Realname);
            }
            sqlBuilder.SetDBNow(BaseContactEntity.FieldCreateOn);
            if (UserInfo != null)
            {
                sqlBuilder.SetValue(BaseContactEntity.FieldModifiedUserId, UserInfo.Id);
                sqlBuilder.SetValue(BaseContactEntity.FieldModifiedBy, UserInfo.Realname);
            }
            sqlBuilder.SetDBNow(BaseContactEntity.FieldModifiedOn);
            if (DbHelper.CurrentDbType == CurrentDbType.SqlServer && this.Identity)
            {
                sequence = sqlBuilder.EndInsert().ToString();
            }
            else
            {
                sqlBuilder.EndInsert();
            }
            return sequence;
        }

        /// <summary>
        /// 更新实体
        /// </summary>
        /// <param name="entity">实体</param>
        public int UpdateObject(BaseContactEntity entity)
        {
            SQLBuilder sqlBuilder = new SQLBuilder(DbHelper);
            sqlBuilder.BeginUpdate(BaseContactEntity.TableName);
            this.SetObject(sqlBuilder, entity);
            if (UserInfo != null)
            {
                sqlBuilder.SetValue(BaseContactEntity.FieldModifiedUserId, UserInfo.Id);
                sqlBuilder.SetValue(BaseContactEntity.FieldModifiedBy, UserInfo.Realname);
            }
            sqlBuilder.SetDBNow(BaseContactEntity.FieldModifiedOn);
            sqlBuilder.SetWhere(BaseContactEntity.FieldId, entity.Id);
            return sqlBuilder.EndUpdate();
        }

        /// <summary>
        /// 设置实体
        /// </summary>
        /// <param name="entity">实体</param>
        private void SetObject(SQLBuilder sqlBuilder, BaseContactEntity entity)
        {
            sqlBuilder.SetValue(BaseContactEntity.FieldParentId, entity.ParentId);
            sqlBuilder.SetValue(BaseContactEntity.FieldTitle, entity.Title);
            sqlBuilder.SetValue(BaseContactEntity.FieldContents, entity.Contents);
            sqlBuilder.SetValue(BaseContactEntity.FieldPriority, entity.Priority);
            sqlBuilder.SetValue(BaseContactEntity.FieldSendCount, entity.SendCount);
            sqlBuilder.SetValue(BaseContactEntity.FieldReadCount, entity.ReadCount);
            sqlBuilder.SetValue(BaseContactEntity.FieldIsOpen, entity.IsOpen);
            sqlBuilder.SetValue(BaseContactEntity.FieldCommentUserId, entity.CommentUserId);
            sqlBuilder.SetValue(BaseContactEntity.FieldCommentUserRealname, entity.CommentUserRealname);
            sqlBuilder.SetValue(BaseContactEntity.FieldCommentDate, entity.CommentDate);
            sqlBuilder.SetValue(BaseContactEntity.FieldDeletionStateCode, entity.DeletionStateCode);
            sqlBuilder.SetValue(BaseContactEntity.FieldEnabled, entity.Enabled);
            sqlBuilder.SetValue(BaseContactEntity.FieldSortCode, entity.SortCode);
            sqlBuilder.SetValue(BaseContactEntity.FieldDescription, entity.Description);
        }

        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns>影响行数</returns>
        public int Delete(string id)
        {
            return this.Delete(new KeyValuePair<string, object>(BaseContactEntity.FieldId, id));
        }
    }
}
