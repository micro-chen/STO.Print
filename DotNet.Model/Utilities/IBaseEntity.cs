//-----------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2016 , Hairihan TECH, Ltd.  
//-----------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Data;

namespace DotNet.Model
{
    /// <summary>
    ///	IBaseEntity
    /// 通用接口部分
    /// 
    /// 修改记录
    /// 
    ///		2012.11.11 版本：1.0 JiRiGaLa 整理接口。
    ///
    /// <author>
    ///		<name>JiRiGaLa</name>
    ///		<date>2012.11.11</date>
    /// </author> 
    /// </summary>
    public interface IBaseEntity
    {
		BaseEntity GetFrom(DataRow dr);

		BaseEntity GetFrom(IDataReader dataReader);

		BaseEntity GetSingle(DataTable dt);
    }
}