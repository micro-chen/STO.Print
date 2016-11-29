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
	/// BaseModuleEntity
	/// 模块（菜单）表
	/// 
	/// 修改记录
	/// 
	/// 2012-05-22 版本：1.0 JiRiGaLa 创建主键。
	/// 
	/// <author>
	///     <name>JiRiGaLa</name>
	///     <date>2012-05-22</date>
	/// </author>
	/// </summary>
	[Serializable, DataContract]
	public partial class BaseModuleEntity : BaseEntity
	{
		/// <summary>
		/// 主键
		/// </summary>
        [FieldDescription("主键", false)]
		[DataMember]
		public string Id { get; set; }

		/// <summary>
		/// 父节点主键
		/// </summary>
        [FieldDescription("父节点主键")]
		[DataMember]
		public string ParentId { get; set; }

		/// <summary>
		/// 编号
		/// </summary>
        [FieldDescription("编号")]
		[DataMember]
		public String Code { get; set; }

		/// <summary>
		/// 名称
		/// </summary>
        [FieldDescription("名称")]
		[DataMember]
		public String FullName { get; set; }

		/// <summary>
		/// 菜单分类System\Application
		/// </summary>
        [FieldDescription("分类")]
        [DataMember]
		public String CategoryCode { get; set; }

		/// <summary>
		/// 图标编号
		/// </summary>
        [FieldDescription("图标编号")]
		[DataMember]
		public String ImageIndex { get; set; }

		/// <summary>
		/// 选中状态图标编号
		/// </summary>
        [FieldDescription("选中状态图标编号")]
		[DataMember]
		public String SelectedImageIndex { get; set; }

		/// <summary>
		/// 导航地址(Web网址)[B\S]
		/// </summary>
        [FieldDescription("导航地址")]
		[DataMember]
		public String NavigateUrl { get; set; }

		/// <summary>
		/// 浏览器
		/// </summary>
		/// public String WebBrowser { get; set; }
		
		/// <summary>
		/// 图标图片地址[B\S]
		/// </summary>
        [FieldDescription("图标图片地址")]
		[DataMember]
		public String ImagUrl { get; set; }
		
		/// <summary>
		/// 目标窗体中打开[B\S]
		/// </summary>
        [FieldDescription("目标窗体")]
		[DataMember]
		public String Target { get; set; }

		/// <summary>
		/// 窗体名[C\S]
		/// </summary>
        [FieldDescription("窗体名")]
		[DataMember]
		public String FormName { get; set; }

		/// <summary>
		/// 动态连接库[C\S]
		/// </summary>
        [FieldDescription("动态连接库")]
		[DataMember]
		public String AssemblyName { get; set; }

		/// <summary>
		/// 需要数据权限过滤的表(,符号分割)
		/// </summary>
        // [FieldDescription("需要数据权限过滤的表")]
		// [DataMember]
		// public String PermissionScopeTables { get; set; }

		private int? authorizedDays = 0;
		/// <summary>
		/// 默认授权天数、默认多少天有数、0表示无限制
		/// </summary>
        [FieldDescription("默认授权天数")]
		[DataMember]
		public int? AuthorizedDays
		{
			get
			{
				return this.authorizedDays;
			}
			set
			{
				this.authorizedDays = value;
			}
		}

		/// <summary>
		/// 排序码
		/// </summary>
        [FieldDescription("排序码", false)]
		[DataMember]
		public int? SortCode { get; set; }

		private int? enabled = 1;
		/// <summary>
		/// 有效标志
		/// </summary>
        [FieldDescription("有效")]
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

        private int? deletionStateCode = 0;
        /// <summary>
        /// 删除标志
        /// </summary>
        [FieldDescription("删除标志", false)]
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

		private int? isMenu = 0;
		/// <summary>
		/// 是菜单项
		/// </summary>
        [FieldDescription("是菜单项")]
		[DataMember]
		public int? IsMenu
		{
			get
			{
				return this.isMenu;
			}
			set
			{
				this.isMenu = value;
			}
		}

		private int? isPublic = 0;
		/// <summary>
		/// 是公开
		/// </summary>
        [FieldDescription("是公开")]
		[DataMember]
		public int? IsPublic
		{
			get
			{
				return this.isPublic;
			}
			set
			{
				this.isPublic = value;
			}
		}

		private int? isVisible = 1;
		/// <summary>
		/// 是可见
		/// </summary>
        [FieldDescription("是可见")]
		[DataMember]
		public int? IsVisible
		{
			get
			{
				return this.isVisible;
			}
			set
			{
				this.isVisible = value;
			}
		}

		private int? isScope = 0;
		/// <summary>
		/// 权限域（需要数据权限）
		/// </summary>
        [FieldDescription("权限域")]
		[DataMember]
		public int? IsScope
		{
			get
			{
				return this.isScope;
			}
			set
			{
				this.isScope = value;
			}
		}

		private DateTime? lastCall = null;
		/// <summary>
		/// 最后被调用日期
		/// </summary>
        [FieldDescription("最后被调用日期", false)]
		[DataMember]
		public DateTime? LastCall
		{
			get
			{
				return this.lastCall;
			}
			set
			{
				this.lastCall = value;
			}
		}

		private int? expand = 0;
		/// <summary>
		/// 展开状态
		/// </summary>
        [FieldDescription("展开状态")]
		[DataMember]
		public int? Expand
		{
			get
			{
				return this.expand;
			}
			set
			{
				this.expand = value;
			}
		}

		private int? allowEdit = 1;
		/// <summary>
		/// 允许编辑
		/// </summary>
        [FieldDescription("允许编辑", false)]
		[DataMember]
		public int? AllowEdit
		{
			get
			{
				return this.allowEdit;
			}
			set
			{
				this.allowEdit = value;
			}
		}

		private int? allowDelete = 1;
		/// <summary>
		/// 允许删除
		/// </summary>
        [FieldDescription("允许删除", false)]
		[DataMember]
		public int? AllowDelete
		{
			get
			{
				return this.allowDelete;
			}
			set
			{
				this.allowDelete = value;
			}
		}

		/// <summary>
		/// 备注
		/// </summary>
        [FieldDescription("备注")]
		[DataMember]
		public String Description { get; set; }

		/// <summary>
		/// 创建日期
		/// </summary>
        [FieldDescription("创建日期", false)]
		[DataMember]
		public DateTime? CreateOn { get; set; }

		/// <summary>
		/// 创建用户主键
		/// </summary>
        [FieldDescription("创建用户主键", false)]
		[DataMember]
		public String CreateUserId { get; set; }

		/// <summary>
		/// 创建用户
		/// </summary>
        [FieldDescription("创建用户", false)]
		[DataMember]
		public String CreateBy { get; set; }

		/// <summary>
		/// 修改日期
		/// </summary>
        [FieldDescription("修改日期", false)]
		[DataMember]
		public DateTime? ModifiedOn { get; set; }

		/// <summary>
		/// 修改用户主键
		/// </summary>
        [FieldDescription("修改用户主键", false)]
		[DataMember]
		public String ModifiedUserId { get; set; }

		/// <summary>
		/// 修改用户
		/// </summary>
        [FieldDescription("修改用户", false)]
		[DataMember]
		public String ModifiedBy { get; set; }

		/// <summary>
		/// 从数据行读取
		/// </summary>
		/// <param name="dr">数据行</param>
		protected override BaseEntity GetFrom(IDataRow dr)
		{
            Id = BaseBusinessLogic.ConvertToString(dr[BaseModuleEntity.FieldId]);
            ParentId = BaseBusinessLogic.ConvertToString(dr[BaseModuleEntity.FieldParentId]);
			Code = BaseBusinessLogic.ConvertToString(dr[BaseModuleEntity.FieldCode]);
			FullName = BaseBusinessLogic.ConvertToString(dr[BaseModuleEntity.FieldFullName]);
			CategoryCode = BaseBusinessLogic.ConvertToString(dr[BaseModuleEntity.FieldCategoryCode]);
			ImageIndex = BaseBusinessLogic.ConvertToString(dr[BaseModuleEntity.FieldImageIndex]);
			SelectedImageIndex = BaseBusinessLogic.ConvertToString(dr[BaseModuleEntity.FieldSelectedImageIndex]);
			NavigateUrl = BaseBusinessLogic.ConvertToString(dr[BaseModuleEntity.FieldNavigateUrl]);
			/// WebBrowser = BaseBusinessLogic.ConvertToString(dr[BaseModuleEntity.FieldWebBrowser]);
			ImagUrl = BaseBusinessLogic.ConvertToString(dr[BaseModuleEntity.FieldImagUrl]);
			Target = BaseBusinessLogic.ConvertToString(dr[BaseModuleEntity.FieldTarget]);
			FormName = BaseBusinessLogic.ConvertToString(dr[BaseModuleEntity.FieldFormName]);
			AssemblyName = BaseBusinessLogic.ConvertToString(dr[BaseModuleEntity.FieldAssemblyName]);
			// PermissionScopeTables = BaseBusinessLogic.ConvertToString(dr[BaseModuleEntity.FieldPermissionScopeTables]);
			AuthorizedDays = BaseBusinessLogic.ConvertToNullableInt(dr[BaseModuleEntity.FieldAuthorizedDays]);
			SortCode = BaseBusinessLogic.ConvertToInt(dr[BaseModuleEntity.FieldSortCode]);
			Enabled = BaseBusinessLogic.ConvertToInt(dr[BaseModuleEntity.FieldEnabled]);
			DeletionStateCode = BaseBusinessLogic.ConvertToInt(dr[BaseModuleEntity.FieldDeletionStateCode]);
			IsMenu = BaseBusinessLogic.ConvertToInt(dr[BaseModuleEntity.FieldIsMenu]);
			IsPublic = BaseBusinessLogic.ConvertToInt(dr[BaseModuleEntity.FieldIsPublic]);
			IsScope = BaseBusinessLogic.ConvertToInt(dr[BaseModuleEntity.FieldIsScope]);
			IsVisible = BaseBusinessLogic.ConvertToInt(dr[BaseModuleEntity.FieldIsVisible]);
			Expand = BaseBusinessLogic.ConvertToInt(dr[BaseModuleEntity.FieldExpand]);
			AllowEdit = BaseBusinessLogic.ConvertToInt(dr[BaseModuleEntity.FieldAllowEdit]);
			AllowDelete = BaseBusinessLogic.ConvertToInt(dr[BaseModuleEntity.FieldAllowDelete]);
			Description = BaseBusinessLogic.ConvertToString(dr[BaseModuleEntity.FieldDescription]);
			CreateOn = BaseBusinessLogic.ConvertToNullableDateTime(dr[BaseModuleEntity.FieldCreateOn]);
			CreateUserId = BaseBusinessLogic.ConvertToString(dr[BaseModuleEntity.FieldCreateUserId]);
			CreateBy = BaseBusinessLogic.ConvertToString(dr[BaseModuleEntity.FieldCreateBy]);
			ModifiedOn = BaseBusinessLogic.ConvertToNullableDateTime(dr[BaseModuleEntity.FieldModifiedOn]);
			ModifiedUserId = BaseBusinessLogic.ConvertToString(dr[BaseModuleEntity.FieldModifiedUserId]);
			ModifiedBy = BaseBusinessLogic.ConvertToString(dr[BaseModuleEntity.FieldModifiedBy]);
			// 获取扩展属性
			GetFromExpand(dr);
			return this;
		}

		///<summary>
		/// 模块（菜单）表
		///</summary>
		[NonSerialized]
        public static string TableName = "BaseModule";

		///<summary>
		/// 主键
		///</summary>
		[NonSerialized]
		public static string FieldId = "Id";

		///<summary>
		/// 父节点主键
		///</summary>
		[NonSerialized]
		public static string FieldParentId = "ParentId";

		///<summary>
		/// 编号
		///</summary>
		[NonSerialized]
		public static string FieldCode = "Code";

		///<summary>
		/// 分类
		///</summary>
		[NonSerialized]
		public static string FieldCategoryCode = "CategoryCode";        

		///<summary>
		/// 名称
		///</summary>
		[NonSerialized]
		public static string FieldFullName = "FullName";

		///<summary>
		/// 图标编号
		///</summary>
		[NonSerialized]
		public static string FieldImageIndex = "ImageIndex";

		///<summary>
		/// 选中状态图标编号
		///</summary>
		[NonSerialized]
		public static string FieldSelectedImageIndex = "SelectedImageIndex";

		///<summary>
		/// 导航地址(Web网址)[B\S]
		///</summary>
		[NonSerialized]
		public static string FieldNavigateUrl = "NavigateUrl";

		///<summary>
		/// 浏览器
		///</summary>
		/// [NonSerialized]
		/// public static string FieldWebBrowser = "WebBrowser";

		///<summary>
		/// 图标图片地址[B\S]
		///</summary>
		[NonSerialized]
		public static string FieldImagUrl = "ImagUrl";

		///<summary>
		/// 目标窗体中打开[B\S]
		///</summary>
		[NonSerialized]
		public static string FieldTarget = "Target";

		///<summary>
		/// 窗体名[C\S]
		///</summary>
		[NonSerialized]
		public static string FieldFormName = "FormName";

		///<summary>
		/// 动态连接库[C\S]
		///</summary>
		[NonSerialized]
		public static string FieldAssemblyName = "AssemblyName";

		///<summary>
		/// 需要数据权限过滤的表(,符号分割)
		///</summary>
		// [NonSerialized]
		// public static string FieldPermissionScopeTables = "PermissionScopeTables";

		///<summary>
		/// 最后呼叫时间
		///</summary>
		[NonSerialized]
		public static string FieldLastCall = "LastCall";

		/// <summary>
		/// 默认授权天数、默认多少天有数、0表示无限制
		/// </summary>
		[NonSerialized]
		public static string FieldAuthorizedDays = "AuthorizedDays";

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
		/// 是菜单项
		///</summary>
		[NonSerialized]
		public static string FieldIsMenu = "IsMenu";

		///<summary>
		/// 是公开
		///</summary>
		[NonSerialized]
		public static string FieldIsPublic = "IsPublic";

		///<summary>
		/// 展开状态
		///</summary>
		[NonSerialized]
		public static string FieldExpand = "Expand";

		///<summary>
		/// 权限域
		///</summary>
		[NonSerialized]
		public static string FieldIsScope = "IsScope";

		///<summary>
		/// 是否可见
		///</summary>
		[NonSerialized]
		public static string FieldIsVisible = "IsVisible";

		///<summary>
		/// 允许编辑
		///</summary>
		[NonSerialized]
		public static string FieldAllowEdit = "AllowEdit";

		///<summary>
		/// 允许删除
		///</summary>
		[NonSerialized]
		public static string FieldAllowDelete = "AllowDelete";

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
	}
}
