using System.Collections.Generic;
using System.Linq;
using Castle.Core.Logging;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Castle.Windsor.Installer;
using EasyNetQ.Host.Core;

namespace EasyNetQ.Host.Service
{
    public interface IService
    {
        void Start();

        void Stop();
    }

    public class Service : IService
    {
        private readonly IWindsorContainer _container;

        private ILogger _log;
        private IEnumerable<ISagaEntryPoint> _sagas;
        private IEnumerable<ISubscriber> _subscribers;

        public Service()
        {
            _container = new WindsorContainer();

            // install all the saga(s) container intaller
            _container.Install(FromAssembly.InDirectory(new AssemblyFilter("")));
        }

        public void Start()
        {
            _log = _container.Resolve<ILogger>();
            _sagas = _container.ResolveAll<ISagaEntryPoint>();
            _subscribers = _container.ResolveAll<ISubscriber>();
            
            foreach (var saga in _sagas)
            {
                saga.OnStart();
            }
            
            foreach (var subscriber in _subscribers)
            {
                subscriber.Subscribe();
            }

            if (!_sagas.Any() && !_subscribers.Any())
            {
                _log.Warn("The service is idle, there's no saga installed to be run. Please copy all the saga(s) you want to run in the executable folder.");
            }
        }

        public void Stop()
        {
            foreach (var saga in _sagas)
            {
                saga.OnStop();
            }

            // release resources
            foreach (var subscriber in _subscribers)
            {
                _container.Release(subscriber);
            }
            foreach (var saga in _sagas)
            {
                _container.Release(saga);
            }
            _container.Release(_log);
            _container.Dispose();
        }
    }
}