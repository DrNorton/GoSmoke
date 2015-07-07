using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using GoSmokeBackend.Dao.Repositories;
using GoSmokeBackend.EfDao;
using GoSmokeBackend.EfDao.Base;
using GoSmokeBackend.EfDao.Repositories;


namespace GoSmokeBackend.Dependencies.Installers
{
    public class RepositoriesInstaller: IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component.For<GoSmokeConnectionString>().LifestyleTransient());

            container.Register(Component.For<IAuthRepository>().ImplementedBy<EfAuthRepository>().LifestyleTransient());
            container.Register(Component.For<IProfileRepository>().ImplementedBy<ProfileRepository>().LifestyleTransient());

        }
    }
}
