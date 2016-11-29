using System.Configuration;

namespace OrderSDK.ConfigSetting
{
    public class ApiConfigElement : ConfigurationElement  
    {
        [ConfigurationProperty("name", IsRequired = true)]
        public string Name
        {
            get { return (string)base["name"]; }
            set { base["name"] = value; }
        }

        [ConfigurationProperty("url", IsRequired = true)]
        public string Url
        {
            get { return (string)base["url"]; }
            set { base["url"] = value; }
        }

        [ConfigurationProperty("companyId", IsRequired = true)]
        public string CompanyId
        {
            get { return (string)base["companyId"]; }
            set { base["companyId"] = value; }
        }

        [ConfigurationProperty("password", IsRequired = true)]
        public string Password
        {
            get { return (string)base["password"]; }
            set { base["password"] = value; }
        }

        [ConfigurationProperty("isbase64", IsRequired = true)]
        public bool IsBase64
        {
            get { return (bool)base["isbase64"]; }
            set { base["isbase64"] = value; }
        }

        public override string ToString()
        {
            return string.Format("Name:{0}\nUrl:{1}\nCompanyId:{2}\nPassword:{3}\nIsBase64{4}", Name, Url, CompanyId, Password,
                IsBase64);
        }
    }
}
