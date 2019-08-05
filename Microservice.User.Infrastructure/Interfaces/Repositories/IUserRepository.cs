using Microservice.User.ServiceModel.Users;

namespace Microservice.User.Infrastructure.Interfaces.Repositories
{
    public interface IUserRepository
    {
        int InsertUser(ServiceModel.Users.User user);
        ServiceModel.Users.User GetUser(int userId);
        void DeleteUserById(int userId);
        void DeleteEmailById(int emailId);
        void DeletePhoneById(int phoneId);
        void UpdateUser(ServiceModel.Users.User user);
        void UpdatePhone(Phone phone);
        void UpdateEmail(Email email);
    }
}
