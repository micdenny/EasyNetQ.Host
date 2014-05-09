using EasyNetQ.Host.Service.Configuration;
using EasyNetQ.Management.Client;

namespace EasyNetQ.Host.Service.BusManagement
{
    public interface IManagementClientFactory
    {
        IManagementClient CreateManagementClient();
    }

    public class ManagementClientFactory : IManagementClientFactory
    {
        public IManagementClient CreateManagementClient()
        {
            var client = new ManagementClient(
                Config.RabbitManagementClientHostUrl, 
                Config.RabbitManagementClientHostUsername, 
                Config.RabbitManagementClientHostPassword,
                Config.RabbitManagementClientHostPort);

            return client;
        }
    }
}