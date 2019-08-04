namespace Microservice.User.Application.Interfaces.Services
{
    public interface IUserService
    {
        int Post(ServiceModel.Users.User user);
    }
}
