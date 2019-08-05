namespace Microservice.User.Application.Interfaces.Services
{
    public interface IUserService
    {
        int Post(ServiceModel.Users.User user);
        ServiceModel.Users.User GetUser(int userId);
        void DeleteUserById(int userId);
        ServiceModel.Users.User UpdateUser(ServiceModel.Users.User user);
    }
}
