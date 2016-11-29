//-----------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2015 , Hairihan TECH, Ltd. 
//-----------------------------------------------------------------

using System.Collections.Generic;

namespace DotNet.Business
{
    using DotNet.Model;

    /// <summary>
    /// BaseUserManager
    /// 用户管理
    /// 
    /// 修改纪录
    /// 
    ///		2013.01.05 版本：1.0 JiRiGaLa	创建文件。
    /// 
    /// <author>
    ///		<name>JiRiGaLa</name>
    ///		<date>2013.01.05</date>
    /// </author> 
    /// </summary>
    public partial class BaseUserManager : BaseManager
    {
        public static string GetUsersName(List<BaseUserEntity> list)
        {
            string result = string.Empty;

            foreach (BaseUserEntity entity in list)
            {
                result += "," + entity.RealName;
            }
            if (!string.IsNullOrEmpty(result))
            {
                result = result.Substring(1);
            }
            return result;
        }
    }
}