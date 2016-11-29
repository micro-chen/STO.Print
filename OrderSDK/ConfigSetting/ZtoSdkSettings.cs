using System.Configuration;

namespace OrderSDK.ConfigSetting
{
    public class ZtoSdkSettings : ConfigurationSection
    {
        [ConfigurationProperty("ZtoApiSettings", IsRequired = true)]
        public ApiConfigSettingsCollection LoadSettings
        {
            get { return (ApiConfigSettingsCollection)base["ZtoApiSettings"]; }
        }
    }
}
