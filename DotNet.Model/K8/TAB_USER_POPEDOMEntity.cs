//-----------------------------------------------------------------------
// <copyright file="TAB_USERPOPEDOMEntity.cs" company="Hairihan">
//     Copyright (c) 2015 , All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System;
using System.ComponentModel.DataAnnotations;

namespace DotNet.Model
{
    using DotNet.Utilities;

    /// <summary>
    /// TAB_USERPOPEDOMEntity
    /// 用户权限
    /// 
    /// 修改纪录
    /// 
    /// 2014-09-01 版本：1.0 songliangliang 创建文件。
    /// 
    /// <author>
    ///     <name>songliangliang</name>
    ///     <date>2014-09-01</date>
    /// </author>
    /// </summary>
    public partial class TAB_USERPOPEDOMEntity : BaseEntity
    {
        /// <summary>
        /// 菜单的唯一标识
        /// </summary>
        [Key] 
        [ScaffoldColumn(false)] 
        public string MENU_GUID { get; set; }


        /// <summary>
        /// 菜单名称
        /// </summary>
        [Display(Name = "菜单名称")]
        [DataType(DataType.Text)]
        public string MENU_NAME { get; set; } 


        /// <summary>
        /// 所属网点
        /// </summary>
        [Display(Name = "所属网点")] 
        [Required(ErrorMessage = "需要输入所属网点")]
        [DataType(DataType.Text)]
        public string OWNER_SITE { get; set; } 

        /// <summary>
        /// 用户名
        /// </summary>
        [Display(Name = "用户名")] 
        [Required(ErrorMessage = "需要输入用户名")]
        [DataType(DataType.Text)]
        public string USER_NAME { get; set; } 

        /// <summary>
        /// 删除标识1表示能删除
        /// </summary>
        [Display(Name = "删除标识1表示能删除")] 
        [Required(ErrorMessage = "需要输入删除标识1表示能删除")]
        [DataType(DataType.Currency)]
        public Decimal BL_DELETE { get; set; } 

        /// <summary>
        /// 修改标识1表示能修改
        /// </summary>
        [Display(Name = "修改标识1表示能修改")] 
        [Required(ErrorMessage = "需要输入修改标识1表示能修改")]
        [DataType(DataType.Currency)]
        public Decimal BL_UPDATE { get; set; } 

        /// <summary>
        /// 添加标识1表示能添加
        /// </summary>
        [Display(Name = "添加标识1表示能添加")] 
        [Required(ErrorMessage = "需要输入添加标识1表示能添加")]
        [DataType(DataType.Currency)]
        public Decimal BL_INSERT { get; set; } 

        /// <summary>
        /// 从数据行读取
        /// </summary>
        /// <param name="dr">数据行</param>
        protected override BaseEntity GetFrom(IDataRow dr)
        {
            GetFromExpand(dr);
            MENU_GUID = BaseBusinessLogic.ConvertToString(dr[TAB_USERPOPEDOMEntity.FieldMENU_GUID]);
            OWNER_SITE = BaseBusinessLogic.ConvertToString(dr[TAB_USERPOPEDOMEntity.FieldOWNER_SITE]);
            USER_NAME = BaseBusinessLogic.ConvertToString(dr[TAB_USERPOPEDOMEntity.FieldUSER_NAME]);
            BL_DELETE = BaseBusinessLogic.ConvertToDecimal(dr[TAB_USERPOPEDOMEntity.FieldBL_DELETE]);
            BL_UPDATE = BaseBusinessLogic.ConvertToDecimal(dr[TAB_USERPOPEDOMEntity.FieldBL_UPDATE]);
            BL_INSERT = BaseBusinessLogic.ConvertToDecimal(dr[TAB_USERPOPEDOMEntity.FieldBL_INSERT]);
            return this;
        }

        ///<summary>
        /// 用户权限
        ///</summary>
        public static string TableName = "TAB_USER_POPEDOM";

        ///<summary>
        /// 菜单的唯一标识
        ///</summary>
        public static string FieldMENU_GUID = "MENU_GUID";

        ///<summary>
        /// 所属网点
        ///</summary>
        public static string FieldOWNER_SITE = "OWNER_SITE";

        ///<summary>
        /// 用户名
        ///</summary>
        public static string FieldUSER_NAME = "USER_NAME";

        ///<summary>
        /// 删除标识1表示能删除
        ///</summary>
        public static string FieldBL_DELETE = "BL_DELETE";

        ///<summary>
        /// 修改标识1表示能修改
        ///</summary>
        public static string FieldBL_UPDATE = "BL_UPDATE";

        ///<summary>
        /// 添加标识1表示能添加
        ///</summary>
        public static string FieldBL_INSERT = "BL_INSERT";
    }
}
