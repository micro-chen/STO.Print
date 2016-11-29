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
    ///	BaseExceptionEntity
    /// 异常记录（程序OK）
    ///
    /// 修改记录
    /// 
    ///     2008.06.18 版本：3.3 JiRiGaLa   按标准格式建立表结构。
    ///     2008.04.22 版本：3.2 JiRiGaLa   在新的数据库连接里保存异常，不影响其它程序逻辑的事务处理。
    ///     2007.12.03 版本：3.1 JiRiGaLa   吉日整理主键规范化。
    ///		2007.08.25 版本：3.0 JiRiGaLa   WriteException 本地写入异常信息。
    ///		2007.08.01 版本：2.0 JiRiGaLa   LogException 时判断 ConnectionStat，对函数方法进行优化。
    ///		2007.06.07 版本：1.3 JiRiGaLa   进行EventLog主键优化。
    ///     2007.06.07 版本：1.2 JiRiGaLa   重新整理主键。
    ///		2006.02.06 版本：1.1 JiRiGaLa   重新调整主键的规范化。
    ///		2004.11.04 版本：1.0 JiRiGaLa   建立表结构，准备着手记录系统中的异常处理部分。
    ///		2005.08.13 版本：1.0 JiRiGaLa   整理主键。
    /// 
    /// <author>
    ///		<name>JiRiGaLa</name>
    ///		<date>2008.06.1</date>
    /// </author> 
    /// </summary>
    [Serializable, DataContract]
	public partial class BaseExceptionEntity : BaseEntity
    {
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

        private string systemCode = string.Empty;
        /// <summary>
        /// 系统编号
        /// </summary>
        [DataMember]
        public string SystemCode
        {
            get
            {
                return this.systemCode;
            }
            set
            {
                this.systemCode = value;
            }
        }

        private string title = string.Empty;
        /// <summary>
        /// 标题
        /// </summary>
        [DataMember]
        public string Title
        {
            get
            {
                return this.title;
            }
            set
            {
                this.title = value;
            }
        }

        private string message = string.Empty;
        /// <summary>
        /// 消息
        /// </summary>
        [DataMember]
        public string Message
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

        private string formattedMessage = string.Empty;
        /// <summary>
        /// 格式化消息
        /// </summary>
        [DataMember]
        public string FormattedMessage
        {
            get
            {
                return this.formattedMessage;
            }
            set
            {
                this.formattedMessage = value;
            }
        }

        private string stackTrace = string.Empty;
        /// <summary>
        /// 格式化消息
        /// </summary>
        [DataMember]
        public string StackTrace
        {
            get
            {
                return this.stackTrace;
            }
            set
            {
                this.stackTrace = value;
            }
        }

        private string ipAddress = string.Empty;
        /// <summary>
        /// IPAddress
        /// </summary>
        [DataMember]
        public string IPAddress
        {
            get
            {
                return this.ipAddress;
            }
            set
            {
                this.ipAddress = value;
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

        private string createBy = string.Empty;
        /// <summary>
        /// 创建者
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

        private string createOn = string.Empty;
        /// <summary>
        /// 创建时间
        /// </summary>
        [DataMember]
        public string CreateOn
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

        #region public void ClearProperty()
        /// <summary>
        /// 清除内容
        /// </summary>
        public void ClearProperty()
        {
            this.Id = string.Empty;
            this.CreateBy = string.Empty;
        }
		#endregion

        #region public BaseExceptionEntity GetFrom(DataRow dr) 从数据行读取
        /// <summary>
        /// 从数据行读取
        /// </summary>
        /// <param name="dr">数据行</param>
        /// <returns>异常的基类表结构定义</returns>
		protected override BaseEntity GetFrom(IDataRow dr)
        {
            Id = BaseBusinessLogic.ConvertToString(dr[BaseExceptionEntity.FieldId]);
            SystemCode = BaseBusinessLogic.ConvertToString(dr[BaseExceptionEntity.FieldSystemCode]);
            Message = BaseBusinessLogic.ConvertToString(dr[BaseExceptionEntity.FieldMessage]);
            FormattedMessage = BaseBusinessLogic.ConvertToString(dr[BaseExceptionEntity.FieldFormattedMessage]);
            IPAddress = BaseBusinessLogic.ConvertToString(dr[BaseExceptionEntity.FieldIPAddress]);
            CreateOn = BaseBusinessLogic.ConvertToString(dr[BaseExceptionEntity.FieldCreateOn]);
            CreateUserId = BaseBusinessLogic.ConvertToString(dr[BaseExceptionEntity.FieldCreateUserId]);
            CreateBy = BaseBusinessLogic.ConvertToString(dr[BaseExceptionEntity.FieldCreateBy]);
            // 获取扩展属性
            GetFromExpand(dr);
            return this;
        }
        #endregion

        [NonSerialized]
        public static string TableName = "BaseException";

        [NonSerialized]
        public static string FieldId = "LogId";

        [NonSerialized]
        public static string FieldSystemCode = "SystemCode";

        [NonSerialized]
        public static string FieldEventID = "EventId";

        [NonSerialized]
        public static string FieldCategory = "Category";

        [NonSerialized]
        public static string FieldPriority = "Priority";

        [NonSerialized]
        public static string FieldSeverity = "Severity";

        [NonSerialized]
        public static string FieldTitle = "Title";

        [NonSerialized]
        public static string FieldTimestamp = "Timestamp";

        [NonSerialized]
        public static string FieldMachineName = "MachineName";

        [NonSerialized]
        public static string FieldIPAddress = "IPAddress";

        [NonSerialized]
        public static string FieldAppDomainName = "AppDomainName";

        [NonSerialized]
        public static string FieldProcessId = "ProcessId";

        [NonSerialized]
        public static string FieldProcessName = "ProcessName";

        [NonSerialized]
        public static string FieldThreadName = "ThreadName";

        [NonSerialized]
        public static string FieldWin32ThreadId = "Win32ThreadId";

        [NonSerialized]
        public static string FieldMessage = "Message";

        [NonSerialized]
        public static string FieldFormattedMessage = "FormattedMessage";

        [NonSerialized]
        public static string FieldCreateUserId = "CreateUserId";

        [NonSerialized]
        public static string FieldCreateBy = "CreateBy"; 

        [NonSerialized]
        public static string FieldCreateOn = "CreateOn";
    }
}