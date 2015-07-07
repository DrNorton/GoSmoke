using System.Web.Http;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using GoSmokeBackend.Controllers.ApiResults;

namespace GoSmokeBackend.Dependencies.Installers
{
    public class ControllersInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Classes.FromAssemblyContaining<ApiResult>()
                    .BasedOn<ApiController>()
                    .LifestyleTransient());
        }
    }
}
