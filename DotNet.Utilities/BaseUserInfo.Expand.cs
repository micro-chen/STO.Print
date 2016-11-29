//-----------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2014 , Hairihan TECH, Ltd. 
//-----------------------------------------------------------------

using System;
using System.Configuration;
using System.Runtime.Serialization;
using System.Web.Script.Serialization;

namespace DotNet.Utilities
{
    /// <summary>
    /// BaseUserInfo
    /// 用户核心基础信息
    /// 
    /// 修改纪录
    /// 
    ///     2014.07.24 JiRiGaLa  版本：1.0 进行分离。
    ///		
    /// <author>
    ///		<name>JiRiGaLa</name>
    ///		<date>2014.07.24</date>
    /// </author> 
    /// </summary>
    public partial class BaseUserInfo
    {
        private string targetUserId = string.Empty;
        /// <summary>
        /// 目标用户
        /// </summary>
        [DataMember]
        public virtual string TargetUserId
        {
            get
            {
                return this.targetUserId;
            }
            set
            {
                this.targetUserId = value;
            }
        }

        private string subCompanyId = null;
        /// <summary>
        /// 分支机构主键
        /// </summary>
        [DataMember]
        public virtual string SubCompanyId
        {
            get
            {
                return this.subCompanyId;
            }
            set
            {
                this.subCompanyId = value;
            }
        }

        private string subCompanyCode = string.Empty;
        /// <summary>
        /// 分支机构编号
        /// </summary>
        [DataMember]
        public virtual string SubCompanyCode
        {
            get
            {
                return this.subCompanyCode;
            }
            set
            {
                this.subCompanyCode = value;
            }
        }

        private string subCompanyName = string.Empty;
        /// <summary>
        /// 分支机构名称
        /// </summary>
        [DataMember]
        public virtual string SubCompanyName
        {
            get
            {
                return this.subCompanyName;
            }
            set
            {
                this.subCompanyName = value;
            }
        }

        private string subDepartmentId = null;
        /// <summary>
        /// 当前的组织结构子部门主键
        /// </summary>
        [DataMember]
        public virtual string SubDepartmentId
        {
            get
            {
                return this.subDepartmentId;
            }
            set
            {
                this.subDepartmentId = value;
            }
        }

        private string subDepartmentCode = string.Empty;
        /// <summary>
        /// 当前的组织结构子部门编号
        /// </summary>
        [DataMember]
        public virtual string SubDepartmentCode
        {
            get
            {
                return this.subDepartmentCode;
            }
            set
            {
                this.subDepartmentCode = value;
            }
        }

        private string subDepartmentName = string.Empty;
        /// <summary>
        /// 当前的组织结构子部门名称
        /// </summary>
        [DataMember]
        public virtual string SubDepartmentName
        {
            get
            {
                return this.subDepartmentName;
            }
            set
            {
                this.subDepartmentName = value;
            }
        }

        private string workgroupId = null;
        /// <summary>
        /// 当前的组织结构工作组主键
        /// </summary>
        [DataMember]
        public virtual string WorkgroupId
        {
            get
            {
                return this.workgroupId;
            }
            set
            {
                this.workgroupId = value;
            }
        }

        private string workgroupCode = string.Empty;
        /// <summary>
        /// 当前的组织结构工作组编号
        /// </summary>
        [DataMember]
        public virtual string WorkgroupCode
        {
            get
            {
                return this.workgroupCode;
            }
            set
            {
                this.workgroupCode = value;
            }
        }

        private string workgroupName = string.Empty;
        /// <summary>
        /// 当前的组织结构工作组名称
        /// </summary>
        [DataMember]
        public virtual string WorkgroupName
        {
            get
            {
                return this.workgroupName;
            }
            set
            {
                this.workgroupName = value;
            }
        }

        private int securityLevel = 0;
        /// <summary>
        /// 安全级别
        /// </summary>
        [DataMember]
        public virtual int SecurityLevel
        {
            get
            {
                return this.securityLevel;
            }
            set
            {
                this.securityLevel = value;
            }
        }

        private string currentLanguage = string.Empty;
        /// <summary>
        /// 当前语言选择
        /// </summary>
        [DataMember]
        public virtual string CurrentLanguage
        {
            get
            {
                return this.currentLanguage;
            }
            set
            {
                this.currentLanguage = value;
            }
        }

        private string themes = string.Empty;
        /// <summary>
        /// 当前布局风格选择
        /// </summary>
        [DataMember]
        public virtual string Themes
        {
            get
            {
                return this.themes;
            }
            set
            {
                this.themes = value;
            }
        }
    }
}