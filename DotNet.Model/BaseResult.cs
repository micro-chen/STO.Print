//-----------------------------------------------------------------------
// <copyright file="ResultBase.cs" company="Hairihan">
//     Copyright (c) 2015 , All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace DotNet.Model
{
    /// <summary>
    /// 返回数据基类 序列化消息格式
    /// 宋彪 2014-12-24
    /// </summary>
    public class BaseResult
    {
        public bool status { get; set; }

        public string statusCode { get; set; }

        public string message { get; set; }
    }
}