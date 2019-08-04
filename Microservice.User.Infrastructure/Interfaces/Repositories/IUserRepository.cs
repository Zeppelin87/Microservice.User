using Microservice.User.ServiceModel.Users;

namespace Microservice.User.Infrastructure.Interfaces.Repositories
{
    public interface IUserRepository
    {
        int InsertUser(ServiceModel.Users.User user);
        ServiceModel.Users.User GetUser(int userId);
    }
}
