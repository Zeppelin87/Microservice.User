using Castle.Windsor;
using Microservice.User.Infrastructure.Interfaces.Factories;
using Microservice.User.Infrastructure.Interfaces.UnitOfWork;

namespace Microservice.User.Windsor.Factories
{
    public class RepositoryFactory : IRepositoryFactory
    {
        private readonly IWindsorContainer _container;

        public RepositoryFactory(IWindsorContainer container)
        {
            _container = container;
        }

        public T Create<T>()
        {
            return _container.Resolve<T>();
        }

        public T Create<T>(IUnitOfWork unitOfWork)
        {
            return _container.Resolve<T>(new { unitOfWork });
        }

        public void Destroy<T>(T repository)
        {
            _container.Release(repository);
        }
    }
}
