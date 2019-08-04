using Microservice.User.ServiceModel.Users;

namespace Microservice.User.Application.Interfaces.Services
{
    public interface IPhoneService
    {
        Phone CleanNumber(string number, string defaultCountryCode = "");
    }
}
