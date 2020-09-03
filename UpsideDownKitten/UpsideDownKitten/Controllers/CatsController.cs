using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Mime;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using UpsideDownKitten.BL;
using UpsideDownKitten.DL;

namespace UpsideDownKitten.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CatsController : ControllerBase
    {
        // GET api/values
        /// <summary>
        /// get items
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "value1", "value2" };
        }

        [HttpGet]
        [Route("UpsideDown")]
        public async Task<ActionResult> UpsideDown()
        {
            var client = new CatsClient();
            var data = await client.GetCatAsync();
            data = ImagesProcessor.Rotate(data);
            return new FileContentResult(data, MediaTypeNames.Image.Jpeg);
        }

        [HttpPost]
        [Route("CreateUser")]
        public async Task<ActionResult> CreateUser(string email, string password, string passwordConfirmation)
        {
            var dao = new UsersRepository();
            dao.CreateUser(email,password);
            return this.Ok();
        }


        [HttpPost]
        [Route("Protected")]
        [Authorize]
        // [BasicAuth]
        public async Task<string> Protected()
        {
            return "value";
        }

        [HttpGet]
        [Route("GetUserInfo")]
        public async Task<User> GetUserInfo(string email)
        {
            var dao = new UsersRepository();
            var userInfo = dao.GetUserInfo(email);
            return userInfo;
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }

        [HttpPost("/token")]
        public object Token(string username, string password)
        {
            var identity = GetIdentity(username, password);
            if (identity == null)
            {
                return BadRequest(new { errorText = "Invalid username or password." });
            }

            var now = DateTime.UtcNow;
            // создаем JWT-токен
            var jwt = new JwtSecurityToken(
                issuer: AuthOptions.ISSUER,
                audience: AuthOptions.AUDIENCE,
                notBefore: now,
                claims: identity.Claims,
                expires: now.Add(TimeSpan.FromMinutes(AuthOptions.LIFETIME)),
                signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            var response = new
            {
                access_token = encodedJwt,
                username = identity.Name
            };

            return response;
        }

        private ClaimsIdentity GetIdentity(string username, string password)
        {
            var usersDao = new UsersRepository();
            var userInfo = usersDao.GetUserInfo(username, password);

            if (userInfo != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, userInfo.Email),
                };
                ClaimsIdentity claimsIdentity =
                    new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
                        ClaimsIdentity.DefaultRoleClaimType);
                return claimsIdentity;
            }

            // если пользователя не найдено
            return null;
        }
    }

}
