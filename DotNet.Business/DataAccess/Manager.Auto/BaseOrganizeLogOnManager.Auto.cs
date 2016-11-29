//-----------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2016 , Hairihan TECH, Ltd. 
//-----------------------------------------------------------------

using System.Collections.Generic;

namespace DotNet.Business
{
    using DotNet.Model;
    using DotNet.Utilities;

    /// <summary>
    /// BaseOrganizeLogOnManager
    /// 系统网点登录信息
    ///
    /// 修改记录
    ///
    ///		2016-03-24 版本：1.0 JiRiGaLa 创建主键。
    ///
    /// <author>
    ///		<name>JiRiGaLa</name>
    ///		<date>2016-03-24</date>
    /// </author>
    /// </summary>
    public partial class BaseOrganizeLogOnManager : BaseManager, IBaseManager
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public BaseOrganizeLogOnManager()
        {
            if (base.dbHelper == null)
            {
                base.dbHelper = DbHelperFactory.GetHelper(BaseSystemInfo.UserCenterDbType, BaseSystemInfo.UserCenterDbConnection);
            }
            if (string.IsNullOrEmpty(base.CurrentTableName))
            {
                base.CurrentTableName = BaseOrganizeLogOnEntity.TableName;
            }
            // 不是自增量添加
            this.Identity = false;
        }

        /// <summary>
        /// 构造函数
        /// <param name="tableName">指定表名</param>
        /// </summary>
        public BaseOrganizeLogOnManager(string tableName)
            : this()
        {
            base.CurrentTableName = tableName;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="dbHelper">数据库连接</param>
        public BaseOrganizeLogOnManager(IDbHelper dbHelper)
            : this()
        {
            DbHelper = dbHelper;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="userInfo">用户信息</param>
        public BaseOrganizeLogOnManager(BaseUserInfo userInfo)
            : this()
        {
            UserInfo = userInfo;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="dbHelper">数据库连接</param>
        /// <param name="userInfo">用户信息</param>
        public BaseOrganizeLogOnManager(IDbHelper dbHelper, BaseUserInfo userInfo)
            : this(dbHelper)
        {
            UserInfo = userInfo;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="userInfo">用户信息</param>
        /// <param name="tableName">指定表名</param>
        public BaseOrganizeLogOnManager(BaseUserInfo userInfo, string tableName)
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
        public BaseOrganizeLogOnManager(IDbHelper dbHelper, BaseUserInfo userInfo, string tableName)
            : this(dbHelper, userInfo)
        {
            base.CurrentTableName = tableName;
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns>主键</returns>
        public string Add(BaseOrganizeLogOnEntity entity)
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
        public string Add(BaseOrganizeLogOnEntity entity, bool identity, bool returnId)
        {
            this.Identity = identity;
            this.ReturnId = returnId;
            return this.AddObject(entity);
        }

        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="id">主键</param>
        public BaseOrganizeLogOnEntity GetObject(int? id)
        {
            return BaseEntity.Create<BaseOrganizeLogOnEntity>(this.ExecuteReader(new KeyValuePair<string, object>(BaseOrganizeLogOnEntity.FieldId, id)));
        }

        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="id">主键</param>
        public BaseOrganizeLogOnEntity GetObject(string id)
        {
            return BaseEntity.Create<BaseOrganizeLogOnEntity>(this.ExecuteReader(new KeyValuePair<string, object>(BaseOrganizeLogOnEntity.FieldId, id)));
        }

        /// <summary>
        /// 添加实体
        /// </summary>
        /// <param name="entity">实体</param>
        public string AddObject(BaseOrganizeLogOnEntity entity)
        {
            string result = string.Empty;
            if (string.IsNullOrEmpty(entity.Id))
            {
                BaseSequenceManager manager = new BaseSequenceManager(DbHelper, this.Identity);
                result = manager.Increment(this.CurrentTableName);
                entity.Id = result;
            }
            SQLBuilder sqlBuilder = new SQLBuilder(DbHelper, this.Identity, this.ReturnId);
            sqlBuilder.BeginInsert(this.CurrentTableName, BaseOrganizeLogOnEntity.FieldId);
            if (!this.Identity)
            {
                sqlBuilder.SetValue(BaseOrganizeLogOnEntity.FieldId, entity.Id);
            }
            else
            {
                if (!this.ReturnId && (DbHelper.CurrentDbType == CurrentDbType.Oracle || DbHelper.CurrentDbType == CurrentDbType.DB2))
                {
                    if (DbHelper.CurrentDbType == CurrentDbType.Oracle)
                    {
                        sqlBuilder.SetFormula(BaseOrganizeLogOnEntity.FieldId, "SEQ_" + this.CurrentTableName.ToUpper() + ".NEXTVAL ");
                    }
                    if (DbHelper.CurrentDbType == CurrentDbType.DB2)
                    {
                        sqlBuilder.SetFormula(BaseOrganizeLogOnEntity.FieldId, "NEXT VALUE FOR SEQ_" + this.CurrentTableName.ToUpper());
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
                            entity.Id = result;
                        }
                        sqlBuilder.SetValue(BaseOrganizeLogOnEntity.FieldId, entity.Id);
                    }
                }
            }
            this.SetObject(sqlBuilder, entity);
            sqlBuilder.SetDBNow(BaseOrganizeLogOnEntity.FieldModifiedOn);
            if (DbHelper.CurrentDbType == CurrentDbType.SqlServer && this.Identity)
            {
                result = sqlBuilder.EndInsert().ToString();
            }
            else
            {
                sqlBuilder.EndInsert();
                result = entity.Id;
            }
            return result;
        }

        /// <summary>
        /// 更新实体
        /// </summary>
        /// <param name="entity">实体</param>
        public int UpdateObject(BaseOrganizeLogOnEntity entity)
        {
            SQLBuilder sqlBuilder = new SQLBuilder(DbHelper);
            sqlBuilder.BeginUpdate(this.CurrentTableName);
            this.SetObject(sqlBuilder, entity);
            sqlBuilder.SetDBNow(BaseOrganizeLogOnEntity.FieldModifiedOn);
            sqlBuilder.SetWhere(BaseOrganizeLogOnEntity.FieldId, entity.Id);
            return sqlBuilder.EndUpdate();
        }

        /// <summary>
        /// 设置实体
        /// </summary>
        /// <param name="entity">实体</param>
        private void SetObject(SQLBuilder sqlBuilder, BaseOrganizeLogOnEntity entity)
        {
            sqlBuilder.SetValue(BaseOrganizeLogOnEntity.FieldAgree, entity.Agree);
            sqlBuilder.SetValue(BaseOrganizeLogOnEntity.FieldOppose, entity.Oppose);
            sqlBuilder.SetValue(BaseOrganizeLogOnEntity.FieldFirstVisit, entity.FirstVisit);
            sqlBuilder.SetValue(BaseOrganizeLogOnEntity.FieldLastVisit, entity.LastVisit);
            sqlBuilder.SetValue(BaseOrganizeLogOnEntity.FieldShowCount, entity.ShowCount);
            sqlBuilder.SetValue(BaseOrganizeLogOnEntity.FieldLogOnCount, entity.LogOnCount);
            sqlBuilder.SetValue(BaseOrganizeLogOnEntity.FieldUserOnLine, entity.UserOnLine);
            sqlBuilder.SetValue(BaseOrganizeLogOnEntity.FieldIPAddress, entity.IPAddress);
        }
    }
}
