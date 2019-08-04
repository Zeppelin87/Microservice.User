using Microservice.User.Infrastructure.Interfaces.UnitOfWork;

namespace Microservice.User.Infrastructure.Repositories
{
    public abstract class DatabaseRepository
    {
        protected IUnitOfWork UnitOfWork { get; set; }

        protected DatabaseRepository(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }
    }
}
