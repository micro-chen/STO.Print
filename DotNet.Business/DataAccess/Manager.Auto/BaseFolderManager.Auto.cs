//--------------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2016 , Hairihan TECH, Ltd. 
//--------------------------------------------------------------------

using System;
using System.Collections.Generic;

namespace DotNet.Business
{
    using DotNet.Model;
    using DotNet.Utilities;

    /// <summary>
    /// BaseFolderManager
    /// 文件夹表
    /// 
    /// 修改记录
    /// 
    /// 2012-05-17 版本：1.0 JiRiGaLa 创建主键。
    /// 
    /// <author>
    ///     <name>JiRiGaLa</name>
    ///     <date>2012-05-17</date>
    /// </author>
    /// </summary>
    public partial class BaseFolderManager : BaseManager, IBaseManager
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public BaseFolderManager()
        {
            if (base.dbHelper == null)
            {
                base.dbHelper = DbHelperFactory.GetHelper(BaseSystemInfo.UserCenterDbType, BaseSystemInfo.UserCenterDbConnection);
            }
            base.CurrentTableName = BaseFolderEntity.TableName;
            base.PrimaryKey = "Id";
        }

        /// <summary>
        /// 构造函数
        /// <param name="tableName">指定表名</param>
        /// </summary>
        public BaseFolderManager(string tableName)
        {
            base.CurrentTableName = tableName;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="dbHelper">数据库连接</param>
        public BaseFolderManager(IDbHelper dbHelper): this()
        {
            DbHelper = dbHelper;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="userInfo">用户信息</param>
        public BaseFolderManager(BaseUserInfo userInfo) : this()
        {
            UserInfo = userInfo;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="dbHelper">数据库连接</param>
        /// <param name="userInfo">用户信息</param>
        public BaseFolderManager(IDbHelper dbHelper, BaseUserInfo userInfo) : this(dbHelper)
        {
            UserInfo = userInfo;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="dbHelper">数据库连接</param>
        /// <param name="userInfo">用户信息</param>
        /// <param name="tableName">指定表名</param>
        public BaseFolderManager(IDbHelper dbHelper, BaseUserInfo userInfo, string tableName) : this(dbHelper, userInfo)
        {
            base.CurrentTableName = tableName;
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns>主键</returns>
        public string Add(BaseFolderEntity entity)
        {
            return this.AddObject(entity);
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="identity">自增量方式</param>
        /// <param name="returnId">返回主键</param>
        /// <returns>主键</returns>
        public string Add(BaseFolderEntity entity, bool identity, bool returnId)
        {
            this.Identity = identity;
            this.ReturnId = returnId;
            return this.AddObject(entity);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entity">实体</param>
        public int Update(BaseFolderEntity entity)
        {
            return this.UpdateObject(entity);
        }

        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="id">主键</param>
        public BaseFolderEntity GetObject(string id)
        {
            return BaseEntity.Create<BaseFolderEntity>(this.ExecuteReader(new KeyValuePair<string, object>(BaseFolderEntity.FieldId, id)));
            // return BaseEntity.Create<BaseFolderEntity>(this.GetDataTable(new KeyValuePair<string, object>(BaseFolderEntity.FieldId, id)));
        }

        /// <summary>
        /// 添加实体
        /// </summary>
        /// <param name="entity">实体</param>
        public string AddObject(BaseFolderEntity entity)
        {
            string sequence = string.Empty;
            this.Identity = false; 
            if (entity.SortCode == null || entity.SortCode == 0)
            {
                BaseSequenceManager sequenceManager = new BaseSequenceManager(DbHelper, this.Identity);
                sequence = sequenceManager.Increment(this.CurrentTableName);
                entity.SortCode = int.Parse(sequence);
            }
            if (entity.Id != null)
            {
                sequence = entity.Id.ToString();
            }
            SQLBuilder sqlBuilder = new SQLBuilder(DbHelper, this.Identity, this.ReturnId);
            sqlBuilder.BeginInsert(this.CurrentTableName, BaseFolderEntity.FieldId);
            if (!this.Identity) 
            {
                if (string.IsNullOrEmpty(entity.Id)) 
                {
                    sequence = Guid.NewGuid().ToString("N"); 
                    entity.Id = sequence ;
                }
                sqlBuilder.SetValue(BaseFolderEntity.FieldId, entity.Id);
            }
            else
            {
                if (!this.ReturnId && (DbHelper.CurrentDbType == CurrentDbType.Oracle || DbHelper.CurrentDbType == CurrentDbType.DB2))
                {
                    if (DbHelper.CurrentDbType == CurrentDbType.Oracle)
                    {
                        sqlBuilder.SetFormula(BaseFolderEntity.FieldId, "SEQ_" + this.CurrentTableName.ToUpper() + ".NEXTVAL ");
                    }
                    if (DbHelper.CurrentDbType == CurrentDbType.DB2)
                    {
                        sqlBuilder.SetFormula(BaseFolderEntity.FieldId, "NEXT VALUE FOR SEQ_" + this.CurrentTableName.ToUpper());
                    }
                }
                else
                {
                    if (this.Identity && (DbHelper.CurrentDbType == CurrentDbType.Oracle || DbHelper.CurrentDbType == CurrentDbType.DB2))
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
                        sqlBuilder.SetValue(BaseFolderEntity.FieldId, entity.Id);
                    }
                }
            }
            this.SetObject(sqlBuilder, entity);
            if (UserInfo != null) 
            { 
                sqlBuilder.SetValue(BaseFolderEntity.FieldCreateUserId, UserInfo.Id);
                sqlBuilder.SetValue(BaseFolderEntity.FieldCreateBy, UserInfo.RealName);
            } 
            sqlBuilder.SetDBNow(BaseFolderEntity.FieldCreateOn);
            if (UserInfo != null) 
            { 
                sqlBuilder.SetValue(BaseFolderEntity.FieldModifiedUserId, UserInfo.Id);
                sqlBuilder.SetValue(BaseFolderEntity.FieldModifiedBy, UserInfo.RealName);
            } 
            sqlBuilder.SetDBNow(BaseFolderEntity.FieldModifiedOn);
            if (this.Identity && (DbHelper.CurrentDbType == CurrentDbType.SqlServer || DbHelper.CurrentDbType == CurrentDbType.Access))
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
        public int UpdateObject(BaseFolderEntity entity)
        {
            SQLBuilder sqlBuilder = new SQLBuilder(DbHelper);
            sqlBuilder.BeginUpdate(this.CurrentTableName);
            this.SetObject(sqlBuilder, entity);
            if (UserInfo != null) 
            { 
                sqlBuilder.SetValue(BaseFolderEntity.FieldModifiedUserId, UserInfo.Id);
                sqlBuilder.SetValue(BaseFolderEntity.FieldModifiedBy, UserInfo.RealName);
            } 
            sqlBuilder.SetDBNow(BaseFolderEntity.FieldModifiedOn);
            sqlBuilder.SetWhere(BaseFolderEntity.FieldId, entity.Id);
            return sqlBuilder.EndUpdate();
        }

        partial void SetObjectExpand(SQLBuilder sqlBuilder, BaseFolderEntity entity);

        /// <summary>
        /// 设置实体
        /// </summary>
        /// <param name="entity">实体</param>
        private void SetObject(SQLBuilder sqlBuilder, BaseFolderEntity entity)
        {
            sqlBuilder.SetValue(BaseFolderEntity.FieldParentId, entity.ParentId);
            sqlBuilder.SetValue(BaseFolderEntity.FieldFolderName, entity.FolderName);
            sqlBuilder.SetValue(BaseFolderEntity.FieldSortCode, entity.SortCode);
            sqlBuilder.SetValue(BaseFolderEntity.FieldAllowEdit, entity.AllowEdit);
            sqlBuilder.SetValue(BaseFolderEntity.FieldAllowDelete, entity.AllowDelete);
            sqlBuilder.SetValue(BaseFolderEntity.FieldIsPublic, entity.IsPublic);
            sqlBuilder.SetValue(BaseFolderEntity.FieldEnabled, entity.Enabled);
            sqlBuilder.SetValue(BaseFolderEntity.FieldDeletionStateCode, entity.DeletionStateCode);
            sqlBuilder.SetValue(BaseFolderEntity.FieldDescription, entity.Description);
            SetObjectExpand(sqlBuilder, entity);
        }

        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns>影响行数</returns>
        public int Delete(string id)
        {
            return this.Delete(new KeyValuePair<string, object>(BaseFolderEntity.FieldId, id));
        }

		protected override List<KeyValuePair<string, object>> GetDeleteExtParam(List<KeyValuePair<string, object>> parameters)
		{
			var result = base.GetDeleteExtParam(parameters);
			result.Add(new KeyValuePair<string, object>(BaseFolderEntity.FieldAllowDelete, 1));
			return result;
		}
    }
}
