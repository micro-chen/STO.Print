//-----------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2016 , Hairihan TECH, Ltd. 
//-----------------------------------------------------------------

using System.Collections.Generic;

namespace DotNet.Business
{
    using DotNet.Model;
    using DotNet.Utilities;

    /// <summary>
    /// BasePermissionManager
    /// 操作权限表
    ///
    /// 修改记录
    ///
    ///		2010-07-15 版本：1.0 JiRiGaLa 创建主键。
    ///
    /// <author>
    ///		<name>JiRiGaLa</name>
    ///		<date>2010-07-15</date>
    /// </author>
    /// </summary>
    public partial class BasePermissionManager : BaseManager, IBaseManager
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public BasePermissionManager()
        {
            if (base.dbHelper == null)
            {
                base.dbHelper = DbHelperFactory.GetHelper(BaseSystemInfo.UserCenterDbType, BaseSystemInfo.UserCenterDbConnection);
            }
            base.CurrentTableName = BasePermissionEntity.TableName;
        }

        /// <summary>
        /// 构造函数
        /// <param name="tableName">指定表名</param>
        /// </summary>
        public BasePermissionManager(string tableName)
        {
            base.CurrentTableName = tableName;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="dbHelper">数据库连接</param>
        public BasePermissionManager(IDbHelper dbHelper)
            : this()
        {
            DbHelper = dbHelper;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="userInfo">用户信息</param>
        public BasePermissionManager(BaseUserInfo userInfo)
            : this()
        {
            UserInfo = userInfo;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="dbHelper">数据库连接</param>
        /// <param name="userInfo">用户信息</param>
        public BasePermissionManager(IDbHelper dbHelper, BaseUserInfo userInfo)
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
        public BasePermissionManager(IDbHelper dbHelper, BaseUserInfo userInfo, string tableName)
            : this(dbHelper, userInfo)
        {
            base.CurrentTableName = tableName;
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns>主键</returns>
        public string Add(BasePermissionEntity entity)
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
        public string Add(BasePermissionEntity entity, bool identity, bool returnId)
        {
            this.Identity = identity;
            this.ReturnId = returnId;
            return this.AddObject(entity);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entity">实体</param>
        public int Update(BasePermissionEntity entity)
        {
            return this.UpdateObject(entity);
        }

        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="id">主键</param>
        public BasePermissionEntity GetObject(int id)
        {
            return BaseEntity.Create<BasePermissionEntity>(this.ExecuteReader(new KeyValuePair<string, object>(BasePermissionEntity.FieldId, id)));
            // return BaseEntity.Create<BasePermissionEntity>(this.GetDataTable(new KeyValuePair<string, object>(BasePermissionEntity.FieldId, id)));
        }

        /// <summary>
        /// 添加实体
        /// </summary>
        /// <param name="entity">实体</param>
        public string AddObject(BasePermissionEntity entity)
        {
            string result = string.Empty;

            SQLBuilder sqlBuilder = new SQLBuilder(DbHelper, this.Identity, this.ReturnId);
            sqlBuilder.BeginInsert(this.CurrentTableName, BasePermissionEntity.FieldId);
            if (!this.Identity)
            {
                sqlBuilder.SetValue(BasePermissionEntity.FieldId, entity.Id);
            }
            else
            {
                if (!this.ReturnId && (DbHelper.CurrentDbType == CurrentDbType.Oracle || DbHelper.CurrentDbType == CurrentDbType.DB2))
                {
                    if (DbHelper.CurrentDbType == CurrentDbType.Oracle)
                    {
                        // 2015-09-25 吉日嘎拉 用一个序列就可以了，不用那么多序列了
                        sqlBuilder.SetFormula(BasePermissionEntity.FieldId, "SEQ_" + BaseParameterEntity.TableName.ToUpper() + ".NEXTVAL ");
                        // sqlBuilder.SetFormula(BasePermissionEntity.FieldId, "SEQ_" + this.CurrentTableName.ToUpper() + ".NEXTVAL ");
                    }
                    if (DbHelper.CurrentDbType == CurrentDbType.DB2)
                    {
                        // sqlBuilder.SetFormula(BasePermissionEntity.FieldId, "NEXT VALUE FOR SEQ_" + this.CurrentTableName.ToUpper());
                        sqlBuilder.SetFormula(BasePermissionEntity.FieldId, "NEXT VALUE FOR SEQ_" + BaseParameterEntity.TableName.ToUpper());
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
                                result = sequenceManager.Increment(this.CurrentTableName);
                            }
                            entity.Id = int.Parse(result);
                        }
                        sqlBuilder.SetValue(BasePermissionEntity.FieldId, entity.Id);
                    }
                }
            }
            this.SetObject(sqlBuilder, entity);
            if (UserInfo != null)
            {
                sqlBuilder.SetValue(BasePermissionEntity.FieldCreateUserId, UserInfo.Id);
                sqlBuilder.SetValue(BasePermissionEntity.FieldCreateBy, UserInfo.RealName);
            }
            sqlBuilder.SetDBNow(BasePermissionEntity.FieldCreateOn);
            if (UserInfo != null)
            {
                sqlBuilder.SetValue(BasePermissionEntity.FieldModifiedUserId, UserInfo.Id);
                sqlBuilder.SetValue(BasePermissionEntity.FieldModifiedBy, UserInfo.RealName);
            }
            sqlBuilder.SetDBNow(BasePermissionEntity.FieldModifiedOn);
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
        public int UpdateObject(BasePermissionEntity entity)
        {
            SQLBuilder sqlBuilder = new SQLBuilder(DbHelper);
            sqlBuilder.BeginUpdate(this.CurrentTableName);
            this.SetObject(sqlBuilder, entity);
            if (UserInfo != null)
            {
                sqlBuilder.SetValue(BasePermissionEntity.FieldModifiedUserId, UserInfo.Id);
                sqlBuilder.SetValue(BasePermissionEntity.FieldModifiedBy, UserInfo.RealName);
            }
            sqlBuilder.SetDBNow(BasePermissionEntity.FieldModifiedOn);
            sqlBuilder.SetWhere(BasePermissionEntity.FieldId, entity.Id);
            return sqlBuilder.EndUpdate();
        }

        partial void SetObjectExpand(SQLBuilder sqlBuilder, BasePermissionEntity entity);

        /// <summary>
        /// 设置实体
        /// </summary>
        /// <param name="entity">实体</param>
        private void SetObject(SQLBuilder sqlBuilder, BasePermissionEntity entity)
        {
            sqlBuilder.SetValue(BasePermissionEntity.FieldResourceId, entity.ResourceId);
            sqlBuilder.SetValue(BasePermissionEntity.FieldResourceCategory, entity.ResourceCategory);
            sqlBuilder.SetValue(BasePermissionEntity.FieldPermissionId, entity.PermissionId);
            sqlBuilder.SetValue(BasePermissionEntity.FieldPermissionConstraint, entity.PermissionConstraint);
            sqlBuilder.SetValue(BasePermissionEntity.FieldCompanyId, entity.CompanyId);
            sqlBuilder.SetValue(BasePermissionEntity.FieldCompanyName, entity.CompanyName);
            sqlBuilder.SetValue(BasePermissionEntity.FieldEnabled, entity.Enabled);
            sqlBuilder.SetValue(BasePermissionEntity.FieldDeletionStateCode, entity.DeletionStateCode);
            sqlBuilder.SetValue(BasePermissionEntity.FieldDescription, entity.Description);
            SetObjectExpand(sqlBuilder, entity);
        }

        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns>影响行数</returns>
        public int Delete(int id)
        {
            return this.Delete(new KeyValuePair<string, object>(BasePermissionEntity.FieldId, id));
        }        
    }
}
