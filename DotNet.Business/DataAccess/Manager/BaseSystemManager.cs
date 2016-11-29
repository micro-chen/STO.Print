//-----------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2016 , Hairihan TECH, Ltd.  
//-----------------------------------------------------------------

using System.Collections.Generic;

namespace DotNet.Business
{
    using DotNet.Model;

    /// <summary>
    /// BaseSystemManager 
    /// 子系统管理
    ///
    /// 修改记录
    ///
    ///		2015.12.10 版本：1.0 JiRiGaLa  创建。
    ///
    /// </summary>
    /// <author>
    ///		<name>JiRiGaLa</name>
    ///		<date>2015.12.10</date>
    /// </author>
    /// </summary>
    public partial class BaseSystemManager : BaseManager
    {
        public static string[] GetSystemCodes()
        {
            string[] result = null;

            List<KeyValuePair<string, object>> parameters = new List<KeyValuePair<string, object>>();
            parameters.Add(new KeyValuePair<string, object>(BaseItemDetailsEntity.FieldIsPublic, 1));
            parameters.Add(new KeyValuePair<string, object>(BaseItemDetailsEntity.FieldEnabled, 1));
            parameters.Add(new KeyValuePair<string, object>(BaseItemDetailsEntity.FieldDeletionStateCode, 0));

            string tableName = "ItemsSystem";
            BaseItemDetailsManager itemDetailsManager = new BaseItemDetailsManager(tableName);
            result = itemDetailsManager.GetProperties(parameters, BaseItemDetailsEntity.FieldItemCode);

            return result;
        }
    }
}