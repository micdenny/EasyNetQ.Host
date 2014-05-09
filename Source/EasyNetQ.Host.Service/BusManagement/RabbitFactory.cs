using EasyNetQ.Host.Service.Configuration;

namespace EasyNetQ.Host.Service.BusManagement
{
    public interface IRabbitFactory
    {
        IEasyNetQLogger EasyNetQLogger { get; set; }

        IBus CreateBus();

        IBus CreateBus(string connectionString);
    }

    public class RabbitFactory : IRabbitFactory
    {
        public IEasyNetQLogger EasyNetQLogger { get; set; }

        public IBus CreateBus()
        {
            return this.CreateBus(Config.DefaultRabbitConnectionString);
        }

        public virtual IBus CreateBus(string connectionString)
        {
            if (this.EasyNetQLogger != null)
            {
                return RabbitHutch.CreateBus(connectionString, register => register.Register(serviceProvider => this.EasyNetQLogger));
            }
            return RabbitHutch.CreateBus(connectionString);
        }
    }
}
