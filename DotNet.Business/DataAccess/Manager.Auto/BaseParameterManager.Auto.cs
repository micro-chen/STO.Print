//-----------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2016 , Hairihan TECH, Ltd.  
//-----------------------------------------------------------------

using System.Collections.Generic;

namespace DotNet.Business
{
    using DotNet.Model;
    using DotNet.Utilities;

	/// <summary>
	/// BaseParameterManager
	/// 参数类
	/// 
	/// 修改记录
	///     2011.04.05 版本：2.2 zgl        修改AddObject 为public 方法，ip限制功能中使用
	///     2009.04.01 版本：2.1 JiRiGaLa   创建者、修改者进行完善。
	///     2008.04.30 版本：2.0 JiRiGaLa   按面向对象，面向服务进行改进。
	///     2007.06.08 版本：1.4 JiRiGaLa   重新调整方法。
	///		2006.02.05 版本：1.3 JiRiGaLa	重新调整主键的规范化。
	///		2006.01.28 版本：1.2 JiRiGaLa	对一些方法进行改进，主键整理，调用性能也进行了修改，主键顺序进行整理。
	///		2005.08.13 版本：1.1 JiRiGaLa	主键整理好。
	///		2004.11.12 版本：1.0 JiRiGaLa	主键进行了绝对的优化，这是个好东西啊，平时要多用，用得要灵活些。
	///
	/// <author>
	///		<name>JiRiGaLa</name>
	///		<date>2008.04.30</date>
	/// </author> 
	/// </summary>
    public partial class BaseParameterManager : BaseManager
	{
		public BaseParameterManager()
		{
			if (base.dbHelper == null)
			{
				base.dbHelper = DbHelperFactory.GetHelper(BaseSystemInfo.UserCenterDbType, BaseSystemInfo.UserCenterDbConnection);
			}
			base.CurrentTableName = BaseParameterEntity.TableName;
		}

		public BaseParameterManager(IDbHelper dbHelper) : this()
		{
			DbHelper = dbHelper;
		}

		public BaseParameterManager(BaseUserInfo userInfo) : this()
		{
			UserInfo = userInfo;
		}

		public BaseParameterManager(IDbHelper dbHelper, BaseUserInfo userInfo) : this()
		{
			DbHelper = dbHelper;
			UserInfo = userInfo;
		}

        public BaseParameterManager(IDbHelper dbHelper, string tableName)
            : this()
        {
            DbHelper = dbHelper;
            this.CurrentTableName = tableName;
        }

        public BaseParameterManager(IDbHelper dbHelper, BaseUserInfo userInfo, string tableName)
            : this()
        {
            DbHelper = dbHelper;
            UserInfo = userInfo;
            this.CurrentTableName = tableName;
        }

        public BaseParameterManager(string tableName)
            : this()
        {
            this.CurrentTableName = tableName;
        }

        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="id">主键</param>
        public BaseParameterEntity GetObject(string id)
        {
            return BaseEntity.Create<BaseParameterEntity>(this.ExecuteReader(new KeyValuePair<string, object>(BaseParameterEntity.FieldId, id)));
            // return BaseEntity.Create<BaseParameterEntity>(this.GetDataTable(new KeyValuePair<string, object>(BaseParameterEntity.FieldId, id)));
        }
        
        /// <summary>
        /// 添加实体
        /// </summary>
        /// <param name="entity">实体</param>
        public string AddObject(BaseParameterEntity entity)
        {
            string result = string.Empty;
            //if (!entity.SortCode.HasValue)
            //{
            //    BaseSequenceManager sequenceManager = new BaseSequenceManager(DbHelper, this.Identity);
            //    result = sequenceManager.Increment(this.CurrentTableName);
            //    entity.SortCode = int.Parse(result);
            //}
            SQLBuilder sqlBuilder = new SQLBuilder(DbHelper, this.Identity, this.ReturnId);
            sqlBuilder.BeginInsert(this.CurrentTableName, BaseParameterEntity.FieldId);
            if (!string.IsNullOrEmpty(entity.Id) || !this.Identity)
            {
                result = entity.Id;
                sqlBuilder.SetValue(BaseParameterEntity.FieldId, entity.Id);
            }
            else
            {
                if (!this.ReturnId && (DbHelper.CurrentDbType == CurrentDbType.Oracle || DbHelper.CurrentDbType == CurrentDbType.DB2))
                {
                    if (DbHelper.CurrentDbType == CurrentDbType.Oracle)
                    {
                        sqlBuilder.SetFormula(BaseParameterEntity.FieldId, "SEQ_" + BaseParameterEntity.TableName.ToUpper() + ".NEXTVAL ");
                        // sqlBuilder.SetFormula(BaseParameterEntity.FieldId, "SEQ_" + this.CurrentTableName.ToUpper() + ".NEXTVAL ");
                    }
                    if (DbHelper.CurrentDbType == CurrentDbType.DB2)
                    {
                        sqlBuilder.SetFormula(BaseParameterEntity.FieldId, "NEXT VALUE FOR SEQ_" + this.CurrentTableName.ToUpper());
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
                                result = sequenceManager.Increment(BaseParameterEntity.TableName);
                                // result = sequenceManager.Increment(this.CurrentTableName);
                            }
                            entity.Id = result;
                        }
                        sqlBuilder.SetValue(BaseParameterEntity.FieldId, entity.Id);
                    }
                }
            }
            this.SetObject(sqlBuilder, entity);

            // 创建人信息
            if (!string.IsNullOrEmpty(entity.CreateUserId))
            {
                sqlBuilder.SetValue(BaseParameterEntity.FieldCreateUserId, entity.CreateUserId);
            }
            else
            {
                if (UserInfo != null)
                {
                    sqlBuilder.SetValue(BaseParameterEntity.FieldCreateUserId, UserInfo.Id);
                }
            }
            if (!string.IsNullOrEmpty(entity.CreateBy))
            {
                sqlBuilder.SetValue(BaseParameterEntity.FieldCreateBy, entity.CreateBy);
            }
            else
            {
                if (UserInfo != null)
                {
                    sqlBuilder.SetValue(BaseParameterEntity.FieldCreateBy, UserInfo.RealName);
                }
            }
            if (entity.CreateOn.HasValue)
            {
                sqlBuilder.SetValue(BaseParameterEntity.FieldCreateOn, entity.CreateOn);
            }
            else
            {
                sqlBuilder.SetDBNow(BaseParameterEntity.FieldCreateOn);
            }

            // 修改人信息
            if (!string.IsNullOrEmpty(entity.ModifiedUserId))
            {
                sqlBuilder.SetValue(BaseParameterEntity.FieldModifiedUserId, entity.ModifiedUserId);
            }
            else
            {
                if (UserInfo != null)
                {
                    sqlBuilder.SetValue(BaseParameterEntity.FieldModifiedUserId, UserInfo.Id);
                }
            }
            if (!string.IsNullOrEmpty(entity.ModifiedBy))
            {
                sqlBuilder.SetValue(BaseParameterEntity.FieldModifiedBy, entity.ModifiedBy);
            }
            else
            {
                if (UserInfo != null)
                {
                    sqlBuilder.SetValue(BaseParameterEntity.FieldModifiedBy, UserInfo.RealName);
                }
            }
            if (entity.ModifiedOn.HasValue)
            {
                sqlBuilder.SetValue(BaseParameterEntity.FieldModifiedOn, entity.ModifiedOn);
            }
            else
            {
                sqlBuilder.SetDBNow(BaseParameterEntity.FieldModifiedOn);
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
        public int UpdateObject(BaseParameterEntity entity)
        {
            SQLBuilder sqlBuilder = new SQLBuilder(DbHelper);
            sqlBuilder.BeginUpdate(this.CurrentTableName);
            this.SetObject(sqlBuilder, entity);
            if (UserInfo != null)
            {
                sqlBuilder.SetValue(BaseParameterEntity.FieldModifiedUserId, UserInfo.Id);
                sqlBuilder.SetValue(BaseParameterEntity.FieldModifiedBy, UserInfo.RealName);
            }
            // 若有修改时间标示，那就按修改时间来，不是按最新的时间来
            if (entity.ModifiedOn.HasValue)
            {
                sqlBuilder.SetValue(BaseParameterEntity.FieldModifiedOn, entity.ModifiedOn.Value);
            }
            else
            {
                sqlBuilder.SetDBNow(BaseParameterEntity.FieldModifiedOn);
            }
            sqlBuilder.SetWhere(BaseParameterEntity.FieldId, entity.Id);
            return sqlBuilder.EndUpdate();
        }

        partial void SetObjectExpand(SQLBuilder sqlBuilder, BaseParameterEntity entity);

        /// <summary>
        /// 设置实体
        /// </summary>
        /// <param name="entity">实体</param>
        private void SetObject(SQLBuilder sqlBuilder, BaseParameterEntity entity)
        {
            sqlBuilder.SetValue(BaseParameterEntity.FieldCategoryCode, entity.CategoryCode);
            sqlBuilder.SetValue(BaseParameterEntity.FieldParameterCode, entity.ParameterCode);
            sqlBuilder.SetValue(BaseParameterEntity.FieldParameterId, entity.ParameterId);
            sqlBuilder.SetValue(BaseParameterEntity.FieldParameterContent, entity.ParameterContent);
            sqlBuilder.SetValue(BaseParameterEntity.FieldWorked, entity.Worked ? 1 : 0);
            sqlBuilder.SetValue(BaseParameterEntity.FieldEnabled, entity.Enabled ? 1 : 0);
            sqlBuilder.SetValue(BaseParameterEntity.FieldDeletionStateCode, entity.DeletionStateCode);
            SetObjectExpand(sqlBuilder, entity);
        }

        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns>影响行数</returns>
        public int Delete(int id)
        {
            return this.Delete(new KeyValuePair<string, object>(BaseParameterEntity.FieldId, id));
        }
	}
}