//--------------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2016 , Hairihan TECH, Ltd. 
//--------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Data;

namespace DotNet.Business
{
    using DotNet.Utilities;
    using DotNet.Model;

    /// <summary>
    /// BaseWorkFlowHistoryManager
    /// 工作流审核历史步骤记录
    /// 
    /// 修改记录
    /// 
    /// 2012-07-03 版本：1.0 JiRiGaLa 创建主键。
    /// 
    /// <author>
    ///     <name>JiRiGaLa</name>
    ///     <date>2012-07-03</date>
    /// </author>
    /// </summary>
    public partial class BaseWorkFlowHistoryManager : BaseManager, IBaseManager
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public BaseWorkFlowHistoryManager()
        {
            if (base.dbHelper == null)
            {
                base.dbHelper = DbHelperFactory.GetHelper(BaseSystemInfo.WorkFlowDbType, BaseSystemInfo.WorkFlowDbConnection);
            }
            base.CurrentTableName = BaseWorkFlowHistoryEntity.TableName;
            base.PrimaryKey = "Id";
        }

        /// <summary>
        /// 构造函数
        /// <param name="tableName">指定表名</param>
        /// </summary>
        public BaseWorkFlowHistoryManager(string tableName)
        {
            base.CurrentTableName = tableName;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="dbHelper">数据库连接</param>
        public BaseWorkFlowHistoryManager(IDbHelper dbHelper): this()
        {
            DbHelper = dbHelper;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="userInfo">用户信息</param>
        public BaseWorkFlowHistoryManager(BaseUserInfo userInfo) : this()
        {
            UserInfo = userInfo;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="dbHelper">数据库连接</param>
        /// <param name="userInfo">用户信息</param>
        public BaseWorkFlowHistoryManager(IDbHelper dbHelper, BaseUserInfo userInfo) : this(dbHelper)
        {
            UserInfo = userInfo;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="dbHelper">数据库连接</param>
        /// <param name="userInfo">用户信息</param>
        /// <param name="tableName">指定表名</param>
        public BaseWorkFlowHistoryManager(IDbHelper dbHelper, BaseUserInfo userInfo, string tableName) : this(dbHelper, userInfo)
        {
            base.CurrentTableName = tableName;
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="baseWorkFlowHistoryEntity">实体</param>
        /// <returns>主键</returns>
        public string Add(BaseWorkFlowHistoryEntity baseWorkFlowHistoryEntity)
        {
            return this.AddObject(baseWorkFlowHistoryEntity);
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="baseWorkFlowHistoryEntity">实体</param>
        /// <param name="identity">自增量方式</param>
        /// <param name="returnId">返回主键</param>
        /// <returns>主键</returns>
        public string Add(BaseWorkFlowHistoryEntity baseWorkFlowHistoryEntity, bool identity, bool returnId)
        {
            this.Identity = identity;
            this.ReturnId = returnId;
            return this.AddObject(baseWorkFlowHistoryEntity);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="baseWorkFlowHistoryEntity">实体</param>
        public int Update(BaseWorkFlowHistoryEntity baseWorkFlowHistoryEntity)
        {
            return this.UpdateObject(baseWorkFlowHistoryEntity);
        }

        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="id">主键</param>
        public BaseWorkFlowHistoryEntity GetObject(string id)
        {
            return GetObject(int.Parse(id));
        }

        public BaseWorkFlowHistoryEntity GetObject(int id)
        {
            return BaseEntity.Create<BaseWorkFlowHistoryEntity>(this.ExecuteReader(new KeyValuePair<string, object>(BaseWorkFlowHistoryEntity.FieldId, id)));
            // return BaseEntity.Create<BaseWorkFlowHistoryEntity>(this.GetDataTable(new KeyValuePair<string, object>(BaseWorkFlowHistoryEntity.FieldId, id)));
        }

        /// <summary>
        /// 添加实体
        /// </summary>
        /// <param name="baseWorkFlowHistoryEntity">实体</param>
        public string AddObject(BaseWorkFlowHistoryEntity baseWorkFlowHistoryEntity)
        {
            string sequence = string.Empty;
            if (baseWorkFlowHistoryEntity.SortCode == null || baseWorkFlowHistoryEntity.SortCode == 0)
            {
                BaseSequenceManager sequenceManager = new BaseSequenceManager(DbHelper, this.Identity);
                sequence = sequenceManager.Increment(this.CurrentTableName);
                baseWorkFlowHistoryEntity.SortCode = int.Parse(sequence);
            }
            SQLBuilder sqlBuilder = new SQLBuilder(DbHelper, this.Identity, this.ReturnId);
            sqlBuilder.BeginInsert(this.CurrentTableName, BaseWorkFlowHistoryEntity.FieldId);
            if (!this.Identity) 
            {
                sqlBuilder.SetValue(BaseWorkFlowHistoryEntity.FieldId, baseWorkFlowHistoryEntity.Id);
            }
            else
            {
                if (!this.ReturnId && (DbHelper.CurrentDbType == CurrentDbType.Oracle || DbHelper.CurrentDbType == CurrentDbType.DB2))
                {
                    if (DbHelper.CurrentDbType == CurrentDbType.Oracle)
                    {
                        sqlBuilder.SetFormula(BaseWorkFlowHistoryEntity.FieldId, "SEQ_" + this.CurrentTableName.ToUpper() + ".NEXTVAL ");
                    }
                    if (DbHelper.CurrentDbType == CurrentDbType.DB2)
                    {
                        sqlBuilder.SetFormula(BaseWorkFlowHistoryEntity.FieldId, "NEXT VALUE FOR SEQ_" + this.CurrentTableName.ToUpper());
                    }
                }
                else
                {
                    if (this.Identity && (DbHelper.CurrentDbType == CurrentDbType.Oracle || DbHelper.CurrentDbType == CurrentDbType.DB2))
                    {
                        if (baseWorkFlowHistoryEntity.Id == null)
                        {
                            if (string.IsNullOrEmpty(sequence))
                            {
                                BaseSequenceManager sequenceManager = new BaseSequenceManager(DbHelper, this.Identity);
                                sequence = sequenceManager.Increment(this.CurrentTableName);
                            }
                            baseWorkFlowHistoryEntity.Id = int.Parse(sequence);
                        }
                        sqlBuilder.SetValue(BaseWorkFlowHistoryEntity.FieldId, baseWorkFlowHistoryEntity.Id);
                    }
                }
            }
            this.SetObject(sqlBuilder, baseWorkFlowHistoryEntity);
            if (UserInfo != null) 
            { 
                sqlBuilder.SetValue(BaseWorkFlowHistoryEntity.FieldCreateUserId, UserInfo.Id);
                sqlBuilder.SetValue(BaseWorkFlowHistoryEntity.FieldCreateBy, UserInfo.RealName);
            } 
            sqlBuilder.SetDBNow(BaseWorkFlowHistoryEntity.FieldCreateOn);
            if (UserInfo != null) 
            { 
                sqlBuilder.SetValue(BaseWorkFlowHistoryEntity.FieldModifiedUserId, UserInfo.Id);
                sqlBuilder.SetValue(BaseWorkFlowHistoryEntity.FieldModifiedBy, UserInfo.RealName);
            } 
            sqlBuilder.SetDBNow(BaseWorkFlowHistoryEntity.FieldModifiedOn);
            if (this.Identity && (DbHelper.CurrentDbType == CurrentDbType.SqlServer || DbHelper.CurrentDbType == CurrentDbType.Access))
            {
                sequence = sqlBuilder.EndInsert().ToString();
            }
            else
            {
                sqlBuilder.EndInsert();
            }
            return sequence;
        }

        /// <summary>
        /// 更新实体
        /// </summary>
        /// <param name="baseWorkFlowHistoryEntity">实体</param>
        public int UpdateObject(BaseWorkFlowHistoryEntity baseWorkFlowHistoryEntity)
        {
            SQLBuilder sqlBuilder = new SQLBuilder(DbHelper);
            sqlBuilder.BeginUpdate(this.CurrentTableName);
            this.SetObject(sqlBuilder, baseWorkFlowHistoryEntity);
            if (UserInfo != null) 
            { 
                sqlBuilder.SetValue(BaseWorkFlowHistoryEntity.FieldModifiedUserId, UserInfo.Id);
                sqlBuilder.SetValue(BaseWorkFlowHistoryEntity.FieldModifiedBy, UserInfo.RealName);
            } 
            sqlBuilder.SetDBNow(BaseWorkFlowHistoryEntity.FieldModifiedOn);
            sqlBuilder.SetWhere(BaseWorkFlowHistoryEntity.FieldId, baseWorkFlowHistoryEntity.Id);
            return sqlBuilder.EndUpdate();
        }

        // 这个是声明扩展方法
        partial void SetObjectExpand(SQLBuilder sqlBuilder, BaseWorkFlowHistoryEntity entity);
        
        /// <summary>
        /// 设置实体
        /// </summary>
        /// <param name="entity">实体</param>
        private void SetObject(SQLBuilder sqlBuilder, BaseWorkFlowHistoryEntity entity)
        {
            SetObjectExpand(sqlBuilder, entity);
            sqlBuilder.SetValue(BaseWorkFlowHistoryEntity.FieldCurrentFlowId, entity.CurrentFlowId);
            sqlBuilder.SetValue(BaseWorkFlowHistoryEntity.FieldProcessId, entity.ProcessId);
            sqlBuilder.SetValue(BaseWorkFlowHistoryEntity.FieldActivityId, entity.ActivityId);
            sqlBuilder.SetValue(BaseWorkFlowHistoryEntity.FieldActivityCode, entity.ActivityCode);
            sqlBuilder.SetValue(BaseWorkFlowHistoryEntity.FieldActivityFullName, entity.ActivityFullName);
            sqlBuilder.SetValue(BaseWorkFlowHistoryEntity.FieldToDepartmentId, entity.ToDepartmentId);
            sqlBuilder.SetValue(BaseWorkFlowHistoryEntity.FieldToDepartmentName, entity.ToDepartmentName);
            sqlBuilder.SetValue(BaseWorkFlowHistoryEntity.FieldToUserId, entity.ToUserId);
            sqlBuilder.SetValue(BaseWorkFlowHistoryEntity.FieldToUserRealName, entity.ToUserRealName);
            sqlBuilder.SetValue(BaseWorkFlowHistoryEntity.FieldToRoleId, entity.ToRoleId);
            sqlBuilder.SetValue(BaseWorkFlowHistoryEntity.FieldToRoleRealName, entity.ToRoleRealName);
            sqlBuilder.SetValue(BaseWorkFlowHistoryEntity.FieldAuditUserId, entity.AuditUserId);
            sqlBuilder.SetValue(BaseWorkFlowHistoryEntity.FieldAuditUserCode, entity.AuditUserCode);
            sqlBuilder.SetValue(BaseWorkFlowHistoryEntity.FieldAuditUserRealName, entity.AuditUserRealName);
            sqlBuilder.SetValue(BaseWorkFlowHistoryEntity.FieldSendDate, entity.SendDate);
            sqlBuilder.SetValue(BaseWorkFlowHistoryEntity.FieldAuditDate, entity.AuditDate);
            sqlBuilder.SetValue(BaseWorkFlowHistoryEntity.FieldAuditIdea, entity.AuditIdea);
            sqlBuilder.SetValue(BaseWorkFlowHistoryEntity.FieldAuditStatus, entity.AuditStatus);
            sqlBuilder.SetValue(BaseWorkFlowHistoryEntity.FieldAuditStatusName, entity.AuditStatusName);
            sqlBuilder.SetValue(BaseWorkFlowHistoryEntity.FieldSortCode, entity.SortCode);
            sqlBuilder.SetValue(BaseWorkFlowHistoryEntity.FieldEnabled, entity.Enabled);
            sqlBuilder.SetValue(BaseWorkFlowHistoryEntity.FieldDeletionStateCode, entity.DeletionStateCode);
            sqlBuilder.SetValue(BaseWorkFlowHistoryEntity.FieldDescription, entity.Description);
        }

        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns>影响行数</returns>
        public int Delete(int id)
        {
            return this.Delete(new KeyValuePair<string, object>(BaseWorkFlowHistoryEntity.FieldId, id));
        }
    }
}
