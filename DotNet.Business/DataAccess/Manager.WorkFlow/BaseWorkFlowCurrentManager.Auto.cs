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
    /// BaseWorkFlowCurrentManager
    /// 工作流当前审核状态
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
    public partial class BaseWorkFlowCurrentManager : BaseManager, IBaseManager
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public BaseWorkFlowCurrentManager()
        {
            if (base.dbHelper == null)
            {
                base.dbHelper = DbHelperFactory.GetHelper(BaseSystemInfo.WorkFlowDbType, BaseSystemInfo.WorkFlowDbConnection);
            }
            base.CurrentTableName = BaseWorkFlowCurrentEntity.TableName;
            base.PrimaryKey = "Id";
        }

        /// <summary>
        /// 构造函数
        /// <param name="tableName">指定表名</param>
        /// </summary>
        public BaseWorkFlowCurrentManager(string tableName)
        {
            base.CurrentTableName = tableName;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="dbHelper">数据库连接</param>
        public BaseWorkFlowCurrentManager(IDbHelper dbHelper): this()
        {
            DbHelper = dbHelper;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="userInfo">用户信息</param>
        public BaseWorkFlowCurrentManager(BaseUserInfo userInfo) : this()
        {
            UserInfo = userInfo;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="dbHelper">数据库连接</param>
        /// <param name="userInfo">用户信息</param>
        public BaseWorkFlowCurrentManager(IDbHelper dbHelper, BaseUserInfo userInfo) : this(dbHelper)
        {
            UserInfo = userInfo;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="dbHelper">数据库连接</param>
        /// <param name="userInfo">用户信息</param>
        /// <param name="tableName">指定表名</param>
        public BaseWorkFlowCurrentManager(IDbHelper dbHelper, BaseUserInfo userInfo, string tableName) : this(dbHelper, userInfo)
        {
            base.CurrentTableName = tableName;
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="workFlowCurrentEntity">实体</param>
        /// <returns>主键</returns>
        public string Add(BaseWorkFlowCurrentEntity workFlowCurrentEntity)
        {
            return this.AddObject(workFlowCurrentEntity);
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="workFlowCurrentEntity">实体</param>
        /// <param name="identity">自增量方式</param>
        /// <param name="returnId">返回主键</param>
        /// <returns>主键</returns>
        public string Add(BaseWorkFlowCurrentEntity workFlowCurrentEntity, bool identity, bool returnId)
        {
            this.Identity = identity;
            this.ReturnId = returnId;
            return this.AddObject(workFlowCurrentEntity);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="workFlowCurrentEntity">实体</param>
        public int Update(BaseWorkFlowCurrentEntity workFlowCurrentEntity)
        {
            return this.UpdateObject(workFlowCurrentEntity);
        }

        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="id">主键</param>
        public BaseWorkFlowCurrentEntity GetObject(string id)
        {
            return BaseEntity.Create<BaseWorkFlowCurrentEntity>(this.ExecuteReader(new KeyValuePair<string, object>(BaseWorkFlowCurrentEntity.FieldId, id)));
            // return BaseEntity.Create<BaseWorkFlowCurrentEntity>(this.GetDataTable(new KeyValuePair<string, object>(BaseWorkFlowCurrentEntity.FieldId, id)));
        }

        /// <summary>
        /// 添加实体
        /// </summary>
        /// <param name="workFlowCurrentEntity">实体</param>
        public string AddObject(BaseWorkFlowCurrentEntity workFlowCurrentEntity)
        {
            string sequence = string.Empty;
            this.Identity = false; 
            if (workFlowCurrentEntity.SortCode == null || workFlowCurrentEntity.SortCode == 0)
            {
                BaseSequenceManager sequenceManager = new BaseSequenceManager(DbHelper, this.Identity);
                sequence = sequenceManager.Increment(this.CurrentTableName);
                workFlowCurrentEntity.SortCode = int.Parse(sequence);
            }
            if (workFlowCurrentEntity.Id != null)
            {
                sequence = workFlowCurrentEntity.Id.ToString();
            }
            SQLBuilder sqlBuilder = new SQLBuilder(DbHelper, this.Identity, this.ReturnId);
            sqlBuilder.BeginInsert(this.CurrentTableName, BaseWorkFlowCurrentEntity.FieldId);
            if (!this.Identity) 
            {
                if (string.IsNullOrEmpty(workFlowCurrentEntity.Id)) 
                {
                    sequence = Guid.NewGuid().ToString("N"); 
                    workFlowCurrentEntity.Id = sequence ;
                }
                sqlBuilder.SetValue(BaseWorkFlowCurrentEntity.FieldId, workFlowCurrentEntity.Id);
            }
            else
            {
                if (!this.ReturnId && (DbHelper.CurrentDbType == CurrentDbType.Oracle || DbHelper.CurrentDbType == CurrentDbType.DB2))
                {
                    if (DbHelper.CurrentDbType == CurrentDbType.Oracle)
                    {
                        sqlBuilder.SetFormula(BaseWorkFlowCurrentEntity.FieldId, "SEQ_" + this.CurrentTableName.ToUpper() + ".NEXTVAL ");
                    }
                    if (DbHelper.CurrentDbType == CurrentDbType.DB2)
                    {
                        sqlBuilder.SetFormula(BaseWorkFlowCurrentEntity.FieldId, "NEXT VALUE FOR SEQ_" + this.CurrentTableName.ToUpper());
                    }
                }
                else
                {
                    if (this.Identity && (DbHelper.CurrentDbType == CurrentDbType.Oracle || DbHelper.CurrentDbType == CurrentDbType.DB2))
                    {
                        if (string.IsNullOrEmpty(workFlowCurrentEntity.Id))
                        {
                            if (string.IsNullOrEmpty(sequence))
                            {
                                BaseSequenceManager sequenceManager = new BaseSequenceManager(DbHelper, this.Identity);
                                sequence = sequenceManager.Increment(this.CurrentTableName);
                            }
                            workFlowCurrentEntity.Id = sequence;
                        }
                        sqlBuilder.SetValue(BaseWorkFlowCurrentEntity.FieldId, workFlowCurrentEntity.Id);
                    }
                }
            }
            this.SetObject(sqlBuilder, workFlowCurrentEntity);
            if (UserInfo != null) 
            { 
                sqlBuilder.SetValue(BaseWorkFlowCurrentEntity.FieldCreateUserId, UserInfo.Id);
                sqlBuilder.SetValue(BaseWorkFlowCurrentEntity.FieldCreateBy, UserInfo.RealName);
            } 
            sqlBuilder.SetDBNow(BaseWorkFlowCurrentEntity.FieldCreateOn);
            if (UserInfo != null) 
            { 
                sqlBuilder.SetValue(BaseWorkFlowCurrentEntity.FieldModifiedUserId, UserInfo.Id);
                sqlBuilder.SetValue(BaseWorkFlowCurrentEntity.FieldModifiedBy, UserInfo.RealName);
            } 
            sqlBuilder.SetDBNow(BaseWorkFlowCurrentEntity.FieldModifiedOn);
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
        /// <param name="workFlowCurrentEntity">实体</param>
        public int UpdateObject(BaseWorkFlowCurrentEntity workFlowCurrentEntity)
        {
            SQLBuilder sqlBuilder = new SQLBuilder(DbHelper);
            sqlBuilder.BeginUpdate(this.CurrentTableName);
            this.SetObject(sqlBuilder, workFlowCurrentEntity);
            if (UserInfo != null) 
            { 
                sqlBuilder.SetValue(BaseWorkFlowCurrentEntity.FieldModifiedUserId, UserInfo.Id);
                sqlBuilder.SetValue(BaseWorkFlowCurrentEntity.FieldModifiedBy, UserInfo.RealName);
            } 
            sqlBuilder.SetDBNow(BaseWorkFlowCurrentEntity.FieldModifiedOn);
            sqlBuilder.SetWhere(BaseWorkFlowCurrentEntity.FieldId, workFlowCurrentEntity.Id);
            return sqlBuilder.EndUpdate();
        }

        // 这个是声明扩展方法
        partial void SetObjectExpand(SQLBuilder sqlBuilder, BaseWorkFlowCurrentEntity workFlowCurrentEntity);
        
        /// <summary>
        /// 设置实体
        /// </summary>
        /// <param name="workFlowCurrentEntity">实体</param>
        private void SetObject(SQLBuilder sqlBuilder, BaseWorkFlowCurrentEntity workFlowCurrentEntity)
        {
            SetObjectExpand(sqlBuilder, workFlowCurrentEntity);
            sqlBuilder.SetValue(BaseWorkFlowCurrentEntity.FieldCategoryCode, workFlowCurrentEntity.CategoryCode);
            sqlBuilder.SetValue(BaseWorkFlowCurrentEntity.FieldCategoryFullName, workFlowCurrentEntity.CategoryFullName);
            sqlBuilder.SetValue(BaseWorkFlowCurrentEntity.FieldObjectId, workFlowCurrentEntity.ObjectId);
            sqlBuilder.SetValue(BaseWorkFlowCurrentEntity.FieldObjectFullName, workFlowCurrentEntity.ObjectFullName);
            sqlBuilder.SetValue(BaseWorkFlowCurrentEntity.FieldProcessId, workFlowCurrentEntity.ProcessId);
            sqlBuilder.SetValue(BaseWorkFlowCurrentEntity.FieldActivityId, workFlowCurrentEntity.ActivityId);
            sqlBuilder.SetValue(BaseWorkFlowCurrentEntity.FieldActivityCode, workFlowCurrentEntity.ActivityCode);
            sqlBuilder.SetValue(BaseWorkFlowCurrentEntity.FieldActivityFullName, workFlowCurrentEntity.ActivityFullName);
            sqlBuilder.SetValue(BaseWorkFlowCurrentEntity.FieldActivityType, workFlowCurrentEntity.ActivityType);
            sqlBuilder.SetValue(BaseWorkFlowCurrentEntity.FieldToDepartmentId, workFlowCurrentEntity.ToDepartmentId);
            sqlBuilder.SetValue(BaseWorkFlowCurrentEntity.FieldToDepartmentName, workFlowCurrentEntity.ToDepartmentName);
            sqlBuilder.SetValue(BaseWorkFlowCurrentEntity.FieldToUserId, workFlowCurrentEntity.ToUserId);
            sqlBuilder.SetValue(BaseWorkFlowCurrentEntity.FieldToUserRealName, workFlowCurrentEntity.ToUserRealName);
            sqlBuilder.SetValue(BaseWorkFlowCurrentEntity.FieldToRoleId, workFlowCurrentEntity.ToRoleId);
            sqlBuilder.SetValue(BaseWorkFlowCurrentEntity.FieldToRoleRealName, workFlowCurrentEntity.ToRoleRealName);
            sqlBuilder.SetValue(BaseWorkFlowCurrentEntity.FieldAuditUserId, workFlowCurrentEntity.AuditUserId);
            sqlBuilder.SetValue(BaseWorkFlowCurrentEntity.FieldAuditUserCode, workFlowCurrentEntity.AuditUserCode);
            sqlBuilder.SetValue(BaseWorkFlowCurrentEntity.FieldAuditUserRealName, workFlowCurrentEntity.AuditUserRealName);
            sqlBuilder.SetValue(BaseWorkFlowCurrentEntity.FieldSendDate, workFlowCurrentEntity.SendDate);
            sqlBuilder.SetValue(BaseWorkFlowCurrentEntity.FieldAuditDate, workFlowCurrentEntity.AuditDate);
            sqlBuilder.SetValue(BaseWorkFlowCurrentEntity.FieldAuditIdea, workFlowCurrentEntity.AuditIdea);
            sqlBuilder.SetValue(BaseWorkFlowCurrentEntity.FieldAuditStatus, workFlowCurrentEntity.AuditStatus);
            sqlBuilder.SetValue(BaseWorkFlowCurrentEntity.FieldAuditStatusName, workFlowCurrentEntity.AuditStatusName);
            sqlBuilder.SetValue(BaseWorkFlowCurrentEntity.FieldSortCode, workFlowCurrentEntity.SortCode);
            sqlBuilder.SetValue(BaseWorkFlowCurrentEntity.FieldEnabled, workFlowCurrentEntity.Enabled);
            sqlBuilder.SetValue(BaseWorkFlowCurrentEntity.FieldDeletionStateCode, workFlowCurrentEntity.DeletionStateCode);
            sqlBuilder.SetValue(BaseWorkFlowCurrentEntity.FieldDescription, workFlowCurrentEntity.Description);
        }

        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns>影响行数</returns>
        public int Delete(string id)
        {
            return this.Delete(new KeyValuePair<string, object>(BaseWorkFlowCurrentEntity.FieldId, id));
        }
    }
}
