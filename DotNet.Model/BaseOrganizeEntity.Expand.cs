//-----------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2013 , Hairihan TECH, Ltd .
//-----------------------------------------------------------------

using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace DotNet.Model
{
    using DotNet.Utilities;

    /// <summary>
    /// BaseOrganizeEntity
    /// 组织机构、部门表
    ///
    /// 修改记录
    ///
    ///		2012-05-07 版本：1.0 JiRiGaLa 创建主键。
    ///
    /// 版本：1.0
    ///
    /// <author>
    ///		<name>JiRiGaLa</name>
    ///		<date>2012-05-07</date>
    /// </author>
    /// </summary>
    public partial class BaseOrganizeEntity
    {
        private string statisticalName = null;
        /// <summary>
        /// 统计名称
        /// </summary>
        [FieldDescription("统计名称")]
        [DataMember]
        public string StatisticalName
        {
            get
            {
                return this.statisticalName;
            }
            set
            {
                this.statisticalName = value;
            }
        }

        private int? weightRatio = 6000;
        /// <summary>
        /// 体积重比率
        /// </summary>
        [FieldDescription("体积重比率")]
        [DataMember]
        public int? WeightRatio
        {
            get
            {
                return this.weightRatio;
            }
            set
            {
                this.weightRatio = value;
            }
        }

        private int? sendAir = 1;
        /// <summary>
        /// 发航空件
        /// </summary>
        [FieldDescription("发航空件")]
        [DataMember]
        public int? SendAir
        {
            get
            {
                return this.sendAir;
            }
            set
            {
                this.sendAir = value;
            }
        }

        private int? calculateComeFee = 1;
        /// <summary>
        /// 后台计算到件中转费标识
        /// </summary>
        [FieldDescription("后台计算到件中转费标识")]
        [DataMember]
        public int? CalculateComeFee
        {
            get
            {
                return this.calculateComeFee;
            }
            set
            {
                this.calculateComeFee = value;
            }
        }

        private int? calculateReceiveFee = 1;
        /// <summary>
        /// 后台计算收件中转费标识
        /// </summary>
        [FieldDescription("后台计算收件中转费标识")]
        [DataMember]
        public int? CalculateReceiveFee
        {
            get
            {
                return this.calculateReceiveFee;
            }
            set
            {
                this.calculateReceiveFee = value;
            }
        }

        private string billBalanceSite = string.Empty;
        ///<summary>
        /// 面单结算网点
        ///</summary>
        [FieldDescription("面单结算网点")]
        [DataMember]
        public string BillBalanceSite
        {
            get
            {
                return this.billBalanceSite;
            }
            set
            {
                this.billBalanceSite = value;
            }
        }

        private string levelTwoTransferCenter = string.Empty;
        ///<summary>
        /// 二级中转费结算中心
        ///</summary>
        [FieldDescription("二级中转费结算中心")]
        [DataMember]
        public string LevelTwoTransferCenter
        {
            get
            {
                return this.levelTwoTransferCenter;
            }
            set
            {
                this.levelTwoTransferCenter = value;
            }
        }

        private string provinceSite = string.Empty;
        ///<summary>
        /// 省级网点
        ///</summary>
        [FieldDescription("省级网点")]
        [DataMember]
        public string ProvinceSite
        {
            get
            {
                return this.provinceSite;
            }
            set
            {
                this.provinceSite = value;
            }
        }

        private string bigArea = string.Empty;
        ///<summary>
        /// 大片区
        ///</summary>
        [FieldDescription("大片区")]
        [DataMember]
        public string BigArea
        {
            get
            {
                return this.bigArea;
            }
            set
            {
                this.bigArea = value;
            }
        }

        private Decimal? sendFee = null;
        ///<summary>
        /// 有偿派送费率
        ///</summary>
        [FieldDescription("有偿派送费率")]
        [DataMember]
        public Decimal? SendFee
        {
            get
            {
                return this.sendFee;
            }
            set
            {
                this.sendFee = value;
            }
        }

        private Decimal? levelTwoTransferFee = null;
        ///<summary>
        /// 二级中转费率
        ///</summary>
        [FieldDescription("二级中转费率")]
        [DataMember]
        public Decimal? LevelTwoTransferFee
        {
            get
            {
                return this.levelTwoTransferFee;
            }
            set
            {
                this.levelTwoTransferFee = value;
            }
        }

        private Decimal? billSubsidy = null;
        ///<summary>
        /// 面单补贴费率
        ///</summary>
        [FieldDescription("面单补贴费率")]
        [DataMember]
        public Decimal? BillSubsidy
        {
            get
            {
                return this.billSubsidy;
            }
            set
            {
                this.billSubsidy = value;
            }
        }

        private string master = string.Empty;
        ///<summary>
        /// 经理
        ///</summary>
        [FieldDescription("经理")]
        [DataMember]
        public string Master
        {
            get
            {
                return this.master;
            }
            set
            {
                this.master = value;
            }
        }

        private string masterMobile = string.Empty;
        ///<summary>
        /// 经理手机
        ///</summary>
        [FieldDescription("经理手机")]
        [DataMember]
        public string MasterMobile
        {
            get
            {
                return this.masterMobile;
            }
            set
            {
                this.masterMobile = value;
            }
        }

        private string masterQQ = string.Empty;
        ///<summary>
        /// 经理QQ
        ///</summary>
        [FieldDescription("经理QQ")]
        [DataMember]
        public string MasterQQ
        {
            get
            {
                return this.masterQQ;
            }
            set
            {
                this.masterQQ = value;
            }
        }

        private string manager = string.Empty;
        ///<summary>
        /// 业务负责人
        ///</summary>
        [FieldDescription("业务负责人")]
        [DataMember]
        public string Manager
        {
            get
            {
                return this.manager;
            }
            set
            {
                this.manager = value;
            }
        }

        private string managerMobile = string.Empty;
        ///<summary>
        /// 业务负责人手机
        ///</summary>
        [FieldDescription("业务负责人手机")]
        [DataMember]
        public string ManagerMobile
        {
            get
            {
                return this.managerMobile;
            }
            set
            {
                this.managerMobile = value;
            }
        }

        private string managerQQ = string.Empty;
        ///<summary>
        /// 业务负责人QQ
        ///</summary>
        [FieldDescription("业务负责人QQ")]
        [DataMember]
        public string ManagerQQ
        {
            get
            {
                return this.managerQQ;
            }
            set
            {
                this.managerQQ = value;
            }
        }

        private string emergencyCall = string.Empty;
        ///<summary>
        /// 紧急联系电话
        ///</summary>
        [FieldDescription("紧急联系电话")]
        [DataMember]
        public string EmergencyCall
        {
            get
            {
                return this.emergencyCall;
            }
            set
            {
                this.emergencyCall = value;
            }
        }

        private string businessPhone = string.Empty;
        ///<summary>
        /// 业务咨询电话
        ///</summary>
        [FieldDescription("业务咨询电话")]
        [DataMember]
        public string BusinessPhone
        {
            get
            {
                return this.businessPhone;
            }
            set
            {
                this.businessPhone = value;
            }
        }

        private int? isCheckBalance = 0;
        ///<summary>
        /// 扫描检测余额
        ///</summary>
        [FieldDescription("扫描检测余额")]
        [DataMember]
        public int? IsCheckBalance
        {
            get
            {
                return this.isCheckBalance;
            }
            set
            {
                this.isCheckBalance = value;
            }
        }

        /// <summary>
        /// 统计名称
        /// </summary>
        [NonSerialized]
        [FieldDescription("统计名称")]
        public static string FieldStatisticalName = "StatisticalName";

        /// <summary>
        /// 体积重比率
        /// </summary>
        [NonSerialized]
        [FieldDescription("体积重比率")]
        public static string FieldWeightRatio = "WeightRatio";

        ///<summary>
        /// 发航空件
        ///</summary>
        [NonSerialized]
        [FieldDescription("发航空件")]
        public static string FieldSendAir = "SendAir";

        ///<summary>
        /// 后台计算到件中转费标识
        ///</summary>
        [NonSerialized]
        [FieldDescription("后台计算到件中转费标识")]
        public static string FieldCalculateComeFee = "CalculateComeFee";

        ///<summary>
        /// 后台计算收件中转费标识
        ///</summary>
        [NonSerialized]
        [FieldDescription("后台计算收件中转费标识")]
        public static string FieldCalculateReceiveFee = "CalculateReceiveFee";

        ///<summary>
        /// 面单结算网点
        ///</summary>
        [NonSerialized]
        [FieldDescription("面单结算网点")]
        public static string FieldBillBalanceSite = "BillBalanceSite";

        ///<summary>
        /// 二级中转费结算中心
        ///</summary>
        [NonSerialized]
        [FieldDescription("二级中转费结算中心")]
        public static string FieldLevelTwoTransferCenter = "LevelTwoTransferCenter";

        ///<summary>
        /// 省级网点
        ///</summary>
        [NonSerialized]
        [FieldDescription("省级网点")]
        public static string FieldProvinceSite = "ProvinceSite";

        ///<summary>
        /// 大片区
        ///</summary>
        [NonSerialized]
        [FieldDescription("大片区")]
        public static string FieldBigArea = "BigArea";

        ///<summary>
        /// 有偿派送费率
        ///</summary>
        [NonSerialized]
        [FieldDescription("有偿派送费率")]
        public static string FieldSendFee = "SendFee";

        ///<summary>
        /// 二级中转费率
        ///</summary>
        [NonSerialized]
        [FieldDescription("二级中转费率")]
        public static string FieldLevelTwoTransferFee = "LevelTwoTransferFee";

        ///<summary>
        /// 面单补贴费率
        ///</summary>
        [NonSerialized]
        [FieldDescription("面单补贴费率")]
        public static string FieldBillSubsidy = "BillSubsidy";

        ///<summary>
        /// 经理
        ///</summary>
        [NonSerialized]
        [FieldDescription("经理")]
        public static string FieldMaster = "Master";

        ///<summary>
        /// 经理手机
        ///</summary>
        [NonSerialized]
        [FieldDescription("经理手机")]
        public static string FieldMasterMobile = "MasterMobile";

        ///<summary>
        /// 经理QQ
        ///</summary>
        [NonSerialized]
        [FieldDescription("经理QQ")]
        public static string FieldMasterQQ = "MasterQQ";

        ///<summary>
        /// 业务负责人
        ///</summary>
        [NonSerialized]
        [FieldDescription("业务负责人")]
        public static string FieldManager = "Manager";

        ///<summary>
        /// 业务负责人手机
        ///</summary>
        [NonSerialized]
        [FieldDescription("业务负责人手")]
        public static string FieldManagerMobile = "ManagerMobile";

        ///<summary>
        /// 业务负责人QQ
        ///</summary>
        [NonSerialized]
        [FieldDescription("业务负责人QQ")]
        public static string FieldManagerQQ = "ManagerQQ";

        ///<summary>
        /// 紧急联系电话
        ///</summary>
        [NonSerialized]
        [FieldDescription("紧急联系电话")]
        public static string FieldEmergencyCall = "EmergencyCall";

        ///<summary>
        /// 业务咨询电话
        ///</summary>
        [NonSerialized]
        [FieldDescription("业务咨询电话")]
        public static string FieldBusinessPhone = "BusinessPhone";

        ///<summary>
        /// 扫描检测余额
        ///</summary>
        [NonSerialized]
        [FieldDescription("扫描检测余额")]
        public static string FieldIsCheckBalance = "IsCheckBalance";
        
        /// <summary>
        /// 从数据行读取
        /// </summary>
        /// <param name="dataRow">数据行</param>
        public override void GetFromExpand(IDataRow dr)
        {
            if (dr.ContainsColumn(BaseOrganizeEntity.FieldWeightRatio))
            {
                StatisticalName = BaseBusinessLogic.ConvertToString(dr[BaseOrganizeEntity.FieldStatisticalName]);
                WeightRatio = BaseBusinessLogic.ConvertToInt(dr[BaseOrganizeEntity.FieldWeightRatio]);
                SendAir = BaseBusinessLogic.ConvertToInt(dr[BaseOrganizeEntity.FieldSendAir]);
                CalculateComeFee = BaseBusinessLogic.ConvertToInt(dr[BaseOrganizeEntity.FieldCalculateComeFee]);
                CalculateReceiveFee = BaseBusinessLogic.ConvertToInt(dr[BaseOrganizeEntity.FieldCalculateReceiveFee]);

                BillBalanceSite = BaseBusinessLogic.ConvertToString(dr[BaseOrganizeEntity.FieldBillBalanceSite]);
                LevelTwoTransferCenter = BaseBusinessLogic.ConvertToString(dr[BaseOrganizeEntity.FieldLevelTwoTransferCenter]);
                ProvinceSite = BaseBusinessLogic.ConvertToString(dr[BaseOrganizeEntity.FieldProvinceSite]);
                BigArea = BaseBusinessLogic.ConvertToString(dr[BaseOrganizeEntity.FieldBigArea]);

                SendFee = BaseBusinessLogic.ConvertToNullableDecimal(dr[BaseOrganizeEntity.FieldSendFee]);
                LevelTwoTransferFee = BaseBusinessLogic.ConvertToNullableDecimal(dr[BaseOrganizeEntity.FieldLevelTwoTransferFee]);
                BillSubsidy = BaseBusinessLogic.ConvertToNullableDecimal(dr[BaseOrganizeEntity.FieldBillSubsidy]);

                Master = BaseBusinessLogic.ConvertToString(dr[BaseOrganizeEntity.FieldMaster]);
                MasterMobile = BaseBusinessLogic.ConvertToString(dr[BaseOrganizeEntity.FieldMasterMobile]);
                MasterQQ = BaseBusinessLogic.ConvertToString(dr[BaseOrganizeEntity.FieldMasterQQ]);
                Manager = BaseBusinessLogic.ConvertToString(dr[BaseOrganizeEntity.FieldManager]);
                ManagerMobile = BaseBusinessLogic.ConvertToString(dr[BaseOrganizeEntity.FieldManagerMobile]);
                ManagerQQ = BaseBusinessLogic.ConvertToString(dr[BaseOrganizeEntity.FieldManagerQQ]);
                EmergencyCall = BaseBusinessLogic.ConvertToString(dr[BaseOrganizeEntity.FieldEmergencyCall]);
                BusinessPhone = BaseBusinessLogic.ConvertToString(dr[BaseOrganizeEntity.FieldBusinessPhone]);
                IsCheckBalance = BaseBusinessLogic.ConvertToInt(dr[BaseOrganizeEntity.FieldIsCheckBalance]);
            }
        }
    }
}
