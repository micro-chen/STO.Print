//-----------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2016 , Hairihan TECH, Ltd.  
//-----------------------------------------------------------------

using System.Runtime.Serialization;

namespace DotNet.Utilities
{
    /// <summary>
    /// BaseUserInfo
    /// 用户核心基础信息
    /// 
    /// 修改记录
    /// 
    ///     2015.04.08 JiRiGaLa  版本：3.7 修改注释方式，可以在其他类调用的时候显示其参数中文名称。
    ///		
    /// <author>
    ///		<name>JiRiGaLa</name>
    ///		<date>2015.04.08</date>
    /// </author> 
    /// </summary>
    public partial class BaseUserInfo
    {
        private string constraint = "50";
        /// <summary>
        /// 每天可以查看的次数
        /// </summary>
        [DataMember]
        public virtual string Constraint
        {
            get
            {
                return this.constraint;
            }
            set
            {
                this.constraint = value;
            }
        }

        private bool permission = false;
        /// <summary>
        /// 查看淘宝信息权限
        /// </summary>
        [DataMember]
        public virtual bool Permission
        {
            get
            {
                return this.permission;
            }
            set
            {
                this.permission = value;
            }
        }

        private string message = "未提交身份证照片认证";
        /// <summary>
        /// 审核信息
        /// </summary>
        [DataMember]
        public virtual string Message
        {
            get
            {
                return this.message;
            }
            set
            {
                this.message = value;
            }
        }
    }
}