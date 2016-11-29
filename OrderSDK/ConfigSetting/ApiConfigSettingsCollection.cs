using System;
using System.Collections.Generic;
using System.Configuration;

namespace OrderSDK.ConfigSetting
{
    public class ApiConfigSettingsCollection : ConfigurationElementCollection
    {
        private IDictionary<string, ApiConfigElement> _settings;

        protected override ConfigurationElement CreateNewElement()
        {
            return new ApiConfigElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            var e = element as ApiConfigElement;
            return e.Name;
        }

        public IDictionary<string, ApiConfigElement> Settings
        {
            get
            {
                if (_settings == null)
                {
                    _settings = new Dictionary<string, ApiConfigElement>();
                    foreach (ApiConfigElement e in this)
                    {
                        _settings.Add(e.Name, e);
                    }
                }
                return _settings;
            }
        }

        public ApiConfigElement this[string name]
        {
            get
            {
                ApiConfigElement isLoad = null;

                if (_settings.TryGetValue(name, out isLoad))
                {
                    return isLoad;
                }
                else
                {
                    throw new ArgumentException("没有对'" + name + "'节点进行配置。");
                }
            }
        }
    }
}
