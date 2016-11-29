//-----------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2016 , Hairihan TECH, Ltd. 
//-----------------------------------------------------------------

namespace DotNet.Business
{
    using DotNet.Model;

    /// <summary>
    /// BaseOrganizeManager
    /// 组织机构管理
    /// 
    /// 修改纪录
    /// 
    ///		2016.02.29 版本：1.0 JiRiGaLa	主键整理。
    /// 
    /// <author>
    ///		<name>JiRiGaLa</name>
    ///		<date>2016.02.29</date>
    /// </author> 
    /// </summary>
    public partial class BaseOrganizeManager : BaseManager
    {
        public static int RefreshCache(string organizeId)
        {
            int result = 0;

            // 2016-02-29 吉日嘎拉 强制刷新缓存
            BaseOrganizeEntity organizeEntity = BaseOrganizeManager.GetObjectByCache(organizeId, true);
            if (organizeEntity != null)
            {
                string[] systemCodes = BaseSystemManager.GetSystemCodes();
                for (int i = 0; i < systemCodes.Length; i++)
                {
                    BaseOrganizePermissionManager.ResetPermissionByCache(systemCodes[i], organizeId);
                }
            }

            return result;
        }
    }
}