//--------------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2016 , Hairihan TECH, Ltd. 
//--------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Data;
using System.Runtime.Serialization;

namespace DotNet.Model
{
    using DotNet.Utilities;

    /// <summary>
    /// BaseWorkFlowProcessEntity
    /// 工作流定义
    ///
    /// 修改记录
    ///
    ///		2012-04-09 版本：1.0 JiRiGaLa 创建主键。
    ///
    /// <author>
    ///		<name>JiRiGaLa</name>
    ///		<date>2012-04-09</date>
    /// </author>
    /// </summary>
    [Serializable, DataContract]
	public partial class BaseWorkFlowProcessEntity : BaseEntity
    {
        /// <summary>
        /// 主键
        /// </summary>
        [DataMember]
        public int? Id { get; set; }

        /// <summary>
        /// 用户主键
        /// </summary>
        [DataMember]
        public String UserId { get; set; }

        /// <summary>
        /// 组织机构主键
        /// </summary>
        [DataMember]
        public String OrganizeId { get; set; }

        /// <summary>
        /// 模版单据主键
        /// </summary>
        [DataMember]
        public String BillTemplateId { get; set; }

        /// <summary>
        /// 审核类型（按用户，按部门，按单据）
        /// </summary>
        [DataMember]
        public String AuditCategoryCode { get; set; }

        /// <summary>
        /// 反射回调类,工作流回写数据库,通过接口定义调用类的方法等
        /// </summary>
        [DataMember]
        public String CallBackClass { get; set; }

        /// <summary>
        /// 回写表
        /// </summary>
        [DataMember]
        public String CallBackTable { get; set; }

        /// <summary>
        /// 分类主键
        /// </summary>
        [DataMember]
        public String CategoryCode { get; set; }

        private int? commitmentDays = 0;
        /// <summary>
        /// 默认承诺天数
        /// </summary>
        [DataMember]
        public int? CommitmentDays
        {
            get
            {
                return this.commitmentDays;
            }
            set
            {
                this.commitmentDays = value;
            }
        }

        /// <summary>
        /// 流程编号
        /// </summary>
        [DataMember]
        public String Code { get; set; }

        /// <summary>
        /// 流程名称
        /// </summary>
        [DataMember]
        public String FullName { get; set; }

        /// <summary>
        /// 流程内容
        /// </summary>
        [DataMember]
        public String Contents { get; set; }

        private string enterConstraint = string.Empty;
        /// <summary>
        /// 入口条件
        /// </summary>
        [DataMember]
        public string EnterConstraint
        {
            get
            {
                return this.enterConstraint;
            }
            set
            {
                this.enterConstraint = value;
            }
        } 

        /// <summary>
        /// 排序码
        /// </summary>
        [DataMember]
        public int? SortCode { get; set; }

        /// <summary>
        /// 有效
        /// </summary>
        [DataMember]
        public int? Enabled { get; set; }

        /// <summary>
        /// 删除标志
        /// </summary>
        [DataMember]
        public int? DeletionStateCode { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [DataMember]
        public String Description { get; set; }

        /// <summary>
        /// 创建日期
        /// </summary>
        [DataMember]
        public DateTime? CreateOn { get; set; }

        /// <summary>
        /// 创建用户主键
        /// </summary>
        [DataMember]
        public String CreateUserId { get; set; }

        /// <summary>
        /// 创建用户
        /// </summary>
        [DataMember]
        public String CreateBy { get; set; }

        /// <summary>
        /// 修改日期
        /// </summary>
        [DataMember]
        public DateTime? ModifiedOn { get; set; }

        /// <summary>
        /// 修改用户主键
        /// </summary>
        [DataMember]
        public String ModifiedUserId { get; set; }

        /// <summary>
        /// 修改用户
        /// </summary>
        [DataMember]
        public String ModifiedBy { get; set; }

        private String callBackAssembly = string.Empty;
        /// <summary>
        /// 回调程序集
        /// </summary>
        [DataMember]
        public String CallBackAssembly
        {
            get
            {
                return this.callBackAssembly;
            }
            set
            {
                this.callBackAssembly = value;
            }
        }

        /// <summary>
        /// 从数据行读取
        /// </summary>
        /// <param name="dr">数据行</param>
		protected override BaseEntity GetFrom(IDataRow dr)
        {
            Id = BaseBusinessLogic.ConvertToInt(dr[BaseWorkFlowProcessEntity.FieldId]);
            UserId = BaseBusinessLogic.ConvertToString(dr[BaseWorkFlowProcessEntity.FieldUserId]);
            OrganizeId = BaseBusinessLogic.ConvertToString(dr[BaseWorkFlowProcessEntity.FieldOrganizeId]);
            BillTemplateId = BaseBusinessLogic.ConvertToString(dr[BaseWorkFlowProcessEntity.FieldBillTemplateId]);
            AuditCategoryCode = BaseBusinessLogic.ConvertToString(dr[BaseWorkFlowProcessEntity.FieldAuditCategoryCode]);
            CallBackClass = BaseBusinessLogic.ConvertToString(dr[BaseWorkFlowProcessEntity.FieldCallBackClass]);
            CallBackTable = BaseBusinessLogic.ConvertToString(dr[BaseWorkFlowProcessEntity.FieldCallBackTable]);
            CategoryCode = BaseBusinessLogic.ConvertToString(dr[BaseWorkFlowProcessEntity.FieldCategoryCode]);
            Code = BaseBusinessLogic.ConvertToString(dr[BaseWorkFlowProcessEntity.FieldCode]);
            FullName = BaseBusinessLogic.ConvertToString(dr[BaseWorkFlowProcessEntity.FieldFullName]);
            Contents = BaseBusinessLogic.ConvertToString(dr[BaseWorkFlowProcessEntity.FieldContents]);
            CommitmentDays = BaseBusinessLogic.ConvertToInt(dr[BaseWorkFlowProcessEntity.FieldCommitmentDays]);
            EnterConstraint = BaseBusinessLogic.ConvertToString(dr[BaseWorkFlowActivityEntity.FieldEnterConstraint]);
            SortCode = BaseBusinessLogic.ConvertToInt(dr[BaseWorkFlowProcessEntity.FieldSortCode]);
            Enabled = BaseBusinessLogic.ConvertToInt(dr[BaseWorkFlowProcessEntity.FieldEnabled]);
            DeletionStateCode = BaseBusinessLogic.ConvertToInt(dr[BaseWorkFlowProcessEntity.FieldDeletionStateCode]);
            Description = BaseBusinessLogic.ConvertToString(dr[BaseWorkFlowProcessEntity.FieldDescription]);
            CreateOn = BaseBusinessLogic.ConvertToNullableDateTime(dr[BaseWorkFlowProcessEntity.FieldCreateOn]);
            CreateUserId = BaseBusinessLogic.ConvertToString(dr[BaseWorkFlowProcessEntity.FieldCreateUserId]);
            CreateBy = BaseBusinessLogic.ConvertToString(dr[BaseWorkFlowProcessEntity.FieldCreateBy]);
            ModifiedOn = BaseBusinessLogic.ConvertToNullableDateTime(dr[BaseWorkFlowProcessEntity.FieldModifiedOn]);
            ModifiedUserId = BaseBusinessLogic.ConvertToString(dr[BaseWorkFlowProcessEntity.FieldModifiedUserId]);
            ModifiedBy = BaseBusinessLogic.ConvertToString(dr[BaseWorkFlowProcessEntity.FieldModifiedBy]);
            CallBackAssembly = BaseBusinessLogic.ConvertToString(dr[BaseWorkFlowProcessEntity.FieldCallBackAssembly]);
            return this;
        }

        ///<summary>
        /// 工作流定义
        ///</summary>
        [NonSerialized]
        public static string TableName = "BaseWorkFlowProcess";

        ///<summary>
        /// 主键
        ///</summary>
        [NonSerialized]
        public static string FieldId = "Id";

        ///<summary>
        /// 用户主键
        ///</summary>
        [NonSerialized]
        public static string FieldUserId = "UserId";

        ///<summary>
        /// 组织机构主键
        ///</summary>
        [NonSerialized]
        public static string FieldOrganizeId = "OrganizeId";

        ///<summary>
        /// 模版单据主键
        ///</summary>
        [NonSerialized]
        public static string FieldBillTemplateId = "BillTemplateId";

        ///<summary>
        /// 审核类型（按用户，按部门，按单据）
        ///</summary>
        [NonSerialized]
        public static string FieldAuditCategoryCode = "AuditCategoryCode";

        ///<summary>
        /// 反射回调类,工作流回写数据库,通过接口定义调用类的方法等
        ///</summary>
        [NonSerialized]
        public static string FieldCallBackClass = "CallBackClass";

        ///<summary>
        /// 回写表
        ///</summary>
        [NonSerialized]
        public static string FieldCallBackTable = "CallBackTable";

        ///<summary>
        /// 分类
        ///</summary>
        [NonSerialized]
        public static string FieldCategoryCode = "CategoryCode";

        ///<summary>
        /// 流程编号
        ///</summary>
        [NonSerialized]
        public static string FieldCode = "Code";

        ///<summary>
        /// 流程名称
        ///</summary>
        [NonSerialized]
        public static string FieldFullName = "FullName";

        ///<summary>
        /// 流程内容
        ///</summary>
        [NonSerialized]
        public static string FieldContents = "Contents";

        /// <summary>
        /// 默认承诺天数
        /// </summary>
        [NonSerialized]
        public static string FieldCommitmentDays = "CommitmentDays";

        ///<summary>
        /// 入口条件
        ///</summary>
        [NonSerialized]
        public static string FieldEnterConstraint = "EnterConstraint";

        ///<summary>
        /// 排序码
        ///</summary>
        [NonSerialized]
        public static string FieldSortCode = "SortCode";

        ///<summary>
        /// 有效
        ///</summary>
        [NonSerialized]
        public static string FieldEnabled = "Enabled";

        ///<summary>
        /// 删除标志
        ///</summary>
        [NonSerialized]
        public static string FieldDeletionStateCode = "DeletionStateCode";

        ///<summary>
        /// 备注
        ///</summary>
        [NonSerialized]
        public static string FieldDescription = "Description";

        ///<summary>
        /// 创建日期
        ///</summary>
        [NonSerialized]
        public static string FieldCreateOn = "CreateOn";

        ///<summary>
        /// 创建用户主键
        ///</summary>
        [NonSerialized]
        public static string FieldCreateUserId = "CreateUserId";

        ///<summary>
        /// 创建用户
        ///</summary>
        [NonSerialized]
        public static string FieldCreateBy = "CreateBy";

        ///<summary>
        /// 修改日期
        ///</summary>
        [NonSerialized]
        public static string FieldModifiedOn = "ModifiedOn";

        ///<summary>
        /// 修改用户主键
        ///</summary>
        [NonSerialized]
        public static string FieldModifiedUserId = "ModifiedUserId";

        ///<summary>
        /// 修改用户
        ///</summary>
        [NonSerialized]
        public static string FieldModifiedBy = "ModifiedBy";

        ///<summary>
        /// 回调程序集
        ///</summary>
        [NonSerialized]
        public static string FieldCallBackAssembly = "CallBackAssembly";

    }
}
