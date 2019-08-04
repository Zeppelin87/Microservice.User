using Microservice.User.Application.Interfaces.Services;
using Microservice.User.Application.Utility;
using Microservice.User.Application.Validators;
using Microservice.User.Infrastructure.Interfaces.Factories;
using Microservice.User.Infrastructure.Interfaces.Repositories;
using System;

namespace Microservice.User.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWorkFactory _unitOfWorkFactory;
        private readonly IRepositoryFactory _repositoryFactory;
        private readonly IPhoneService _phoneService;

        public UserService(IUnitOfWorkFactory unitOfWorkFactory, IRepositoryFactory repositoryFactory, IPhoneService phoneService)
        {
            _unitOfWorkFactory = unitOfWorkFactory;
            _repositoryFactory = repositoryFactory;
            _phoneService = phoneService;
        }

        public void DeleteUserById(int userId)
        {
            using (var unitOfWork = _unitOfWorkFactory.Create())
            {
                var userRepository = _repositoryFactory.Create<IUserRepository>(unitOfWork);
                var user = userRepository.GetUser(userId);

                userRepository.DeleteUserById(user.Id);
                userRepository.DeletePhoneById(user.Phone.Id);
                foreach (var email in user.Emails)
                {
                    userRepository.DeleteEmailById(email.Id);
                }

                unitOfWork.Commit();
            }
        }

        public ServiceModel.Users.User GetUser(int userId)
        {
            try
            {
                using (var unitOfWork = _unitOfWorkFactory.Create())
                {
                    var userRepository = _repositoryFactory.Create<IUserRepository>(unitOfWork);
                    var user = userRepository.GetUser(userId);

                    if (user == null)
                    {
                        throw new Exception($"User with an id of {userId} could not be found.");
                    }

                    user.HashedPassword = null;
                    user.Salt = null;

                    return user;
                }
            }
            catch (Exception ex)
            {
                // TODO: Implement logging
                throw;
            }
        }

        public int Post(ServiceModel.Users.User user)
        {
            try
            {
                var number = user.Phone.Number;
                if (!string.IsNullOrEmpty(user.Phone.Extension))
                {
                    number += " ext " + user.Phone.Extension;
                }

                user.Phone = _phoneService.CleanNumber(number);

                var userValidator = new UserValidator();
                var validationResult = userValidator.Validate(user);

                if (!validationResult.IsValid)
                {
                    // TODO: Implement custom exceptions in order to better handle validation failures
                    throw new Exception($"User validation failed: {validationResult.Errors[0]?.ErrorMessage}");
                }

                var credential = Security.GenerateSecurePassword(user.Password);
                user.HashedPassword = credential.HashedPassword;
                user.Salt = credential.Salt;

                using (var unitOfWork = _unitOfWorkFactory.Create())
                {
                    var userRepository = _repositoryFactory.Create<IUserRepository>(unitOfWork);
                    var userId = userRepository.InsertUser(user);
                    unitOfWork.Commit();

                    return userId;
                }
            }
            catch (Exception ex)
            {
                // TODO: Implement logging
                throw;
            }
        }
    }
}
