using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Microsoft.ServiceBus.Notifications;

namespace GoSmokeBackend.Dependencies.Installers
{
  
    public class ServiceInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
           

            container.Register(
              Component.For<NotificationHubClient>()
                  .UsingFactoryMethod((kernel, parameters) => NotificationHubClient.CreateClientFromConnectionString("Endpoint=sb://gosmoke-ns.servicebus.windows.net/;SharedAccessKeyName=DefaultFullSharedAccessSignature;SharedAccessKey=8sToEqopBCMfsEessZ3oGnKqPcAsl7KTN2IcaesjNh0=", "gosmoke"))
                  .LifestyleTransient());

        }
    }
}
