//-----------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2016 , Hairihan TECH, Ltd. 
//-----------------------------------------------------------------

using System;
using System.ServiceModel;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Caching;

namespace DotNet.Business
{
    using DotNet.Utilities;

    public partial class Utilities
    {
        /// <summary>
        /// 获取中通单号规则
        /// </summary>
        /// <param name="fromDbConfigure">是否从数据库里获取</param>
        /// <param name="internalSystem">是否从内部系统获取</param>
        /// <returns>单号规则</returns>
        public static string GetBillCodeRegex(bool fromDbConfigure = true, bool internalSystem = false, string hots = "https://userCenter.zt-express.com")
        {
            string billRule = string.Empty;

            string cacheObject = "BillRule";
            // 从数据库配置读取
            if (fromDbConfigure)
            {
                System.Web.Caching.Cache cache = HttpRuntime.Cache;
                if (cache != null && cache[cacheObject] == null)
                {
                    lock (BaseSystemInfo.UserLock)
                    {
                        if (cache != null && cache[cacheObject] == null)
                        {
                            // 是内部系统
                            if (internalSystem)
                            {
                                // 通过WCF或者本地模式获取
                                DotNetService dotNetService = new DotNetService();
                                billRule = dotNetService.ParameterService.GetParameter(BaseSystemInfo.UserInfo, "SystemParameter", "System", "ZTO", "AllBillRule");
                                if (dotNetService.ParameterService is ICommunicationObject)
                                {
                                    ((ICommunicationObject)dotNetService.ParameterService).Close();
                                }
                            }
                            else
                            {
                                // 通过BS远程获取单号规则参数获取
                                if (string.IsNullOrWhiteSpace(hots))
                                {
                                    hots = "https://userCenter.zt-express.com/";
                                }
                                string url = hots + "/UserCenterV42/ParameterService.ashx?function=GetParameter&tableName=SystemParameter&CategoryCode=System&ParameterId=ZTO&ParameterCode=AllBillRule";
                                billRule = DotNet.Business.Utilities.GetResponse(url);
                            }
                            if (string.IsNullOrEmpty(billRule))
                            {
                                billRule = "^((768|778|828|618|680|518|688|010|880|660|805|988|628|205|717|718|728|761|762|701|757|751|358|100|200|128|689)[0-9]{9})$|^((5711|2008|2009|2010)[0-9]{8})$|^((8010|8021)[0-9]{6})$|^([0-9a-zA-Z]{12})$|^(1111[0-9]{10})$|^((a|b|h)[0-9]{13})$|^((9|90|10|19)[0-9]{12})$|^((5)[0-9]{9})$|^(50|51)[0-9]{11}$|^[A-Z]{2}[0-9]{9}[A-Z]{2}$|^[0-9]{13}$|^((88|89|91|92|93|94|95|96|99)[0-9]{8})$|^((8|9)[0-9]{7})$|^((90|36|68)[0-9]{10})$";
                            }
                            cache.Add(cacheObject, billRule, null, DateTime.Now.AddHours(8), TimeSpan.Zero, CacheItemPriority.Normal, null);
                        }
                    }
                }
                billRule = cache[cacheObject] as string;
            }
            if (string.IsNullOrEmpty(billRule))
            {
                billRule = "^((768|778|828|618|680|518|688|010|880|660|805|988|628|205|717|718|728|761|762|701|757|751|358|100|200|128|689)[0-9]{9})$|^((5711|2008|2009|2010)[0-9]{8})$|^((8010|8021)[0-9]{6})$|^([0-9a-zA-Z]{12})$|^(1111[0-9]{10})$|^((a|b|h)[0-9]{13})$|^((9|90|10|19)[0-9]{12})$|^((5)[0-9]{9})$|^(50|51)[0-9]{11}$|^[A-Z]{2}[0-9]{9}[A-Z]{2}$|^[0-9]{13}$|^((88|89|91|92|93|94|95|96|99)[0-9]{8})$|^((8|9)[0-9]{7})$|^((90|36|68)[0-9]{10})$";
            }
            return billRule;
        }

        /// <summary>
        /// 验证中通单号
        /// </summary>
        /// <param name="billCode">单号</param>
        /// <param name="fromDbConfigure">从数据库配置读取</param>
        /// <param name="internalSystem">内部系统(是中天系统)</param>
        /// <returns>是否正确</returns>
        public static bool ValidateBillCode(string billCode, bool fromDbConfigure = true, bool internalSystem = false, string hots = "https://userCenter.zt-express.com/")
        {
            bool result = false;
            string billRule = GetBillCodeRegex(fromDbConfigure, internalSystem, hots);
            result = Regex.IsMatch(billCode, billRule);
            return result;
        }
    }
}