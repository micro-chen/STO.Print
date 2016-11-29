//-----------------------------------------------------------------------
// <copyright file="TAB_USERManager.Auto.cs" company="Hairihan">
//     Copyright (c) 2013 , All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System.Collections.Generic;

namespace DotNet.Business
{
    using DotNet.Model;
    using DotNet.Utilities;

    /// <summary>
    /// TAB_USERManager
    /// 用户
    /// 
    /// 修改纪录
    /// 
    /// 2013-11-23 版本：1.0 SongBiao 创建文件。
    /// 
    /// <author>
    ///     <name>SongBiao</name>
    ///     <date>2013-11-23</date>
    /// </author>
    /// </summary>
    public partial class TAB_USERManager : BaseManager, IBaseManager
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TAB_USERManager()
        {
            if (base.dbHelper == null)
            {
                base.dbHelper = DbHelperFactory.GetHelper(BaseSystemInfo.BusinessDbType, BaseSystemInfo.BusinessDbConnection);
            }
            if (string.IsNullOrEmpty(base.CurrentTableName))
            {
                base.CurrentTableName = TAB_USEREntity.TableName;
            }
            base.PrimaryKey = "ID";
        }

        /// <summary>
        /// 构造函数
        /// <param name="tableName">指定表名</param>
        /// </summary>
        public TAB_USERManager(string tableName)
        {
            base.CurrentTableName = tableName;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="dbHelper">数据库连接</param>
        public TAB_USERManager(IDbHelper dbHelper): this()
        {
            DbHelper = dbHelper;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="userInfo">用户信息</param>
        public TAB_USERManager(BaseUserInfo userInfo) : this()
        {
            UserInfo = userInfo;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="userInfo">用户信息</param>
        /// <param name="tableName">指定表名</param>
        public TAB_USERManager(BaseUserInfo userInfo, string tableName) : this(userInfo)
        {
            base.CurrentTableName = tableName;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="dbHelper">数据库连接</param>
        /// <param name="userInfo">用户信息</param>
        public TAB_USERManager(IDbHelper dbHelper, BaseUserInfo userInfo) : this(dbHelper)
        {
            UserInfo = userInfo;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="dbHelper">数据库连接</param>
        /// <param name="userInfo">用户信息</param>
        /// <param name="tableName">指定表名</param>
        public TAB_USERManager(IDbHelper dbHelper, BaseUserInfo userInfo, string tableName) : this(dbHelper, userInfo)
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
        public string Add(TAB_USEREntity entity, bool identity = false, bool returnId = false)
        {
            this.Identity = identity;
            this.ReturnId = returnId;
            return this.AddObject(entity);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entity">实体</param>
        public int Update(TAB_USEREntity entity)
        {
            return this.UpdateObject(entity);
        }

        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="id">主键</param>
        public TAB_USEREntity GetObject(string id)
        {
            return BaseEntity.Create<TAB_USEREntity>(this.ExecuteReader(new KeyValuePair<string, object>(this.PrimaryKey, id)));
            // return BaseEntity.Create<TAB_USEREntity>(this.GetDataTable(new KeyValuePair<string, object>(this.PrimaryKey, id)));
        }

        /// <summary>
        /// 添加实体
        /// </summary>
        /// <param name="entity">实体</param>
        public string AddObject(TAB_USEREntity entity)
        {
            string key = string.Empty;
            key = entity.ID.ToString();
            SQLBuilder sqlBuilder = new SQLBuilder(DbHelper, this.Identity, this.ReturnId);
            sqlBuilder.BeginInsert(this.CurrentTableName, this.PrimaryKey);
            if (!this.Identity) 
            {
                // 这里已经是指定了主键了，所以不需要返回主键了
                sqlBuilder.ReturnId = false;
                sqlBuilder.SetValue(this.PrimaryKey, entity.ID);
            }
            else
            {
                if (!this.ReturnId && (DbHelper.CurrentDbType == CurrentDbType.Oracle || DbHelper.CurrentDbType == CurrentDbType.DB2))
                {
                    if (DbHelper.CurrentDbType == CurrentDbType.Oracle)
                    {
                        sqlBuilder.SetFormula(this.PrimaryKey, "SEQ_" + this.CurrentTableName.ToUpper() + ".NEXTVAL ");
                    }
                    if (DbHelper.CurrentDbType == CurrentDbType.DB2)
                    {
                        sqlBuilder.SetFormula(this.PrimaryKey, "NEXT VALUE FOR SEQ_" + this.CurrentTableName.ToUpper());
                    }
                }
                else
                {
                    if (this.Identity && (DbHelper.CurrentDbType == CurrentDbType.Oracle || DbHelper.CurrentDbType == CurrentDbType.DB2))
                    {
                        BaseSequenceManager sequenceManager = new BaseSequenceManager();
                        entity.ID = int.Parse(sequenceManager.Increment(this.CurrentTableName));
                        sqlBuilder.SetValue(this.PrimaryKey, entity.ID);
                    }
                }
            }
            this.SetObject(sqlBuilder, entity);
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
                return entity.ID.ToString();
            }
            return key;
        }

        /// <summary>
        /// 更新实体
        /// </summary>
        /// <param name="entity">实体</param>
        public int UpdateObject(TAB_USEREntity entity)
        {
            SQLBuilder sqlBuilder = new SQLBuilder(DbHelper);
            sqlBuilder.BeginUpdate(this.CurrentTableName);
            this.SetObject(sqlBuilder, entity);
            sqlBuilder.SetWhere(this.PrimaryKey, entity.ID);
            return sqlBuilder.EndUpdate();
        }

        // 这个是声明扩展方法
        partial void SetObjectExpand(SQLBuilder sqlBuilder, TAB_USEREntity entity);
        
        /// <summary>
        /// 设置实体
        /// </summary>
        /// <param name="entity">实体</param>
        private void SetObject(SQLBuilder sqlBuilder, TAB_USEREntity entity)
        {
            SetObjectExpand(sqlBuilder, entity);
            sqlBuilder.SetValue(TAB_USEREntity.Field_COMPUT_ID, entity.COMPUT_ID);
            sqlBuilder.SetValue(TAB_USEREntity.Field_EMPLOYEE_CODE, entity.EMPLOYEE_CODE);
            sqlBuilder.SetValue(TAB_USEREntity.Field_OWNER_ROLE, entity.OWNER_ROLE);
            sqlBuilder.SetValue(TAB_USEREntity.Field_BL_REC_PLACARD, entity.BL_REC_PLACARD);
            sqlBuilder.SetValue(TAB_USEREntity.Field_USER_SITE, entity.USER_SITE);
            sqlBuilder.SetValue(TAB_USEREntity.Field_ROLEID, entity.ROLEID);
            sqlBuilder.SetValue(TAB_USEREntity.Field_MODIFIER, entity.MODIFIER);
            sqlBuilder.SetValue(TAB_USEREntity.Field_USER_WEBPOWER, entity.USER_WEBPOWER);
            sqlBuilder.SetValue(TAB_USEREntity.Field_OWNER_CENTER, entity.OWNER_CENTER);
            sqlBuilder.SetValue(TAB_USEREntity.Field_MODIFIER_CODE, entity.MODIFIER_CODE);
            sqlBuilder.SetValue(TAB_USEREntity.Field_CREATE_SITE, entity.CREATE_SITE);
            sqlBuilder.SetValue(TAB_USEREntity.Field_IM_NAME, entity.IM_NAME);
            sqlBuilder.SetValue(TAB_USEREntity.Field_USER_PART, entity.USER_PART);
            sqlBuilder.SetValue(TAB_USEREntity.Field_BL_CHECK_COMPUTER, entity.BL_CHECK_COMPUTER);
            sqlBuilder.SetValue(TAB_USEREntity.Field_DEPT_NAME, entity.DEPT_NAME);
            //sqlBuilder.SetValue(TAB_USEREntity.Field_USER_PASSWORD, entity.USER_PASSWORD);
            sqlBuilder.SetValue(TAB_USEREntity.Field_USER_PASSWD, entity.USER_PASSWD);
            sqlBuilder.SetValue(TAB_USEREntity.Field_CALL_CENTER_ID, entity.CALL_CENTER_ID);
            sqlBuilder.SetValue(TAB_USEREntity.Field_EMPLOYEE_NAME, entity.EMPLOYEE_NAME);
            sqlBuilder.SetValue(TAB_USEREntity.Field_DELETIONSTATECODE, entity.DELETIONSTATECODE);
            sqlBuilder.SetValue(TAB_USEREntity.Field_OWNER_GROUP, entity.OWNER_GROUP);
            sqlBuilder.SetValue(TAB_USEREntity.Field_MODIFY_SITE, entity.MODIFY_SITE);
            sqlBuilder.SetValue(TAB_USEREntity.Field_BL_LOCK_FLAG, entity.BL_LOCK_FLAG);
            sqlBuilder.SetValue(TAB_USEREntity.Field_OWNER_SITE, entity.OWNER_SITE);
            sqlBuilder.SetValue(TAB_USEREntity.Field_CREATE_DATE, entity.CREATE_DATE);
            sqlBuilder.SetValue(TAB_USEREntity.Field_CREATE_USER, entity.CREATE_USER);
            sqlBuilder.SetValue(TAB_USEREntity.Field_USER_NAME, entity.USER_NAME);
            sqlBuilder.SetValue(TAB_USEREntity.Field_REMARK, entity.REMARK);
            sqlBuilder.SetValue(TAB_USEREntity.Field_BL_AO, entity.BL_AO);
            sqlBuilder.SetValue(TAB_USEREntity.Field_BL_TYPE, entity.BL_TYPE);
            sqlBuilder.SetValue(TAB_USEREntity.Field_LOGINNAME, entity.LOGINNAME);
            sqlBuilder.SetValue(TAB_USEREntity.Field_MOBILE, entity.MOBILE);
            sqlBuilder.SetValue(TAB_USEREntity.Field_ID_CARD, entity.ID_CARD);
            sqlBuilder.SetValue(TAB_USEREntity.Field_ONLY_USER_NAME, entity.ONLY_USER_NAME);

            sqlBuilder.SetValue(TAB_USEREntity.Field_BL_CHECK_NAME, entity.BL_CHECK_NAME);
            sqlBuilder.SetValue(TAB_USEREntity.Field_CHECK_NAME_DATE, entity.CHECK_NAME_DATE);
            sqlBuilder.SetValue(TAB_USEREntity.Field_POSITION_NAME, entity.POSITION_NAME);
            sqlBuilder.SetValue(TAB_USEREntity.Field_REAL_NAME, entity.REAL_NAME);
            if (entity.USER_DATE.ToShortDateString()!="0001-01-01")
            sqlBuilder.SetValue(TAB_USEREntity.Field_USER_DATE, entity.USER_DATE);
        }

        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns>影响行数</returns>
        public int Delete(string id)
        {
            return this.Delete(new KeyValuePair<string, object>(this.PrimaryKey, id));
        }
    }
}
