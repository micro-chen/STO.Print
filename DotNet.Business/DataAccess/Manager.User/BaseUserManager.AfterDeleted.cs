//-----------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2016 , Hairihan TECH, Ltd.  
//-----------------------------------------------------------------

using System.Collections.Generic;

namespace DotNet.Business
{
    using DotNet.Model;
    using DotNet.Utilities;

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
        public int AfterDeleted(object[] ids, bool enabled = false, bool modifiedUser = false)
        {
            int result = 0;

            for (int i = 0; i < ids.Length; i++)
            {
                string id = ids[i].ToString();
                BaseUserEntity entity = GetObjectByCache(id);

                // 2016-02-19 宋彪 PooledRedisHelper.GetClient() 改为 PooledRedisHelper.GetSpellingClient()
                using (var redisClient = PooledRedisHelper.GetSpellingClient())
                {
                    // 2016-02-02 吉日嘎拉，修改数据后重新设置缓存。
                    BaseUserManager.CachePreheatingSpelling(redisClient, entity);
                }
            }

            if (ids != null && ids.Length > 0)
            {
                // 删除后把已经删除的数据搬迁到被删除表里去。
                /*
                string where = BaseUserEntity.FieldId + " IN (" + StringUtil.ArrayToList((string[])ids, "") + ") ";

                string commandText = @"INSERT INTO BASEUSER_DELETED SELECT * FROM " + BaseUserEntity.TableName + " WHERE " + where;
                IDbHelper dbHelper1 = DbHelperFactory.GetHelper(BaseSystemInfo.UserCenterDbType, BaseSystemInfo.UserCenterDbConnection);
                dbHelper1.ExecuteNonQuery(commandText);
                */

                // 进行删除操作
                this.Delete(ids);
            }

            return result;
        }
    }
}