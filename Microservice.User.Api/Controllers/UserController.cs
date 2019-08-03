using Microservice.User.Application.Interfaces.Services;
using System.Web.Http;

namespace Microservice.User.Api.Controllers
{
    public class UserController : ApiController
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }
    }
}