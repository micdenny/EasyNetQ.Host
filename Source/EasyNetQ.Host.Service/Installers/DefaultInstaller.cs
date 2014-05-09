using System.Text.RegularExpressions;
using Castle.Facilities.Logging;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using EasyNetQ.Host.Core;
using EasyNetQ.Host.Service.BusManagement;
using EasyNetQ.Host.Service.Logging;
using EasyNetQ.Management.Client;

namespace EasyNetQ.Host.Service.Installers
{
    public class DefaultInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.AddFacility<LoggingFacility>(f => f.UseLog4Net("log4net.config"));

            // register factories
            container.Register(
                Component.For<IEasyNetQLogger>().ImplementedBy<CastleLoggingEasyNetQLogger>(),
                Component.For<IRabbitFactory>().ImplementedBy<RabbitFactory>(),
                Component.For<IManagementClientFactory>().ImplementedBy<ManagementClientFactory>()
                );

            // register the default IManagementClient for whom does not have multiple RabbitMQ instances
            // TODO: it should be also created an IManagementClientContainer to register multiple management client over multiple RabbitMQ instance/vhost (same as IBusContainer)
            container.Register(
                Component.For<IManagementClient>().UsingFactoryMethod(kernel => kernel.Resolve<IManagementClientFactory>().CreateManagementClient())
                );

            // register the default IBus for whom does not have multiple RabbitMQ instances
            container.Register(
                Component.For<IBus>().UsingFactoryMethod(kernel => kernel.Resolve<IRabbitFactory>().CreateBus())
                );

            // register the IBus container for the supporting multiple RabbitMQ instances
            container.Register(
                Component.For<IBusContainer, IBusProvider, IBusRegister>().ImplementedBy<BusContainer>()
                );

            var assemblyFilter = new AssemblyFilter("");

            // register all the saga(s)
            container.Register(
                Classes.FromAssemblyInDirectory(assemblyFilter)
                       .BasedOn<ISagaEntryPoint>()
                       .WithServiceDefaultInterfaces()
                );

            // register all the subscribers
            container.Register(
                Classes.FromAssemblyInDirectory(assemblyFilter)
                       .BasedOn<ISubscriber>()
                       .WithServiceAllInterfaces()
                );

            // register all the services (classes under *.Services namespace)
            container.Register(
                Classes.FromAssemblyInDirectory(assemblyFilter)
                       .Where(t => Regex.IsMatch(t.Namespace, @"^.*\.Services$", RegexOptions.IgnoreCase))
                       .WithServiceAllInterfaces()
                );
        }
    }
}