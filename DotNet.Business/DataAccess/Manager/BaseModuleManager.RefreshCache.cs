//-----------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2016 , Hairihan TECH, Ltd. 
//-----------------------------------------------------------------

using System.Collections.Generic;
using System.Data;

namespace DotNet.Business
{
    using DotNet.Model;

    /// <summary>
    /// BaseModuleManager
    /// 模块菜单权限管理
    /// 
    /// 修改纪录
    /// 
    ///		2016.03.01 版本：1.0 JiRiGaLa	分离代码。
    /// 
    /// <author>
    ///		<name>JiRiGaLa</name>
    ///		<date>2016.03.01</date>
    /// </author> 
    /// </summary>
    public partial class BaseModuleManager : BaseManager
    {
        /// <summary>
        /// 缓存预热,强制重新缓存
        /// </summary>
        /// <returns></returns>
        public static int CachePreheating()
        {
            int result = 0;

            string[] systemCodes = BaseSystemManager.GetSystemCodes();
            for (int i = 0; i < systemCodes.Length; i++)
            {
                GetEntitiesByCache(systemCodes[i], true);
                result += CachePreheating(systemCodes[i]);
            }

            return result;
        }

        /// <summary>
        /// 缓存预热,强制重新缓存
        /// </summary>
        /// <param name="systemCode">系统编号</param>
        /// <returns>影响行数</returns>
        public static int CachePreheating(string systemCode)
        {
            int result = 0;

            // 把所有的组织机构都缓存起来的代码
            BaseModuleManager manager = new BaseModuleManager();
            manager.CurrentTableName = systemCode + "Module";
            using (IDataReader dataReader = manager.ExecuteReader())
            {
                while (dataReader.Read())
                {
                    BaseModuleEntity entity = BaseEntity.Create<BaseModuleEntity>(dataReader, false);
                    if (entity != null)
                    {
                        BaseModuleManager.SetCache(systemCode, entity);
                        result++;
                        System.Console.WriteLine(result.ToString() + " : " + entity.Code);
                    }
                }
                dataReader.Close();
            }

            return result;
        }

        public static int RefreshCache(string systemCode, string moduleId)
        {
            int result = 0;

            // 2016-02-29 吉日嘎拉 强制刷新缓存
            BaseModuleManager.GetObjectByCache(systemCode, moduleId, true);
            
            return result;
        }

        public static int RefreshCache(string systemCode)
        {
            int result = 0;

            List<BaseModuleEntity> list = BaseModuleManager.GetEntitiesByCache(systemCode, true);
            foreach (var entity in list)
            {
                // 2016-02-29 吉日嘎拉 强制刷新缓存
                BaseModuleManager.GetObjectByCache(systemCode, entity.Id, true);
            }

            return result;
        }
    }
}