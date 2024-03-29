﻿using Microservice.User.Application.Interfaces.Services;
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
            return Request.CreateResponse(HttpStatusCode.Created, userId);
        }

        [HttpGet]
        [Route("~/users/{userId}")]
        public HttpResponseMessage GetUser(int userId)
        {
            var user = _userService.GetUser(userId);
            return Request.CreateResponse(HttpStatusCode.OK, user);
        }

        [HttpDelete]
        [Route("~/users/{userId}")]
        public HttpResponseMessage DeleteUserById(int userId)
        {
            _userService.DeleteUserById(userId);
            return Request.CreateResponse(HttpStatusCode.NoContent);
        }

        [HttpPut]
        [Route("~/users")]
        public HttpResponseMessage PutUser(ServiceModel.Users.User user)
        {
            var updatedUser = _userService.UpdateUser(user);
            return Request.CreateResponse(HttpStatusCode.OK, updatedUser);
        }
    }
}