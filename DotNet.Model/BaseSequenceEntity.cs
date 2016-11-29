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
    /// BaseSequenceEntity
    /// 序列产生器表
    /// 
    /// 修改记录
    /// 
    /// 2012-04-23 版本：1.0 JiRiGaLa 创建主键。
    /// 
    /// <author>
    ///     <name>JiRiGaLa</name>
    ///     <date>2012-04-23</date>
    /// </author>
    /// </summary>
    [Serializable, DataContract]
	public partial class BaseSequenceEntity : BaseEntity
    {
        /// <summary>
        /// 主键
        /// </summary>
        [DataMember]
        public String Id { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        [DataMember]
        public String FullName { get; set; }

        /// <summary>
        /// 序列号前缀
        /// </summary>
        [DataMember]
        public String Prefix { get; set; }

        /// <summary>
        /// 序列号分隔符
        /// </summary>
        [DataMember]
        public String Delimiter { get; set; }

        /// <summary>
        /// 升序序列
        /// </summary>
        [DataMember]
        public int? Sequence { get; set; }

        /// <summary>
        /// 倒序序列
        /// </summary>
        [DataMember]
        public int? Reduction { get; set; }

        /// <summary>
        /// 步骤
        /// </summary>
        [DataMember]
        public int? Step { get; set; }

        /// <summary>
        /// 是否显示
        /// </summary>
        [DataMember]
        public int? IsVisible { get; set; }

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

        /// <summary>
        /// 从数据行读取
        /// </summary>
        /// <param name="dr">数据行</param>
		protected override BaseEntity GetFrom(IDataRow dr)
        {
            Id = BaseBusinessLogic.ConvertToString(dr[BaseSequenceEntity.FieldId]);
            FullName = BaseBusinessLogic.ConvertToString(dr[BaseSequenceEntity.FieldFullName]);
            Prefix = BaseBusinessLogic.ConvertToString(dr[BaseSequenceEntity.FieldPrefix]);
            Delimiter = BaseBusinessLogic.ConvertToString(dr[BaseSequenceEntity.FieldDelimiter]);
            Sequence = BaseBusinessLogic.ConvertToInt(dr[BaseSequenceEntity.FieldSequence]);
            Reduction = BaseBusinessLogic.ConvertToInt(dr[BaseSequenceEntity.FieldReduction]);
            Step = BaseBusinessLogic.ConvertToInt(dr[BaseSequenceEntity.FieldStep]);
            IsVisible = BaseBusinessLogic.ConvertToInt(dr[BaseSequenceEntity.FieldIsVisible]);
            Description = BaseBusinessLogic.ConvertToString(dr[BaseSequenceEntity.FieldDescription]);
            CreateOn = BaseBusinessLogic.ConvertToNullableDateTime(dr[BaseSequenceEntity.FieldCreateOn]);
            CreateUserId = BaseBusinessLogic.ConvertToString(dr[BaseSequenceEntity.FieldCreateUserId]);
            CreateBy = BaseBusinessLogic.ConvertToString(dr[BaseSequenceEntity.FieldCreateBy]);
            ModifiedOn = BaseBusinessLogic.ConvertToNullableDateTime(dr[BaseSequenceEntity.FieldModifiedOn]);
            ModifiedUserId = BaseBusinessLogic.ConvertToString(dr[BaseSequenceEntity.FieldModifiedUserId]);
            ModifiedBy = BaseBusinessLogic.ConvertToString(dr[BaseSequenceEntity.FieldModifiedBy]);
            // 获取扩展属性
            GetFromExpand(dr);
            return this;
        }

        ///<summary>
        /// 序列产生器表
        ///</summary>
        [NonSerialized]
        public static string TableName = "basesequence";

        ///<summary>
        /// 主键
        ///</summary>
        [NonSerialized]
        public static string FieldId = "Id";

        ///<summary>
        /// 名称
        ///</summary>
        [NonSerialized]
        public static string FieldFullName = "FullName";

        ///<summary>
        /// 序列号前缀
        ///</summary>
        [NonSerialized]
        public static string FieldPrefix = "Prefix";

        ///<summary>
        /// 序列号分隔符
        ///</summary>
        [NonSerialized]
        public static string FieldDelimiter = "Delimiter";

        ///<summary>
        /// 升序序列
        ///</summary>
        [NonSerialized]
        public static string FieldSequence = "Sequence";

        ///<summary>
        /// 倒序序列
        ///</summary>
        [NonSerialized]
        public static string FieldReduction = "Reduction";

        ///<summary>
        /// 步骤
        ///</summary>
        [NonSerialized]
        public static string FieldStep = "Step";

        ///<summary>
        /// 是否显示
        ///</summary>
        [NonSerialized]
        public static string FieldIsVisible = "IsVisible";

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
