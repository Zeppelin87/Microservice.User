using Microservice.User.Application.Interfaces.Services;
using Microservice.User.ServiceModel.Users;

namespace Microservice.User.Application.Services
{
    public class PhoneService : IPhoneService
    {
        public Phone CleanNumber(Phone phone, string defaultCountryCode = "")
        {
            if (!string.IsNullOrEmpty(phone.Extension))
            {
                phone.Number += " ext " + phone.Extension;
            }

            if (string.IsNullOrEmpty(defaultCountryCode))
            {
                defaultCountryCode = "US";
            }

            var phoneNumberUtility = PhoneNumbers.PhoneNumberUtil.GetInstance();
            var phoneNumber = phoneNumberUtility.Parse(phone.Number, defaultCountryCode);

            return new Phone()
            {
                Id = phone.Id,
                CountryCode = phoneNumber.HasCountryCode ? phoneNumber.CountryCode.ToString() : "",
                Number = phoneNumber.HasNationalNumber ? phoneNumber.NationalNumber.ToString() : "",
                Extension = phoneNumber.Extension
            };
        }
    }
}
