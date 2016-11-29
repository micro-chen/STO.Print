//-----------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2016 , Hairihan TECH, Ltd. 
//-----------------------------------------------------------------

using System.Collections.Generic;

namespace DotNet.Business
{
    using DotNet.Model;
    using DotNet.Utilities;

    /// <summary>
    /// BaseUserRoleManager
    /// 用户-角色 关系
    ///
    /// 修改记录
    ///
    ///		2016-03-02 版本：2.0 JiRiGaLa 角色用户关联关系、方便下拉属于自己公司的数据。
    ///		2010-07-15 版本：1.0 JiRiGaLa 创建主键。
    ///
    /// <author>
    ///		<name>JiRiGaLa</name>
    ///		<date>2016-03-02</date>
    /// </author>
    /// </summary>
    public partial class BaseUserRoleManager : BaseManager, IBaseManager
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public BaseUserRoleManager()
        {
            if (base.dbHelper == null)
            {
                base.dbHelper = DbHelperFactory.GetHelper(BaseSystemInfo.UserCenterDbType, BaseSystemInfo.UserCenterDbConnection);
            }
            base.CurrentTableName = BaseUserRoleEntity.TableName;
        }

        /// <summary>
        /// 构造函数
        /// <param name="tableName">指定表名</param>
        /// </summary>
        public BaseUserRoleManager(string tableName)
        {
            base.CurrentTableName = tableName;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="dbHelper">数据库连接</param>
        public BaseUserRoleManager(IDbHelper dbHelper)
            : this()
        {
            DbHelper = dbHelper;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="userInfo">用户信息</param>
        public BaseUserRoleManager(BaseUserInfo userInfo)
            : this()
        {
            UserInfo = userInfo;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="dbHelper">数据库连接</param>
        /// <param name="userInfo">用户信息</param>
        public BaseUserRoleManager(IDbHelper dbHelper, BaseUserInfo userInfo)
            : this(dbHelper)
        {
            UserInfo = userInfo;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="userInfo">用户信息</param>
        /// <param name="tableName">指定表名</param>
        public BaseUserRoleManager(BaseUserInfo userInfo, string tableName)
            : this(userInfo)
        {
            base.CurrentTableName = tableName;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="dbHelper">数据库连接</param>
        /// <param name="userInfo">用户信息</param>
        /// <param name="tableName">指定表名</param>
        public BaseUserRoleManager(IDbHelper dbHelper, BaseUserInfo userInfo, string tableName)
            : this(dbHelper, userInfo)
        {
            base.CurrentTableName = tableName;
        }

        /// <summary>
        /// 添加(判断数据是否重复，防止垃圾数据产生)
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns>主键</returns>
        public string Add(BaseUserRoleEntity entity)
        {
            string result = string.Empty;

            // 判断是否数据重复
            List<KeyValuePair<string, object>> whereParameters = new List<KeyValuePair<string, object>>();
            // parameters.Add(new KeyValuePair<string, object>(BaseUserRoleEntity.FieldEnabled, 1));
            whereParameters.Add(new KeyValuePair<string, object>(BaseUserRoleEntity.FieldDeletionStateCode, 0));
            whereParameters.Add(new KeyValuePair<string, object>(BaseUserRoleEntity.FieldRoleId, entity.RoleId));
            whereParameters.Add(new KeyValuePair<string, object>(BaseUserRoleEntity.FieldUserId, entity.UserId));
            if (!this.Exists(whereParameters))
            {
                result = this.AddObject(entity);
            }
            else
            {
                // 2015-12-04 吉日嘎拉 这里有严重错误，重复申请就会变成自己审核了
                List<KeyValuePair<string, object>> parameters = new List<KeyValuePair<string, object>>();
                if (this.UserInfo != null)
                {
                    parameters.Add(new KeyValuePair<string, object>(BaseUserRoleEntity.FieldModifiedUserId, this.UserInfo.Id));
                    parameters.Add(new KeyValuePair<string, object>(BaseUserRoleEntity.FieldModifiedBy, this.UserInfo.RealName));
                    parameters.Add(new KeyValuePair<string, object>(BaseUserRoleEntity.FieldModifiedOn, System.DateTime.Now));
                }
                else
                {
                    parameters.Add(new KeyValuePair<string, object>(BaseUserRoleEntity.FieldModifiedUserId, entity.ModifiedUserId));
                    parameters.Add(new KeyValuePair<string, object>(BaseUserRoleEntity.FieldModifiedBy, entity.ModifiedBy));
                    parameters.Add(new KeyValuePair<string, object>(BaseUserRoleEntity.FieldModifiedOn, System.DateTime.Now));
                }
                parameters.Add(new KeyValuePair<string, object>(BaseUserRoleEntity.FieldEnabled, entity.Enabled));
                this.SetProperty(whereParameters, parameters);
            }

            return result;
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="identity">自增量方式</param>
        /// <param name="returnId">返回主鍵</param>
        /// <returns>主键</returns>
        public string Add(BaseUserRoleEntity entity, bool identity, bool returnId)
        {
            this.Identity = identity;
            this.ReturnId = returnId;
            return this.AddObject(entity);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entity">实体</param>
        public int Update(BaseUserRoleEntity entity)
        {
            return this.UpdateObject(entity);
        }

        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="id">主键</param>
        public BaseUserRoleEntity GetObject(int id)
        {
            return BaseEntity.Create<BaseUserRoleEntity>(this.ExecuteReader(new KeyValuePair<string, object>(BaseUserRoleEntity.FieldId, id)));
        }

        /// <summary>
        /// 添加实体
        /// </summary>
        /// <param name="entity">实体</param>
        public string AddObject(BaseUserRoleEntity entity)
        {
            string result = string.Empty;

            SQLBuilder sqlBuilder = new SQLBuilder(DbHelper, this.Identity, this.ReturnId);
            sqlBuilder.BeginInsert(this.CurrentTableName, BaseUserRoleEntity.FieldId);
            if (!this.Identity)
            {
                sqlBuilder.SetValue(BaseUserRoleEntity.FieldId, entity.Id);
            }
            else
            {
                if (!this.ReturnId && (DbHelper.CurrentDbType == CurrentDbType.Oracle || DbHelper.CurrentDbType == CurrentDbType.DB2))
                {
                    if (DbHelper.CurrentDbType == CurrentDbType.Oracle)
                    {
                        // 2015-09-25 吉日嘎拉 用一个序列就可以了，不用那么多序列了
                        // sqlBuilder.SetFormula(BaseUserRoleEntity.FieldId, "SEQ_" + this.CurrentTableName.ToUpper() + ".NEXTVAL ");
                        sqlBuilder.SetFormula(BaseUserRoleEntity.FieldId, "SEQ_" + BaseRoleEntity.TableName.ToUpper() + ".NEXTVAL ");
                    }
                    if (DbHelper.CurrentDbType == CurrentDbType.DB2)
                    {
                        // sqlBuilder.SetFormula(BaseUserRoleEntity.FieldId, "NEXT VALUE FOR SEQ_" + this.CurrentTableName.ToUpper());
                        sqlBuilder.SetFormula(BaseUserRoleEntity.FieldId, "NEXT VALUE FOR SEQ_" + BaseRoleEntity.TableName.ToUpper());
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
                                result = sequenceManager.Increment(BaseRoleEntity.TableName);
                            }
                            entity.Id = int.Parse(result);
                        }
                        sqlBuilder.SetValue(BaseUserRoleEntity.FieldId, entity.Id);
                    }
                }
            }
            this.SetObject(sqlBuilder, entity);
            if (UserInfo != null)
            {
                sqlBuilder.SetValue(BaseUserRoleEntity.FieldCreateUserId, UserInfo.Id);
                sqlBuilder.SetValue(BaseUserRoleEntity.FieldCreateBy, UserInfo.RealName);
            }
            sqlBuilder.SetDBNow(BaseUserRoleEntity.FieldCreateOn);
            if (UserInfo != null)
            {
                sqlBuilder.SetValue(BaseUserRoleEntity.FieldModifiedUserId, UserInfo.Id);
                sqlBuilder.SetValue(BaseUserRoleEntity.FieldModifiedBy, UserInfo.RealName);
            }
            sqlBuilder.SetDBNow(BaseUserRoleEntity.FieldModifiedOn);
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
        public int UpdateObject(BaseUserRoleEntity entity)
        {
            SQLBuilder sqlBuilder = new SQLBuilder(DbHelper);
            sqlBuilder.BeginUpdate(this.CurrentTableName);
            this.SetObject(sqlBuilder, entity);
            if (UserInfo != null)
            {
                sqlBuilder.SetValue(BaseUserRoleEntity.FieldModifiedUserId, UserInfo.Id);
                sqlBuilder.SetValue(BaseUserRoleEntity.FieldModifiedBy, UserInfo.RealName);
            }
            sqlBuilder.SetDBNow(BaseUserRoleEntity.FieldModifiedOn);
            sqlBuilder.SetWhere(BaseUserRoleEntity.FieldId, entity.Id);
            return sqlBuilder.EndUpdate();
        }

        partial void SetObjectExpand(SQLBuilder sqlBuilder, BaseUserRoleEntity entity);

        /// <summary>
        /// 设置实体
        /// </summary>
        /// <param name="entity">实体</param>
        private void SetObject(SQLBuilder sqlBuilder, BaseUserRoleEntity entity)
        {
            sqlBuilder.SetValue(BaseUserRoleEntity.FieldUserId, entity.UserId);
            sqlBuilder.SetValue(BaseUserRoleEntity.FieldRoleId, entity.RoleId);
            if (!string.IsNullOrEmpty(entity.CompanyId))
            {
                sqlBuilder.SetValue(BaseUserRoleEntity.FieldCompanyId, entity.CompanyId);
            }
            sqlBuilder.SetValue(BaseUserRoleEntity.FieldEnabled, entity.Enabled);
            sqlBuilder.SetValue(BaseUserRoleEntity.FieldDescription, entity.Description);
            sqlBuilder.SetValue(BaseUserRoleEntity.FieldDeletionStateCode, entity.DeletionStateCode);
            SetObjectExpand(sqlBuilder, entity);
        }

        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns>影响行数</returns>
        public int Delete(int id)
        {
            return this.Delete(new KeyValuePair<string, object>(BaseUserRoleEntity.FieldId, id));
        }
    }
}
