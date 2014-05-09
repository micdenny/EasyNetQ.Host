using EasyNetQ.Host.Core;
using EasyNetQ.Host.TestSaga.ServiceContract;

namespace EasyNetQ.Host.TestSaga.Subscribers
{
    public class TestMultipleInstancesSubscriber : ISubscriber
    {
        private readonly IBusContainer _busContainer;

        public TestMultipleInstancesSubscriber(IBusContainer busContainer)
        {
            _busContainer = busContainer;
        }

        public void Subscribe()
        {
            _busContainer["Broker_A"].Subscribe<TestMultipleInstancesMessage>("TestMultipleInstances", message =>
            {
                _busContainer["Broker_B"].Request<GetSomethingRequest, GetSomethingResponse>(new GetSomethingRequest());
                _busContainer["Broker_C"].Request<GetSomethingRequest, GetSomethingResponse>(new GetSomethingRequest());
            });
        }
    }
}
