using Castle.MicroKernel.Registration;
using Castle.Windsor;

namespace Microservice.User.Windsor
{
    public static class WindsorConfig
    {
        private static readonly IWindsorContainer _container;

        public static IWindsorContainer Container
        {
            get { return _container; }
        }

        static WindsorConfig()
        {
            _container = new WindsorContainer();
        }

        public static void Configure(params IWindsorInstaller[] installers)
        {
            foreach (IWindsorInstaller installer in installers)
            {
                _container.Install(installer);
            }
        }
    }
}
