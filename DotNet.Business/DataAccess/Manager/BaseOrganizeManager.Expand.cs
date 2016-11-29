//-----------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2012 , Hairihan TECH, Ltd .
//-----------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.Caching;

namespace DotNet.Business
{
    using DotNet.Model;
    using DotNet.Utilities;

    /// <summary>
    /// BaseOrganizeManager
    /// 组织机构、部门表
    ///
    /// 修改记录
    ///
    ///		2012-05-17 版本：1.0 JiRiGaLa 创建主键。
    ///
    /// 版本：1.0
    ///
    /// <author>
    ///		<name>JiRiGaLa</name>
    ///		<date>2012-05-17</date>
    /// </author>
    /// </summary>
    public partial class BaseOrganizeManager : BaseManager, IBaseManager
    {
        public static List<BaseOrganizeEntity> GetTransferCenterByCache()
        {
            List<BaseOrganizeEntity> result = null;
            System.Web.Caching.Cache cache = HttpRuntime.Cache;
            string cacheObject = "TransferCenter";

            if (cache != null && cache[cacheObject] == null)
            {
                BaseOrganizeManager manager = new DotNet.Business.BaseOrganizeManager();
                result = manager.GetTransferCenter();
                // 若是空的不用缓存，继续读取实体
                if (result != null)
                {
                    cache.Add(cacheObject, result, null, DateTime.Now.AddMinutes(10), TimeSpan.Zero, CacheItemPriority.Normal, null);
                    System.Console.WriteLine(System.DateTime.Now.ToString(BaseSystemInfo.DateTimeFormat) + " cache TransferCenter ");
                }
            }
            result = cache[cacheObject] as List<BaseOrganizeEntity>;
            return result;
        }

        public List<BaseOrganizeEntity> GetTransferCenter()
        {
            List<BaseOrganizeEntity> result = new List<BaseOrganizeEntity>();
            string commandText = "SELECT * FROM " + BaseOrganizeEntity.TableName
                                 + " WHERE id IN (SELECT id FROM baseorganize_express WHERE is_transfer_center = 1) "
                                 + " AND enabled = 1 AND deletionstatecode = 0 ";
            // -- order by sortcode";
            using (IDataReader dataReader = DbHelper.ExecuteReader(commandText))
            {
                result = this.GetList<BaseOrganizeEntity>(dataReader);
            }
            return result;
        }

        /// <summary>
        /// 设置实体
        /// </summary>
        /// <param name="organizeEntity">实体</param>
        partial void SetObjectExpand(SQLBuilder sqlBuilder, BaseOrganizeEntity entity)
        {
            sqlBuilder.SetValue(BaseOrganizeEntity.FieldStatisticalName, entity.StatisticalName);
            sqlBuilder.SetValue(BaseOrganizeEntity.FieldWeightRatio, entity.WeightRatio);
            sqlBuilder.SetValue(BaseOrganizeEntity.FieldSendAir, entity.SendAir);
            sqlBuilder.SetValue(BaseOrganizeEntity.FieldCalculateComeFee, entity.CalculateComeFee);
            sqlBuilder.SetValue(BaseOrganizeEntity.FieldCalculateReceiveFee, entity.CalculateReceiveFee);

            sqlBuilder.SetValue(BaseOrganizeEntity.FieldBillBalanceSite, entity.BillBalanceSite);
            sqlBuilder.SetValue(BaseOrganizeEntity.FieldLevelTwoTransferCenter, entity.LevelTwoTransferCenter);
            sqlBuilder.SetValue(BaseOrganizeEntity.FieldProvinceSite, entity.ProvinceSite);
            sqlBuilder.SetValue(BaseOrganizeEntity.FieldBigArea, entity.BigArea);
            sqlBuilder.SetValue(BaseOrganizeEntity.FieldSendFee, entity.SendFee);
            sqlBuilder.SetValue(BaseOrganizeEntity.FieldLevelTwoTransferFee, entity.LevelTwoTransferFee);
            sqlBuilder.SetValue(BaseOrganizeEntity.FieldBillSubsidy, entity.BillSubsidy);

            sqlBuilder.SetValue(BaseOrganizeEntity.FieldMaster, entity.Master);
            sqlBuilder.SetValue(BaseOrganizeEntity.FieldMasterMobile, entity.MasterMobile);
            sqlBuilder.SetValue(BaseOrganizeEntity.FieldMasterQQ, entity.MasterQQ);
            sqlBuilder.SetValue(BaseOrganizeEntity.FieldManager, entity.Manager);
            sqlBuilder.SetValue(BaseOrganizeEntity.FieldManagerMobile, entity.ManagerMobile);
            sqlBuilder.SetValue(BaseOrganizeEntity.FieldManagerQQ, entity.ManagerQQ);
            sqlBuilder.SetValue(BaseOrganizeEntity.FieldEmergencyCall, entity.EmergencyCall);
            sqlBuilder.SetValue(BaseOrganizeEntity.FieldBusinessPhone, entity.BusinessPhone);
        }
    }
}
