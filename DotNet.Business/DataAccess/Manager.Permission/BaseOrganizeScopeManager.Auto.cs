//-----------------------------------------------------------------------
// <copyright file="BaseOrganizeScopeManager.Auto.cs" company="Hairihan">
//     Copyright (C) 2016 , All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System.Collections.Generic;

namespace DotNet.Business
{
    using DotNet.Model;
    using DotNet.Utilities;

    /// <summary>
    /// BaseOrganizeScopeManager
    /// 基于组织机构的权限范围
    /// 
    /// 修改记录
    /// 
    /// 2013-12-24 版本：1.0 JiRiGaLa 创建文件。
    /// 
    /// <author>
    ///     <name>JiRiGaLa</name>
    ///     <date>2013-12-24</date>
    /// </author>
    /// </summary>
    public partial class BaseOrganizeScopeManager : BaseManager, IBaseManager
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public BaseOrganizeScopeManager()
        {
            if (base.dbHelper == null)
            {
                base.dbHelper = DbHelperFactory.GetHelper(BaseSystemInfo.UserCenterDbType, BaseSystemInfo.UserCenterDbConnection);
            }
            if (string.IsNullOrEmpty(base.CurrentTableName))
            {
                base.CurrentTableName = BaseOrganizeScopeEntity.TableName;
            }
            base.PrimaryKey = "Id";
        }

        /// <summary>
        /// 构造函数
        /// <param name="tableName">指定表名</param>
        /// </summary>
        public BaseOrganizeScopeManager(string tableName)
        {
            base.CurrentTableName = tableName;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="dbHelper">数据库连接</param>
        public BaseOrganizeScopeManager(IDbHelper dbHelper): this()
        {
            DbHelper = dbHelper;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="userInfo">用户信息</param>
        public BaseOrganizeScopeManager(BaseUserInfo userInfo) : this()
        {
            UserInfo = userInfo;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="userInfo">用户信息</param>
        /// <param name="tableName">指定表名</param>
        public BaseOrganizeScopeManager(BaseUserInfo userInfo, string tableName) : this(userInfo)
        {
            base.CurrentTableName = tableName;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="dbHelper">数据库连接</param>
        /// <param name="userInfo">用户信息</param>
        public BaseOrganizeScopeManager(IDbHelper dbHelper, BaseUserInfo userInfo) : this(dbHelper)
        {
            UserInfo = userInfo;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="dbHelper">数据库连接</param>
        /// <param name="userInfo">用户信息</param>
        /// <param name="tableName">指定表名</param>
        public BaseOrganizeScopeManager(IDbHelper dbHelper, BaseUserInfo userInfo, string tableName) : this(dbHelper, userInfo)
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
        public string Add(BaseOrganizeScopeEntity entity, bool identity = true, bool returnId = true)
        {
            this.Identity = identity;
            this.ReturnId = returnId;
            entity.Id = int.Parse(this.AddObject(entity));
            return entity.Id.ToString();
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entity">实体</param>
        public int Update(BaseOrganizeScopeEntity entity)
        {
            return this.UpdateObject(entity);
        }

        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="id">主键</param>
        public BaseOrganizeScopeEntity GetObject(string id)
        {
            return GetObject(int.Parse(id));
        }

        public BaseOrganizeScopeEntity GetObject(int id)
        {
            return BaseEntity.Create<BaseOrganizeScopeEntity>(this.ExecuteReader(new KeyValuePair<string, object>(BaseOrganizeScopeEntity.FieldId, id)));
            // return BaseEntity.Create<BaseOrganizeScopeEntity>(this.GetDataTable(new KeyValuePair<string, object>(this.PrimaryKey, id)));
        }

        /// <summary>
        /// 添加实体
        /// </summary>
        /// <param name="entity">实体</param>
        public string AddObject(BaseOrganizeScopeEntity entity)
        {
            string key = string.Empty;
            SQLBuilder sqlBuilder = new SQLBuilder(DbHelper, this.Identity, this.ReturnId);
            sqlBuilder.BeginInsert(this.CurrentTableName, this.PrimaryKey);
            if (!this.Identity) 
            {
                // 这里已经是指定了主键了，所以不需要返回主键了
                sqlBuilder.ReturnId = false;
                sqlBuilder.SetValue(this.PrimaryKey, entity.Id);
            }
            else
            {
                if (!this.ReturnId && (DbHelper.CurrentDbType == CurrentDbType.Oracle || DbHelper.CurrentDbType == CurrentDbType.DB2))
                {
                    if (DbHelper.CurrentDbType == CurrentDbType.Oracle)
                    {
                        // 2015-09-25 吉日嘎拉 用一个序列就可以了，不用那么多序列了
                        sqlBuilder.SetFormula(this.PrimaryKey, "SEQ_" + BaseOrganizeScopeEntity.TableName.ToUpper() + ".NEXTVAL ");
                        // sqlBuilder.SetFormula(this.PrimaryKey, "SEQ_" + this.CurrentTableName.ToUpper() + ".NEXTVAL ");
                    }
                    if (DbHelper.CurrentDbType == CurrentDbType.DB2)
                    {
                        // sqlBuilder.SetFormula(this.PrimaryKey, "NEXT VALUE FOR SEQ_" + this.CurrentTableName.ToUpper());
                        sqlBuilder.SetFormula(this.PrimaryKey, "NEXT VALUE FOR SEQ_" + BaseOrganizeScopeEntity.TableName.ToUpper());
                    }
                }
                else
                {
                    if (this.Identity && (DbHelper.CurrentDbType == CurrentDbType.Oracle || DbHelper.CurrentDbType == CurrentDbType.DB2))
                    {
                        // BaseSequenceManager sequenceManager = new BaseSequenceManager(DbHelper);
                        // entity.Id = int.Parse(sequenceManager.Increment(BasePermissionEntity.TableName));
                        // entity.Id = int.Parse(sequenceManager.Increment(this.CurrentTableName));
                        // sqlBuilder.SetValue(this.PrimaryKey, entity.Id);
                        sqlBuilder.SetFormula(this.PrimaryKey, "SEQ_" + BaseOrganizeScopeEntity.TableName.ToUpper() + ".NEXTVAL ");
                    }
                }
            }
            this.SetObject(sqlBuilder, entity);
            if (UserInfo != null) 
            { 
                sqlBuilder.SetValue(BaseOrganizeScopeEntity.FieldCreateUserId, UserInfo.Id);
                sqlBuilder.SetValue(BaseOrganizeScopeEntity.FieldCreateBy, UserInfo.RealName);
            } 
            sqlBuilder.SetDBNow(BaseOrganizeScopeEntity.FieldCreateOn);
            if (UserInfo != null) 
            { 
                sqlBuilder.SetValue(BaseOrganizeScopeEntity.FieldModifiedUserId, UserInfo.Id);
                sqlBuilder.SetValue(BaseOrganizeScopeEntity.FieldModifiedBy, UserInfo.RealName);
            } 
            sqlBuilder.SetDBNow(BaseOrganizeScopeEntity.FieldModifiedOn);
            if (this.Identity && (DbHelper.CurrentDbType == CurrentDbType.SqlServer || DbHelper.CurrentDbType == CurrentDbType.Access))
            {
                key = sqlBuilder.EndInsert().ToString();
            }
            else
            {
                sqlBuilder.EndInsert();
            }
            if (this.Identity && (DbHelper.CurrentDbType == CurrentDbType.Oracle || DbHelper.CurrentDbType == CurrentDbType.DB2))
            {
                return entity.Id.ToString();
            }
            return key;
        }

        /// <summary>
        /// 更新实体
        /// </summary>
        /// <param name="entity">实体</param>
        public int UpdateObject(BaseOrganizeScopeEntity entity)
        {
            SQLBuilder sqlBuilder = new SQLBuilder(DbHelper);
            sqlBuilder.BeginUpdate(this.CurrentTableName);
            this.SetObject(sqlBuilder, entity);
            if (UserInfo != null) 
            { 
                sqlBuilder.SetValue(BaseOrganizeScopeEntity.FieldModifiedUserId, UserInfo.Id);
                sqlBuilder.SetValue(BaseOrganizeScopeEntity.FieldModifiedBy, UserInfo.RealName);
            } 
            sqlBuilder.SetDBNow(BaseOrganizeScopeEntity.FieldModifiedOn);
            sqlBuilder.SetWhere(this.PrimaryKey, entity.Id);
            return sqlBuilder.EndUpdate();
        }

        // 这个是声明扩展方法
        partial void SetObjectExpand(SQLBuilder sqlBuilder, BaseOrganizeScopeEntity entity);
        
        /// <summary>
        /// 设置实体
        /// </summary>
        /// <param name="entity">实体</param>
        private void SetObject(SQLBuilder sqlBuilder, BaseOrganizeScopeEntity entity)
        {
            SetObjectExpand(sqlBuilder, entity);
            sqlBuilder.SetValue(BaseOrganizeScopeEntity.FieldResourceCategory, entity.ResourceCategory);
            sqlBuilder.SetValue(BaseOrganizeScopeEntity.FieldResourceId, entity.ResourceId);
            sqlBuilder.SetValue(BaseOrganizeScopeEntity.FieldPermissionId, entity.PermissionId);
            sqlBuilder.SetValue(BaseOrganizeScopeEntity.FieldAllData, entity.AllData);
            sqlBuilder.SetValue(BaseOrganizeScopeEntity.FieldProvince, entity.Province);
            sqlBuilder.SetValue(BaseOrganizeScopeEntity.FieldCity, entity.City);
            sqlBuilder.SetValue(BaseOrganizeScopeEntity.FieldDistrict, entity.District);
            sqlBuilder.SetValue(BaseOrganizeScopeEntity.FieldStreet, entity.Street);
            sqlBuilder.SetValue(BaseOrganizeScopeEntity.FieldUserCompany, entity.UserCompany);
            sqlBuilder.SetValue(BaseOrganizeScopeEntity.FieldUserSubCompany, entity.UserSubCompany);
            sqlBuilder.SetValue(BaseOrganizeScopeEntity.FieldUserDepartment, entity.UserDepartment);
            sqlBuilder.SetValue(BaseOrganizeScopeEntity.FieldUserSubDepartment, entity.UserSubDepartment);
            sqlBuilder.SetValue(BaseOrganizeScopeEntity.FieldUserWorkgroup, entity.UserWorkgroup);
            sqlBuilder.SetValue(BaseOrganizeScopeEntity.FieldOnlyOwnData, entity.OnlyOwnData);
            sqlBuilder.SetValue(BaseOrganizeScopeEntity.FieldNotAllowed, entity.NotAllowed);
            sqlBuilder.SetValue(BaseOrganizeScopeEntity.FieldByDetails, entity.ByDetails);
            sqlBuilder.SetValue(BaseOrganizeScopeEntity.FieldContainChild, entity.ContainChild);
            sqlBuilder.SetValue(BaseOrganizeScopeEntity.FieldEnabled, entity.Enabled);
            sqlBuilder.SetValue(BaseOrganizeScopeEntity.FieldDeletionStateCode, entity.DeletionStateCode);
            sqlBuilder.SetValue(BaseOrganizeScopeEntity.FieldDescription, entity.Description);
        }

        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns>影响行数</returns>
        public int Delete(int id)
        {
            return this.Delete(new KeyValuePair<string, object>(this.PrimaryKey, id));
        }
    }
}
