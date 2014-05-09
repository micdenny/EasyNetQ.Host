namespace EasyNetQ.Host.Core
{
    public interface IBusContainer : IBusProvider, IBusRegister
    {
    }

    public interface IBusProvider
    {
        IBus this[string key] { get; }

        IBus GetBus(string key);
    }

    public interface IBusRegister
    {
        void Register(string key, IBus bus);
    }
}
