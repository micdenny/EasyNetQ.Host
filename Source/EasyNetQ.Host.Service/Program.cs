using Topshelf;

namespace EasyNetQ.Host.Service
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            HostFactory.Run(t =>
            {
                t.Service<IService>(s =>
                {
                    s.ConstructUsing(c => new Service());
                    s.WhenStarted(tc => tc.Start());
                    s.WhenStopped(tc => tc.Stop());
                });

                t.RunAsLocalSystem();

                t.SetServiceName("EasyNetQHostService");
                t.SetDisplayName("EasyNetQ Host Service");
                t.SetDescription("This is the EasyNetQ Host service.");

                t.StartAutomatically();
            });
        }
    }
}