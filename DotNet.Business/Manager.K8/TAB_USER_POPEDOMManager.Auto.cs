//-----------------------------------------------------------------------
// <copyright file="TAB_USERPOPEDOMManager.Auto.cs" company="Hairihan">
//     Copyright (c) 2015 , All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System.Collections.Generic;

namespace DotNet.Business
{
    using DotNet.Model;
    using DotNet.Utilities;

    /// <summary>
    /// TAB_USERPOPEDOMManager
    /// 用户权限
    /// 
    /// 修改纪录
    /// 
    /// 2014-09-01 版本：1.0 SongBiao 创建文件。
    /// 
    /// <author>
    ///     <name>SongBiao</name>
    ///     <date>2014-09-01</date>
    /// </author>
    /// </summary>
    public partial class TAB_USERPOPEDOMManager : BaseManager, IBaseManager
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TAB_USERPOPEDOMManager()
        {
            if (base.dbHelper == null)
            {
                base.dbHelper = DbHelperFactory.GetHelper(BaseSystemInfo.BusinessDbType, BaseSystemInfo.BusinessDbConnection);
            }
            if (string.IsNullOrEmpty(base.CurrentTableName))
            {
                base.CurrentTableName = TAB_USERPOPEDOMEntity.TableName;
            }
            base.PrimaryKey = "MENU_GUID";
        }

        /// <summary>
        /// 构造函数
        /// <param name="tableName">指定表名</param>
        /// </summary>
        public TAB_USERPOPEDOMManager(string tableName)
        {
            base.CurrentTableName = tableName;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="dbHelper">数据库连接</param>
        public TAB_USERPOPEDOMManager(IDbHelper dbHelper): this()
        {
            DbHelper = dbHelper;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="userInfo">用户信息</param>
        public TAB_USERPOPEDOMManager(BaseUserInfo userInfo) : this()
        {
            UserInfo = userInfo;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="userInfo">用户信息</param>
        /// <param name="tableName">指定表名</param>
        public TAB_USERPOPEDOMManager(BaseUserInfo userInfo, string tableName) : this(userInfo)
        {
            base.CurrentTableName = tableName;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="dbHelper">数据库连接</param>
        /// <param name="userInfo">用户信息</param>
        public TAB_USERPOPEDOMManager(IDbHelper dbHelper, BaseUserInfo userInfo) : this(dbHelper)
        {
            UserInfo = userInfo;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="dbHelper">数据库连接</param>
        /// <param name="userInfo">用户信息</param>
        /// <param name="tableName">指定表名</param>
        public TAB_USERPOPEDOMManager(IDbHelper dbHelper, BaseUserInfo userInfo, string tableName) : this(dbHelper, userInfo)
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
        public string Add(TAB_USERPOPEDOMEntity entity, bool identity = false, bool returnId = false)
        {
            this.Identity = identity;
            this.ReturnId = returnId;
            return this.AddObject(entity);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entity">实体</param>
        public int Update(TAB_USERPOPEDOMEntity entity)
        {
            return this.UpdateObject(entity);
        }

        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="id">主键</param>
        public TAB_USERPOPEDOMEntity GetObject(string id)
        {
            return BaseEntity.Create<TAB_USERPOPEDOMEntity>(this.ExecuteReader(new KeyValuePair<string, object>(this.PrimaryKey, id)));
            // return BaseEntity.Create<TAB_USERPOPEDOMEntity>(this.GetDataTable(new KeyValuePair<string, object>(this.PrimaryKey, id)));
        }

        /// <summary>
        /// 添加实体
        /// </summary>
        /// <param name="entity">实体</param>
        public string AddObject(TAB_USERPOPEDOMEntity entity)
        {
            string key = string.Empty;
            if (entity.MENU_GUID != null)
            {
                key = entity.MENU_GUID.ToString();
            }
            SQLBuilder sqlBuilder = new SQLBuilder(DbHelper, this.Identity, this.ReturnId);
            sqlBuilder.BeginInsert(this.CurrentTableName, this.PrimaryKey);
            if (!this.Identity) 
            {
                // 这里已经是指定了主键了，所以不需要返回主键了
                sqlBuilder.ReturnId = false;
                sqlBuilder.SetValue(this.PrimaryKey, entity.MENU_GUID);
            }
            else
            {
                //if (!this.ReturnId && (DbHelper.CurrentDbType == CurrentDbType.Oracle || DbHelper.CurrentDbType == CurrentDbType.DB2))
                //{
                //    if (DbHelper.CurrentDbType == CurrentDbType.Oracle)
                //    {
                //        sqlBuilder.SetFormula(this.PrimaryKey, "SEQ_" + this.CurrentTableName.ToUpper() + ".NEXTVAL ");
                //    }
                //    if (DbHelper.CurrentDbType == CurrentDbType.DB2)
                //    {
                //        sqlBuilder.SetFormula(this.PrimaryKey, "NEXT VALUE FOR SEQ_" + this.CurrentTableName.ToUpper());
                //    }
                //}
                //else
                //{
                //    if (this.Identity && (DbHelper.CurrentDbType == CurrentDbType.Oracle || DbHelper.CurrentDbType == CurrentDbType.DB2))
                //    {
                //        BaseSequenceManager sequenceManager = new BaseSequenceManager(DbHelper);
                //        entity.Id = int.Parse(sequenceManager.Increment(this.CurrentTableName));
                //        sqlBuilder.SetValue(this.PrimaryKey, entity.MENU_GUID);
                //    }
                //}
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
            //if (this.Identity && (DbHelper.CurrentDbType == CurrentDbType.Oracle || DbHelper.CurrentDbType == CurrentDbType.DB2))
            //{
            //    return entity.Id.ToString();
            //}
            return key;
        }

        /// <summary>
        /// 更新实体
        /// </summary>
        /// <param name="entity">实体</param>
        public int UpdateObject(TAB_USERPOPEDOMEntity entity)
        {
            SQLBuilder sqlBuilder = new SQLBuilder(DbHelper);
            sqlBuilder.BeginUpdate(this.CurrentTableName);
            this.SetObject(sqlBuilder, entity);
            sqlBuilder.SetWhere(this.PrimaryKey, entity.MENU_GUID);
            return sqlBuilder.EndUpdate();
        }

        // 这个是声明扩展方法
        partial void SetObjectExpand(SQLBuilder sqlBuilder, TAB_USERPOPEDOMEntity entity);
        
        /// <summary>
        /// 设置实体
        /// </summary>
        /// <param name="entity">实体</param>
        private void SetObject(SQLBuilder sqlBuilder, TAB_USERPOPEDOMEntity entity)
        {
            SetObjectExpand(sqlBuilder, entity);
            sqlBuilder.SetValue(TAB_USERPOPEDOMEntity.FieldOWNER_SITE, entity.OWNER_SITE);
            sqlBuilder.SetValue(TAB_USERPOPEDOMEntity.FieldUSER_NAME, entity.USER_NAME);
            sqlBuilder.SetValue(TAB_USERPOPEDOMEntity.FieldBL_DELETE, entity.BL_DELETE);
            sqlBuilder.SetValue(TAB_USERPOPEDOMEntity.FieldBL_UPDATE, entity.BL_UPDATE);
            sqlBuilder.SetValue(TAB_USERPOPEDOMEntity.FieldBL_INSERT, entity.BL_INSERT);
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
