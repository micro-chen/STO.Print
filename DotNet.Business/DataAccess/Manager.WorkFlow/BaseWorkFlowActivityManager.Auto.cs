//-----------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2016 , Hairihan TECH, Ltd. 
//-----------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Data;

namespace DotNet.Business
{
    using DotNet.Utilities;
    using DotNet.Model;

    /// <summary>
    /// BaseWorkFlowActivityManager
    /// 工作流步骤定义
    ///
    /// 修改记录
    ///
    ///		2010-08-04 版本：1.0 JiRiGaLa 创建主键。
    ///
    /// <author>
    ///		<name>JiRiGaLa</name>
    ///		<date>2010-08-04</date>
    /// </author>
    /// </summary>
    public partial class BaseWorkFlowActivityManager : BaseManager, IBaseManager
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public BaseWorkFlowActivityManager()
        {
            if (base.dbHelper == null)
            {
                base.dbHelper = DbHelperFactory.GetHelper(BaseSystemInfo.WorkFlowDbType, BaseSystemInfo.WorkFlowDbConnection);
            }
            if (string.IsNullOrEmpty(base.CurrentTableName))
            {
                base.CurrentTableName = BaseWorkFlowActivityEntity.TableName;
            }
        }

        /// <summary>
        /// 构造函数
        /// <param name="tableName">指定表名</param>
        /// </summary>
        public BaseWorkFlowActivityManager(string tableName) :this()
        {
            base.CurrentTableName = tableName;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="dbHelper">数据库连接</param>
        public BaseWorkFlowActivityManager(IDbHelper dbHelper)
            : this()
        {
            DbHelper = dbHelper;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="userInfo">用户信息</param>
        public BaseWorkFlowActivityManager(BaseUserInfo userInfo)
            : this()
        {
            UserInfo = userInfo;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="dbHelper">数据库连接</param>
        /// <param name="userInfo">用户信息</param>
        public BaseWorkFlowActivityManager(IDbHelper dbHelper, BaseUserInfo userInfo)
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
        public BaseWorkFlowActivityManager(IDbHelper dbHelper, BaseUserInfo userInfo, string tableName)
            : this(dbHelper, userInfo)
        {
            base.CurrentTableName = tableName;
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="baseWorkFlowActivityEntity">实体</param>
        /// <returns>主键</returns>
        public string Add(BaseWorkFlowActivityEntity baseWorkFlowActivityEntity)
        {
            return this.AddObject(baseWorkFlowActivityEntity);
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="baseWorkFlowActivityEntity">实体</param>
        /// <param name="identity">自增量方式</param>
        /// <param name="returnId">返回主鍵</param>
        /// <returns>主键</returns>
        public string Add(BaseWorkFlowActivityEntity baseWorkFlowActivityEntity, bool identity, bool returnId)
        {
            this.Identity = identity;
            this.ReturnId = returnId;
            return this.AddObject(baseWorkFlowActivityEntity);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="baseWorkFlowActivityEntity">实体</param>
        public int Update(BaseWorkFlowActivityEntity baseWorkFlowActivityEntity)
        {
            return this.UpdateObject(baseWorkFlowActivityEntity);
        }

        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="id">主键</param>
        public BaseWorkFlowActivityEntity GetObject(int id)
        {
            return GetObject(id.ToString());
        }

        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="id">主键</param>
        public BaseWorkFlowActivityEntity GetObject(int? id)
        {
            return GetObject((int)id);
        }

        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="id">主键</param>
        public BaseWorkFlowActivityEntity GetObject(string id)
        {
            return BaseEntity.Create<BaseWorkFlowActivityEntity>(this.ExecuteReader(new KeyValuePair<string, object>(BaseWorkFlowActivityEntity.FieldId, id)));
            // return BaseEntity.Create<BaseWorkFlowActivityEntity>(this.GetDataTable(new KeyValuePair<string, object>(BaseWorkFlowActivityEntity.FieldId, id)));
        }

        /// <summary>
        /// 添加实体
        /// </summary>
        /// <param name="workFlowActivityEntity">实体</param>
        public string AddObject(BaseWorkFlowActivityEntity workFlowActivityEntity)
        {
            string sequence = string.Empty;
            if (workFlowActivityEntity.SortCode == null || workFlowActivityEntity.SortCode == 0)
            {
                BaseSequenceManager sequenceManager = new BaseSequenceManager(DbHelper, this.Identity);
                sequence = sequenceManager.Increment(this.CurrentTableName);
                workFlowActivityEntity.SortCode = int.Parse(sequence);
            }
            SQLBuilder sqlBuilder = new SQLBuilder(DbHelper, this.Identity, this.ReturnId);
            sqlBuilder.BeginInsert(this.CurrentTableName, BaseWorkFlowActivityEntity.FieldId);
            if (!this.Identity)
            {
                sqlBuilder.SetValue(BaseWorkFlowActivityEntity.FieldId, workFlowActivityEntity.Id);
            }
            else
            {
                if (!this.ReturnId && (DbHelper.CurrentDbType == CurrentDbType.Oracle || DbHelper.CurrentDbType == CurrentDbType.DB2))
                {
                    if (DbHelper.CurrentDbType == CurrentDbType.Oracle)
                    {
                        sqlBuilder.SetFormula(BaseWorkFlowActivityEntity.FieldId, "SEQ_" + this.CurrentTableName.ToUpper() + ".NEXTVAL ");
                    }
                    if (DbHelper.CurrentDbType == CurrentDbType.DB2)
                    {
                        sqlBuilder.SetFormula(BaseWorkFlowActivityEntity.FieldId, "NEXT VALUE FOR SEQ_" + this.CurrentTableName.ToUpper());
                    }
                }
                else
                {
                    if (this.Identity && (DbHelper.CurrentDbType == CurrentDbType.Oracle || DbHelper.CurrentDbType == CurrentDbType.DB2))
                    {
                        if (workFlowActivityEntity.Id == null)
                        {
                            if (string.IsNullOrEmpty(sequence))
                            {
                                BaseSequenceManager sequenceManager = new BaseSequenceManager(DbHelper, this.Identity);
                                sequence = sequenceManager.Increment(this.CurrentTableName);
                            }
                            workFlowActivityEntity.Id = int.Parse(sequence);
                        }
                        sqlBuilder.SetValue(BaseWorkFlowActivityEntity.FieldId, workFlowActivityEntity.Id);
                    }
                }
            }
            this.SetObject(sqlBuilder, workFlowActivityEntity);
            if (UserInfo != null)
            {
                sqlBuilder.SetValue(BaseWorkFlowActivityEntity.FieldCreateUserId, UserInfo.Id);
                sqlBuilder.SetValue(BaseWorkFlowActivityEntity.FieldCreateBy, UserInfo.RealName);
            }
            sqlBuilder.SetDBNow(BaseWorkFlowActivityEntity.FieldCreateOn);
            if (UserInfo != null)
            {
                sqlBuilder.SetValue(BaseWorkFlowActivityEntity.FieldModifiedUserId, UserInfo.Id);
                sqlBuilder.SetValue(BaseWorkFlowActivityEntity.FieldModifiedBy, UserInfo.RealName);
            }
            sqlBuilder.SetDBNow(BaseWorkFlowActivityEntity.FieldModifiedOn);
            if (DbHelper.CurrentDbType == CurrentDbType.SqlServer && this.Identity)
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
        /// <param name="baseWorkFlowActivityEntity">实体</param>
        public int UpdateObject(BaseWorkFlowActivityEntity baseWorkFlowActivityEntity)
        {
            SQLBuilder sqlBuilder = new SQLBuilder(DbHelper);
            sqlBuilder.BeginUpdate(this.CurrentTableName);
            this.SetObject(sqlBuilder, baseWorkFlowActivityEntity);
            if (UserInfo != null)
            {
                sqlBuilder.SetValue(BaseWorkFlowActivityEntity.FieldModifiedUserId, UserInfo.Id);
                sqlBuilder.SetValue(BaseWorkFlowActivityEntity.FieldModifiedBy, UserInfo.RealName);
            }
            sqlBuilder.SetDBNow(BaseWorkFlowActivityEntity.FieldModifiedOn);
            sqlBuilder.SetWhere(BaseWorkFlowActivityEntity.FieldId, baseWorkFlowActivityEntity.Id);
            return sqlBuilder.EndUpdate();
        }

        /// <summary>
        /// 设置实体
        /// </summary>
        /// <param name="entity">实体</param>
        private void SetObject(SQLBuilder sqlBuilder, BaseWorkFlowActivityEntity entity)
        {
            sqlBuilder.SetValue(BaseWorkFlowActivityEntity.FieldProcessId, entity.ProcessId);
            sqlBuilder.SetValue(BaseWorkFlowActivityEntity.FieldCode, entity.Code);
            sqlBuilder.SetValue(BaseWorkFlowActivityEntity.FieldFullName, entity.FullName);
            sqlBuilder.SetValue(BaseWorkFlowActivityEntity.FieldCommitmentDays, entity.CommitmentDays);
            sqlBuilder.SetValue(BaseWorkFlowActivityEntity.FieldAuditDepartmentId, entity.AuditDepartmentId);
            sqlBuilder.SetValue(BaseWorkFlowActivityEntity.FieldAuditDepartmentName, entity.AuditDepartmentName);
            sqlBuilder.SetValue(BaseWorkFlowActivityEntity.FieldAuditUserId, entity.AuditUserId);
            sqlBuilder.SetValue(BaseWorkFlowActivityEntity.FieldAuditUserCode, entity.AuditUserCode);
            sqlBuilder.SetValue(BaseWorkFlowActivityEntity.FieldAuditUserRealName, entity.AuditUserRealName);
            sqlBuilder.SetValue(BaseWorkFlowActivityEntity.FieldAuditRoleId, entity.AuditRoleId);
            sqlBuilder.SetValue(BaseWorkFlowActivityEntity.FieldAuditRoleRealName, entity.AuditRoleRealName);
            sqlBuilder.SetValue(BaseWorkFlowActivityEntity.FieldActivityType, entity.ActivityType);
            sqlBuilder.SetValue(BaseWorkFlowActivityEntity.FieldDefaultAuditUserId, entity.DefaultAuditUserId);
            sqlBuilder.SetValue(BaseWorkFlowActivityEntity.FieldDefaultAuditUserRealName, entity.DefaultAuditUserRealName);
            sqlBuilder.SetValue(BaseWorkFlowActivityEntity.FieldEditFields, entity.EditFields);
            sqlBuilder.SetValue(BaseWorkFlowActivityEntity.FieldAllowSkip, entity.AllowSkip);
            sqlBuilder.SetValue(BaseWorkFlowActivityEntity.FieldAllowPrint, entity.AllowPrint);
            sqlBuilder.SetValue(BaseWorkFlowActivityEntity.FieldAllowEditDocuments, entity.AllowEditDocuments);
            sqlBuilder.SetValue(BaseWorkFlowActivityEntity.FieldAllowAuditQuash, entity.AllowAuditQuash);
            sqlBuilder.SetValue(BaseWorkFlowActivityEntity.FieldAllowAuditPass, entity.AllowAuditPass);
            sqlBuilder.SetValue(BaseWorkFlowActivityEntity.FieldEnterConstraint, entity.EnterConstraint);
            sqlBuilder.SetValue(BaseWorkFlowActivityEntity.FieldEndConstraint, entity.EndConstraint);
            sqlBuilder.SetValue(BaseWorkFlowActivityEntity.FieldSortCode, entity.SortCode);
            sqlBuilder.SetValue(BaseWorkFlowActivityEntity.FieldEnabled, entity.Enabled);
            sqlBuilder.SetValue(BaseWorkFlowActivityEntity.FieldDeletionStateCode, entity.DeletionStateCode);
            sqlBuilder.SetValue(BaseWorkFlowActivityEntity.FieldDescription, entity.Description);
        }

        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns>影响行数</returns>
        public int Delete(int id)
        {
            return this.Delete(new KeyValuePair<string, object>(BaseWorkFlowActivityEntity.FieldId, id));
        }        
    }
}
