//-----------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2016 , Hairihan TECH, Ltd.  
//-----------------------------------------------------------------

using System;
using System.Data;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace DotNet.Model
{
    using DotNet.Utilities;

    /// <summary>
    /// BaseParameterEntity
    /// 参数表的基类结构定义
    ///
    /// 修改记录
    /// 
    ///     2014.08.25 版本：2.2 JiRiGaLa   修改日期，创建日期进行日期类型规范化。
    ///     2011.07.05 版本：2.1 zgl        修改enable  默认值为true
    ///     2007.06.07 版本：2.0 JiRiGaLa   字段名变更
    ///		2006.02.05 版本：1.1 JiRiGaLa	重新调整主键的规范化。
    ///		2004.08.29 版本：1.0 JiRiGaLa	主键ID需要进行倒序排序，这样数据库管理员很方便地找到最新的纪录及被修改的纪录。
    ///										CategoryId 需要进行外键数据库完整性约束。
    ///										CreateOn 需要进行有默认值，不需要赋值也能获得当前的系统时间。
    ///										CreateUserId、ModifiedUserId 需要有外件数据库完整性约束。
    ///										Content、CreateUserId 不可以为空，减少垃圾数据。
    ///		2005.08.13 版本：1.0 JiRiGaLa	增加版权信息。
    /// 
    /// <author>
    ///		<name>JiRiGaLa</name>
    ///		<date>2014.08.25</date>
    /// </author> 
    /// </summary>
    [Serializable, DataContract]
	public partial class BaseParameterEntity : BaseEntity
    {
        #region public void ClearProperty(BaseParameterEntity entity)
        /// <summary>
        /// 清除内容
        /// <param name="entity">实体</param>
        /// </summary>
        public void ClearProperty(BaseParameterEntity entity)
        {
            entity.Id = string.Empty;
            entity.CategoryCode = string.Empty;
            entity.ParameterId = string.Empty;
            entity.ParameterCode = string.Empty;
            entity.ParameterContent = string.Empty;
            entity.Worked = false;
            entity.Enabled = false;
            entity.SortCode = null;
            entity.Description = string.Empty;
            entity.CreateUserId = string.Empty;
            entity.CreateOn = null;
            entity.ModifiedUserId = string.Empty;
            entity.ModifiedOn = null;
        }
        #endregion

        private string id = string.Empty;
        /// <summary>
        /// 主键
        /// </summary>
        [DataMember]
        public string Id
        {
            get
            {
                return this.id;
            }
            set
            {
                this.id = value;
            }
        }

        private string categoryCode = string.Empty;
        /// <summary>
        /// 分类编号
        /// </summary>
        [DataMember]
        public string CategoryCode
        {
            get
            {
                return this.categoryCode;
            }
            set
            {
                this.categoryCode = value;
            }
        }

        private string parameterId = string.Empty;
        /// <summary>
        /// 参数主键
        /// </summary>
        [DataMember]
        public string ParameterId
        {
            get
            {
                return this.parameterId;
            }
            set
            {
                this.parameterId = value;
            }
        }

        private string parameterCode = string.Empty;
        /// <summary>
        /// 参数编码
        /// </summary>
        [DataMember]
        public string ParameterCode
        {
            get
            {
                return this.parameterCode;
            }
            set
            {
                this.parameterCode = value;
            }
        }

        private string parameterContent = string.Empty;
        /// <summary>
        /// 参数编码
        /// </summary>
        [DataMember]
        public string ParameterContent
        {
            get
            {
                return this.parameterContent;
            }
            set
            {
                this.parameterContent = value;
            }
        }

        private bool worked = false;
        /// <summary>
        /// 处理状态
        /// </summary>
        [DataMember]
        public bool Worked
        {
            get
            {
                return this.worked;
            }
            set
            {
                this.worked = value;
            }
        }

        private bool enabled = true;
        /// <summary>
        /// 有效
        /// </summary>
        [DataMember]
        public bool Enabled
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

        private string description = string.Empty;
        /// <summary>
        /// 备注
        /// </summary>
        [DataMember]
        public string Description
        {
            get
            {
                return this.description;
            }
            set
            {
                this.description = value;
            }
        }

        private int? sortCode = 0;
        /// <summary>
        /// 排序码
        /// </summary>
        [DataMember]
        public int? SortCode
        {
            get
            {
                return this.sortCode;
            }
            set
            {
                this.sortCode = value;
            }
        }

        private string createUserId = string.Empty;
        /// <summary>
        /// 创建者主键
        /// </summary>
        [DataMember]
        public string CreateUserId
        {
            get
            {
                return this.createUserId;
            }
            set
            {
                this.createUserId = value;
            }
        }

        private string createBy = null;
        /// <summary>
        /// 创建用户
        /// </summary>
        [DataMember]
        public string CreateBy
        {
            get
            {
                return this.createBy;
            }
            set
            {
                this.createBy = value;
            }
        }

        private DateTime? createOn = null;
        /// <summary>
        /// 创建日期
        /// </summary>
        [DataMember]
        public DateTime? CreateOn
        {
            get
            {
                return this.createOn;
            }
            set
            {
                this.createOn = value;
            }
        }

        private string modifiedUserId = string.Empty;
        /// <summary>
        /// 最后修改者主键
        /// </summary>
        [DataMember]
        public string ModifiedUserId
        {
            get
            {
                return this.modifiedUserId;
            }
            set
            {
                this.modifiedUserId = value;
            }
        }

        private string modifiedBy = null;
        /// <summary>
        /// 修改用户
        /// </summary>
        [DataMember]
        public string ModifiedBy
        {
            get
            {
                return this.modifiedBy;
            }
            set
            {
                this.modifiedBy = value;
            }
        }

        private DateTime? modifiedOn = null;
        /// <summary>
        /// 修改日期
        /// </summary>
        [DataMember]
        public DateTime? ModifiedOn
        {
            get
            {
                return this.modifiedOn;
            }
            set
            {
                this.modifiedOn = value;
            }
        }

        #region public BaseParameterEntity GetFrom(DataRow dr)
        /// <summary>
        /// 从数据行读取
        /// </summary>
        /// <param name="dr">数据行</param>
        /// <returns>BaseParameterEntity</returns>
		protected override BaseEntity GetFrom(IDataRow dr)
        {
            Id = BaseBusinessLogic.ConvertToString(dr[BaseParameterEntity.FieldId]);
            CategoryCode = BaseBusinessLogic.ConvertToString(dr[BaseParameterEntity.FieldCategoryCode]);
            ParameterId = BaseBusinessLogic.ConvertToString(dr[BaseParameterEntity.FieldParameterId]);
            ParameterCode = BaseBusinessLogic.ConvertToString(dr[BaseParameterEntity.FieldParameterCode]);
            ParameterContent = BaseBusinessLogic.ConvertToString(dr[BaseParameterEntity.FieldParameterContent]);
            Worked = BaseBusinessLogic.ConvertIntToBoolean(dr[BaseParameterEntity.FieldWorked]);
            Enabled = BaseBusinessLogic.ConvertIntToBoolean(dr[BaseParameterEntity.FieldEnabled]);
            SortCode = BaseBusinessLogic.ConvertToNullableInt(dr[BaseParameterEntity.FieldSortCode]);
            Description = BaseBusinessLogic.ConvertToString(dr[BaseParameterEntity.FieldDescription]);
            CreateUserId = BaseBusinessLogic.ConvertToString(dr[BaseParameterEntity.FieldCreateUserId]);
            CreateOn = BaseBusinessLogic.ConvertToNullableDateTime(dr[BaseParameterEntity.FieldCreateOn]);
            CreateBy = BaseBusinessLogic.ConvertToString(dr[BaseParameterEntity.FieldCreateBy]);
            ModifiedUserId = BaseBusinessLogic.ConvertToString(dr[BaseParameterEntity.FieldModifiedUserId]);
            ModifiedOn = BaseBusinessLogic.ConvertToNullableDateTime(dr[BaseParameterEntity.FieldModifiedOn]);
            ModifiedBy = BaseBusinessLogic.ConvertToString(dr[BaseParameterEntity.FieldModifiedBy]);
            // 获取扩展属性
            GetFromExpand(dr);
            return this;
        }
        #endregion

        /// <summary>
        /// 表名
        /// </summary>
        [NonSerialized]
        public static string TableName = "BaseParameter";

        /// <summary>
        /// 主键
        /// </summary>
        [NonSerialized]
        public static string FieldId = "Id";

        /// <summary>
        /// 类别主键
        /// </summary>
        [NonSerialized]
        public static string FieldCategoryCode = "CategoryCode";

        /// <summary>
        /// 参数主键
        /// </summary>
        [NonSerialized]
        public static string FieldParameterId = "ParameterId";

        /// <summary>
        /// 参数编码
        /// </summary>
        [NonSerialized]
        public static string FieldParameterCode = "ParameterCode";

        /// <summary>
        /// 参数内容
        /// </summary>
        [NonSerialized]
        public static string FieldParameterContent = "ParameterContent";

        /// <summary>
        /// 处理状态
        /// </summary>
        [NonSerialized]
        public static string FieldWorked = "Worked";

        /// <summary>
        /// 有效性
        /// </summary>
        [NonSerialized]
        public static string FieldEnabled = "Enabled";

        /// <summary>
        /// 备注
        /// </summary>
        [NonSerialized]
        public static string FieldDescription = "Description";

        ///<summary>
        /// 删除标志
        ///</summary>
        [NonSerialized]
        public static string FieldDeletionStateCode = "DeletionStateCode";

        /// <summary>
        /// 排序码
        /// </summary>
        [NonSerialized]
        public static string FieldSortCode = "SortCode";

        /// <summary>
        /// 创建者
        /// </summary>
        [NonSerialized]
        public static string FieldCreateBy = "CreateBy";

        /// <summary>
        /// 创建者主键
        /// </summary>
        [NonSerialized]
        public static string FieldCreateUserId = "CreateUserId";

        /// <summary>
        /// 创建时间
        /// </summary>
        [NonSerialized]
        public static string FieldCreateOn = "CreateOn";

        /// <summary>
        /// 最后修改者主键
        /// </summary>
        [NonSerialized]
        public static string FieldModifiedUserId = "ModifiedUserId";

        /// <summary>
        /// 最后修改者
        /// </summary>
        [NonSerialized]
        public static string FieldModifiedBy = "ModifiedBy";

        /// <summary>
        /// 最后修改时间
        /// </summary>
        [NonSerialized]
        public static string FieldModifiedOn = "ModifiedOn";
    }
}