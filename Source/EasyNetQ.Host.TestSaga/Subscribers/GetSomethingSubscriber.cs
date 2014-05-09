using System;
using EasyNetQ.Host.Core;
using EasyNetQ.Host.TestSaga.ServiceContract;

namespace EasyNetQ.Host.TestSaga.Subscribers
{
    public class GetSomethingSubscriber : ISubscriber
    {
        private readonly IBus _bus;

        public GetSomethingSubscriber(IBus bus)
        {
            _bus = bus;
        }

        public void Subscribe()
        {
            _bus.Respond<GetSomethingRequest, GetSomethingResponse>(request =>
            {
                Console.WriteLine("GetSomethingRequest received and replied.");
                return new GetSomethingResponse();
            });
        }
    }
}
