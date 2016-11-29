//-----------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2016 , Hairihan TECH, Ltd. 
//-----------------------------------------------------------------

using System;
using System.Data;

namespace DotNet.Model
{
    using DotNet.Utilities;

    /// <summary>
    /// HRCheckInEntity
    /// 上班签到 实体
    /// 
    /// 修改记录
    /// 
    /// 2014-09-30 版本：1.0 JiRiGaLa 创建文件。
    /// 2015-08-05 版本：2.0 SongBiao 改造成最新的版本
    /// 
    /// <author>
    ///     <name>JiRiGaLa</name>
    ///     <date>2014-09-30</date>
    /// </author>
    /// </summary>
    [Serializable]
    public partial class HRCheckInEntity : BaseEntity
    {
        private string id;
        /// <summary>
        /// 主键ID
        /// </summary>
        [FieldDescription("主键")]
        public string Id
        {
            get
            {
                return id;
            }
            set
            {
                id = value;
            }
        }

        private string companyId = null;
        /// <summary>
        /// 公司主键
        /// </summary>
         [FieldDescription("主键")]
        public string CompanyId
        {
            get
            {
                return companyId;
            }
            set
            {
                this.companyId = value;
            }
        }

        private string companyName = string.Empty;
        /// <summary>
        /// 公司名称
        /// </summary>
         [FieldDescription("主键")]
        public string CompanyName
        {
            get
            {
                return companyName;
            }
            set
            {
                this.companyName = value;
            }
        }

        private string departmentId = string.Empty;
        /// <summary>
        /// 部门主键
        /// </summary>
       [FieldDescription("主键")]
        public string DepartmentId
        {
            get
            {
                return departmentId;
            }
            set
            {
                departmentId = value;
            }
        }

        private string departmentName = string.Empty;
        /// <summary>
        /// 部门名称
        /// </summary>
           [FieldDescription("主键")]
        public string DepartmentName
        {
            get
            {
                return departmentName;
            }
            set
            {
                departmentName = value;
            }
        }

        private string userId = string.Empty;
        /// <summary>
        /// 用户ID
        /// </summary>
           [FieldDescription("主键")]
        public string UserId
        {
            get
            {
                return userId;
            }
            set
            {
                userId = value;
            }
        }

        private string userName = string.Empty;
        /// <summary>
        /// 用户名称
        /// </summary>
           [FieldDescription("主键")]
        public string UserName
        {
            get
            {
                return userName;
            }
            set
            {
                userName = value;
            }
        }

        private string checkInDay = string.Empty;
        /// <summary>
        /// 考勤日期
        /// </summary>
           [FieldDescription("主键")]
        public string CheckInDay
        {
            get
            {
                return checkInDay;
            }
            set
            {
                checkInDay = value;
            }
        }

        private DateTime? aMStartTime = null;
        /// <summary>
        /// 上午上班时间
        /// </summary>
           [FieldDescription("主键")]
        public DateTime? AMStartTime
        {
            get
            {
                return aMStartTime;
            }
            set
            {
                aMStartTime = value;
            }
        }

        private string aMStartIp = string.Empty;
        /// <summary>
        /// 上午上班IP
        /// </summary>
           [FieldDescription("主键")]
        public string AMStartIp
        {
            get
            {
                return aMStartIp;
            }
            set
            {
                aMStartIp = value;
            }
        }

        private DateTime? aMEndTime = null;
        /// <summary>
        /// 上午下班时间
        /// </summary>
           [FieldDescription("主键")]
        public DateTime? AMEndTime
        {
            get
            {
                return aMEndTime;
            }
            set
            {
                aMEndTime = value;
            }
        }

        private string aMEndIp = string.Empty;
        /// <summary>
        /// 上午下班IP
        /// </summary>
           [FieldDescription("主键")]
        public string AMEndIp
        {
            get
            {
                return aMEndIp;
            }
            set
            {
                aMEndIp = value;
            }
        }

        private DateTime? pMStartTime = null;
        /// <summary>
        /// 下午上班时间
        /// </summary>
            [FieldDescription("主键")]
        public DateTime? PMStartTime
        {
            get
            {
                return pMStartTime;
            }
            set
            {
                pMStartTime = value;
            }
        }

        private string pMStartIp = string.Empty;
        /// <summary>
        /// 下午上班IP
        /// </summary>
          [FieldDescription("主键")]
        public string PMStartIp
        {
            get
            {
                return pMStartIp;
            }
            set
            {
                pMStartIp = value;
            }
        }

        private DateTime? pMEndTime = null;
        /// <summary>
        /// 下午下班时间
        /// </summary>
            [FieldDescription("主键")]
        public DateTime? PMEndTime
        {
            get
            {
                return pMEndTime;
            }
            set
            {
                pMEndTime = value;
            }
        }

        private string pMEndIp = string.Empty;
        /// <summary>
        /// 下午下班IP
        /// </summary>
           [FieldDescription("主键")]
        public string PMEndIp
        {
            get
            {
                return pMEndIp;
            }
            set
            {
                pMEndIp = value;
            }
        }

        private DateTime? nightStartTime = null;
        /// <summary>
        /// 夜晚上班时间
        /// </summary>
           [FieldDescription("主键")]
        public DateTime? NightStartTime
        {
            get
            {
                return nightStartTime;
            }
            set
            {
                nightStartTime = value;
            }
        }

        private string nightStartIp = string.Empty;
        /// <summary>
        /// 夜晚上班IP
        /// </summary>
           [FieldDescription("主键")]
        public string NightStartIp
        {
            get
            {
                return nightStartIp;
            }
            set
            {
                nightStartIp = value;
            }
        }

        private DateTime? nightEndTime = null;
        /// <summary>
        /// 夜晚下班时间
        /// </summary>
           [FieldDescription("主键")]
        public DateTime? NightEndTime
        {
            get
            {
                return nightEndTime;
            }
            set
            {
                nightEndTime = value;
            }
        }

        private string nightEndIp = string.Empty;
        /// <summary>
        /// 夜晚下班IP
        /// </summary>
           [FieldDescription("主键")]
        public string NightEndIp
        {
            get
            {
                return nightEndIp;
            }
            set
            {
                nightEndIp = value;
            }
        }


        private DateTime? createOn = null;
        /// <summary>
        /// 创建日期
        /// </summary>
           [FieldDescription("主键")]
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
           [FieldDescription("主键")]
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
            [FieldDescription("主键")]
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
            [FieldDescription("主键")]
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
            [FieldDescription("主键")]
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
           [FieldDescription("主键")]
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

        private string description = null;
        /// <summary>
        /// 备注信息
        /// </summary>
           [FieldDescription("主键")]
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


        /// <summary>
        /// 从数据表读取
        /// </summary>
        /// <param name="dataTable">数据表</param>
        public HRCheckInEntity GetSingle(DataTable dataTable)
        {
            if ((dataTable == null) || (dataTable.Rows.Count == 0))
            {
                return null;
            }
            foreach (DataRow dataRow in dataTable.Rows)
            {
                this.GetFrom(dataRow);
                break;
            }
            return this;
        }

        /// <summary>
        /// 从数据流读取
        /// </summary>
        /// <param name="dr">数据流</param>
        protected override BaseEntity GetFrom(IDataRow dr)
        {
            Id = BaseBusinessLogic.ConvertToString(dr[HRCheckInEntity.FieldId]);
            CompanyId = BaseBusinessLogic.ConvertToString(dr[HRCheckInEntity.FieldCompanyId]);
            CompanyName = BaseBusinessLogic.ConvertToString(dr[HRCheckInEntity.FieldCompanyName]);
            DepartmentId = BaseBusinessLogic.ConvertToString(dr[HRCheckInEntity.FieldDepartmentId]);
            DepartmentName = BaseBusinessLogic.ConvertToString(dr[HRCheckInEntity.FieldDepartmentName]);
            UserId = BaseBusinessLogic.ConvertToString(dr[HRCheckInEntity.FieldUserId]);
            UserName = BaseBusinessLogic.ConvertToString(dr[HRCheckInEntity.FieldUserName]);
            CheckInDay = BaseBusinessLogic.ConvertToString(dr[HRCheckInEntity.FieldCheckInDay]);
            AMStartTime = BaseBusinessLogic.ConvertToNullableDateTime(dr[HRCheckInEntity.FieldAMStartTime]);
            AMStartIp = BaseBusinessLogic.ConvertToString(dr[HRCheckInEntity.FieldAMStartIp]);
            AMEndTime = BaseBusinessLogic.ConvertToNullableDateTime(dr[HRCheckInEntity.FieldAMEndTime]);
            AMEndIp = BaseBusinessLogic.ConvertToString(dr[HRCheckInEntity.FieldAMEndIp]);
            PMStartTime = BaseBusinessLogic.ConvertToNullableDateTime(dr[HRCheckInEntity.FieldPMStartTime]);
            PMStartIp = BaseBusinessLogic.ConvertToString(dr[HRCheckInEntity.FieldPMStartIp]);
            PMEndTime = BaseBusinessLogic.ConvertToNullableDateTime(dr[HRCheckInEntity.FieldPMEndTime]);
            PMEndIp = BaseBusinessLogic.ConvertToString(dr[HRCheckInEntity.FieldPMEndIp]);
            NightStartTime = BaseBusinessLogic.ConvertToNullableDateTime(dr[HRCheckInEntity.FieldNightStartTime]);
            NightStartIp = BaseBusinessLogic.ConvertToString(dr[HRCheckInEntity.FieldNightStartIp]);
            NightEndTime = BaseBusinessLogic.ConvertToNullableDateTime(dr[HRCheckInEntity.FieldNightEndTime]);
            NightEndIp = BaseBusinessLogic.ConvertToString(dr[HRCheckInEntity.FieldNightEndIp]);
            CreateOn = BaseBusinessLogic.ConvertToNullableDateTime(dr[HRCheckInEntity.FieldCreateOn]);
            CreateUserId = BaseBusinessLogic.ConvertToString(dr[HRCheckInEntity.FieldCreateUserId]);
            CreateBy = BaseBusinessLogic.ConvertToString(dr[HRCheckInEntity.FieldCreateBy]);
            ModifiedOn = BaseBusinessLogic.ConvertToNullableDateTime(dr[HRCheckInEntity.FieldModifiedOn]);
            ModifiedUserId = BaseBusinessLogic.ConvertToString(dr[HRCheckInEntity.FieldModifiedUserId]);
            ModifiedBy = BaseBusinessLogic.ConvertToString(dr[HRCheckInEntity.FieldModifiedBy]);
            Description = BaseBusinessLogic.ConvertToString(dr[HRCheckInEntity.FieldDescription]);
            // 获取扩展属性
            GetFromExpand(dr);
            return this;
        }

        ///<summary>
        /// 人力考勤表
        ///</summary>
        public static string TableName = "WDOA_HRCheckIn";

        ///<summary>
        /// 主键ID
        ///</summary>
        public static string FieldId = "Id";

        ///<summary>
        /// 公司主键
        ///</summary>
        [NonSerialized]
        public static string FieldCompanyId = "CompanyId";

        ///<summary>
        /// 公司名称
        ///</summary>
        [NonSerialized]
        public static string FieldCompanyName = "CompanyName";

        ///<summary>
        /// 部门主键
        ///</summary>
        public static string FieldDepartmentId = "DepartmentId";

        ///<summary>
        /// 部门名称
        ///</summary>
        public static string FieldDepartmentName = "DepartmentName";

        ///<summary>
        /// 用户ID
        ///</summary>
        public static string FieldUserId = "UserId";

        ///<summary>
        /// 用户名称
        ///</summary>
        public static string FieldUserName = "UserName";

        ///<summary>
        /// 打开签到日期
        ///</summary>
        public static string FieldCheckInDay = "CheckInDay";

        ///<summary>
        /// 上午上班时间
        ///</summary>
        public static string FieldAMStartTime = "AMStartTime";

        ///<summary>
        /// 上午上班IP
        ///</summary>
        public static string FieldAMStartIp = "AMStartIp";

        ///<summary>
        /// 上午下班时间
        ///</summary>
        public static string FieldAMEndTime = "AMEndTime";

        ///<summary>
        ///上午下班IP
        ///</summary>
        public static string FieldAMEndIp = "AMEndIp";

        ///<summary>
        /// 下午上班时间
        ///</summary>
        public static string FieldPMStartTime = "PMStartTime";

        ///<summary>
        /// 下午上班IP
        ///</summary>
        public static string FieldPMStartIp = "PMStartIp";

        ///<summary>
        /// 下午下班时间
        ///</summary>
        public static string FieldPMEndTime = "PMEndTime";

        ///<summary>
        /// 下午下班IP
        ///</summary>
        public static string FieldPMEndIp = "PMEndIp";

        ///<summary>
        /// 夜晚上班时间
        ///</summary>
        public static string FieldNightStartTime = "NightStartTime";

        ///<summary>
        /// 夜晚上班IP
        ///</summary>
        public static string FieldNightStartIp = "NightStartIp";

        ///<summary>
        /// 夜晚下班时间
        ///</summary>
        public static string FieldNightEndTime = "NightEndTime";

        ///<summary>
        /// 夜晚下班IP
        ///</summary>
        public static string FieldNightEndIp = "NightEndIp";

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

        ///<summary>
        /// 备注信息
        ///</summary>
        [NonSerialized]
        public static string FieldDescription = "Description";



    }
}