using Microsoft.AspNetCore.Mvc;
using UpsideDownKitten.BL;

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

        [HttpPost]
        [Route("Create")]
        public ActionResult Create(string email, string password)
        {
            _usersService.Create(email,password);
            return this.Ok();
        }

        [HttpGet]
        [Route("Get")]
        public UserDto Get(string email)
        {
            var user = _usersService.Get(email);
            return user;
        }
    }

}
