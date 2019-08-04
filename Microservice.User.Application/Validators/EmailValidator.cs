using FluentValidation;
using Microservice.User.ServiceModel.Users;
using System;
using System.Linq;

namespace Microservice.User.Application.Validators
{
    public class EmailValidator : AbstractValidator<Email>
    {
        public EmailValidator()
        {
            RuleFor(email => email.Address).Must(BeValidEmailAddress).WithMessage("Invalid email address.");
        }

        private static bool BeValidEmailAddress(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return false;
            }

            // must contain only 1 @
            var count = input.Count(c => c == '@');
            if (count != 1)
            {
                return false;
            }

            // must contain a period
            if (!input.Contains("."))
            {
                return false;
            }

            // @ must occur before the last period
            var indexOfAt = input.IndexOf("@", StringComparison.Ordinal);
            var lastPeriodIndex = input.LastIndexOf(".", StringComparison.Ordinal);
            var atBeforeLastPeriod = indexOfAt < lastPeriodIndex;

            if (!atBeforeLastPeriod)
            {
                return false;
            }

            return true;
        }
    }
}
