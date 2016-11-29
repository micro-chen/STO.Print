﻿//-----------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2016 , Hairihan TECH, Ltd.  
//-----------------------------------------------------------------

using ServiceStack.Redis;
using System;
using System.Collections.Generic;
using System.Data;

namespace DotNet.Business
{
    using DotNet.Model;
    using DotNet.Utilities;

    /// <summary>
    /// BaseOrganizeManager
    /// 组织机构
    ///
    /// 修改记录
    /// 
    ///		2016.02.02 版本：1.0 JiRiGaLa	进行独立。
    ///
    /// <author>
    ///		<name>JiRiGaLa</name>
    ///		<date>2016.02.02</date>
    /// </author>
    /// </summary>
    public partial class BaseOrganizeManager : BaseManager
    {
        /// <summary>
        /// 编辑之后，需要重新刷新缓存，否则其他读取数据的地方会乱了，或者不及时了
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns>影响行数</returns>
        public int AfterUpdate(BaseOrganizeEntity entity)
        {
            int result = 0;

            // 用户的缓存需要更新
            BaseOrganizeManager.SetCache(entity);
            
            return result;
        }
    }
}