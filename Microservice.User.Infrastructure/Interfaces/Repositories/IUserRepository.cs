namespace Microservice.User.Infrastructure.Interfaces.Repositories
{
    public interface IUserRepository
    {
        int InsertUser(ServiceModel.Users.User user);
        ServiceModel.Users.User GetUser(int userId);
        void DeleteUserById(int userId);
        void DeleteEmailById(int emailId);
        void DeletePhoneById(int phoneId);
    }
}
