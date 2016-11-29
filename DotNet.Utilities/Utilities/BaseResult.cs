//-----------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2016 , Hairihan TECH, Ltd.  
//-----------------------------------------------------------------

using System;

namespace DotNet.Utilities
{
    /// <summary>
    /// BaseResult  JsonResult<T>
    /// 
    /// 修改记录
    /// 
    ///		2016.01.07 版本：2.0 JiRiGaLa 增加 RecordCount。
    ///		2015.11.16 版本：1.1 SongBiao 增加JsonResult<T> 泛型 可以带数据返回。
    ///		2015.09.16 版本：1.1 JiRiGaLa Result 修改为 Status。
    ///		2015.09.15 版本：1.0 JiRiGaLa 添加返回标准定义。
    ///		
    /// <author>
    ///		<name>JiRiGaLa</name>
    ///		<date>2016.01.07</date>
    /// </author> 
    /// </summary>
    [Serializable]
    public class BaseResult
    {
        /// <summary>
        /// 操作是否成功
        /// 2015-09-16 吉日嘎拉 按宋彪建议进行修正
        /// </summary>
        public bool Status = false;

        /// <summary>
        /// 返回状态代码
        /// </summary>
        public string StatusCode = "UnknownError";

        /// <summary>
        /// 返回消息内容
        /// </summary>
        public string StatusMessage = "未知错误";

        /// <summary>
        /// 查询分页数据时返回记录条数用
        /// </summary>
        public int RecordCount = 0;
    }

    /// <summary>
    /// Json格式带返回数据
    /// </summary>
    /// <typeparam name="T"></typeparam>
    [Serializable]
    public class JsonResult<T> : BaseResult
    {
        public T Data { get; set; }
    }
}