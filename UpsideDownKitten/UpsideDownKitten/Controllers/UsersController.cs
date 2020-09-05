using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using UpsideDownKitten.BL.Models;
using UpsideDownKitten.BL.Services.Interfaces;
using UpsideDownKitten.Models;

namespace UpsideDownKitten.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUsersService _usersService;

        public UsersController(IUsersService usersService)
        {
            _usersService = usersService;
        }

        /// <summary>
        /// Creates user with specified email and password
        /// </summary>
        /// <param name="request">Request must have valid email and not empty password</param>
        /// <returns>200 if everything is ok</returns>
        [HttpPost]
        [Route("Create")]
        [ProducesResponseType(201)]
        [ProducesResponseType(typeof(ErrorResponse), 400)]
        public ActionResult Create([FromBody] UserCreateRequest request)
        {
            _usersService.Create(request.Email, request.Password);
            return this.Ok();
        }

        /// <summary>
        /// Returning user with all necessary fields
        /// </summary>
        /// <param name="email">Valid email</param>
        /// <returns>User with fields</returns>
        [HttpGet]
        [Route("Get")]
        [ProducesResponseType(typeof(UserDto),201)]
        [ProducesResponseType(typeof(ErrorResponse), 400)]
        public UserDto Get(string email)
        {
            var user = _usersService.Get(email);
            return user;
        }

        /// <summary>
        /// Returning all users
        /// </summary>
        /// <returns>List of users</returns>
        [HttpGet]
        [Route("")]
        [ProducesResponseType(typeof(List<UserDto>), 201)]
        [ProducesResponseType(typeof(ErrorResponse), 400)]
        public ActionResult<IEnumerable<UserDto>> Get()
        {
            var all = _usersService.All();
            return all;
        }
    }

}
