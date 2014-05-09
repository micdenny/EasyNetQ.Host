using System.Configuration;

namespace EasyNetQ.Host.Service.Configuration
{
    public class HostSection : ConfigurationSection
    {
        [ConfigurationProperty("connectionStrings", IsRequired = true, IsDefaultCollection = true)]
        public ConnectionStringSettingsCollection ConnectionStrings
        {
            get { return (ConnectionStringSettingsCollection)this["connectionStrings"]; }
            set { this["connectionStrings"] = value; }
        }
    }
}