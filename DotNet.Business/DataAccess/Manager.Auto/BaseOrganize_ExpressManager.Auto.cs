//-----------------------------------------------------------------------
// <copyright file="BaseOrganize_ExpressManager.Auto.cs" company="Hairihan">
//     Copyright (c) 2015 , All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System.Collections.Generic;

namespace DotNet.Business
{
    using DotNet.Model;
    using DotNet.Utilities;

    /// <summary>
    /// BaseOrganize_ExpressManager
    /// 网点基础资料扩展表
    /// 
    /// 修改纪录
    /// 
    /// 2014-11-08 版本：1.0 JiRiGaLa 创建文件。
    /// 
    /// <author>
    ///     <name>JiRiGaLa</name>
    ///     <date>2014-11-08</date>
    /// </author>
    /// </summary>
    public partial class BaseOrganize_ExpressManager : BaseManager, IBaseManager
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public BaseOrganize_ExpressManager()
        {
            if (base.dbHelper == null)
            {
                base.dbHelper = DbHelperFactory.GetHelper(BaseSystemInfo.UserCenterDbType, BaseSystemInfo.UserCenterDbConnection);
            }
            if (string.IsNullOrEmpty(base.CurrentTableName))
            {
                base.CurrentTableName = BaseOrganize_ExpressEntity.TableName;
            }
            base.PrimaryKey = "Id";
        }

        /// <summary>
        /// 构造函数
        /// <param name="tableName">指定表名</param>
        /// </summary>
        public BaseOrganize_ExpressManager(string tableName)
        {
            base.CurrentTableName = tableName;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="dbHelper">数据库连接</param>
        public BaseOrganize_ExpressManager(IDbHelper dbHelper): this()
        {
            DbHelper = dbHelper;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="userInfo">用户信息</param>
        public BaseOrganize_ExpressManager(BaseUserInfo userInfo) : this()
        {
            UserInfo = userInfo;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="userInfo">用户信息</param>
        /// <param name="tableName">指定表名</param>
        public BaseOrganize_ExpressManager(BaseUserInfo userInfo, string tableName) : this(userInfo)
        {
            base.CurrentTableName = tableName;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="dbHelper">数据库连接</param>
        /// <param name="userInfo">用户信息</param>
        public BaseOrganize_ExpressManager(IDbHelper dbHelper, BaseUserInfo userInfo) : this(dbHelper)
        {
            UserInfo = userInfo;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="dbHelper">数据库连接</param>
        /// <param name="userInfo">用户信息</param>
        /// <param name="tableName">指定表名</param>
        public BaseOrganize_ExpressManager(IDbHelper dbHelper, BaseUserInfo userInfo, string tableName) : this(dbHelper, userInfo)
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
        public string Add(BaseOrganize_ExpressEntity entity, bool identity = false, bool returnId = false)
        {
            this.Identity = identity;
            this.ReturnId = returnId;
            return this.AddObject(entity);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entity">实体</param>
        public int Update(BaseOrganize_ExpressEntity entity)
        {
            return this.UpdateObject(entity);
        }

        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="id">主键</param>
        public BaseOrganize_ExpressEntity GetObject(string id)
        {
            return BaseEntity.Create<BaseOrganize_ExpressEntity>(this.GetDataTable(new KeyValuePair<string, object>(BaseOrganize_ExpressEntity.FieldID, id)));
        }

        /// <summary>
        /// 添加实体
        /// </summary>
        /// <param name="entity">实体</param>
        public string AddObject(BaseOrganize_ExpressEntity entity)
        {
            string key = string.Empty;
            if (entity.Id != null)
            {
                key = entity.Id.ToString();
            }
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
                        BaseSequenceManager sequenceManager = new BaseSequenceManager(DbHelper);
                        entity.Id = int.Parse(sequenceManager.Increment(this.CurrentTableName));
                        sqlBuilder.SetValue(this.PrimaryKey, entity.Id);
                    }
                }
            }
            this.SetObject(sqlBuilder, entity);
            if (UserInfo != null) 
            { 
                sqlBuilder.SetValue(BaseOrganize_ExpressEntity.FieldCreateUserId, UserInfo.Id);
                sqlBuilder.SetValue(BaseOrganize_ExpressEntity.FieldCreateBy, UserInfo.RealName);
            } 
            sqlBuilder.SetDBNow(BaseOrganize_ExpressEntity.FieldCreateOn);
            if (UserInfo != null) 
            { 
                sqlBuilder.SetValue(BaseOrganize_ExpressEntity.FieldModifiedUserId, UserInfo.Id);
                sqlBuilder.SetValue(BaseOrganize_ExpressEntity.FieldModifiedBy, UserInfo.RealName);
            } 
            sqlBuilder.SetDBNow(BaseOrganize_ExpressEntity.FieldModifiedOn);
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
        public int UpdateObject(BaseOrganize_ExpressEntity entity)
        {
            SQLBuilder sqlBuilder = new SQLBuilder(DbHelper);
            sqlBuilder.BeginUpdate(this.CurrentTableName);
            this.SetObject(sqlBuilder, entity);
            if (UserInfo != null) 
            { 
                sqlBuilder.SetValue(BaseOrganize_ExpressEntity.FieldModifiedUserId, UserInfo.Id);
                sqlBuilder.SetValue(BaseOrganize_ExpressEntity.FieldModifiedBy, UserInfo.RealName);
            } 
            sqlBuilder.SetDBNow(BaseOrganize_ExpressEntity.FieldModifiedOn);
            sqlBuilder.SetWhere(this.PrimaryKey, entity.Id);
            return sqlBuilder.EndUpdate();
        }

        // 这个是声明扩展方法
        partial void SetObjectExpand(SQLBuilder sqlBuilder, BaseOrganize_ExpressEntity entity);
        
        /// <summary>
        /// 设置实体
        /// </summary>
        /// <param name="entity">实体</param>
        private void SetObject(SQLBuilder sqlBuilder, BaseOrganize_ExpressEntity entity)
        {
            SetObjectExpand(sqlBuilder, entity);
            sqlBuilder.SetValue(BaseOrganize_ExpressEntity.FieldNot_Dispatch_Range, entity.Not_Dispatch_Range);
            sqlBuilder.SetValue(BaseOrganize_ExpressEntity.FieldScan_Select, entity.Scan_Select);
            sqlBuilder.SetValue(BaseOrganize_ExpressEntity.FieldSite_Prior, entity.Site_Prior);
            sqlBuilder.SetValue(BaseOrganize_ExpressEntity.FieldPrivate_Remark, entity.Private_Remark);
            sqlBuilder.SetValue(BaseOrganize_ExpressEntity.FieldPublic_Remark, entity.Public_Remark);
            sqlBuilder.SetValue(BaseOrganize_ExpressEntity.FieldDispatch_Time_Limit, entity.Dispatch_Time_Limit);
            sqlBuilder.SetValue(BaseOrganize_ExpressEntity.FieldAgent_Money_Limited, entity.Agent_Money_Limited);
            sqlBuilder.SetValue(BaseOrganize_ExpressEntity.FieldDefault_Send_Place, entity.Default_Send_Place);
            sqlBuilder.SetValue(BaseOrganize_ExpressEntity.FieldDispatch_Range, entity.Dispatch_Range);
            sqlBuilder.SetValue(BaseOrganize_ExpressEntity.FieldAllow_ToPayment, entity.Allow_ToPayment);
            sqlBuilder.SetValue(BaseOrganize_ExpressEntity.FieldAllow_Agent_Money, entity.Allow_Agent_Money);
            sqlBuilder.SetValue(BaseOrganize_ExpressEntity.FieldDispatch_OutRange_Fee, entity.Dispatch_OutRange_Fee);
            sqlBuilder.SetValue(BaseOrganize_ExpressEntity.FieldCurrency, entity.Currency);
            sqlBuilder.SetValue(BaseOrganize_ExpressEntity.FieldDispatch_Range_Fee, entity.Dispatch_Range_Fee);
            sqlBuilder.SetValue(BaseOrganize_ExpressEntity.FieldDispatch_Money_Desc, entity.Dispatch_Money_Desc);
            sqlBuilder.SetValue(BaseOrganize_ExpressEntity.FieldWebSiteName, entity.WebSiteName);
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
