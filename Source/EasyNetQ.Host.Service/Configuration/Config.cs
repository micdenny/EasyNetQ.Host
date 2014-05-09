using System.Configuration;

namespace EasyNetQ.Host.Service.Configuration
{
    internal static class Config
    {
        public static string DefaultRabbitConnectionString
        {
            get
            {
                var connConfig = ConfigurationManager.ConnectionStrings["rabbit"];
                var conn = connConfig != null ? connConfig.ConnectionString : "host=localhost";
                return conn;
            }
        }
        
        public static string RabbitManagementClientHostUrl
        {
            get
            {
                var config = ConfigurationManager.AppSettings["RabbitManagementClientHostUrl"];
                return config ?? "http://localhost";
            }
        }

        public static string RabbitManagementClientHostUsername
        {
            get
            {
                var config = ConfigurationManager.AppSettings["RabbitManagementClientHostUsername"];
                return config ?? "guest";
            }
        }

        public static string RabbitManagementClientHostPassword
        {
            get
            {
                var config = ConfigurationManager.AppSettings["RabbitManagementClientHostPassword"];
                return config ?? "guest";
            }
        }

        public static int RabbitManagementClientHostPort
        {
            get
            {
                var config = ConfigurationManager.AppSettings["RabbitManagementClientHostPort"];
                int port;
                return int.TryParse(config, out port) ? port : 15672;
            }
        }
    }
}