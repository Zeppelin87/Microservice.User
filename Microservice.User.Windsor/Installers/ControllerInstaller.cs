using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using System;
using System.Reflection;

namespace Microservice.User.Windsor.Installers
{
    public class ControllerInstaller : IWindsorInstaller
    {
        public Type ControllerType { get; set; }

        public Assembly ExecutingAssembly { get; set; }

        protected virtual void RegisterController(IWindsorContainer container, IConfigurationStore store) { }

        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                Classes.FromAssembly(ExecutingAssembly)
                .BasedOn(ControllerType)
                .Configure((componentRegistration) => componentRegistration.LifestyleTransient())
            );

            RegisterController(container, store);
        }
    }
}
