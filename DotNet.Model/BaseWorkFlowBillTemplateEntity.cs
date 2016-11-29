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
    /// BaseWorkFlowBillTemplateEntity
    /// 工作流模板表
    /// 
    /// 修改记录
    /// 
    /// 2012-05-18 版本：1.0 JiRiGaLa 创建主键。
    /// 
    /// <author>
    ///     <name>JiRiGaLa</name>
    ///     <date>2012-05-18</date>
    /// </author>
    /// </summary>
    [Serializable, DataContract]
	public partial class BaseWorkFlowBillTemplateEntity : BaseEntity
    {
        /// <summary>
        /// 主键
        /// </summary>
        [DataMember]
        public String Id { get; set; }

        /// <summary>
        /// 模版标题
        /// </summary>
        [DataMember]
        public String Title { get; set; }

        /// <summary>
        /// 模版编号
        /// </summary>
        [DataMember]
        public String Code { get; set; }

        /// <summary>
        /// 模版分类
        /// </summary>
        [DataMember]
        public String CategoryCode { get; set; }

        /// <summary>
        /// 标准流程
        /// </summary>
        [DataMember]
        public String Introduction { get; set; }

        /// <summary>
        /// 文件内容
        /// </summary>
        [DataMember]
        public String Contents { get; set; }

        /// <summary>
        /// 模版类型
        /// </summary>
        [DataMember]
        public String TemplateType { get; set; }

        private int? useWorkFlow = 1;
        /// <summary>
        /// 流转审批
        /// </summary>
        [DataMember]
        public int? UseWorkFlow
        {
            get
            {
                return this.useWorkFlow;
            }
            set
            {
                this.useWorkFlow = value;
            }
        }

        /// <summary>
        /// 添加页面
        /// </summary>
        [DataMember]
        public String AddPage { get; set; }

        /// <summary>
        /// 编辑页面
        /// </summary>
        [DataMember]
        public String EditPage { get; set; }

        /// <summary>
        /// 显示页面
        /// </summary>
        [DataMember]
        public String ShowPage { get; set; }

        /// <summary>
        /// 列表页面
        /// </summary>
        [DataMember]
        public String ListPage { get; set; }

        /// <summary>
        /// 管理页面
        /// </summary>
        [DataMember]
        public String AdminPage { get; set; }

        /// <summary>
        /// 审核状态
        /// </summary>
        [DataMember]
        public String AuditStatus { get; set; }

        private int? deletionStateCode = 0;
        /// <summary>
        /// 删除标志
        /// </summary>
        [DataMember]
        public int? DeletionStateCode
        {
            get
            {
                return this.deletionStateCode;
            }
            set
            {
                this.deletionStateCode = value;
            }
        }

        /// <summary>
        /// 备注
        /// </summary>
        [DataMember]
        public String Description { get; set; }

        private int? enabled = 1;
        /// <summary>
        /// 有效
        /// </summary>
        [DataMember]
        public int? Enabled
        {
            get
            {
                return this.enabled;
            }
            set
            {
                this.enabled = value;
            }
        }

        /// <summary>
        /// 排序码
        /// </summary>
        [DataMember]
        public int? SortCode { get; set; }

        /// <summary>
        /// 创建日期
        /// </summary>
        [DataMember]
        public DateTime? CreateOn { get; set; }

        /// <summary>
        /// 创建用户
        /// </summary>
        [DataMember]
        public String CreateBy { get; set; }

        /// <summary>
        /// 创建用户主键
        /// </summary>
        [DataMember]
        public String CreateUserId { get; set; }

        /// <summary>
        /// 修改日期
        /// </summary>
        [DataMember]
        public DateTime? ModifiedOn { get; set; }

        /// <summary>
        /// 修改用户
        /// </summary>
        [DataMember]
        public String ModifiedBy { get; set; }

        /// <summary>
        /// 修改用户主键
        /// </summary>
        [DataMember]
        public String ModifiedUserId { get; set; }

        /// <summary>
        /// 从数据行读取
        /// </summary>
        /// <param name="dr">数据行</param>
		protected override BaseEntity GetFrom(IDataRow dr)
        {
            Id = BaseBusinessLogic.ConvertToString(dr[BaseWorkFlowBillTemplateEntity.FieldId]);
            Title = BaseBusinessLogic.ConvertToString(dr[BaseWorkFlowBillTemplateEntity.FieldTitle]);
            Code = BaseBusinessLogic.ConvertToString(dr[BaseWorkFlowBillTemplateEntity.FieldCode]);
            CategoryCode = BaseBusinessLogic.ConvertToString(dr[BaseWorkFlowBillTemplateEntity.FieldCategoryCode]);
            Introduction = BaseBusinessLogic.ConvertToString(dr[BaseWorkFlowBillTemplateEntity.FieldIntroduction]);
            Contents = BaseBusinessLogic.ConvertToString(dr[BaseWorkFlowBillTemplateEntity.FieldContents]);
            TemplateType = BaseBusinessLogic.ConvertToString(dr[BaseWorkFlowBillTemplateEntity.FieldTemplateType]);
            UseWorkFlow = BaseBusinessLogic.ConvertToInt(dr[BaseWorkFlowBillTemplateEntity.FieldUseWorkFlow]);
            AddPage = BaseBusinessLogic.ConvertToString(dr[BaseWorkFlowBillTemplateEntity.FieldAddPage]);
            EditPage = BaseBusinessLogic.ConvertToString(dr[BaseWorkFlowBillTemplateEntity.FieldEditPage]);
            ShowPage = BaseBusinessLogic.ConvertToString(dr[BaseWorkFlowBillTemplateEntity.FieldShowPage]);
            ListPage = BaseBusinessLogic.ConvertToString(dr[BaseWorkFlowBillTemplateEntity.FieldListPage]);
            AdminPage = BaseBusinessLogic.ConvertToString(dr[BaseWorkFlowBillTemplateEntity.FieldAdminPage]);
            AuditStatus = BaseBusinessLogic.ConvertToString(dr[BaseWorkFlowBillTemplateEntity.FieldAuditStatus]);
            DeletionStateCode = BaseBusinessLogic.ConvertToInt(dr[BaseWorkFlowBillTemplateEntity.FieldDeletionStateCode]);
            Description = BaseBusinessLogic.ConvertToString(dr[BaseWorkFlowBillTemplateEntity.FieldDescription]);
            Enabled = BaseBusinessLogic.ConvertToInt(dr[BaseWorkFlowBillTemplateEntity.FieldEnabled]);
            SortCode = BaseBusinessLogic.ConvertToInt(dr[BaseWorkFlowBillTemplateEntity.FieldSortCode]);
            CreateOn = BaseBusinessLogic.ConvertToNullableDateTime(dr[BaseWorkFlowBillTemplateEntity.FieldCreateOn]);
            CreateBy = BaseBusinessLogic.ConvertToString(dr[BaseWorkFlowBillTemplateEntity.FieldCreateBy]);
            CreateUserId = BaseBusinessLogic.ConvertToString(dr[BaseWorkFlowBillTemplateEntity.FieldCreateUserId]);
            ModifiedOn = BaseBusinessLogic.ConvertToNullableDateTime(dr[BaseWorkFlowBillTemplateEntity.FieldModifiedOn]);
            ModifiedBy = BaseBusinessLogic.ConvertToString(dr[BaseWorkFlowBillTemplateEntity.FieldModifiedBy]);
            ModifiedUserId = BaseBusinessLogic.ConvertToString(dr[BaseWorkFlowBillTemplateEntity.FieldModifiedUserId]);
            // 获取扩展属性
            GetFromExpand(dr);
            return this;
        }

        ///<summary>
        /// 工作流模板表
        ///</summary>
        [NonSerialized]
        public static string TableName = "BaseWorkFlowBillTemplate";

        ///<summary>
        /// 主键
        ///</summary>
        [NonSerialized]
        public static string FieldId = "Id";

        ///<summary>
        /// 模版标题
        ///</summary>
        [NonSerialized]
        public static string FieldTitle = "Title";

        ///<summary>
        /// 模版编号
        ///</summary>
        [NonSerialized]
        public static string FieldCode = "Code";

        ///<summary>
        /// 模版分类
        ///</summary>
        [NonSerialized]
        public static string FieldCategoryCode = "CategoryCode";

        ///<summary>
        /// 标准流程
        ///</summary>
        [NonSerialized]
        public static string FieldIntroduction = "Introduction";

        ///<summary>
        /// 文件内容
        ///</summary>
        [NonSerialized]
        public static string FieldContents = "Contents";

        ///<summary>
        /// 模版类型
        ///</summary>
        [NonSerialized]
        public static string FieldTemplateType = "TemplateType";

        ///<summary>
        /// 流转审批
        ///</summary>
        [NonSerialized]
        public static string FieldUseWorkFlow = "UseWorkFlow";

        ///<summary>
        /// 添加页面
        ///</summary>
        [NonSerialized]
        public static string FieldAddPage = "AddPage";

        ///<summary>
        /// 编辑页面
        ///</summary>
        [NonSerialized]
        public static string FieldEditPage = "EditPage";

        ///<summary>
        /// 显示页面
        ///</summary>
        [NonSerialized]
        public static string FieldShowPage = "ShowPage";

        ///<summary>
        /// 列表页面
        ///</summary>
        [NonSerialized]
        public static string FieldListPage = "ListPage";

        ///<summary>
        /// 管理页面
        ///</summary>
        [NonSerialized]
        public static string FieldAdminPage = "AdminPage";

        ///<summary>
        /// 审核状态
        ///</summary>
        [NonSerialized]
        public static string FieldAuditStatus = "AuditStatus";

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
        /// 有效
        ///</summary>
        [NonSerialized]
        public static string FieldEnabled = "Enabled";

        ///<summary>
        /// 排序码
        ///</summary>
        [NonSerialized]
        public static string FieldSortCode = "SortCode";

        ///<summary>
        /// 创建日期
        ///</summary>
        [NonSerialized]
        public static string FieldCreateOn = "CreateOn";

        ///<summary>
        /// 创建用户
        ///</summary>
        [NonSerialized]
        public static string FieldCreateBy = "CreateBy";

        ///<summary>
        /// 创建用户主键
        ///</summary>
        [NonSerialized]
        public static string FieldCreateUserId = "CreateUserId";

        ///<summary>
        /// 修改日期
        ///</summary>
        [NonSerialized]
        public static string FieldModifiedOn = "ModifiedOn";

        ///<summary>
        /// 修改用户
        ///</summary>
        [NonSerialized]
        public static string FieldModifiedBy = "ModifiedBy";

        ///<summary>
        /// 修改用户主键
        ///</summary>
        [NonSerialized]
        public static string FieldModifiedUserId = "ModifiedUserId";
    }
}
