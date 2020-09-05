using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UpsideDownKitten.BL.Services.Interfaces;
using UpsideDownKitten.Common;
using UpsideDownKitten.Models;

namespace UpsideDownKitten.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly IUsersService _usersService;

        public TokenController(IUsersService usersService)
        {
            _usersService = usersService;
        }

        /// <summary>
        /// Generates new JWT token, which have to be used in order to have access to api resources
        /// </summary>
        /// <param name="request">Request must have valid email and not empty password</param>
        /// <returns>token</returns>
        [HttpPost]
        [AllowAnonymous]
        [ProducesResponseType(typeof(string), 201)]
        [ProducesResponseType(typeof(ErrorResponse), 400)]
        public string Get(AuthenticateRequest request)
        {
            var user = _usersService.Get(request.Email, request.Password);
            var token = TokenHelper.GetToken(user);
            return token;
        }
    }

}
