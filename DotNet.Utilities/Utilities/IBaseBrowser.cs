//-----------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2016 , Hairihan TECH, Ltd.  
//-----------------------------------------------------------------

namespace DotNet.Utilities
{
    /// <summary>
    /// IBaseBrowser
    /// 
    /// 修改记录
    /// 
    ///		2014.05.15 版本：1.0 JiRiGaLa 添加接口定义。
    ///		
    /// <author>
    ///		<name>JiRiGaLa</name>
    ///		<date>2014.05.15</date>
    /// </author> 
    /// </summary>
    public interface IBaseBrowser
    {
        /// <summary>
        /// 导航地址
        /// </summary>
        string NavigateUrl { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        string NavigateText { get; set; }

        /// <summary>
        /// 导航
        /// </summary>
        /// <param name="url">导航地址</param>
        /// <returns></returns>
        void Navigate(string url);
    }
}
