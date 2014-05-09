using System;
using EasyNetQ.Host.Core;
using EasyNetQ.Host.TestSaga.ServiceContract;

namespace EasyNetQ.Host.TestSaga.Subscribers
{
    public class TestSubscriber : ISubscriber
    {
        private readonly IBus _bus;

        public TestSubscriber(IBus bus)
        {
            _bus = bus;
        }

        public void Subscribe()
        {
            _bus.Subscribe<TestMessage>("JustTesting", message =>
            {
                Console.WriteLine("TestMessage received.");
            });
        }
    }
}
