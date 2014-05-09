using System.Configuration;

namespace EasyNetQ.Host.Service.Configuration
{
    public static class HostConfig
    {
        private static HostSection HostSettings
        {
            get { return (HostSection)ConfigurationManager.GetSection("easyNetQHost"); }
        }

        public static ConnectionStringSettingsCollection ConnectionStrings
        {
            get
            {
                if (HostSettings == null) return null;
                return HostSettings.ConnectionStrings;
            }
        }
    }
}