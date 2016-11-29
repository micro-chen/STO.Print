//-----------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2016 , Hairihan TECH, Ltd. 
//-----------------------------------------------------------------

using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace DotNet.Model
{
    using DotNet.Utilities;

    /// <summary>
    /// BaseAreaProvinceMarkEntity
    /// 地区表(省、市、县)
    ///
    /// 修改记录
    ///
    ///		2015-06-23 版本：1.0 JiRiGaLa 创建主键。
    ///
    /// <author>
    ///		<name>JiRiGaLa</name>
    ///		<date>2015-06-23</date>
    /// </author>
    /// </summary>
    [Serializable, DataContract]
    public partial class BaseAreaProvinceMarkEntity : BaseEntity
    {
        private string id = null;
        /// <summary>
        /// 主键
        /// </summary>
        [FieldDescription("主键", false)]
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

        private int areaId = 0;
        /// <summary>
        /// 区域主键
        /// </summary>
        [FieldDescription("区域主键")]
        [DataMember]
        public int AreaId
        {
            get
            {
                return this.areaId;
            }
            set
            {
                this.areaId = value;
            }
        }

        private string area = null;
        /// <summary>
        /// 区域
        /// </summary>
        [FieldDescription("区域")]
        [DataMember]
        public string Area
        {
            get
            {
                return this.area;
            }
            set
            {
                this.area = value;
            }
        }

        private int provinceId = 0;
        /// <summary>
        /// 省主键
        /// </summary>
        [FieldDescription("省主键")]
        [DataMember]
        public int ProvinceId
        {
            get
            {
                return this.provinceId;
            }
            set
            {
                this.provinceId = value;
            }
        }

        private string province = null;
        /// <summary>
        /// 省
        /// </summary>
        [FieldDescription("省")]
        [DataMember]
        public string Province
        {
            get
            {
                return this.province;
            }
            set
            {
                this.province = value;
            }
        }

        private string mark = null;
        /// <summary>
        /// 机打大头笔
        /// </summary>
        [FieldDescription("机打大头笔")]
        [DataMember]
        public string Mark
        {
            get
            {
                return this.mark;
            }
            set
            {
                this.mark = value;
            }
        }

        private string description = null;
        /// <summary>
        /// 备注
        /// </summary>
        [FieldDescription("备注")]
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

        private int? enabled = 1;
        /// <summary>
        /// 有效
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

        private DateTime? createOn = null;
        /// <summary>
        /// 创建日期
        /// </summary>
        [FieldDescription("创建日期", false)]
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

        private string createUserId = null;
        /// <summary>
        /// 创建用户主键
        /// </summary>
        [FieldDescription("创建用户主键", false)]
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
        [FieldDescription("创建用户", false)]
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

        private DateTime? modifiedOn = null;
        /// <summary>
        /// 修改日期
        /// </summary>
        [FieldDescription("修改日期", false)]
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

        private string modifiedUserId = null;
        /// <summary>
        /// 修改用户主键
        /// </summary>
        [FieldDescription("修改用户主键", false)]
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
        [FieldDescription("修改用户", false)]
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

        /// <summary>
        /// 从数据行读取
        /// </summary>
        /// <param name="dr">数据行</param>
        protected override BaseEntity GetFrom(IDataRow dr)
        {
            Id = BaseBusinessLogic.ConvertToString(dr[BaseAreaProvinceMarkEntity.FieldId]);
            AreaId = BaseBusinessLogic.ConvertToInt(dr[BaseAreaProvinceMarkEntity.FieldAreaId]);
            Area = BaseBusinessLogic.ConvertToString(dr[BaseAreaProvinceMarkEntity.FieldArea]);
            ProvinceId = BaseBusinessLogic.ConvertToInt(dr[BaseAreaProvinceMarkEntity.FieldProvinceId]);
            Province = BaseBusinessLogic.ConvertToString(dr[BaseAreaProvinceMarkEntity.FieldProvince]);
            Mark = BaseBusinessLogic.ConvertToString(dr[BaseAreaProvinceMarkEntity.FieldMark]);
            Description = BaseBusinessLogic.ConvertToString(dr[BaseAreaProvinceMarkEntity.FieldDescription]);
            Enabled = BaseBusinessLogic.ConvertToInt(dr[BaseAreaProvinceMarkEntity.FieldEnabled]);
            CreateOn = BaseBusinessLogic.ConvertToNullableDateTime(dr[BaseAreaProvinceMarkEntity.FieldCreateOn]);
            CreateUserId = BaseBusinessLogic.ConvertToString(dr[BaseAreaProvinceMarkEntity.FieldCreateUserId]);
            CreateBy = BaseBusinessLogic.ConvertToString(dr[BaseAreaProvinceMarkEntity.FieldCreateBy]);
            ModifiedOn = BaseBusinessLogic.ConvertToNullableDateTime(dr[BaseAreaProvinceMarkEntity.FieldModifiedOn]);
            ModifiedUserId = BaseBusinessLogic.ConvertToString(dr[BaseAreaProvinceMarkEntity.FieldModifiedUserId]);
            ModifiedBy = BaseBusinessLogic.ConvertToString(dr[BaseAreaProvinceMarkEntity.FieldModifiedBy]);
            // 获取扩展属性
            GetFromExpand(dr);
            return this;
        }

        ///<summary>
        /// 机打大头笔表
        ///</summary>
        [NonSerialized]
        [FieldDescription("机打大头笔表")]
        public static string TableName = "BaseArea_ProvinceMark";

        ///<summary>
        /// 主键
        ///</summary>
        [NonSerialized]
        [FieldDescription("主键")]
        public static string FieldId = "Id";

        ///<summary>
        /// 区域主键
        ///</summary>
        [NonSerialized]
        [FieldDescription("区域键")]
        public static string FieldAreaId = "AreaId";

        ///<summary>
        /// 区域
        ///</summary>
        [NonSerialized]
        [FieldDescription("区域")]
        public static string FieldArea = "Area";

        ///<summary>
        /// 省主键
        ///</summary>
        [NonSerialized]
        [FieldDescription("省主键")]
        public static string FieldProvinceId = "ProvinceId";

        ///<summary>
        /// 省
        ///</summary>
        [NonSerialized]
        [FieldDescription("省")]
        public static string FieldProvince = "Province";
        
        /// <summary>
        /// 手写大头笔
        /// </summary>
        [NonSerialized]
        [FieldDescription("机打大头笔")]
        public static string FieldMark = "Mark";

        ///<summary>
        /// 备注
        ///</summary>
        [NonSerialized]
        [FieldDescription("备注")]
        public static string FieldDescription = "Description";

        ///<summary>
        /// 有效
        ///</summary>
        [NonSerialized]
        [FieldDescription("有效")]
        public static string FieldEnabled = "Enabled";

        ///<summary>
        /// 创建日期
        ///</summary>
        [NonSerialized]
        [FieldDescription("创建日期")]
        public static string FieldCreateOn = "CreateOn";

        ///<summary>
        /// 创建用户主键
        ///</summary>
        [NonSerialized]
        [FieldDescription("创建用户主键")]
        public static string FieldCreateUserId = "CreateUserId";

        ///<summary>
        /// 创建用户
        ///</summary>
        [NonSerialized]
        [FieldDescription("创建用户")]
        public static string FieldCreateBy = "CreateBy";

        ///<summary>
        /// 修改日期
        ///</summary>
        [NonSerialized]
        [FieldDescription("修改日期")]
        public static string FieldModifiedOn = "ModifiedOn";

        ///<summary>
        /// 修改用户主键
        ///</summary>
        [NonSerialized]
        [FieldDescription("修改用户主键")]
        public static string FieldModifiedUserId = "ModifiedUserId";

        ///<summary>
        /// 修改用户
        ///</summary>
        [NonSerialized]
        [FieldDescription("修改用户")]
        public static string FieldModifiedBy = "ModifiedBy";
    }
}
