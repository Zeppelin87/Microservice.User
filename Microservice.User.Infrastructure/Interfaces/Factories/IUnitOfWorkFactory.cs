using Microservice.User.Infrastructure.Interfaces.UnitOfWork;

namespace Microservice.User.Infrastructure.Interfaces.Factories
{
    public interface IUnitOfWorkFactory
    {
        IUnitOfWork Create();
    }
}
