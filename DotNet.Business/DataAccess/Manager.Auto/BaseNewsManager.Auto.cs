//-----------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2016 , Hairihan TECH, Ltd. 
//-----------------------------------------------------------------

using System.Collections.Generic;

namespace DotNet.Business
{
    using DotNet.Model;
    using DotNet.Utilities;

    /// <summary>
    /// BaseNewsManager
    /// 新闻表
    ///
    /// 修改记录
    ///
    ///		2010-07-28 版本：1.0 JiRiGaLa 创建主键。
    ///
    /// <author>
    ///		<name>JiRiGaLa</name>
    ///		<date>2010-07-28</date>
    /// </author>
    /// </summary>
    public partial class BaseNewsManager : BaseManager, IBaseManager
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public BaseNewsManager()
        {
            if (base.dbHelper == null)
            {
                base.dbHelper = DbHelperFactory.GetHelper(BaseSystemInfo.UserCenterDbType, BaseSystemInfo.UserCenterDbConnection);
            }
            base.CurrentTableName = BaseNewsEntity.TableName;
        }

        /// <summary>
        /// 构造函数
        /// <param name="tableName">指定表名</param>
        /// </summary>
        public BaseNewsManager(string tableName)
        {
            base.CurrentTableName = tableName;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="dbHelper">数据库连接</param>
        public BaseNewsManager(IDbHelper dbHelper)
            : this()
        {
            DbHelper = dbHelper;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="result">操作员信息</param>
        public BaseNewsManager(BaseUserInfo userInfo)
            : this()
        {
            UserInfo = userInfo;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="dbHelper">数据库连接</param>
        /// <param name="result">操作员信息</param>
        public BaseNewsManager(IDbHelper dbHelper, BaseUserInfo userInfo)
            : this(dbHelper)
        {
            UserInfo = userInfo;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="dbHelper">数据库连接</param>
        /// <param name="result">操作员信息</param>
        /// <param name="tableName">指定表名</param>
        public BaseNewsManager(IDbHelper dbHelper, BaseUserInfo userInfo, string tableName)
            : this(dbHelper, userInfo)
        {
            base.CurrentTableName = tableName;
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns>主键</returns>
        public string Add(BaseNewsEntity entity)
        {
            return this.AddObject(entity);
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="identity">自增量方式</param>
        /// <param name="returnId">返回主鍵</param>
        /// <returns>主键</returns>
        public string Add(BaseNewsEntity entity, bool identity, bool returnId)
        {
            this.Identity = identity;
            this.ReturnId = returnId;
            return this.AddObject(entity);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entity">实体</param>
        public int Update(BaseNewsEntity entity)
        {
            return this.UpdateObject(entity);
        }

        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="id">主键</param>
        public BaseNewsEntity GetObject(string id)
        {
            return BaseEntity.Create<BaseNewsEntity>(this.ExecuteReader(new KeyValuePair<string, object>(BaseNewsEntity.FieldId, id)));
            // return BaseEntity.Create<BaseNewsEntity>(this.GetDataTable(new KeyValuePair<string, object>(BaseNewsEntity.FieldId, id)));
        }

        /// <summary>
        /// 添加实体
        /// </summary>
        /// <param name="entity">实体</param>
        public string AddObject(BaseNewsEntity entity)
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
            }
            SQLBuilder sqlBuilder = new SQLBuilder(DbHelper, this.Identity, this.ReturnId);
            sqlBuilder.BeginInsert(this.CurrentTableName, BaseNewsEntity.FieldId);
            if (!this.Identity)
            {
                sqlBuilder.SetValue(BaseNewsEntity.FieldId, entity.Id);
            }
            else
            {
                if (!this.ReturnId && DbHelper.CurrentDbType == CurrentDbType.Oracle)
                {
                    sqlBuilder.SetFormula(BaseNewsEntity.FieldId, "SEQ_" + this.CurrentTableName.ToUpper() + ".NEXTVAL ");
                }
                else
                {
                    if (this.Identity && DbHelper.CurrentDbType == CurrentDbType.Oracle)
                    {
                        if (string.IsNullOrEmpty(entity.Id))
                        {
                            if (string.IsNullOrEmpty(sequence))
                            {
                                BaseSequenceManager sequenceManager = new BaseSequenceManager(DbHelper, this.Identity);
                                sequence = sequenceManager.Increment(this.CurrentTableName);
                            }
                            entity.Id = sequence;
                        }
                        sqlBuilder.SetValue(BaseNewsEntity.FieldId, entity.Id);
                    }
                }
            }
            this.SetObject(sqlBuilder, entity);
            if (UserInfo != null)
            {
                entity.CreateUserId = UserInfo.Id;
                entity.CreateBy = UserInfo.RealName;
                sqlBuilder.SetValue(BaseNewsEntity.FieldCreateUserId, entity.CreateUserId);
                sqlBuilder.SetValue(BaseNewsEntity.FieldCreateBy, entity.CreateBy);
                entity.ModifiedBy = UserInfo.RealName;
                sqlBuilder.SetValue(BaseNewsEntity.FieldModifiedUserId, entity.CreateUserId);
                sqlBuilder.SetValue(BaseNewsEntity.FieldModifiedBy, entity.CreateBy);
            }
            sqlBuilder.SetDBNow(BaseNewsEntity.FieldCreateOn);
            sqlBuilder.SetDBNow(BaseNewsEntity.FieldModifiedOn);
            /*
            if (UserInfo != null)
            {
                sqlBuilder.SetValue(BaseNewsEntity.FieldModifiedUserId, UserInfo.Id);
                sqlBuilder.SetValue(BaseNewsEntity.FieldModifiedBy, UserInfo.RealName);
            }
            sqlBuilder.SetDBNow(BaseNewsEntity.FieldModifiedOn);
             */
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
        public int UpdateObject(BaseNewsEntity entity)
        {
            SQLBuilder sqlBuilder = new SQLBuilder(DbHelper);
            sqlBuilder.BeginUpdate(this.CurrentTableName);
            this.SetObject(sqlBuilder, entity);
            if (UserInfo != null)
            {
                sqlBuilder.SetValue(BaseNewsEntity.FieldModifiedUserId, UserInfo.Id);
                sqlBuilder.SetValue(BaseNewsEntity.FieldModifiedBy, UserInfo.RealName);
            }
            sqlBuilder.SetDBNow(BaseNewsEntity.FieldModifiedOn);
            sqlBuilder.SetWhere(BaseNewsEntity.FieldId, entity.Id);
            return sqlBuilder.EndUpdate();
        }

        partial void SetObjectExpand(SQLBuilder sqlBuilder, BaseNewsEntity entity);

        /// <summary>
        /// 设置实体
        /// </summary>
        /// <param name="entity">实体</param>
        private void SetObject(SQLBuilder sqlBuilder, BaseNewsEntity entity)
        {
            if (entity.Contents == null)
            {
                entity.FileSize = 0;
            }
            else
            {
                entity.FileSize = entity.Contents.Length;
            }
            sqlBuilder.SetValue(BaseNewsEntity.FieldCompanyId, entity.CompanyId);
            sqlBuilder.SetValue(BaseNewsEntity.FieldCompanyName, entity.CompanyName);
            sqlBuilder.SetValue(BaseNewsEntity.FieldDepartmentId, entity.DepartmentId);
            sqlBuilder.SetValue(BaseNewsEntity.FieldDepartmentName, entity.DepartmentName);
            sqlBuilder.SetValue(BaseNewsEntity.FieldFolderId, entity.FolderId);
            sqlBuilder.SetValue(BaseNewsEntity.FieldCategoryCode, entity.CategoryCode);
            sqlBuilder.SetValue(BaseNewsEntity.FieldCode, entity.Code);
            sqlBuilder.SetValue(BaseNewsEntity.FieldTitle, entity.Title);
            sqlBuilder.SetValue(BaseNewsEntity.FieldFilePath, entity.FilePath);
            sqlBuilder.SetValue(BaseNewsEntity.FieldIntroduction, entity.Introduction);
            sqlBuilder.SetValue(BaseNewsEntity.FieldContents, entity.Contents);
            sqlBuilder.SetValue(BaseNewsEntity.FieldSource, entity.Source);
            sqlBuilder.SetValue(BaseNewsEntity.FieldKeywords, entity.Keywords);
            sqlBuilder.SetValue(BaseNewsEntity.FieldFileSize, entity.FileSize);
            sqlBuilder.SetValue(BaseNewsEntity.FieldImageUrl, entity.ImageUrl);
            sqlBuilder.SetValue(BaseNewsEntity.FieldHomePage, entity.HomePage);
            sqlBuilder.SetValue(BaseNewsEntity.FieldSubPage, entity.SubPage);
            sqlBuilder.SetValue(BaseNewsEntity.FieldAuditStatus, entity.AuditStatus);
            sqlBuilder.SetValue(BaseNewsEntity.FieldReadCount, entity.ReadCount);
            sqlBuilder.SetValue(BaseNewsEntity.FieldDeletionStateCode, entity.DeletionStateCode);
            sqlBuilder.SetValue(BaseNewsEntity.FieldDescription, entity.Description);
            sqlBuilder.SetValue(BaseNewsEntity.FieldEnabled, entity.Enabled);
            sqlBuilder.SetValue(BaseNewsEntity.FieldSortCode, entity.SortCode);
            SetObjectExpand(sqlBuilder, entity);
        }

        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns>影响行数</returns>
        public int Delete(string id)
        {
            return this.Delete(new KeyValuePair<string, object>(BaseNewsEntity.FieldId, id));
        }
    }
}
