//-----------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2016 , Hairihan TECH, Ltd.  
//-----------------------------------------------------------------

using System;
using System.Data;
using System.Collections.Generic;

namespace DotNet.Business
{
    using DotNet.Utilities;
    using DotNet.Model;

    /// <summary>
    /// BaseHolidaysManager 
    /// 节假日表
    ///
    /// 修改记录
    ///
    ///     2012.12.24 版本：1.0 JiRiGaLa 创建主键。
    ///     
    /// </summary>
    /// <author>
    ///		<name>JiRiGaLa</name>
    ///		<date>2012.12.24</date>
    /// </author>
    /// </summary>
    public partial class BaseHolidaysManager : BaseManager //, IBaseRoleManager
    {
        public void AddHoliday(string holiday, bool checkExists = true)
        {
            bool holidayExists = false;
            if (checkExists)
            {
                if (this.Exists(new KeyValuePair<string, object>(BaseHolidaysEntity.FieldHoliday, holiday)))
                {
                    // 这里表示不存在
                    holidayExists = true;
                }
            }
            // 若不存在，那就加记录
            if (!holidayExists)
            {
                BaseHolidaysEntity entity = new BaseHolidaysEntity();
                    entity.Holiday = holiday;
                    entity.Enabled = 1;
                    entity.DeletionStateCode = 0;
                    entity.CreateUserId = this.UserInfo.Id;
                    entity.CreateBy = this.UserInfo.RealName;
                    this.AddObject(entity);
            }
        }
    }
}
