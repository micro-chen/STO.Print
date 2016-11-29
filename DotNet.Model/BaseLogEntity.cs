//-----------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2016 , Hairihan TECH, Ltd.  
//-----------------------------------------------------------------

using System;
using System.Runtime.Serialization;

namespace DotNet.Model
{
    using DotNet.Utilities;

    /// <summary>
    /// BaseLogEntity
    /// 日志的基类（程序OK）
    /// 
    /// 想在这里实现访问记录、继承以前的比较好的思路。
    ///
    /// 修改记录
    /// 
    ///     2016.02.13 版本：2.7 JiRiGaLa   增加字段 TaskId、Service、ElapsedTicks。
    ///     2011.03.24 版本：2.6 JiRiGaLa   讲程序转移到DotNet.BaseManager命名空间中。
    ///     2007.12.03 版本：2.3 JiRiGaLa   进行规范化整理。
    ///     2007.11.08 版本：2.2 JiRiGaLa   整理多余的主键（OK）。
    ///		2007.07.09 版本：2.1 JiRiGaLa   程序整理，修改 Static 方法，采用 Instance 方法。
    ///		2006.12.02 版本：2.0 JiRiGaLa   程序整理，错误方法修改。
    ///		2004.07.28 版本：1.0 JiRiGaLa   进行了排版、方法规范化、接口继承、索引器。
    ///		2004.11.12 版本：1.0 JiRiGaLa   删除了一些方法。
    ///		2005.09.30 版本：1.0 JiRiGaLa   又进行一次飞跃，把一些思想进行了统一。
    /// 
    /// <author>
    ///		<name>JiRiGaLa</name>
    ///		<date>2016.02.13</date>
    /// </author> 
    /// </summary>
    [Serializable, DataContract]
    public partial class BaseLogEntity : BaseEntity
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

        private string companyId = null;
        /// <summary>
        /// 公司主键
        /// </summary>
        [FieldDescription("公司主键")]
        [DataMember]
        public string CompanyId
        {
            get
            {
                return this.companyId;
            }
            set
            {
                this.companyId = value;
            }
        }

        private string userId = string.Empty;
        /// <summary>
        /// 用户主键
        /// </summary>
        [DataMember]
        public string UserId
        {
            get
            {
                return this.userId;
            }
            set
            {
                this.userId = value;
            }
        }

        private string userRealName = string.Empty;
        /// <summary>
        /// 用户姓名
        /// </summary>
        [DataMember]
        public string UserRealName
        {
            get
            {
                return this.userRealName;
            }
            set
            {
                this.userRealName = value;
            }
        }

        private string service = string.Empty;
        /// <summary>
        /// 服务
        /// </summary>
        [DataMember]
        public string Service
        {
            get
            {
                return this.service;
            }
            set
            {
                this.service = value;
            }
        }

        private string taskId = string.Empty;
        /// <summary>
        /// 任务
        /// </summary>
        [DataMember]
        public string TaskId
        {
            get
            {
                return this.taskId;
            }
            set
            {
                this.taskId = value;
            }
        }

        private string parameters = string.Empty;
        /// <summary>
        /// 操作记录,添加,编辑,删除参数
        /// </summary>
        [DataMember]
        public string Parameters
        {
            get
            {
                return this.parameters;
            }
            set
            {
                this.parameters = value;
            }
        }

        private string clientIP = string.Empty;
        /// <summary>
        /// IP地址
        /// </summary>
        [DataMember]
        public string ClientIP
        {
            get
            {
                return this.clientIP;
            }
            set
            {
                this.clientIP = value;
            }
        }

        private string serverIP = string.Empty;
        /// <summary>
        /// IP地址
        /// </summary>
        [DataMember]
        public string ServerIP
        {
            get
            {
                return this.serverIP;
            }
            set
            {
                this.serverIP = value;
            }
        }

        private string urlReferrer = string.Empty;
        /// <summary>
        /// 上一网络地址
        /// </summary>
        [DataMember]
        public string UrlReferrer
        {
            get
            {
                return this.urlReferrer;
            }
            set
            {
                this.urlReferrer = value;
            }
        }

        private string webUrl = string.Empty;
        /// <summary>
        /// 网络地址
        /// </summary>
        [DataMember]
        public string WebUrl
        {
            get
            {
                return this.webUrl;
            }
            set
            {
                this.webUrl = value;
            }
        }

        private long elapsedTicks = 0;
        /// <summary>
        /// 耗时
        /// </summary>
        [DataMember]
        public long ElapsedTicks
        {
            get
            {
                return this.elapsedTicks;
            }
            set
            {
                this.elapsedTicks = value;
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

        private DateTime startTime = DateTime.Now;
        /// <summary>
        /// 开始时间
        /// </summary>
        [DataMember]
        public DateTime StartTime
        {
            get
            {
                return this.startTime;
            }
            set
            {
                this.startTime = value;
            }
        }

        #region public BaseLogEntity GetFrom(DataRow dr)
        /// <summary>
        /// 从数据行读取
        /// </summary>
        /// <param name="dr">数据行</param>
        protected override BaseEntity GetFrom(IDataRow dr)
        {
            Id = BaseBusinessLogic.ConvertToString(dr[BaseLogEntity.FieldId]);

            // 2016-02-14 吉日嘎拉 改进日志的记录数据、把一些核心参数记录下来。
            TaskId = BaseBusinessLogic.ConvertToString(dr[BaseLogEntity.FieldTaskId]);
            Service = BaseBusinessLogic.ConvertToString(dr[BaseLogEntity.FieldService]);
            CompanyId = BaseBusinessLogic.ConvertToString(dr[BaseLogEntity.FieldCompanyId]);
            ElapsedTicks = BaseBusinessLogic.ConvertToInt(dr[BaseLogEntity.FieldElapsedTicks]);
            StartTime = BaseBusinessLogic.ConvertToDateTime(dr[BaseLogEntity.FieldStartTime]);
            UserId = BaseBusinessLogic.ConvertToString(dr[BaseLogEntity.FieldUserId]);
            UserRealName = BaseBusinessLogic.ConvertToString(dr[BaseLogEntity.FieldUserRealName]);
            Parameters = BaseBusinessLogic.ConvertToString(dr[BaseLogEntity.FieldParameters]);
            UrlReferrer = BaseBusinessLogic.ConvertToString(dr[BaseLogEntity.FieldUrlReferrer]);
            WebUrl = BaseBusinessLogic.ConvertToString(dr[BaseLogEntity.FieldWebUrl]);
            ClientIP = BaseBusinessLogic.ConvertToString(dr[BaseLogEntity.FieldClientIP]);
            ServerIP = BaseBusinessLogic.ConvertToString(dr[BaseLogEntity.FieldServerIP]);
            Description = BaseBusinessLogic.ConvertToString(dr[BaseLogEntity.FieldDescription]);

            // 获取扩展属性
            GetFromExpand(dr);
            return this;
        }
        #endregion

        /// <summary>
        /// 系统日志表
        /// </summary>
        [NonSerialized]
        public static string TableName = "BaseLog";

        /// <summary>
        /// 主键
        /// </summary>
        [NonSerialized]
        public static string FieldId = "Id";

        /// <summary>
        /// 任务的唯一标识
        ///（用来识别一个任务都调用了哪个服务，耗费了多少时间）
        /// </summary>
        public static string FieldTaskId = "TaskId";

        /// <summary>
        /// 哪个服务器上运行的？
        /// machine
        /// </summary>
        public static string FieldService = "Service";

        /// <summary>
        /// 用户主键
        /// </summary>
        [NonSerialized]
        public static string FieldUserId = "UserId";

        /// <summary>
        /// 用户名称
        /// </summary>
        [NonSerialized]
        public static string FieldUserRealName = "UserRealName";

        /// <summary>
        /// 操作参数
        /// </summary>
        [NonSerialized]
        public static string FieldParameters = "Parameters";

        /// <summary>
        /// 上一个网络地址
        /// </summary>
        [NonSerialized]
        public static string FieldUrlReferrer = "UrlReferrer";

        /// <summary>
        /// 网络地址
        /// </summary>
        [NonSerialized]
        public static string FieldWebUrl = "WebUrl";

        /// <summary>
        /// IP地址
        /// </summary>
        [NonSerialized]
        public static string FieldClientIP = "ClientIP";

        /// <summary>
        /// IP地址
        /// </summary>
        [NonSerialized]
        public static string FieldServerIP = "ServerIP";

        /// <summary>
        /// 耗时
        /// </summary>
        [NonSerialized]
        public static string FieldElapsedTicks = "ElapsedTicks";

        /// <summary>
        /// 备注
        /// </summary>
        [NonSerialized]
        public static string FieldDescription = "Description";

        /// <summary>
        /// 创建时间
        /// </summary>
        [NonSerialized]
        public static string FieldStartTime = "StartTime";

        ///<summary>
        /// 公司主键
        /// (每个公司可以看每个公司的访问日志、每个公司老板、管理员可以看自己公司的访问日志)
        ///</summary>
        [NonSerialized]
        public static string FieldCompanyId = "CompanyId";
    }
}