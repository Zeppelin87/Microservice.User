namespace Microservice.User.ServiceModel.Users
{
    public class Credential
    {
        public string HashedPassword { get; set; }
        public string Salt { get; set; }
    }
}
