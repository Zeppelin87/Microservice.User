using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using System.Configuration;
using Microservice.User.Infrastructure.Factories;
using Microservice.User.Infrastructure.Interfaces.Factories;
using Microservice.User.Windsor.Factories;

namespace Microservice.User.Windsor.Installers
{
    public class RepositoryInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                Component
                .For<IUnitOfWorkFactory>()
                .ImplementedBy<UnitOfWorkFactory>()
                .Named("unitOfWorkFactoryUserService")
                .DependsOn(Dependency.OnConfigValue("connectionString", ConfigurationManager.ConnectionStrings["Microservices"].ConnectionString))
            );

            container.Register(
                Component
                .For<IRepositoryFactory>()
                .ImplementedBy<RepositoryFactory>()
                .DependsOn(Dependency.OnValue<IWindsorContainer>(container))
                .LifestyleSingleton()
            );
        }
    }
}
