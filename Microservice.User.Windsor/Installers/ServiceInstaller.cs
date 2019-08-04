using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Microservice.User.Application.Interfaces.Services;
using Microservice.User.Application.Services;

namespace Microservice.User.Windsor.Installers
{
    public class ServiceInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                Component
                .For<IUserService>()
                .ImplementedBy<UserService>()
                .DependsOn(Dependency.OnComponent("unitOfWorkFactory", "unitOfWorkFactoryUserService"))
                .LifestyleTransient()
            );

            container.Register(
                Component
                .For<IPhoneService>()
                .ImplementedBy<PhoneService>()
                .LifestyleTransient()
            );
        }
    }
}
