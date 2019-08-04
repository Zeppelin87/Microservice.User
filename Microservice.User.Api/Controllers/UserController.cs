using Microservice.User.Application.Interfaces.Services;
using System.Net;
using System.Net.Http;
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

        [HttpPost]
        [Route("~/users")]
        public HttpResponseMessage Post(ServiceModel.Users.User user)
        {
            var userId = _userService.Post(user);
            return Request.CreateResponse(HttpStatusCode.OK, userId);
        }
    }
}