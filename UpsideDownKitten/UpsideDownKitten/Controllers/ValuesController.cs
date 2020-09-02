using System.Collections.Generic;
using System.Net.Mime;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using UpsideDownKitten.BL;
using UpsideDownKitten.DL;
using UpsideDownKitten.Utils;

namespace UpsideDownKitten.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "value1", "value2" };
        }

        [HttpGet]
        [Route("GetUpsideDownCat")]
        public async Task<ActionResult> GetUpsideDownCat()
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
            return null;
        }


        [HttpPost]
        [Route("Protected")]
        public async Task<ActionResult> Protected(string email, string password, string passwordConfirmation)
        {
            return null;
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
    }

}
