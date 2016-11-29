//--------------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2016 , Hairihan TECH, Ltd.  
//--------------------------------------------------------------------

namespace DotNet.Model
{
    /// <summary>
    ///	Java接口返回json格式对应解析类型
    ///
    /// 修改记录
    /// 
    ///     2015.05.11 版本：1.0 YangHengLian 创建类型
    /// 
    /// <author>
    ///		<name>YangHengLian</name>
    ///		<date>2015.05.11</date>
    /// </author> 
    /// </summary>
    public class OrderPhoneNewEntity
    {
        // Java返回Json格式： {"message":"","result":{"recMobile":"13524382205","recName":"沈凌云"},"status":true,"statusCode":""}
        public string message { get; set; }
        public OrderPhoneExtendEntity result { get; set; }

        public bool status { get; set; }

        public string statusCode { get; set; }

    }
    /// <summary>
    /// 收件人实体类
    /// </summary>
    public class OrderPhoneExtendEntity
    {
        public string recMobile { get; set; }
        public string recName { get; set; }
    }
}