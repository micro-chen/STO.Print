//-----------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2016 , Hairihan TECH, Ltd. 
//-----------------------------------------------------------------

using System.Collections.Generic;

namespace DotNet.Business
{
    using DotNet.Model;
    using DotNet.Utilities;

    /// <summary>
    /// BaseOrganize_ExpressManager
    /// 网点基础资料扩展表
    /// 
    /// 修改记录
    ///
    /// 2015-01-31 版本：1.1 潘齐民   添加外网启用字段。
    /// 2014-11-08 版本：1.0 JiRiGaLa 创建文件。
    /// 
    /// <author>
    ///     <name>JiRiGaLa</name>
    ///     <date>2014-11-08</date>
    /// </author>
    /// </summary>
    public partial class BaseOrganizeExpressManager : BaseManager, IBaseManager
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public BaseOrganizeExpressManager()
        {
            if (base.dbHelper == null)
            {
                base.dbHelper = DbHelperFactory.GetHelper(BaseSystemInfo.UserCenterDbType, BaseSystemInfo.UserCenterDbConnection);
            }
            if (string.IsNullOrEmpty(base.CurrentTableName))
            {
                base.CurrentTableName = BaseOrganizeExpressEntity.TableName;
            }
            base.PrimaryKey = "Id";
        }

        /// <summary>
        /// 构造函数
        /// <param name="tableName">指定表名</param>
        /// </summary>
        public BaseOrganizeExpressManager(string tableName)
        {
            base.CurrentTableName = tableName;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="dbHelper">数据库连接</param>
        public BaseOrganizeExpressManager(IDbHelper dbHelper): this()
        {
            DbHelper = dbHelper;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="userInfo">用户信息</param>
        public BaseOrganizeExpressManager(BaseUserInfo userInfo) : this()
        {
            UserInfo = userInfo;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="userInfo">用户信息</param>
        /// <param name="tableName">指定表名</param>
        public BaseOrganizeExpressManager(BaseUserInfo userInfo, string tableName) : this(userInfo)
        {
            base.CurrentTableName = tableName;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="dbHelper">数据库连接</param>
        /// <param name="userInfo">用户信息</param>
        public BaseOrganizeExpressManager(IDbHelper dbHelper, BaseUserInfo userInfo) : this(dbHelper)
        {
            UserInfo = userInfo;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="dbHelper">数据库连接</param>
        /// <param name="userInfo">用户信息</param>
        /// <param name="tableName">指定表名</param>
        public BaseOrganizeExpressManager(IDbHelper dbHelper, BaseUserInfo userInfo, string tableName) : this(dbHelper, userInfo)
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
        public string Add(BaseOrganizeExpressEntity entity, bool identity = false, bool returnId = false)
        {
            this.Identity = identity;
            this.ReturnId = returnId;
            return this.AddObject(entity);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entity">实体</param>
        public int Update(BaseOrganizeExpressEntity entity)
        {
            return this.UpdateObject(entity);
        }

        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="id">主键</param>
        public BaseOrganizeExpressEntity GetObject(string id)
        {
            return BaseEntity.Create<BaseOrganizeExpressEntity>(this.ExecuteReader(new KeyValuePair<string, object>(this.PrimaryKey, id)));
            // return BaseEntity.Create<BaseOrganizeExpressEntity>(this.GetDataTable(new KeyValuePair<string, object>(BaseOrganizeExpressEntity.FieldID, id)));
        }

        /// <summary>
        /// 添加实体
        /// </summary>
        /// <param name="entity">实体</param>
        public string AddObject(BaseOrganizeExpressEntity entity)
        {
            string key = string.Empty;
            if (entity != null)
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
                sqlBuilder.SetValue(BaseOrganizeExpressEntity.FieldCreateUserId, UserInfo.Id);
                sqlBuilder.SetValue(BaseOrganizeExpressEntity.FieldCreateBy, UserInfo.RealName);
            } 
            sqlBuilder.SetDBNow(BaseOrganizeExpressEntity.FieldCreateOn);
            if (UserInfo != null) 
            { 
                sqlBuilder.SetValue(BaseOrganizeExpressEntity.FieldModifiedUserId, UserInfo.Id);
                sqlBuilder.SetValue(BaseOrganizeExpressEntity.FieldModifiedBy, UserInfo.RealName);
            } 
            sqlBuilder.SetDBNow(BaseOrganizeExpressEntity.FieldModifiedOn);
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
        public int UpdateObject(BaseOrganizeExpressEntity entity)
        {
            SQLBuilder sqlBuilder = new SQLBuilder(DbHelper);
            sqlBuilder.BeginUpdate(this.CurrentTableName);
            this.SetObject(sqlBuilder, entity);
            if (UserInfo != null) 
            { 
                sqlBuilder.SetValue(BaseOrganizeExpressEntity.FieldModifiedUserId, UserInfo.Id);
                sqlBuilder.SetValue(BaseOrganizeExpressEntity.FieldModifiedBy, UserInfo.RealName);
            } 
            sqlBuilder.SetDBNow(BaseOrganizeExpressEntity.FieldModifiedOn);
            sqlBuilder.SetWhere(this.PrimaryKey, entity.Id);
            return sqlBuilder.EndUpdate();
        }

        // 这个是声明扩展方法
        partial void SetObjectExpand(SQLBuilder sqlBuilder, BaseOrganizeExpressEntity entity);
        
        /// <summary>
        /// 设置实体
        /// </summary>
        /// <param name="entity">实体</param>
        private void SetObject(SQLBuilder sqlBuilder, BaseOrganizeExpressEntity entity)
        {
            SetObjectExpand(sqlBuilder, entity);
            sqlBuilder.SetValue(BaseOrganizeExpressEntity.FieldNotDispatchRange, entity.NotDispatchRange);
            sqlBuilder.SetValue(BaseOrganizeExpressEntity.FieldScanSelect, entity.ScanSelect);
            sqlBuilder.SetValue(BaseOrganizeExpressEntity.FieldSitePrior, entity.SitePrior);
            sqlBuilder.SetValue(BaseOrganizeExpressEntity.FieldPrivateRemark, entity.PrivateRemark);
            sqlBuilder.SetValue(BaseOrganizeExpressEntity.FieldPublicRemark, entity.PublicRemark);
            sqlBuilder.SetValue(BaseOrganizeExpressEntity.FieldDispatchTimeLimit, entity.DispatchTimeLimit);
            sqlBuilder.SetValue(BaseOrganizeExpressEntity.FieldAgentMoneyLimited, entity.AgentMoneyLimited);
            sqlBuilder.SetValue(BaseOrganizeExpressEntity.FieldDefaultSendPlace, entity.DefaultSendPlace);
            sqlBuilder.SetValue(BaseOrganizeExpressEntity.FieldDispatchRange, entity.DispatchRange);
            sqlBuilder.SetValue(BaseOrganizeExpressEntity.FieldAllowToPayment, entity.AllowToPayment);
            sqlBuilder.SetValue(BaseOrganizeExpressEntity.FieldAllowAgentMoney, entity.AllowAgentMoney);
            sqlBuilder.SetValue(BaseOrganizeExpressEntity.FieldDispatchOutRangeFee, entity.DispatchOutRangeFee);
            sqlBuilder.SetValue(BaseOrganizeExpressEntity.FieldCurrency, entity.Currency);
            sqlBuilder.SetValue(BaseOrganizeExpressEntity.FieldDispatchRangeFee, entity.DispatchRangeFee);
            sqlBuilder.SetValue(BaseOrganizeExpressEntity.FieldDispatchMoneyDesc, entity.DispatchMoneyDesc);
            sqlBuilder.SetValue(BaseOrganizeExpressEntity.FieldWebSiteName, entity.WebSiteName);
            sqlBuilder.SetValue(BaseOrganizeExpressEntity.FieldWebEnabled, entity.WebEnabled);
            sqlBuilder.SetValue(BaseOrganizeExpressEntity.FieldInternalDispatch, entity.InternalDispatch);
            sqlBuilder.SetValue(BaseOrganizeExpressEntity.FieldIsTransferCenter, entity.IsTransferCenter);
            sqlBuilder.SetValue(BaseOrganizeExpressEntity.FieldIsErpOpen, entity.IsErpOpen);
            sqlBuilder.SetValue(BaseOrganizeExpressEntity.FieldIsReceiveOrder, entity.IsReceiveOrder);
            sqlBuilder.SetValue(BaseOrganizeExpressEntity.FieldIsReceiveComplain, entity.IsReceiveComplain);
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
