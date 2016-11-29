//-----------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2016 , Hairihan TECH, Ltd.  
//-----------------------------------------------------------------

namespace DotNet.Business
{
    using DotNet.Model;

    /// <summary>
    /// BaseUserManager
    /// 用户管理
    /// 
    /// 修改记录
    /// 
    ///		2015.05.22 版本：1.0 JiRiGaLa 删除之后的处理。
    /// 
    /// <author>
    ///		<name>JiRiGaLa</name>
    ///		<date>2015.05.22</date>
    /// </author> 
    /// </summary>
    public partial class BaseUserManager : BaseManager
    {
        /// <summary>
        /// 添加之后，需要重新刷新缓存，否则其他读取数据的地方会乱了，或者不及时了
        /// </summary>
        /// <param name="entity">用户实体</param>
        /// <returns></returns>
        public int AfterAdd(BaseUserEntity entity)
        {
            int result = 0;

            // 2016-01-28 更新用户缓存
            BaseUserManager.SetCache(entity);

            // 2016-02-19 宋彪 PooledRedisHelper.GetClient() 改为 PooledRedisHelper.GetSpellingClient()
            using (var redisClient = PooledRedisHelper.GetSpellingClient())
            {
                BaseUserManager.CachePreheatingSpelling(redisClient, entity);
            }
            // AfterAddSynchronous(entity);

            return result;
        }
    }
}