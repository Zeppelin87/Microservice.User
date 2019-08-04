using FluentValidation;

namespace Microservice.User.Application.Validators
{
    public class UserValidator : AbstractValidator<ServiceModel.Users.User>
    {
        public UserValidator()
        {
            RuleFor(user => user).NotNull();
            RuleFor(user => user.FirstName).Must(NotBeNullOrEmpty).WithMessage("FirstName must not be null or empty.");
            RuleFor(user => user.LastName).Must(NotBeNullOrEmpty).WithMessage("LastName must not be null or empty.");
            RuleFor(user => user.Username).Must(NotBeNullOrEmpty).WithMessage("Username must not be null or empty.");
            RuleFor(user => user.Password).Must(NotBeNullOrEmpty).WithMessage("Password must not be null or empty.");
            RuleFor(user => user.Phone.Number).Must(NotBeNullOrEmpty);
            When(user => user?.Emails.Count > 0, () => {
                RuleForEach(user => user.Emails).SetValidator(new EmailValidator());
            });
        }

        private static bool NotBeNullOrEmpty(string str)
        {
            return !string.IsNullOrEmpty(str);
        }
    }
}
