//-----------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2015 , Hairihan TECH, Ltd. 
//-----------------------------------------------------------------

using System;
using System.Data;

namespace DotNet.Business
{
    using DotNet.Model;
    using DotNet.Utilities;

    /// <summary>
    /// BaseOrganizeManager
    /// 组织机构管理
    /// 
    /// 修改纪录
    /// 
    ///		2013.10.20 版本：2.0 JiRiGaLa	集成K8物流系统的登录功能。
    ///		2011.10.17 版本：1.0 JiRiGaLa	主键整理。
    /// 
    /// <author>
    ///		<name>JiRiGaLa</name>
    ///		<date>2011.10.17</date>
    /// </author> 
    /// </summary>
    public partial class BaseOrganizeManager : BaseManager
    {
        /// <summary>
        /// 同步数据
        /// </summary>
        /// <param name="all">同步所有数据</param>
        /// <returns>影响行数</returns>
        public int Synchronous(bool all = false)
        {
            int result = 0;
            string connectionString = ConfigurationHelper.AppSettings("K8Connection", BaseSystemInfo.EncryptDbConnection);
            string conditional = null;
            if (!all)
            {
                int id = 0;
                string commandText = "SELECT MAX(id) FROM BaseOrganize WHERE id < 1000000";
                Object maxObject = DbHelper.ExecuteScalar(commandText);
                if (maxObject != null)
                {
                    id = int.Parse(maxObject.ToString());
                }
                conditional = " AND ID > " + id.ToString();
            }
            result = Synchronous(connectionString, conditional);
            return result;
        }

        /// <summary>
        /// 导入K8系统网点信息
        /// </summary>
        /// <param name="connectionString">数据库连接</param>
        /// <param name="conditional">条件，不需要同步所有的数据</param>
        /// <returns>影响行数</returns>
        public int Synchronous(string connectionString = null, string conditional = null)
        {
            // delete from baseorganize where id < 1000000
            int result = 0;
            if (string.IsNullOrEmpty(connectionString))
            {
                connectionString = ConfigurationHelper.AppSettings("K8Connection", BaseSystemInfo.EncryptDbConnection);
            }
            if (!string.IsNullOrEmpty(connectionString))
            {
                // 01：可以从k8里读取公司、用户、密码的。
                IDbHelper dbHelper = DbHelperFactory.GetHelper(CurrentDbType.Oracle, connectionString);
                BaseOrganizeManager organizeManager = new Business.BaseOrganizeManager(this.DbHelper, this.UserInfo);
                // 不不存在的组织机构删除掉TAB_SITE是远程试图
                string commandText = string.Empty;
                if (string.IsNullOrWhiteSpace(conditional))
                {
                    commandText = "DELETE FROM BASEORGANIZE WHERE id <= 10000 AND id NOT IN (SELECT id FROM TAB_SITE WHERE id <= 10000)";
                    organizeManager.DbHelper.ExecuteNonQuery(commandText);
                    System.Console.WriteLine(commandText);
                    commandText = "DELETE FROM BASEORGANIZE WHERE id > 10000 AND id <= 20000 AND id NOT IN (SELECT id FROM TAB_SITE WHERE id > 10000 AND id <= 20000)";
                    organizeManager.DbHelper.ExecuteNonQuery(commandText);
                    System.Console.WriteLine(commandText);
                    commandText = "DELETE FROM BASEORGANIZE WHERE id > 20000 AND id <= 30000 AND id NOT IN (SELECT id FROM TAB_SITE WHERE id > 20000 AND id <= 30000)";
                    organizeManager.DbHelper.ExecuteNonQuery(commandText);
                    System.Console.WriteLine(commandText);
                    commandText = "DELETE FROM BASEORGANIZE WHERE id > 30000 AND id <= 40000 AND id NOT IN (SELECT id FROM TAB_SITE WHERE id > 30000 AND id <= 40000)";
                    organizeManager.DbHelper.ExecuteNonQuery(commandText);
                    System.Console.WriteLine(commandText);
                    commandText = "DELETE FROM BASEORGANIZE WHERE id > 40000 AND id <= 50000 AND id NOT IN (SELECT id FROM TAB_SITE WHERE id > 40000 AND id <= 50000)";
                    organizeManager.DbHelper.ExecuteNonQuery(commandText);
                    System.Console.WriteLine(commandText);
                    commandText = "DELETE FROM BASEORGANIZE WHERE id > 50000 AND id <= 60000 AND id NOT IN (SELECT id FROM TAB_SITE WHERE id > 50000 AND id <= 60000)";
                    organizeManager.DbHelper.ExecuteNonQuery(commandText);
                    System.Console.WriteLine(commandText);
                    commandText = "DELETE FROM BASEORGANIZE WHERE id > 60000 AND id <= 70000 AND id NOT IN (SELECT id FROM TAB_SITE WHERE id > 60000 AND id <= 70000)";
                    organizeManager.DbHelper.ExecuteNonQuery(commandText);
                    System.Console.WriteLine(commandText);
                    commandText = "DELETE FROM BASEORGANIZE WHERE id > 70000 AND id <= 80000 AND id NOT IN (SELECT id FROM TAB_SITE WHERE id > 70000 AND id <= 80000)";
                    organizeManager.DbHelper.ExecuteNonQuery(commandText);
                    System.Console.WriteLine(commandText);
                    commandText = "DELETE FROM BASEORGANIZE WHERE id > 80000 AND id <= 90000 AND id NOT IN (SELECT id FROM TAB_SITE WHERE id > 80000 AND id <= 90000)";
                    organizeManager.DbHelper.ExecuteNonQuery(commandText);
                    System.Console.WriteLine(commandText);
                    commandText = "DELETE FROM BASEORGANIZE WHERE id > 90000 AND id <= 100000 AND id NOT IN (SELECT id FROM TAB_SITE WHERE id > 90000 AND id <= 100000)";
                    organizeManager.DbHelper.ExecuteNonQuery(commandText);
                    System.Console.WriteLine(commandText);
                    commandText = "DELETE FROM BASEORGANIZE WHERE id > 100000 AND id <= 1000000 AND id NOT IN (SELECT id FROM TAB_SITE WHERE id > 100000 AND id <= 1000000)";
                    organizeManager.DbHelper.ExecuteNonQuery(commandText);
                    System.Console.WriteLine(commandText);
                }
                /*
                string commandText = "SELECT Id FROM BASEORGANIZE WHERE id < 1000000";
                using (IDataReader dataReader = organizeManager.DbHelper.ExecuteReader(commandText))
                {
                    while (dataReader.Read())
                    {
                        string id = dataReader["id"].ToString();
                        commandText = "SELECT COUNT(1) AS Rcount FROM TAB_SITE WHERE id='" + id + "'";
                        object rcount = dbHelper.ExecuteScalar(commandText);
                        if (rcount == null || rcount.ToString().Equals("0"))
                        {
                            commandText = "DELETE FROM BASEORGANIZE WHERE id ='" + id + "'";
                            organizeManager.DbHelper.ExecuteNonQuery(commandText);
                        }
                    }
                }
                */
                
                // 同步数据
                // commandText = "SELECT * FROM TAB_SITE WHERE (BL_NOT_INPUT IS NULL OR BL_NOT_INPUT = 0) ";
                commandText = "SELECT * FROM TAB_SITE WHERE 1=1 ";
                if (!string.IsNullOrEmpty(conditional))
                {
                    commandText += conditional;
                }
                commandText += " ORDER BY ID DESC";
                using (IDataReader dr = DbHelper.ExecuteReader(commandText))
                {
                    System.Console.WriteLine(commandText); 
                    while (dr.Read())
                    {
                        // 这里需要从数据库读取、否则容易造成丢失数据
                        BaseOrganizeEntity entity = organizeManager.GetObject(dr["ID"].ToString());
                        if (entity == null)
                        {
                            entity = new BaseOrganizeEntity();
                            entity.Id = int.Parse(dr["ID"].ToString());
                        }
                        entity.Code = dr["SITE_CODE"].ToString();
                        if (string.IsNullOrEmpty(entity.ParentName) || !entity.ParentName.Equals(dr["SUPERIOR_SITE"].ToString()))
                        {
                            entity.ParentName = dr["SUPERIOR_SITE"].ToString();
                            entity.ParentId = null;
                        }
                        
                        entity.FullName = dr["SITE_NAME"].ToString();
                        entity.ShortName = dr["SITE_NAME"].ToString();
                        entity.CategoryCode = dr["TYPE"].ToString();
                        entity.OuterPhone = dr["PHONE"].ToString();
                        entity.Fax = dr["FAX"].ToString();
                        entity.Province = dr["PROVINCE"].ToString();
                        entity.City = dr["CITY"].ToString();
                        entity.District = dr["RANGE_NAME"].ToString();
                        entity.FinancialCenter = dr["SUPERIOR_FINANCE_CENTER"].ToString();
                        entity.Area = dr["AREA_NAME"].ToString();
                        entity.CostCenter = dr["SUPERIOR_TWO_FINANCE_CENTER"].ToString();
                        entity.Manager = dr["PRINCIPAL"].ToString();
                        entity.Master = dr["MANAGER"].ToString();
                        entity.BusinessPhone = dr["SALE_PHONE"].ToString();
                        entity.Description = dr["SITE_DESC"].ToString();
                        entity.MasterMobile = dr["MANAGER_PHONE"].ToString();
                        entity.EmergencyCall = dr["EXIGENCE_PHONE"].ToString();
                        entity.BankAccount = dr["FINANCIAL_ACCOUNT"].ToString();
                        entity.Street = dr["COUNTY_NAME"].ToString();
                        int weightRatio = 0;
                        int.TryParse(dr["THROW_RATE"].ToString(), out weightRatio);
                        entity.WeightRatio = weightRatio;

                        entity.CompanyName = dr["SITE1_NAME"].ToString();
                        entity.JoiningMethods = dr["NC_TYPE"].ToString();

                        int sendAir = 0;
                        if (!string.IsNullOrEmpty(dr["BL_AIR"].ToString()) && ValidateUtil.IsInt(dr["BL_AIR"].ToString()))
                        {
                            sendAir = int.Parse(dr["BL_AIR"].ToString());
                        }
                        entity.SendAir = sendAir;
                        entity.LevelTwoTransferCenter = dr["TWO_BALANCE_CENTER"].ToString();

                        Decimal billSubsidy = 0;
                        if (!string.IsNullOrEmpty(dr["CENTER_DISPATCH_FEE"].ToString()) && ValidateUtil.IsDouble(dr["CENTER_DISPATCH_FEE"].ToString()))
                        {
                            billSubsidy = Decimal.Parse(dr["CENTER_DISPATCH_FEE"].ToString());
                        }
                        entity.BillSubsidy = billSubsidy;
                        Decimal sendFee = 0;
                        if (!string.IsNullOrEmpty(dr["DISP_FEE"].ToString()) && ValidateUtil.IsDouble(dr["DISP_FEE"].ToString()))
                        {
                            sendFee = Decimal.Parse(dr["DISP_FEE"].ToString());
                        }
                        entity.SendFee = sendFee;
                        Decimal levelTwoTransferFee = 0;
                        if (!string.IsNullOrEmpty(dr["TRANSFER_FEE"].ToString()) && ValidateUtil.IsDouble(dr["TRANSFER_FEE"].ToString()))
                        {
                            levelTwoTransferFee = Decimal.Parse(dr["TRANSFER_FEE"].ToString());
                        }
                        entity.LevelTwoTransferFee = levelTwoTransferFee;
                        entity.BigArea = dr["BIG_AREA_NAME"].ToString();
                        entity.StatisticalName = dr["REC_CENTER"].ToString();
                        entity.BillBalanceSite = dr["TRANSFER_CENTER"].ToString();
                        entity.ProvinceSite = dr["PROV_SITE"].ToString();
                        int calculateComeFee = 0;
                        if (!string.IsNullOrEmpty(dr["BL_WEB"].ToString()) && ValidateUtil.IsInt(dr["BL_WEB"].ToString()))
                        {
                            calculateComeFee = int.Parse(dr["BL_WEB"].ToString());
                        }
                        entity.CalculateComeFee = calculateComeFee;
                        int calculateReceiveFee = 0;
                        if (!string.IsNullOrEmpty(dr["TB_SER_WW"].ToString()) && ValidateUtil.IsInt(dr["TB_SER_WW"].ToString()))
                        {
                            calculateReceiveFee = int.Parse(dr["TB_SER_WW"].ToString());
                        }
                        entity.CalculateReceiveFee = calculateReceiveFee;;

                        if (!string.IsNullOrEmpty(dr["BL_NOT_INPUT"].ToString()) && dr["BL_NOT_INPUT"].ToString().Equals("1"))
                        {
                            entity.Enabled = 0;
                        }
                        else
                        {
                            entity.Enabled = 1;
                        }

                        if (!string.IsNullOrEmpty(dr["ORDER_BY"].ToString()))
                        {
                            entity.SortCode = int.Parse(dr["ORDER_BY"].ToString());
                        }
                        // 02：可以把读取到的数据能写入到用户中心的。
                        System.Console.WriteLine("同步：" + entity.FullName + " " +  entity.Id.ToString() + "...");
                        result = organizeManager.UpdateObject(entity);
                        if (result == 0)
                        {
                            organizeManager.AddObject(entity);
                        }
                        //更新网点基础资料扩展表
                        BaseOrganizeExpressManager organizeExpressManager = new Business.BaseOrganizeExpressManager(this.DbHelper, this.UserInfo);
                        BaseOrganizeExpressEntity organizeExpressEntity = organizeExpressManager.GetObject(dr["ID"].ToString());
                        if (organizeExpressEntity == null)
                        {
                            organizeExpressEntity = new BaseOrganizeExpressEntity();
                            organizeExpressEntity.Id = int.Parse(dr["ID"].ToString());
                        }
                        organizeExpressEntity.Currency = dr["DEFAULT_CURRENCY"].ToString();
                        organizeExpressEntity.DefaultSendPlace = dr["DEFAULT_SEND_PLACE"].ToString();
                        int allow_ToPayment = 0;
                        if (!string.IsNullOrEmpty(dr["BL_ALLOW_TOPAYMENT"].ToString()) && ValidateUtil.IsInt(dr["BL_ALLOW_TOPAYMENT"].ToString()))
                        {
                            allow_ToPayment = int.Parse(dr["BL_ALLOW_TOPAYMENT"].ToString());
                        }
                        organizeExpressEntity.AllowToPayment = allow_ToPayment;
                        organizeExpressEntity.DispatchRange = dr["DISPATCH_RANGE"].ToString();
                        organizeExpressEntity.NotDispatchRange = dr["NOT_DISPATCH_RANGE"].ToString();
                        organizeExpressEntity.DispatchTimeLimit = dr["DISPATCH_TIME_LIMIT"].ToString();
                        int allowAgentMoney = 0;
                        if (!string.IsNullOrEmpty(dr["BL_ALLOW_AGENT_MONEY"].ToString()) && ValidateUtil.IsInt(dr["BL_ALLOW_AGENT_MONEY"].ToString()))
                        {
                            allowAgentMoney = int.Parse(dr["BL_ALLOW_AGENT_MONEY"].ToString());
                        }
                        organizeExpressEntity.AllowAgentMoney = allowAgentMoney;
                        organizeExpressEntity.PublicRemark = dr["PUBLIC_REMARK"].ToString();
                        organizeExpressEntity.PrivateRemark = dr["PRIVATE_REMARK"].ToString();
                        organizeExpressEntity.DispatchMoneyDesc = dr["DISPATCH_MONEY_DESC"].ToString();
                        int scanSelect = 0;
                        if (!string.IsNullOrEmpty(dr["BL_CALC_SITE"].ToString()) && ValidateUtil.IsInt(dr["BL_CALC_SITE"].ToString()))
                        {
                            scanSelect = int.Parse(dr["BL_CALC_SITE"].ToString());
                        }
                        organizeExpressEntity.ScanSelect = scanSelect;
                        Decimal dispatchRangeFee = 0;
                        if (!string.IsNullOrEmpty(dr["DISP_FEE1"].ToString()) && ValidateUtil.IsDouble(dr["DISP_FEE1"].ToString()))
                        {
                            dispatchRangeFee = Decimal.Parse(dr["DISP_FEE1"].ToString());
                        }
                        organizeExpressEntity.DispatchRangeFee = dispatchRangeFee;
                        Decimal dispatchOutRangeFee = 0;
                        if (!string.IsNullOrEmpty(dr["DISP_FEE2"].ToString()) && ValidateUtil.IsDouble(dr["DISP_FEE2"].ToString()))
                        {
                            dispatchOutRangeFee = Decimal.Parse(dr["DISP_FEE2"].ToString());
                        }
                        organizeExpressEntity.DispatchOutRangeFee = dispatchOutRangeFee;
                        Decimal agentMoneyLimited = 0;
                        if (!string.IsNullOrEmpty(dr["GOODS_PAYMENT_LIMITED"].ToString()) && ValidateUtil.IsDouble(dr["GOODS_PAYMENT_LIMITED"].ToString()))
                        {
                            agentMoneyLimited = Decimal.Parse(dr["GOODS_PAYMENT_LIMITED"].ToString());
                        }
                        organizeExpressEntity.AgentMoneyLimited = agentMoneyLimited;
                        Decimal sitePrior = 0;
                        if (!string.IsNullOrEmpty(dr["SITE_PRIOR"].ToString()) && ValidateUtil.IsDouble(dr["SITE_PRIOR"].ToString()))
                        {
                            sitePrior = Decimal.Parse(dr["SITE_PRIOR"].ToString());
                        }
                        organizeExpressEntity.SitePrior = sitePrior;
                        // 02001：可以把读取到的数据能写入到用户中心的网点基础资料扩展表。
                        System.Console.WriteLine("同步网点基础资料扩展表：" + entity.FullName + " " + entity.Id.ToString() + "...");
                        result = organizeExpressManager.Update(organizeExpressEntity);
                        if (result == 0)
                        {
                            organizeExpressManager.Add(organizeExpressEntity);
                        }
                        //更新省市县
                        //更新省份ID
                        commandText = @"UPDATE baseorganize SET provinceId = (SELECT Code FROM basearea t WHERE t.shortname = baseorganize.province) WHERE id = '" + entity.Id + "' ";
                        this.DbHelper.ExecuteNonQuery(commandText);
                        System.Console.WriteLine(commandText);
                        //更新城市ID，由于城市可能出现重复的情况，所以需要选择当前省份的城市
                        commandText = @"UPDATE baseorganize SET cityId = (SELECT Code FROM basearea t WHERE t.fullname = baseorganize.city and t.parentid = baseorganize.provinceid) WHERE id = '" + entity.Id + "' ";
                        this.DbHelper.ExecuteNonQuery(commandText);
                        System.Console.WriteLine(commandText);
                        //更新区县ID，由于区县可能出现重复的情况，所以需要选择当前城市的区县
                        commandText = @"UPDATE baseorganize SET districtId = (SELECT Code FROM basearea t WHERE t.fullname = baseorganize.district and t.parentid = baseorganize.cityid) WHERE id = '" + entity.Id + "' ";
                        this.DbHelper.ExecuteNonQuery(commandText);
                        System.Console.WriteLine(commandText);
                        //更新乡镇ID，由于乡镇可能出现重复的情况，所以需要选择当前区县的乡镇
                        commandText = @"UPDATE baseorganize SET streetid = (SELECT Code FROM basearea t WHERE t.fullname = baseorganize.street and t.parentid = baseorganize.districtId) WHERE id = '" + entity.Id + "' ";
                        this.DbHelper.ExecuteNonQuery(commandText);
                        System.Console.WriteLine(commandText);

                        commandText = @"UPDATE baseorganize SET parentId = (SELECT Id FROM baseorganize t WHERE t.fullname = baseorganize.parentname AND t.deletionstatecode = 0) WHERE id = '" + entity.Id + "' "; // AND t.enabled = 1 WHERE parentId IS NULL
                        this.DbHelper.ExecuteNonQuery(commandText);
                        System.Console.WriteLine(commandText);

                        commandText = @"UPDATE baseorganize SET CompanyId = (SELECT Id FROM baseorganize t WHERE t.fullname = baseorganize.CompanyName AND t.deletionstatecode = 0) WHERE CompanyId IS NULL "; // AND t.enabled = 1 id = '" + entity.Id + "' "; 
                        this.DbHelper.ExecuteNonQuery(commandText);
                        System.Console.WriteLine(commandText);

                        commandText = @"UPDATE baseorganize SET CompanyCode = (SELECT Code FROM baseorganize t WHERE t.fullname = baseorganize.CompanyName AND t.deletionstatecode = 0) WHERE CompanyCode IS NULL "; //  AND t.enabled = 1WHERE id = '" + entity.Id + "'
                        this.DbHelper.ExecuteNonQuery(commandText);
                        System.Console.WriteLine(commandText);
                    }
                    dr.Close();
                    System.Console.WriteLine("完整同步网点表...");
                }

                if (string.IsNullOrWhiteSpace(conditional))
                {
                    // 填充 parentname
                    // select * from baseorganize where parentname is null
                    commandText = @"UPDATE baseorganize SET parentname = (SELECT fullname FROM baseorganize t WHERE t.id = baseorganize.parentId) WHERE parentname IS NULL AND id < 1000000";
                    this.DbHelper.ExecuteNonQuery(commandText);
                    System.Console.WriteLine(commandText);
                    // 填充 parentId
                    // select * from baseorganize where parentId is null
                    commandText = @"UPDATE baseorganize SET parentId = (SELECT Id FROM baseorganize t WHERE t.fullname = baseorganize.parentname AND t.fullname = baseorganize.parentname AND t.deletionstatecode = 0 AND t.enabled = 1 AND t.id < 1000000)"; // WHERE parentId IS NULL
                    // 100000 以下是基础数据的，100000 以上是通用权限管理系统的
                    // UPDATE baseorganize SET parentId = (SELECT Id FROM baseorganize t WHERE t.fullname = baseorganize.parentname) WHERE parentId < 100000
                    this.DbHelper.ExecuteNonQuery(commandText);
                    System.Console.WriteLine(commandText);

                    commandText = @"UPDATE baseorganize SET CompanyId = (SELECT Id FROM baseorganize t WHERE t.fullname = baseorganize.CompanyName AND t.deletionstatecode = 0 AND t.enabled = 1 AND t.id < 1000000)"; // WHERE CompanyId IS NULL
                    this.DbHelper.ExecuteNonQuery(commandText);
                    System.Console.WriteLine(commandText);
                    commandText = @"UPDATE baseorganize SET CompanyCode = (SELECT Code FROM baseorganize t WHERE t.fullname = baseorganize.CompanyName AND t.deletionstatecode = 0 AND t.enabled = 1 AND t.id < 1000000) "; // WHERE CompanyCode IS NULL
                    this.DbHelper.ExecuteNonQuery(commandText);
                    System.Console.WriteLine(commandText);

                    // 更新错误数据
                    commandText = @"UPDATE baseorganize SET parentId = null WHERE id = parentId";
                    this.DbHelper.ExecuteNonQuery(commandText);
                    System.Console.WriteLine(commandText);
                    // 设置员工的公司主键
                    commandText = @"UPDATE " + BaseUserEntity.TableName + " SET CompanyId = (SELECT MAX(Id) FROM baseorganize WHERE baseorganize.fullname = " + BaseUserEntity.TableName + ".companyname AND baseorganize.Id < 1000000) WHERE companyId IS NULL OR companyId = '' AND id < 1000000";
                    this.DbHelper.ExecuteNonQuery(commandText);
                    System.Console.WriteLine(commandText);

                    commandText = @"update baseorganize set baseorganize.city = (select basearea.fullname from basearea where basearea.id = baseorganize.cityid) WHERE cityid IS NOT NULL AND id < 1000000";
                    this.DbHelper.ExecuteNonQuery(commandText);
                    System.Console.WriteLine(commandText);

                    commandText = @"update baseorganize set baseorganize.district = (select basearea.fullname from basearea where basearea.id = baseorganize.districtid) WHERE districtid IS NOT NULL AND id < 1000000";
                    this.DbHelper.ExecuteNonQuery(commandText);
                    System.Console.WriteLine(commandText);

                    commandText = @"update baseorganize set baseorganize.street = (select basearea.fullname from basearea where basearea.id = baseorganize.streetid) WHERE streetid IS NOT NULL AND id < 1000000";
                    this.DbHelper.ExecuteNonQuery(commandText);
                    System.Console.WriteLine(commandText);
                }
            }
            return result;
        }

        #region public virtual void AfterUpdate(BaseOrganizeEntity entity) 组织机构更新后执行的方法
        /// <summary>
        /// 组织机构更新后执行的方法
        /// </summary>
        /// <param name="entity">组织机构实体</param>
        public virtual void AfterUpdate(BaseOrganizeEntity entity)
        {
            #region 更新网点基础资料
            string connectionString = ConfigurationHelper.AppSettings("K8Connection", BaseSystemInfo.EncryptDbConnection);
            if (!string.IsNullOrEmpty(connectionString))
            {
                IDbHelper dbHelper = DbHelperFactory.GetHelper(CurrentDbType.Oracle, connectionString);
                string commandText = string.Format(@"UPDATE TAB_SITE 
                                                        SET PROVINCE = {0}, 
                                                            CITY = {1} , 
                                                            RANGE_NAME = {2} , 
                                                            COUNTY_NAME = {3} ,
                                                            MANAGER = {4} , 
                                                            MANAGER_PHONE = {5}, 
                                                            PRINCIPAL = {6},
                                                            EXIGENCE_PHONE={7},
                                                            SALE_PHONE={8},
                                                            SITE_DESC={9}
                                                      WHERE Id = {10}"
                                        , dbHelper.GetParameter(BaseOrganizeEntity.FieldProvince)
                                        , dbHelper.GetParameter(BaseOrganizeEntity.FieldCity)
                                        , dbHelper.GetParameter(BaseOrganizeEntity.FieldDistrict)
                                        , dbHelper.GetParameter(BaseOrganizeEntity.FieldStreet)
                                        , dbHelper.GetParameter(BaseOrganizeEntity.FieldMaster)
                                        , dbHelper.GetParameter(BaseOrganizeEntity.FieldMasterMobile)
                                        , dbHelper.GetParameter(BaseOrganizeEntity.FieldManager)
                                        , dbHelper.GetParameter(BaseOrganizeEntity.FieldEmergencyCall)
                                        , dbHelper.GetParameter(BaseOrganizeEntity.FieldBusinessPhone)
                                        , dbHelper.GetParameter(BaseOrganizeEntity.FieldDescription)
                                        , dbHelper.GetParameter(BaseOrganizeEntity.FieldId));
                dbHelper.ExecuteNonQuery(commandText, new IDbDataParameter[] {
                                    dbHelper.MakeParameter(BaseOrganizeEntity.FieldProvince, entity.Province)
                                    , dbHelper.MakeParameter(BaseOrganizeEntity.FieldCity, entity.City)
                                    , dbHelper.MakeParameter(BaseOrganizeEntity.FieldDistrict, entity.District)
                                    , dbHelper.MakeParameter(BaseOrganizeEntity.FieldStreet, entity.Street)
                                    , dbHelper.MakeParameter(BaseOrganizeEntity.FieldMaster, entity.Master)
                                    , dbHelper.MakeParameter(BaseOrganizeEntity.FieldMasterMobile, entity.MasterMobile)
                                    , dbHelper.MakeParameter(BaseOrganizeEntity.FieldManager, entity.Manager)
                                    , dbHelper.MakeParameter(BaseOrganizeEntity.FieldEmergencyCall, entity.EmergencyCall)
                                    , dbHelper.MakeParameter(BaseOrganizeEntity.FieldBusinessPhone, entity.BusinessPhone)
                                    , dbHelper.MakeParameter(BaseOrganizeEntity.FieldDescription, entity.Description)
                                    , dbHelper.MakeParameter(BaseOrganizeEntity.FieldId, entity.Id)
                                    });
                System.Console.WriteLine(commandText);
            }
            #endregion
        }
        #endregion
    }
}