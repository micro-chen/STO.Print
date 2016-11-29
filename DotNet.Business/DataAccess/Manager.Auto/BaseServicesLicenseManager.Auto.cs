//-----------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2016 , Hairihan TECH, Ltd.  
//-----------------------------------------------------------------

using System.Collections.Generic;

namespace DotNet.Business
{
    using DotNet.Model;
    using DotNet.Utilities;

	/// <summary>
    /// BaseServicesLicenseManager
	/// 参数类
	/// 
	/// 修改记录
	///		2015.12.25 版本：1.0 JiRiGaLa	创建。
	///
	/// <author>
	///		<name>JiRiGaLa</name>
    ///		<date>2015.12.25</date>
	/// </author> 
	/// </summary>
    public partial class BaseServicesLicenseManager : BaseManager
	{
		public BaseServicesLicenseManager()
		{
			if (base.dbHelper == null)
			{
				base.dbHelper = DbHelperFactory.GetHelper(BaseSystemInfo.UserCenterDbType, BaseSystemInfo.UserCenterDbConnection);
			}
			base.CurrentTableName = BaseServicesLicenseEntity.TableName;
		}

		public BaseServicesLicenseManager(IDbHelper dbHelper) : this()
		{
			DbHelper = dbHelper;
		}

		public BaseServicesLicenseManager(BaseUserInfo userInfo) : this()
		{
			UserInfo = userInfo;
		}

		public BaseServicesLicenseManager(IDbHelper dbHelper, BaseUserInfo userInfo) : this()
		{
			DbHelper = dbHelper;
			UserInfo = userInfo;
		}

        public BaseServicesLicenseManager(IDbHelper dbHelper, string tableName)
            : this()
        {
            DbHelper = dbHelper;
            this.CurrentTableName = tableName;
        }

        public BaseServicesLicenseManager(IDbHelper dbHelper, BaseUserInfo userInfo, string tableName)
            : this()
        {
            DbHelper = dbHelper;
            UserInfo = userInfo;
            this.CurrentTableName = tableName;
        }

        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="id">主键</param>
        public BaseServicesLicenseEntity GetObject(string id)
        {
            return BaseEntity.Create<BaseServicesLicenseEntity>(this.ExecuteReader(new KeyValuePair<string, object>(BaseServicesLicenseEntity.FieldId, id)));
        }
        
        /// <summary>
        /// 添加实体
        /// </summary>
        /// <param name="entity">实体</param>
        public string AddObject(BaseServicesLicenseEntity entity)
        {
            string result = string.Empty;
            SQLBuilder sqlBuilder = new SQLBuilder(DbHelper, this.Identity, this.ReturnId);
            sqlBuilder.BeginInsert(this.CurrentTableName, BaseServicesLicenseEntity.FieldId);
            if (!string.IsNullOrEmpty(entity.Id) || !this.Identity)
            {
                result = entity.Id;
                sqlBuilder.SetValue(BaseServicesLicenseEntity.FieldId, entity.Id);
            }
            else
            {
                if (!this.ReturnId && (DbHelper.CurrentDbType == CurrentDbType.Oracle || DbHelper.CurrentDbType == CurrentDbType.DB2))
                {
                    if (DbHelper.CurrentDbType == CurrentDbType.Oracle)
                    {
                        sqlBuilder.SetFormula(BaseServicesLicenseEntity.FieldId, "SEQ_" + BaseServicesLicenseEntity.TableName.ToUpper() + ".NEXTVAL ");
                    }
                    if (DbHelper.CurrentDbType == CurrentDbType.DB2)
                    {
                        sqlBuilder.SetFormula(BaseServicesLicenseEntity.FieldId, "NEXT VALUE FOR SEQ_" + this.CurrentTableName.ToUpper());
                    }
                }
                else
                {
                    if (this.Identity && (DbHelper.CurrentDbType == CurrentDbType.Oracle || DbHelper.CurrentDbType == CurrentDbType.DB2))
                    {
                        if (entity.Id == null)
                        {
                            if (string.IsNullOrEmpty(result))
                            {
                                BaseSequenceManager sequenceManager = new BaseSequenceManager(DbHelper, this.Identity);
                                result = sequenceManager.Increment(BaseServicesLicenseEntity.TableName);
                            }
                            entity.Id = result;
                        }
                        sqlBuilder.SetValue(BaseServicesLicenseEntity.FieldId, entity.Id);
                    }
                }
            }
            this.SetObject(sqlBuilder, entity);

            // 创建人信息
            if (!string.IsNullOrEmpty(entity.CreateUserId))
            {
                sqlBuilder.SetValue(BaseServicesLicenseEntity.FieldCreateUserId, entity.CreateUserId);
            }
            else
            {
                if (UserInfo != null)
                {
                    sqlBuilder.SetValue(BaseServicesLicenseEntity.FieldCreateUserId, UserInfo.Id);
                }
            }
            if (!string.IsNullOrEmpty(entity.CreateBy))
            {
                sqlBuilder.SetValue(BaseServicesLicenseEntity.FieldCreateBy, entity.CreateBy);
            }
            else
            {
                if (UserInfo != null)
                {
                    sqlBuilder.SetValue(BaseServicesLicenseEntity.FieldCreateBy, UserInfo.RealName);
                }
            }
            if (entity.CreateOn.HasValue)
            {
                sqlBuilder.SetValue(BaseServicesLicenseEntity.FieldCreateOn, entity.CreateOn);
            }
            else
            {
                sqlBuilder.SetDBNow(BaseServicesLicenseEntity.FieldCreateOn);
            }

            // 修改人信息
            if (!string.IsNullOrEmpty(entity.ModifiedUserId))
            {
                sqlBuilder.SetValue(BaseServicesLicenseEntity.FieldModifiedUserId, entity.ModifiedUserId);
            }
            else
            {
                if (UserInfo != null)
                {
                    sqlBuilder.SetValue(BaseServicesLicenseEntity.FieldModifiedUserId, UserInfo.Id);
                }
            }
            if (!string.IsNullOrEmpty(entity.ModifiedBy))
            {
                sqlBuilder.SetValue(BaseServicesLicenseEntity.FieldModifiedBy, entity.ModifiedBy);
            }
            else
            {
                if (UserInfo != null)
                {
                    sqlBuilder.SetValue(BaseServicesLicenseEntity.FieldModifiedBy, UserInfo.RealName);
                }
            }
            if (entity.ModifiedOn.HasValue)
            {
                sqlBuilder.SetValue(BaseServicesLicenseEntity.FieldModifiedOn, entity.ModifiedOn);
            }
            else
            {
                sqlBuilder.SetDBNow(BaseServicesLicenseEntity.FieldModifiedOn);
            }

            if (DbHelper.CurrentDbType == CurrentDbType.SqlServer && this.Identity)
            {
                result = sqlBuilder.EndInsert().ToString();
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
        public int UpdateObject(BaseServicesLicenseEntity entity)
        {
            SQLBuilder sqlBuilder = new SQLBuilder(DbHelper);
            sqlBuilder.BeginUpdate(this.CurrentTableName);
            this.SetObject(sqlBuilder, entity);
            if (UserInfo != null)
            {
                sqlBuilder.SetValue(BaseServicesLicenseEntity.FieldModifiedUserId, UserInfo.Id);
                sqlBuilder.SetValue(BaseServicesLicenseEntity.FieldModifiedBy, UserInfo.RealName);
            }
            // 若有修改时间标示，那就按修改时间来，不是按最新的时间来
            if (entity.ModifiedOn.HasValue)
            {
                sqlBuilder.SetValue(BaseServicesLicenseEntity.FieldModifiedOn, entity.ModifiedOn.Value);
            }
            else
            {
                sqlBuilder.SetDBNow(BaseServicesLicenseEntity.FieldModifiedOn);
            }
            sqlBuilder.SetWhere(BaseServicesLicenseEntity.FieldId, entity.Id);
            return sqlBuilder.EndUpdate();
        }

        partial void SetObjectExpand(SQLBuilder sqlBuilder, BaseServicesLicenseEntity entity);

        /// <summary>
        /// 设置实体
        /// </summary>
        /// <param name="entity">实体</param>
        private void SetObject(SQLBuilder sqlBuilder, BaseServicesLicenseEntity entity)
        {
            sqlBuilder.SetValue(BaseServicesLicenseEntity.FieldUserId, entity.UserId);
            sqlBuilder.SetValue(BaseServicesLicenseEntity.FieldPrivateKey, entity.PrivateKey);
            sqlBuilder.SetValue(BaseServicesLicenseEntity.FieldPublicKey, entity.PublicKey);
            sqlBuilder.SetValue(BaseServicesLicenseEntity.FieldStartDate, entity.StartDate);
            sqlBuilder.SetValue(BaseServicesLicenseEntity.FieldEndDate, entity.EndDate);
            sqlBuilder.SetValue(BaseServicesLicenseEntity.FieldEnabled, entity.Enabled ? 1 : 0);
            sqlBuilder.SetValue(BaseServicesLicenseEntity.FieldDeletionStateCode, entity.DeletionStateCode);
            SetObjectExpand(sqlBuilder, entity);
        }

        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns>影响行数</returns>
        public int Delete(int id)
        {
            return this.Delete(new KeyValuePair<string, object>(BaseServicesLicenseEntity.FieldId, id));
        }
	}
}