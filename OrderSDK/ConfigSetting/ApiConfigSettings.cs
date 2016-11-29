using System.Collections.Generic;
using System.Configuration;

namespace OrderSDK.ConfigSetting
{
    /// <summary>
    /// API配置读取
    /// </summary>
    public static class ApiConfigSettings
    {
        /// <summary>
        /// 根据名称获取配置节
        /// </summary>
        public static IDictionary<string,ApiConfigElement> Settings
        {
            get
            {
                return (ConfigurationManager.GetSection("HttpApiSections") as ZtoSdkSettings).LoadSettings.Settings;
            }
        }
    }
}
