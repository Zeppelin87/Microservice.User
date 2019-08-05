using Microservice.User.Infrastructure.Extensions;
using Microservice.User.Infrastructure.Interfaces.Repositories;
using Microservice.User.Infrastructure.Interfaces.UnitOfWork;
using Microservice.User.ServiceModel.Users;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Microservice.User.Infrastructure.Repositories
{
    public class UserRepository : DatabaseRepository, IUserRepository
    {
        private readonly string InsertUserSproc = "[dbo].[InsertUser]";
        private readonly string InsertEmailSproc = "[dbo].[InsertEmail]";
        private readonly string InsertPhoneSproc = "[dbo].[InsertPhone]";
        private readonly string InsertUserEmailSproc = "[dbo].[InsertUserEmail]";
        private readonly string InsertUserPhoneSproc = "[dbo].[InsertUserPhone]";
        private readonly string GetUserByIdSproc = "[dbo].[GetUserById]";
        private readonly string DeleteUserByIdSproc = "[dbo].[DeleteUserById]";
        private readonly string DeleteEmailByIdSproc = "[dbo].[DeleteEmailById]";
        private readonly string DeletePhoneByIdSproc = "[dbo].[DeletePhoneById]";
        private readonly string UpdateUserSproc = "[dbo].[UpdateUser]";
        private readonly string UpdateEmailSproc = "[dbo].[UpdateEmail]";
        private readonly string UpdatePhoneSproc = "[dbo].[UpdatePhone]";

        public UserRepository(IUnitOfWork unitOfWork) : base(unitOfWork) { }

        public int InsertUser(ServiceModel.Users.User user)
        {
            using (var insertUserCommand = UnitOfWork.CreateCommand())
            {
                insertUserCommand.CommandType = CommandType.StoredProcedure;
                insertUserCommand.CommandText = InsertUserSproc;

                insertUserCommand.Parameters.Add(new SqlParameter("@FirstName", user.FirstName));
                insertUserCommand.Parameters.Add(new SqlParameter("@LastName", user.LastName));
                insertUserCommand.Parameters.Add(new SqlParameter("@Username", user.Username));
                insertUserCommand.Parameters.Add(new SqlParameter("@HashedPassword", user.HashedPassword));
                insertUserCommand.Parameters.Add(new SqlParameter("@Salt", user.Salt));

                var userId = (int)insertUserCommand.ExecuteScalar();

                foreach (var email in user?.Emails)
                {
                    var emailId = InsertEmail(email);
                    InsertUserEmail(userId, emailId);
                }

                var phoneId = InsertPhone(user.Phone);
                InsertUserPhone(userId, phoneId);

                return userId;
            }
        }

        private int InsertEmail(Email email)
        {
            using (var insertEmailCommand = UnitOfWork.CreateCommand())
            {
                insertEmailCommand.CommandType = CommandType.StoredProcedure;
                insertEmailCommand.CommandText = InsertEmailSproc;

                insertEmailCommand.Parameters.Add(new SqlParameter("@Address", email.Address));

                var emailId = (int)insertEmailCommand.ExecuteScalar();
                return emailId;
            }
        }

        private void InsertUserEmail(int userId, int emailId)
        {
            using (var insertUserEmailCommand = UnitOfWork.CreateCommand())
            {
                insertUserEmailCommand.CommandType = CommandType.StoredProcedure;
                insertUserEmailCommand.CommandText = InsertUserEmailSproc;

                insertUserEmailCommand.Parameters.Add(new SqlParameter("@UserId", userId));
                insertUserEmailCommand.Parameters.Add(new SqlParameter("@EmailId", emailId));

                insertUserEmailCommand.ExecuteNonQuery();
            }
        }

        private int InsertPhone(Phone phone)
        {
            using (var insertPhoneCommand = UnitOfWork.CreateCommand())
            {
                insertPhoneCommand.CommandType = CommandType.StoredProcedure;
                insertPhoneCommand.CommandText = InsertPhoneSproc;

                insertPhoneCommand.Parameters.Add(new SqlParameter("@CountryCode", phone.CountryCode));
                insertPhoneCommand.Parameters.Add(new SqlParameter("@Number", phone.Number));
                insertPhoneCommand.Parameters.Add(new SqlParameter("@Extension", phone.Extension));

                var phoneId = (int)insertPhoneCommand.ExecuteScalar();
                return phoneId;
            }
        }

        private void InsertUserPhone(int userId, int phoneId)
        {
            using (var insertUserPhoneCommand = UnitOfWork.CreateCommand())
            {
                insertUserPhoneCommand.CommandType = CommandType.StoredProcedure;
                insertUserPhoneCommand.CommandText = InsertUserPhoneSproc;

                insertUserPhoneCommand.Parameters.Add(new SqlParameter("@UserId", userId));
                insertUserPhoneCommand.Parameters.Add(new SqlParameter("@PhoneId", phoneId));

                insertUserPhoneCommand.ExecuteNonQuery();
            }
        }

        public ServiceModel.Users.User GetUser(int userId)
        {
            using (var getUserByIdCommand = UnitOfWork.CreateCommand())
            {
                getUserByIdCommand.CommandType = CommandType.StoredProcedure;
                getUserByIdCommand.CommandText = GetUserByIdSproc;

                getUserByIdCommand.Parameters.Add(new SqlParameter("@UserId", userId));

                using (var reader = getUserByIdCommand.ExecuteReader())
                {
                    return PopulateUserFromDataReader(reader);
                }
            }
        }

        private ServiceModel.Users.User PopulateUserFromDataReader(IDataReader reader)
        {
            var user = new ServiceModel.Users.User();

            if (reader.Read())
            {
                user = new ServiceModel.Users.User()
                {
                    Id = (int)reader["Id"],
                    FirstName = reader.ValueOrNull<string>("FirstName"),
                    LastName = reader.ValueOrNull<string>("LastName"),
                    Username = reader.ValueOrNull<string>("Username"),
                    HashedPassword = reader.ValueOrNull<string>("HashedPassword"),
                    Salt = reader.ValueOrNull<string>("Salt")
                };


                if (reader.NextResult())
                {
                    user.Emails = new List<Email>();
                    while (reader.Read())
                    {
                        var email = new Email()
                        {
                            Id = (int)reader["Id"],
                            Address = reader.ValueOrNull<string>("Address")
                        };

                        user.Emails.Add(email);
                    }
                }

                if (reader.NextResult())
                {
                    if (reader.Read())
                    {
                        user.Phone = new Phone()
                        {
                            Id = (int)reader["Id"],
                            CountryCode = reader.ValueOrNull<string>("CountryCode"),
                            Number = reader.ValueOrNull<string>("Number"),
                            Extension = reader.ValueOrNull<string>("Extension"),
                        };
                    }
                }

                return user;
            }

            return null;
        }

        void IUserRepository.DeleteUserById(int userId)
        {
            using (var deleteUserByIdCommand = UnitOfWork.CreateCommand())
            {
                deleteUserByIdCommand.CommandType = CommandType.StoredProcedure;
                deleteUserByIdCommand.CommandText = DeleteUserByIdSproc;

                deleteUserByIdCommand.Parameters.Add(new SqlParameter("@UserId", userId));

                deleteUserByIdCommand.ExecuteNonQuery();
            }
        }

        void IUserRepository.DeleteEmailById(int emailId)
        {
            using (var deleteEmailByIdCommand = UnitOfWork.CreateCommand())
            {
                deleteEmailByIdCommand.CommandType = CommandType.StoredProcedure;
                deleteEmailByIdCommand.CommandText = DeleteEmailByIdSproc;

                deleteEmailByIdCommand.Parameters.Add(new SqlParameter("@EmailId", emailId));

                deleteEmailByIdCommand.ExecuteNonQuery();
            }
        }

        void IUserRepository.DeletePhoneById(int phoneId)
        {
            using (var deletePhoneByIdCommand = UnitOfWork.CreateCommand())
            {
                deletePhoneByIdCommand.CommandType = CommandType.StoredProcedure;
                deletePhoneByIdCommand.CommandText = DeletePhoneByIdSproc;

                deletePhoneByIdCommand.Parameters.Add(new SqlParameter("@PhoneId", phoneId));

                deletePhoneByIdCommand.ExecuteNonQuery();
            }
        }

        public void UpdateUser(ServiceModel.Users.User user)
        {
            using (var updateUserCommand = UnitOfWork.CreateCommand())
            {
                updateUserCommand.CommandType = CommandType.StoredProcedure;
                updateUserCommand.CommandText = UpdateUserSproc;


                updateUserCommand.Parameters.Add(new SqlParameter("@UserId", user.Id));
                updateUserCommand.Parameters.Add(new SqlParameter("@FirstName", user.FirstName));
                updateUserCommand.Parameters.Add(new SqlParameter("@LastName", user.LastName));
                updateUserCommand.Parameters.Add(new SqlParameter("@Username", user.Username));
                updateUserCommand.Parameters.Add(new SqlParameter("@HashedPassword", user.HashedPassword));
                updateUserCommand.Parameters.Add(new SqlParameter("@Salt", user.Salt));

                updateUserCommand.ExecuteNonQuery();
            }
        }

        public void UpdatePhone(Phone phone)
        {
            using (var updatePhoneCommand = UnitOfWork.CreateCommand())
            {
                updatePhoneCommand.CommandType = CommandType.StoredProcedure;
                updatePhoneCommand.CommandText = UpdatePhoneSproc;

                updatePhoneCommand.Parameters.Add(new SqlParameter("@PhoneId", phone.Id));
                updatePhoneCommand.Parameters.Add(new SqlParameter("@CountryCode", phone.CountryCode));
                updatePhoneCommand.Parameters.Add(new SqlParameter("@Number", phone.Number));
                updatePhoneCommand.Parameters.Add(new SqlParameter("@Extension", phone.Extension));

                updatePhoneCommand.ExecuteNonQuery();
            }
        }

        public void UpdateEmail(Email email)
        {
            using (var updateEmailCommand = UnitOfWork.CreateCommand())
            {
                updateEmailCommand.CommandType = CommandType.StoredProcedure;
                updateEmailCommand.CommandText = UpdateEmailSproc;

                updateEmailCommand.Parameters.Add(new SqlParameter("@EmailId", email.Id));
                updateEmailCommand.Parameters.Add(new SqlParameter("@Address", email.Address));

                updateEmailCommand.ExecuteNonQuery();
            }
        }
    }
}
