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
    /// BaseMessageEntity
    /// 消息表
    /// 
    /// 修改记录
    /// 
    /// 2015-10-12 版本：1.1 JiRiGaLa 增加创建公司。
    /// 2012-07-03 版本：1.0 JiRiGaLa 创建主键。
    /// 
    /// <author>
    ///     <name>JiRiGaLa</name>
    ///     <date>2015-10-12</date>
    /// </author>
    /// </summary>
    [Serializable, DataContract]
    public partial class BaseMessageEntity : BaseEntity
    {
        private String id = string.Empty;
        /// <summary>
        /// 主键
        /// </summary>
        [DataMember]
        public String Id
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

        private String parentId = string.Empty;
        /// <summary>
        /// 父亲节点主键
        /// </summary>
        [DataMember]
        public String ParentId
        {
            get
            {
                return this.parentId;
            }
            set
            {
                this.parentId = value;
            }
        }

        private String receiverDepartmentId = string.Empty;
        /// <summary>
        /// 部门主键
        /// </summary>
        [DataMember]
        public String ReceiverDepartmentId
        {
            get
            {
                return this.receiverDepartmentId;
            }
            set
            {
                this.receiverDepartmentId = value;
            }
        }

        private String receiverDepartmentName = string.Empty;
        /// <summary>
        /// 部门名称
        /// </summary>
        [DataMember]
        public String ReceiverDepartmentName
        {
            get
            {
                return this.receiverDepartmentName;
            }
            set
            {
                this.receiverDepartmentName = value;
            }
        }

        private String receiverId = string.Empty;
        /// <summary>
        /// 接收者主键
        /// </summary>
        [DataMember]
        public String ReceiverId
        {
            get
            {
                return this.receiverId;
            }
            set
            {
                this.receiverId = value;
            }
        }

        private String receiverRealName = string.Empty;
        /// <summary>
        /// 接收着姓名
        /// </summary>
        [DataMember]
        public String ReceiverRealName
        {
            get
            {
                return this.receiverRealName;
            }
            set
            {
                this.receiverRealName = value;
            }
        }

        private String functionCode = "Message";
        /// <summary>
        /// 功能分类主键
        /// </summary>
        [DataMember]
        public String FunctionCode
        {
            get
            {
                return this.functionCode;
            }
            set
            {
                this.functionCode = value;
            }
        }

        private String categoryCode = string.Empty;
        /// <summary>
        /// Send发送、Receiver接收
        /// </summary>
        [DataMember]
        public String CategoryCode
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

        private String objectId = string.Empty;
        /// <summary>
        /// 唯一识别主键
        /// </summary>
        [DataMember]
        public String ObjectId
        {
            get
            {
                return this.objectId;
            }
            set
            {
                this.objectId = value;
            }
        }

        private String title = string.Empty;
        /// <summary>
        /// 主题
        /// </summary>
        [DataMember]
        public String Title
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

        private String contents = string.Empty;
        /// <summary>
        /// 内容
        /// </summary>
        [DataMember]
        public String Contents
        {
            get
            {
                return this.contents;
            }
            set
            {
                this.contents = value;
            }
        }

        private String qq = string.Empty;
        /// <summary>
        /// QQ
        /// </summary>
        [DataMember]
        public String QQ
        {
            get
            {
                return this.qq;
            }
            set
            {
                this.qq = value;
            }
        }

        private String email = string.Empty;
        /// <summary>
        /// 电子邮件
        /// </summary>
        [DataMember]
        public String Email
        {
            get
            {
                return this.email;
            }
            set
            {
                this.email = value;
            }
        }

        private String telephone = string.Empty;
        /// <summary>
        /// 电话
        /// </summary>
        [DataMember]
        public String Telephone
        {
            get
            {
                return this.telephone;
            }
            set
            {
                this.telephone = value;
            }
        }

        private int? isNew = 1;
        /// <summary>
        /// 是否新信息
        /// </summary>
        [DataMember]
        public int? IsNew
        {
            get
            {
                return this.isNew;
            }
            set
            {
                this.isNew = value;
            }
        }

        private int? readCount = 0;
        /// <summary>
        /// 被阅读次数
        /// </summary>
        [DataMember]
        public int? ReadCount
        {
            get
            {
                return this.readCount;
            }
            set
            {
                this.readCount = value;
            }
        }

        private DateTime? readDate = null;
        /// <summary>
        /// 阅读日期
        /// </summary>
        [DataMember]
        public DateTime? ReadDate
        {
            get
            {
                return this.readDate;
            }
            set
            {
                this.readDate = value;
            }
        }

        private String targetURL = string.Empty;
        /// <summary>
        /// 消息的指向网页页面
        /// </summary>
        [DataMember]
        public String TargetURL
        {
            get
            {
                return this.targetURL;
            }
            set
            {
                this.targetURL = value;
            }
        }

        private String iPAddress = string.Empty;
        /// <summary>
        /// IP地址
        /// </summary>
        [DataMember]
        public String IPAddress
        {
            get
            {
                return this.iPAddress;
            }
            set
            {
                this.iPAddress = value;
            }
        }

        private String createCompanyId = string.Empty;
        /// <summary>
        /// 公司主键
        /// </summary>
        [DataMember]
        public String CreateCompanyId
        {
            get
            {
                return this.createCompanyId;
            }
            set
            {
                this.createCompanyId = value;
            }
        }

        private String createCompanyName = string.Empty;
        /// <summary>
        /// 公司名称
        /// </summary>
        [DataMember]
        public String CreateCompanyName
        {
            get
            {
                return this.createCompanyName;
            }
            set
            {
                this.createCompanyName = value;
            }
        }

        private String createDepartmentId = string.Empty;
        /// <summary>
        /// 部门主键
        /// </summary>
        [DataMember]
        public String CreateDepartmentId
        {
            get
            {
                return this.createDepartmentId;
            }
            set
            {
                this.createDepartmentId = value;
            }
        }

        private String createDepartmentName = string.Empty;
        /// <summary>
        /// 部门名称
        /// </summary>
        [DataMember]
        public String CreateDepartmentName
        {
            get
            {
                return this.createDepartmentName;
            }
            set
            {
                this.createDepartmentName = value;
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

        /*

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

        private String description = string.Empty;
        /// <summary>
        /// 备注
        /// </summary>
        [DataMember]
        public String Description
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
        */

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

        private String createUserId = string.Empty;
        /// <summary>
        /// 创建用户主键
        /// </summary>
        [DataMember]
        public String CreateUserId
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

        private String createBy = string.Empty;
        /// <summary>
        /// 创建用户
        /// </summary>
        [DataMember]
        public String CreateBy
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

        /// <summary>
        /// 从数据行读取
        /// </summary>
        /// <param name="dr">数据行</param>
        protected override BaseEntity GetFrom(IDataRow dr)
        {
            Id = BaseBusinessLogic.ConvertToString(dr[BaseMessageEntity.FieldId]);
            ParentId = BaseBusinessLogic.ConvertToString(dr[BaseMessageEntity.FieldParentId]);
            ReceiverDepartmentId = BaseBusinessLogic.ConvertToString(dr[BaseMessageEntity.FieldReceiverDepartmentId]);
            ReceiverDepartmentName = BaseBusinessLogic.ConvertToString(dr[BaseMessageEntity.FieldReceiverDepartmentName]);
            ReceiverId = BaseBusinessLogic.ConvertToString(dr[BaseMessageEntity.FieldReceiverId]);
            ReceiverRealName = BaseBusinessLogic.ConvertToString(dr[BaseMessageEntity.FieldReceiverRealName]);
            FunctionCode = BaseBusinessLogic.ConvertToString(dr[BaseMessageEntity.FieldFunctionCode]);
            CategoryCode = BaseBusinessLogic.ConvertToString(dr[BaseMessageEntity.FieldCategoryCode]);
            ObjectId = BaseBusinessLogic.ConvertToString(dr[BaseMessageEntity.FieldObjectId]);
            Title = BaseBusinessLogic.ConvertToString(dr[BaseMessageEntity.FieldTitle]);
            Contents = BaseBusinessLogic.ConvertToString(dr[BaseMessageEntity.FieldContents]);
            QQ = BaseBusinessLogic.ConvertToString(dr[BaseMessageEntity.FieldQQ]);
            Email = BaseBusinessLogic.ConvertToString(dr[BaseMessageEntity.FieldEmail]);
            Telephone = BaseBusinessLogic.ConvertToString(dr[BaseMessageEntity.FieldTelephone]);
            IsNew = BaseBusinessLogic.ConvertToInt(dr[BaseMessageEntity.FieldIsNew]);
            ReadCount = BaseBusinessLogic.ConvertToInt(dr[BaseMessageEntity.FieldReadCount]);
            ReadDate = BaseBusinessLogic.ConvertToNullableDateTime(dr[BaseMessageEntity.FieldReadDate]);
            TargetURL = BaseBusinessLogic.ConvertToString(dr[BaseMessageEntity.FieldTargetURL]);
            IPAddress = BaseBusinessLogic.ConvertToString(dr[BaseMessageEntity.FieldIPAddress]);
            DeletionStateCode = BaseBusinessLogic.ConvertToInt(dr[BaseMessageEntity.FieldDeletionStateCode]);
            CreateCompanyId = BaseBusinessLogic.ConvertToString(dr[BaseMessageEntity.FieldCreateCompanyId]);
            CreateCompanyName = BaseBusinessLogic.ConvertToString(dr[BaseMessageEntity.FieldCreateCompanyName]);
            CreateDepartmentId = BaseBusinessLogic.ConvertToString(dr[BaseMessageEntity.FieldCreateDepartmentId]);
            CreateDepartmentName = BaseBusinessLogic.ConvertToString(dr[BaseMessageEntity.FieldCreateDepartmentName]);
            CreateUserId = BaseBusinessLogic.ConvertToString(dr[BaseMessageEntity.FieldCreateUserId]);
            CreateBy = BaseBusinessLogic.ConvertToString(dr[BaseMessageEntity.FieldCreateBy]);
            CreateOn = BaseBusinessLogic.ConvertToNullableDateTime(dr[BaseMessageEntity.FieldCreateOn]);
            // 获取扩展属性
            GetFromExpand(dr);
            return this;
        }

        ///<summary>
        /// 消息表
        ///</summary>
        [NonSerialized]
        public static string TableName = "BaseMessage";

        ///<summary>
        /// 主键
        ///</summary>
        [NonSerialized]
        public static string FieldId = "Id";

        ///<summary>
        /// 父亲节点主键
        ///</summary>
        [NonSerialized]
        public static string FieldParentId = "ParentId";

        ///<summary>
        /// 部门主键
        ///</summary>
        [NonSerialized]
        public static string FieldReceiverDepartmentId = "ReceiverDepartmentId";

        ///<summary>
        /// 部门名称
        ///</summary>
        [NonSerialized]
        public static string FieldReceiverDepartmentName = "ReceiverDepartmentName";

        ///<summary>
        /// 接收者主键
        ///</summary>
        [NonSerialized]
        public static string FieldReceiverId = "ReceiverId";

        ///<summary>
        /// 接收着姓名
        ///</summary>
        [NonSerialized]
        public static string FieldReceiverRealName = "ReceiverRealName";

        ///<summary>
        /// 功能分类主键，Send发送、Receiver接收
        ///</summary>
        [NonSerialized]
        public static string FieldFunctionCode = "FunctionCode";

        ///<summary>
        /// 分类主键
        ///</summary>
        [NonSerialized]
        public static string FieldCategoryCode = "CategoryCode";

        ///<summary>
        /// 唯一识别主键
        ///</summary>
        [NonSerialized]
        public static string FieldObjectId = "ObjectId";

        ///<summary>
        /// 主题
        ///</summary>
        [NonSerialized]
        public static string FieldTitle = "Title";

        ///<summary>
        /// 内容
        ///</summary>
        [NonSerialized]
        public static string FieldContents = "Contents";

        ///<summary>
        /// QQ
        ///</summary>
        [NonSerialized]
        public static string FieldQQ = "QQ";

        ///<summary>
        /// Email
        ///</summary>
        [NonSerialized]
        public static string FieldEmail = "Email";

        ///<summary>
        /// 电话
        ///</summary>
        [NonSerialized]
        public static string FieldTelephone = "Telephone";

        ///<summary>
        /// 是否新信息
        ///</summary>
        [NonSerialized]
        public static string FieldIsNew = "IsNew";

        ///<summary>
        /// 被阅读次数
        ///</summary>
        [NonSerialized]
        public static string FieldReadCount = "ReadCount";

        ///<summary>
        /// 阅读日期
        ///</summary>
        [NonSerialized]
        public static string FieldReadDate = "ReadDate";

        ///<summary>
        /// 消息的指向网页页面
        ///</summary>
        [NonSerialized]
        public static string FieldTargetURL = "TargetURL";

        ///<summary>
        /// IP地址
        ///</summary>
        [NonSerialized]
        public static string FieldIPAddress = "IPAddress";

        ///<summary>
        /// 删除标志
        ///</summary>
        [NonSerialized]
        public static string FieldDeletionStateCode = "DeletionStateCode";

        ///<summary>
        /// 公司主键
        ///</summary>
        [NonSerialized]
        public static string FieldCreateCompanyId = "CreateCompanyId";

        ///<summary>
        /// 公司名称
        ///</summary>
        [NonSerialized]
        public static string FieldCreateCompanyName = "CreateCompanyName";

        ///<summary>
        /// 部门主键
        ///</summary>
        [NonSerialized]
        public static string FieldCreateDepartmentId = "CreateDepartmentId";

        ///<summary>
        /// 部门名称
        ///</summary>
        [NonSerialized]
        public static string FieldCreateDepartmentName = "CreateDepartmentName";


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
        /// 创建日期
        ///</summary>
        [NonSerialized]
        public static string FieldCreateOn = "CreateOn";
    }
}
