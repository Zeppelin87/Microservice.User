using System.Collections.Generic;

namespace Microservice.User.ServiceModel.Users
{
    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string HashedPassword { get; set; }
        public string Salt { get; set; }
        public IList<Email> Emails { get; set; }
        public IList<Phone> Phones { get; set; }
    }
}
