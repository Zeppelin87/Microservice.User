using Microservice.User.Infrastructure.Interfaces.UnitOfWork;

namespace Microservice.User.Infrastructure.Interfaces.Factories
{
    public interface IRepositoryFactory
    {
        /// <summary>
        /// Creates a repository that does not require a unit of work.
        /// These repositories are likely directly interacting with web
        /// services or other libraries.
        /// </summary>
        /// <returns>
        /// A Repository that does not directly interact with a database.
        /// </returns>
        T Create<T>();

        /// <summary>
        /// Creates a repository that requires a unit of work. These
        /// repositories are likely directly interacting with a database.
        /// </summary>
        /// <typeparam name="T">
        /// The type representing a repository that is to be created.
        /// </typeparam>
        /// <typeparam name="T"></typeparam>
        /// <param name="unitOfWork"></param>
        /// <returns>
        /// A repository that directly interacts with a database.
        /// </returns>
        T Create<T>(IUnitOfWork unitOfWork);

        void Destroy<T>(T repository);
    }
}
