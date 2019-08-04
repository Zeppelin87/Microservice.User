using Microservice.User.Application.Interfaces.Services;
using Microservice.User.ServiceModel.Users;

namespace Microservice.User.Application.Services
{
    public class PhoneService : IPhoneService
    {
        public Phone CleanNumber(string number, string defaultCountryCode = "")
        {
            if (string.IsNullOrEmpty(defaultCountryCode))
            {
                defaultCountryCode = "US";
            }

            var phoneNumberUtility = PhoneNumbers.PhoneNumberUtil.GetInstance();
            var phoneNumber = phoneNumberUtility.Parse(number, defaultCountryCode);

            return new Phone()
            {
                CountryCode = phoneNumber.HasCountryCode ? phoneNumber.CountryCode.ToString() : "",
                Number = phoneNumber.HasNationalNumber ? phoneNumber.NationalNumber.ToString() : "",
                Extension = phoneNumber.Extension
            };
        }
    }
}
