using System.Threading;
using EasyNetQ.Host.Core;
using EasyNetQ.Host.TestSaga.ServiceContract;

namespace EasyNetQ.Host.TestSaga
{
    public class SagaEntryPoint : ISagaEntryPoint
    {
        private readonly IBus _bus;

        private Thread _thread;
        private bool _stopped;

        public SagaEntryPoint(IBus bus)
        {
            _bus = bus;
        }

        public void OnStart()
        {
            _thread = new Thread(() =>
            {
                while (!_stopped)
                {
                    _bus.Publish(new TestMultipleInstancesMessage());
                    Thread.Sleep(1000);
                }
            });
            _thread.Start();
        }

        public void OnStop()
        {
            _stopped = true;
            _thread.Join();
        }
    }
}
