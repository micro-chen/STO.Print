//-----------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2016 , Hairihan TECH, Ltd.  
//-----------------------------------------------------------------

using System.Data;

namespace DotNet.Business
{
    using DotNet.Model;

    /// <summary>
    /// BaseUserLogOnManager
    /// 用户管理
    /// 
    /// 修改记录
    /// 
    ///		2015.12.08 版本：1.1 JiRiGaLa	缓存预热功能实现。
    ///     
    /// <author>
    ///		<name>JiRiGaLa</name>
    ///		<date>2015.12.08</date>
    /// </author> 
    /// </summary>
    public partial class BaseUserLogOnManager
    {   
        /// <summary>
        /// 缓存预热,强制重新缓存
        /// </summary>
        /// <returns>影响行数</returns>
        public static int CachePreheating()
        {
            int result = 0;

            // 把所有的数据都缓存起来的代码
            BaseUserLogOnManager manager = new BaseUserLogOnManager();
            // 基础用户的登录信息重新缓存起来
            string commandText = "SELECT * FROM baseuserlogon t WHERE t.userpassword IS NOT NULL AND t.openidtimeout IS NOT NULL AND t.enabled = 1 AND t.openidtimeout - sysdate < 0.5";
            using (IDataReader dataReader = manager.ExecuteReader(commandText))
            {
                using (var redisClient = PooledRedisHelper.GetClient())
                {
                    while (dataReader.Read())
                    {
                        BaseUserLogOnEntity entity = BaseEntity.Create<BaseUserLogOnEntity>(dataReader, false);
# if Redis
                        Utilities.SetUserOpenId(redisClient, entity.Id, entity.OpenId);
#else
                        Utilities.SetUserOpenId(entity.Id, entity.OpenId);
# endif
                        result++;
                        System.Console.WriteLine(result.ToString() + " : User : " + entity.Id);
                    }
                    dataReader.Close();
                }
            }

            return result;
        }


        /// <summary>
        /// 缓存预热,强制重新缓存
        /// </summary>
        /// <returns>影响行数</returns>
        public static int CachePreheating_PDA()
        {
            int result = 0;

            // 把所有的数据都缓存起来的代码
            BaseUserLogOnManager manager = new BaseUserLogOnManager("PDAUserLogOn");
            string commandText = "SELECT * FROM pdauserlogon t WHERE t.userpassword IS NOT NULL AND t.openidtimeout IS NOT NULL AND t.enabled = 1 AND t.openidtimeout - sysdate < 0.5";
            // 基础用户的登录信息重新缓存起来
            using (IDataReader dataReader = manager.ExecuteReader(commandText))
            {
                using (var redisClient = PooledRedisHelper.GetClient())
                {
                    while (dataReader.Read())
                    {
                        BaseUserLogOnEntity entity = BaseEntity.Create<BaseUserLogOnEntity>(dataReader, false);
# if Redis
                        Utilities.SetUserOpenId(redisClient, entity.Id, entity.OpenId, "PDA");
# endif
                        result++;
                        System.Console.WriteLine(result.ToString() + " : PDA : " + entity.Id);
                    }
                    dataReader.Close();
                }
            }

            return result;
        }
    }
}