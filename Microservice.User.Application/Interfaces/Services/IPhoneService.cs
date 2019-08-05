using Microservice.User.ServiceModel.Users;

namespace Microservice.User.Application.Interfaces.Services
{
    public interface IPhoneService
    {
        Phone CleanNumber(Phone phone, string defaultCountryCode = "");
    }
}
