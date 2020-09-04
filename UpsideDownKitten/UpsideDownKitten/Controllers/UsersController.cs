using Microsoft.AspNetCore.Mvc;
using UpsideDownKitten.BL.Models;
using UpsideDownKitten.BL.Services;

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
        /// <param name="email">Valid email</param>
        /// <param name="password">Any not empty password</param>
        /// <returns>Ok result will be returned upon successfull execution</returns>
        [HttpPost]
        [Route("Create")]
        public ActionResult Create(string email, string password)
        {
            _usersService.Create(email,password);
            return this.Ok();
        }

        /// <summary>
        /// Returning user with all necessary fields
        /// </summary>
        /// <param name="email">Valid email</param>
        /// <returns>User with fields</returns>
        [HttpGet]
        [Route("Get")]
        public UserDto Get(string email)
        {
            var user = _usersService.Get(email);
            return user;
        }
    }

}
