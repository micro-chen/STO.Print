//-----------------------------------------------------------------------
// <copyright file="BaseContactManager.Auto.cs" company="Hairihan">
//     Copyright (C) 2016 , All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System.Collections.Generic;

namespace DotNet.Business
{
    using DotNet.Model;
    using DotNet.Utilities;

    /// <summary>
    /// BaseContactManager
    /// 联络单主表
    ///
    /// 修改记录
    ///
    ///		2015-10-30 版本：2.0 JiRiGaLa 必读、必回。
    ///		2015-09-09 版本：2.0 JiRiGaLa 添加支持单实例。
    ///		2010-07-15 版本：1.0 JiRiGaLa 创建主键。
    ///
    /// 版本：2.0
    ///
    /// <author>
    ///		<name>JiRiGaLa</name>
    ///		<date>2015-10-30</date>
    /// </author>
    /// </summary>
    public partial class BaseContactManager : BaseManager, IBaseManager
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public BaseContactManager()
        {
            if (base.dbHelper == null)
            {
                base.dbHelper = DbHelperFactory.GetHelper(BaseSystemInfo.MessageDbType, BaseSystemInfo.MessageDbConnection);
            }
            base.CurrentTableName = BaseContactEntity.TableName;
            base.PrimaryKey = "Id";
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
            return this.AddEntity(entity);
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="userInfo">操作员信息</param>
        /// <param name="entity">实体</param>
        /// <returns>主键</returns>
        public string Add(BaseUserInfo userInfo, BaseContactEntity entity)
        {
            return this.AddEntity(userInfo, entity);
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
            return this.AddEntity(entity);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns>影响行数</returns>
        public int Update(BaseContactEntity entity)
        {
            return this.UpdateEntity(this.UserInfo, entity);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="userInfo">操作员</param>
        /// <param name="entity">实体</param>
        /// <returns>影响行数</returns>
        public int Update(BaseUserInfo userInfo, BaseContactEntity entity)
        {
            return this.UpdateEntity(userInfo, entity);
        }

        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="id">主键</param>
        public BaseContactEntity GetEntity(string id)
        {
            return new BaseContactEntity(this.GetDataTableById(id));
        }

        /// <summary>
        /// 添加实体
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns>主键</returns>
        public string AddEntity(BaseContactEntity entity)
        {
            return AddEntity(this.UserInfo, entity);
        }

        /// <summary>
        /// 添加实体
        /// </summary>
        /// <param name="userInfo">操作员</param>
        /// <param name="entity">实体</param>
        /// <returns>主键</returns>
        public string AddEntity(BaseUserInfo userInfo, BaseContactEntity entity)
        {
            string sequence = string.Empty;
            sequence = entity.Id;
            if (entity.SortCode == 0)
            {
                BaseSequenceManager sequenceManager = new BaseSequenceManager(DbHelper, this.Identity);
                sequence = sequenceManager.Increment(this.CurrentTableName);
                entity.SortCode = int.Parse(sequence);
            }
            if (entity.Id is string)
            {
                this.Identity = false;
                this.ReturnId = false;
            }
            SQLBuilder sqlBuilder = new SQLBuilder(DbHelper, this.Identity, this.ReturnId);
            sqlBuilder.BeginInsert(BaseContactEntity.TableName, BaseContactEntity.FieldId);
            if (!this.Identity)
            {
                sqlBuilder.SetValue(BaseContactEntity.FieldId, entity.Id);
            }
            this.SetEntity(sqlBuilder, entity);
            if (userInfo != null)
            {
                sqlBuilder.SetValue(BaseContactEntity.FieldCreateUserId, userInfo.Id);
                sqlBuilder.SetValue(BaseContactEntity.FieldCreateBy, userInfo.RealName);
            }
            sqlBuilder.SetDBNow(BaseContactEntity.FieldCreateOn);
            if (userInfo != null)
            {
                sqlBuilder.SetValue(BaseContactEntity.FieldModifiedUserId, userInfo.Id);
                sqlBuilder.SetValue(BaseContactEntity.FieldModifiedBy, userInfo.RealName);
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
        /// <param name="userInfo">操作员</param>
        /// <param name="entity">实体</param>
        /// <returns>影响行数</returns>
        public int UpdateEntity(BaseUserInfo userInfo, BaseContactEntity entity)
        {
            SQLBuilder sqlBuilder = new SQLBuilder(DbHelper);
            sqlBuilder.BeginUpdate(BaseContactEntity.TableName);
            this.SetEntity(sqlBuilder, entity);
            if (userInfo != null)
            {
                sqlBuilder.SetValue(BaseContactEntity.FieldModifiedUserId, userInfo.Id);
                sqlBuilder.SetValue(BaseContactEntity.FieldModifiedBy, userInfo.RealName);
            }
            sqlBuilder.SetDBNow(BaseContactEntity.FieldModifiedOn);
            sqlBuilder.SetWhere(BaseContactEntity.FieldId, entity.Id);
            return sqlBuilder.EndUpdate();
        }

        /// <summary>
        /// 设置实体
        /// </summary>
        /// <param name="entity">实体</param>
        private void SetEntity(SQLBuilder sqlBuilder, BaseContactEntity entity)
        {
            sqlBuilder.SetValue(BaseContactEntity.FieldParentId, entity.ParentId);
            sqlBuilder.SetValue(BaseContactEntity.FieldTitle, entity.Title);
            sqlBuilder.SetValue(BaseContactEntity.FieldColor, entity.Color);
            sqlBuilder.SetValue(BaseContactEntity.FieldStyle, entity.Style);
            sqlBuilder.SetValue(BaseContactEntity.FieldContents, entity.Contents);
            sqlBuilder.SetValue(BaseContactEntity.FieldPriority, entity.Priority);
            sqlBuilder.SetValue(BaseContactEntity.FieldCancelTopDay, entity.CancelTopDay);
            sqlBuilder.SetValue(BaseContactEntity.FieldSendCount, entity.SendCount);
            sqlBuilder.SetValue(BaseContactEntity.FieldReadCount, entity.ReadCount);
            sqlBuilder.SetValue(BaseContactEntity.FieldReplyCount, entity.ReplyCount);
            sqlBuilder.SetValue(BaseContactEntity.FieldSource, entity.Source);
            sqlBuilder.SetValue(BaseContactEntity.FieldIsOpen, entity.IsOpen);
            sqlBuilder.SetValue(BaseContactEntity.FieldCategoryCode, entity.CategoryCode);
            sqlBuilder.SetValue(BaseContactEntity.FieldLabelMark, entity.LabelMark);
            sqlBuilder.SetValue(BaseContactEntity.FieldIPAddress, entity.IPAddress);
            sqlBuilder.SetValue(BaseContactEntity.FieldAllowComments, entity.AllowComments);
            sqlBuilder.SetValue(BaseContactEntity.FieldMustRead, entity.MustRead);
            sqlBuilder.SetValue(BaseContactEntity.FieldMustReply, entity.MustReply);
            sqlBuilder.SetValue(BaseContactEntity.FieldCommentUserId, entity.CommentUserId);
            sqlBuilder.SetValue(BaseContactEntity.FieldCommentUserRealName, entity.CommentUserRealName);
            sqlBuilder.SetValue(BaseContactEntity.FieldCommentDate, entity.CommentDate);
            sqlBuilder.SetValue(BaseContactEntity.FieldDeletionStateCode, entity.DeletionStateCode);
            sqlBuilder.SetValue(BaseContactEntity.FieldAuditStatus, entity.AuditStatus);
            sqlBuilder.SetValue(BaseContactEntity.FieldAuditUserId, entity.AuditUserId);
            sqlBuilder.SetValue(BaseContactEntity.FieldAuditUserRealName, entity.AuditUserRealName);
            sqlBuilder.SetValue(BaseContactEntity.FieldEnabled, entity.Enabled);
            sqlBuilder.SetValue(BaseContactEntity.FieldCreateDepartment, entity.CreateDepartment);
            sqlBuilder.SetValue(BaseContactEntity.FieldCreateCompany, entity.CreateCompany);
            sqlBuilder.SetValue(BaseContactEntity.FieldCreateCompanyId, entity.CreateCompanyId);
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
