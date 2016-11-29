//--------------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2014 , Hairihan TECH, Ltd.
//--------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Data;

namespace DotNet.Business
{
    using DotNet.Utilities;

    /// <summary>
    /// BaseWorkFlowAuditInfo
    /// 当前审核状态表
    /// 
    /// 修改纪录
    /// 
    /// 2012-12-31 版本：1.0 JiRiGaLa 创建主键。
    /// 
    /// <author>
    ///     <name>JiRiGaLa</name>
    ///     <date>2012-12-31</date>
    /// </author>
    /// </summary>
    [Serializable]
    public partial class BaseWorkFlowAuditInfo
    {
        public BaseWorkFlowAuditInfo()
        {
        }

        public BaseWorkFlowAuditInfo(BaseUserInfo userInfo)
        {
            this.AuditUserId = userInfo.Id;
            this.AuditUserCode = userInfo.Code;
            this.AuditUserRealName = userInfo.RealName;
            this.AuditDate = DateTime.Now;
            this.AuditStatus = DotNet.Utilities.AuditStatus.AuditPass.ToString();
            this.AuditStatusName = DotNet.Utilities.AuditStatus.AuditPass.ToDescription();
        }

        private String id = string.Empty;
        /// <summary>
        /// 代码 currentId
        /// </summary>
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

        private String categoryCode = string.Empty;
        /// <summary>
        /// 实体分类主键
        /// </summary>
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

        private String categoryFullName = string.Empty;
        /// <summary>
        /// 实体分类名称
        /// </summary>
        public String CategoryFullName
        {
            get
            {
                return this.categoryFullName;
            }
            set
            {
                this.categoryFullName = value;
            }
        }

        public String ProcessCode { get; set; }

        /// <summary>
        /// 反射回调类,工作流回写数据库,通过接口定义调用类的方法等
        /// </summary>
        public String CallBackClass { get; set; }

        /// <summary>
        /// 回写表
        /// </summary>
        public String CallBackTable { get; set; }

        private String objectId = string.Empty;
        /// <summary>
        /// 实体主键
        /// </summary>
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

        private String objectFullName = string.Empty;
        /// <summary>
        /// 实体名称
        /// </summary>
        public String ObjectFullName
        {
            get
            {
                return this.objectFullName;
            }
            set
            {
                this.objectFullName = value;
            }
        }

        private int? processId = null;
        /// <summary>
        /// 工作流主键
        /// </summary>
        public int? ProcessId
        {
            get
            {
                return this.processId;
            }
            set
            {
                this.processId = value;
            }
        }

        private string activityId = null;
        /// <summary>
        /// 审核步骤主键
        /// </summary>
        public string ActivityId
        {
            get
            {
                return this.activityId;
            }
            set
            {
                this.activityId = value;
            }
        }

        private string activityCode = null;
        /// <summary>
        /// 审核步骤编号
        /// </summary>
        public string ActivityCode
        {
            get
            {
                return this.activityCode;
            }
            set
            {
                this.activityCode = value;
            }
        }

        private String activityFullName = string.Empty;
        /// <summary>
        /// 审核步骤名称
        /// </summary>
        public String ActivityFullName
        {
            get
            {
                return this.activityFullName;
            }
            set
            {
                this.activityFullName = value;
            }
        }

        private string activityType = null;
        /// <summary>
        /// 节点审核类型
        /// </summary>
        public string ActivityType
        {
            get
            {
                return this.activityType;
            }
            set
            {
                this.activityType = value;
            }
        }

        private String toDepartmentId = string.Empty;
        /// <summary>
        /// 待审核部门主键
        /// </summary>
        public String ToDepartmentId
        {
            get
            {
                return this.toDepartmentId;
            }
            set
            {
                this.toDepartmentId = value;
            }
        }

        private String toDepartmentName = string.Empty;
        /// <summary>
        /// 待审核部门名称
        /// </summary>
        public String ToDepartmentName
        {
            get
            {
                return this.toDepartmentName;
            }
            set
            {
                this.toDepartmentName = value;
            }
        }

        private String toUserId = string.Empty;
        /// <summary>
        /// 待审核用户主键
        /// </summary>
        public String ToUserId
        {
            get
            {
                return this.toUserId;
            }
            set
            {
                this.toUserId = value;
            }
        }

        private String toUserRealName = string.Empty;
        /// <summary>
        /// 待审核用户
        /// </summary>
        public String ToUserRealName
        {
            get
            {
                return this.toUserRealName;
            }
            set
            {
                this.toUserRealName = value;
            }
        }

        private String toRoleId = string.Empty;
        /// <summary>
        /// 待审核角色主键
        /// </summary>
        public String ToRoleId
        {
            get
            {
                return this.toRoleId;
            }
            set
            {
                this.toRoleId = value;
            }
        }

        private String toRoleRealName = string.Empty;
        /// <summary>
        /// 待审核角色
        /// </summary>
        public String ToRoleRealName
        {
            get
            {
                return this.toRoleRealName;
            }
            set
            {
                this.toRoleRealName = value;
            }
        }

        private DateTime? auditDate = null;
        /// <summary>
        /// 审核日期
        /// </summary>
        public DateTime? AuditDate
        {
            get
            {
                return this.auditDate;
            }
            set
            {
                this.auditDate = value;
            }
        }

        private String auditIdea = string.Empty;
        /// <summary>
        /// 审核意见
        /// </summary>
        public String AuditIdea
        {
            get
            {
                return this.auditIdea;
            }
            set
            {
                this.auditIdea = value;
            }
        }

        private String auditUserId = string.Empty;
        /// <summary>
        /// 审核用户主键
        /// </summary>
        public String AuditUserId
        {
            get
            {
                return this.auditUserId;
            }
            set
            {
                this.auditUserId = value;
            }
        }

        private String auditUserCode = string.Empty;
        /// <summary>
        /// 审核用户主键
        /// </summary>
        public String AuditUserCode
        {
            get
            {
                return this.auditUserCode;
            }
            set
            {
                this.auditUserCode = value;
            }
        }

        private String auditUserRealName = string.Empty;
        /// <summary>
        /// 审核用户
        /// </summary>
        public String AuditUserRealName
        {
            get
            {
                return this.auditUserRealName;
            }
            set
            {
                this.auditUserRealName = value;
            }
        }

        private String auditStatus = string.Empty;
        /// <summary>
        /// 审核状态码
        /// </summary>
        public String AuditStatus
        {
            get
            {
                return this.auditStatus;
            }
            set
            {
                this.auditStatus = value;
            }
        }

        private String auditStatusName = string.Empty;
        /// <summary>
        /// 审核状态
        /// </summary>
        public String AuditStatusName
        {
            get
            {
                return this.auditStatusName;
            }
            set
            {
                this.auditStatusName = value;
            }
        }

        private String description = string.Empty;
        /// <summary>
        /// 备注
        /// </summary>
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
    }
}
