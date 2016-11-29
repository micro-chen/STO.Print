//-----------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2016 , Hairihan TECH, Ltd. 
//-----------------------------------------------------------------

using System.Collections.Generic;

namespace DotNet.Business
{
    using DotNet.Model;
    using DotNet.Utilities;

    /// <summary>
    /// BaseDepartmentManager
    /// 部门表
    ///
    /// 修改记录
    ///
    ///		2014-12-08 版本：1.0 JiRiGaLa 创建主键。
    ///
    /// <author>
    ///		<name>JiRiGaLa</name>
    ///		<date>2014-12-08</date>
    /// </author>
    /// </summary>
    public partial class BaseDepartmentManager : BaseManager, IBaseManager
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public BaseDepartmentManager()
        {
            if (base.dbHelper == null)
            {
                base.dbHelper = DbHelperFactory.GetHelper(BaseSystemInfo.UserCenterDbType, BaseSystemInfo.UserCenterDbConnection);
            }
            if (string.IsNullOrEmpty(base.CurrentTableName))
            {
                base.CurrentTableName = BaseDepartmentEntity.TableName;
            }
        }

        /// <summary>
        /// 构造函数
        /// <param name="tableName">指定表名</param>
        /// </summary>
        public BaseDepartmentManager(string tableName) : this()
        {
            base.CurrentTableName = tableName;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="dbHelper">数据库连接</param>
        public BaseDepartmentManager(IDbHelper dbHelper)
            : this()
        {
            DbHelper = dbHelper;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="userInfo">用户信息</param>
        public BaseDepartmentManager(BaseUserInfo userInfo)
            : this()
        {
            UserInfo = userInfo;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="dbHelper">数据库连接</param>
        /// <param name="userInfo">用户信息</param>
        public BaseDepartmentManager(IDbHelper dbHelper, BaseUserInfo userInfo)
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
        public BaseDepartmentManager(IDbHelper dbHelper, BaseUserInfo userInfo, string tableName)
            : this(dbHelper, userInfo)
        {
            base.CurrentTableName = tableName;
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns>主键</returns>
        public string Add(BaseDepartmentEntity entity)
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
        public string Add(BaseDepartmentEntity entity, bool identity, bool returnId)
        {
            this.Identity = identity;
            this.ReturnId = returnId;
            return this.AddObject(entity);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entity">实体</param>
        public int Update(BaseDepartmentEntity entity)
        {
            return this.UpdateObject(entity);
        }

        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="id">主键</param>
        public BaseDepartmentEntity GetObject(int? id)
        {
            return GetObject(id.ToString());
        }

        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="id">主键</param>
        public BaseDepartmentEntity GetObject(string id)
        {
            return BaseEntity.Create<BaseDepartmentEntity>(this.ExecuteReader(new KeyValuePair<string, object>(BaseDepartmentEntity.FieldId, id)));
            // return BaseEntity.Create<BaseDepartmentEntity>(this.GetDataTable(new KeyValuePair<string, object>(BaseDepartmentEntity.FieldId, id)));
        }

        /// <summary>
        /// 添加实体
        /// </summary>
        /// <param name="entity">实体</param>
        public string AddObject(BaseDepartmentEntity entity)
        {
            string sequence = string.Empty;
            if (!entity.SortCode.HasValue)
            {
                BaseSequenceManager sequenceManager = new BaseSequenceManager(DbHelper, this.Identity);
                sequence = sequenceManager.Increment(this.CurrentTableName);
                entity.SortCode = int.Parse(sequence);
            }
            SQLBuilder sqlBuilder = new SQLBuilder(DbHelper, this.Identity, this.ReturnId);
            sqlBuilder.BeginInsert(this.CurrentTableName, BaseDepartmentEntity.FieldId);
            if (entity.Id.HasValue || !this.Identity)
            {
                sqlBuilder.SetValue(BaseDepartmentEntity.FieldId, entity.Id);
            }
            else
            {
                if (!this.ReturnId && (DbHelper.CurrentDbType == CurrentDbType.Oracle || DbHelper.CurrentDbType == CurrentDbType.DB2))
                {
                    if (DbHelper.CurrentDbType == CurrentDbType.Oracle)
                    {
                        sqlBuilder.SetFormula(BaseDepartmentEntity.FieldId, "SEQ_" + BaseDepartmentEntity.TableName.ToUpper() + ".NEXTVAL ");
                    }
                    if (DbHelper.CurrentDbType == CurrentDbType.DB2)
                    {
                        sqlBuilder.SetFormula(BaseDepartmentEntity.FieldId, "NEXT VALUE FOR SEQ_" + BaseDepartmentEntity.TableName.ToUpper());
                    }
                }
                else
                {
                    if (this.Identity && (DbHelper.CurrentDbType == CurrentDbType.Oracle || DbHelper.CurrentDbType == CurrentDbType.DB2))
                    {
                        if (entity.Id == null)
                        {
                            if (string.IsNullOrEmpty(sequence))
                            {
                                BaseSequenceManager sequenceManager = new BaseSequenceManager(DbHelper, this.Identity);
                                sequence = sequenceManager.Increment(BaseDepartmentEntity.TableName);
                            }
                            entity.Id = int.Parse(sequence);
                        }
                        sqlBuilder.SetValue(BaseDepartmentEntity.FieldId, entity.Id);
                    }
                }
            }
            this.SetObject(sqlBuilder, entity);

            // 创建人信息
            if (!string.IsNullOrEmpty(entity.CreateUserId))
            {
                sqlBuilder.SetValue(BaseDepartmentEntity.FieldCreateUserId, entity.CreateUserId);
            }
            else
            {
                if (UserInfo != null)
                {
                    sqlBuilder.SetValue(BaseDepartmentEntity.FieldCreateUserId, UserInfo.Id);
                }
            }
            if (!string.IsNullOrEmpty(entity.CreateBy))
            {
                sqlBuilder.SetValue(BaseDepartmentEntity.FieldCreateBy, entity.CreateBy);
            }
            else
            {
                if (UserInfo != null)
                {
                    sqlBuilder.SetValue(BaseDepartmentEntity.FieldCreateBy, UserInfo.RealName);
                }
            }
            if (entity.CreateOn.HasValue)
            {
                sqlBuilder.SetValue(BaseDepartmentEntity.FieldCreateOn, entity.CreateOn);
            }
            else
            {
                sqlBuilder.SetDBNow(BaseDepartmentEntity.FieldCreateOn);
            }

            // 修改人信息
            if (!string.IsNullOrEmpty(entity.ModifiedUserId))
            {
                sqlBuilder.SetValue(BaseDepartmentEntity.FieldModifiedUserId, entity.ModifiedUserId);
            }
            else
            {
                if (UserInfo != null)
                {
                    sqlBuilder.SetValue(BaseDepartmentEntity.FieldModifiedUserId, UserInfo.Id);
                }
            }
            if (!string.IsNullOrEmpty(entity.ModifiedBy))
            {
                sqlBuilder.SetValue(BaseDepartmentEntity.FieldModifiedBy, entity.ModifiedBy);
            }
            else
            {
                if (UserInfo != null)
                {
                    sqlBuilder.SetValue(BaseDepartmentEntity.FieldModifiedBy, UserInfo.RealName);
                }
            }
            if (entity.ModifiedOn.HasValue)
            {
                sqlBuilder.SetValue(BaseDepartmentEntity.FieldModifiedOn, entity.ModifiedOn);
            }
            else
            {
                sqlBuilder.SetDBNow(BaseDepartmentEntity.FieldModifiedOn);
            }

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
        public int UpdateObject(BaseDepartmentEntity entity)
        {
            SQLBuilder sqlBuilder = new SQLBuilder(DbHelper);
            sqlBuilder.BeginUpdate(this.CurrentTableName);
            this.SetObject(sqlBuilder, entity);
            if (UserInfo != null)
            {
                sqlBuilder.SetValue(BaseDepartmentEntity.FieldModifiedUserId, UserInfo.Id);
                sqlBuilder.SetValue(BaseDepartmentEntity.FieldModifiedBy, UserInfo.RealName);
            }
            // 若有修改时间标示，那就按修改时间来，不是按最新的时间来
            if (entity.ModifiedOn.HasValue)
            {
                sqlBuilder.SetValue(BaseDepartmentEntity.FieldModifiedOn, entity.ModifiedOn.Value);
            }
            else
            {
                sqlBuilder.SetDBNow(BaseDepartmentEntity.FieldModifiedOn);
            }
            sqlBuilder.SetWhere(BaseDepartmentEntity.FieldId, entity.Id);
            return sqlBuilder.EndUpdate();
        }

        partial void SetObjectExpand(SQLBuilder sqlBuilder, BaseDepartmentEntity entity);

        /// <summary>
        /// 设置实体
        /// </summary>
        /// <param name="entity">实体</param>
        private void SetObject(SQLBuilder sqlBuilder, BaseDepartmentEntity entity)
        {
            sqlBuilder.SetValue(BaseDepartmentEntity.FieldParentId, entity.ParentId);
            sqlBuilder.SetValue(BaseDepartmentEntity.FieldCode, entity.Code);
            sqlBuilder.SetValue(BaseDepartmentEntity.FieldShortName, entity.ShortName);
            sqlBuilder.SetValue(BaseDepartmentEntity.FieldFullName, entity.FullName);
            sqlBuilder.SetValue(BaseDepartmentEntity.FieldCategoryCode, entity.CategoryCode);
            sqlBuilder.SetValue(BaseDepartmentEntity.FieldOuterPhone, entity.OuterPhone);
            sqlBuilder.SetValue(BaseDepartmentEntity.FieldInnerPhone, entity.InnerPhone);
            sqlBuilder.SetValue(BaseDepartmentEntity.FieldFax, entity.Fax);
            sqlBuilder.SetValue(BaseDepartmentEntity.FieldDeletionStateCode, entity.DeletionStateCode);
            sqlBuilder.SetValue(BaseDepartmentEntity.FieldEnabled, entity.Enabled);
            sqlBuilder.SetValue(BaseDepartmentEntity.FieldSortCode, entity.SortCode);
            sqlBuilder.SetValue(BaseDepartmentEntity.FieldDescription, entity.Description);
            sqlBuilder.SetValue(BaseDepartmentEntity.FieldParentName, entity.ParentName);
            sqlBuilder.SetValue(BaseDepartmentEntity.FieldCompanyId, entity.CompanyId);
            sqlBuilder.SetValue(BaseDepartmentEntity.FieldCompanyName, entity.CompanyName);
            sqlBuilder.SetValue(BaseDepartmentEntity.FieldCompanyCode, entity.CompanyCode);
            sqlBuilder.SetValue(BaseDepartmentEntity.FieldParentCode, entity.ParentCode);
            sqlBuilder.SetValue(BaseDepartmentEntity.FieldCategoryName, entity.CategoryName);
            sqlBuilder.SetValue(BaseDepartmentEntity.FieldManagerId, entity.ManagerId);
            sqlBuilder.SetValue(BaseDepartmentEntity.FieldManager, entity.Manager);
            sqlBuilder.SetValue(BaseDepartmentEntity.FieldManagerQQ, entity.ManagerQQ);
            sqlBuilder.SetValue(BaseDepartmentEntity.FieldManagerMobile, entity.ManagerMobile);
            SetObjectExpand(sqlBuilder, entity);
        }

        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns>影响行数</returns>
        public int Delete(int id)
        {
            return this.Delete(new KeyValuePair<string, object>(BaseDepartmentEntity.FieldId, id));
        }
    }
}
