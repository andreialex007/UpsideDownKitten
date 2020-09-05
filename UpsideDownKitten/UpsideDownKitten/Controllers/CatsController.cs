using System.Net.Mime;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using UpsideDownKitten.BL.Services.Interfaces;

namespace UpsideDownKitten.Controllers
{
    /// <summary>
    /// Provides different methods for modifying cat images
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class CatsController : ControllerBase
    {
        private readonly ICatsService _catsService;

        public CatsController(ICatsService catsService)
        {
            _catsService = catsService;
        }

        /// <summary>
        /// Returning random image rotated to 180 degree cat image
        /// </summary>
        /// <returns>UpsideDown cat</returns>
        [HttpGet]
        [Route("UpsideDown")]
        public async Task<ActionResult> UpsideDown()
        {
            var result =  await _catsService.GetRotatedAsync().ConfigureAwait(false);
            return new FileContentResult(result, MediaTypeNames.Image.Jpeg);
        }

        /// <summary>
        /// Returning random image converted to black and white image
        /// </summary>
        /// <returns>Black white cat</returns>
        [HttpGet]
        [Route("BlackWhite")]
        public async Task<ActionResult> BlackWhite()
        {
            var result = await _catsService.GetBlackWhiteAsync().ConfigureAwait(false);
            return new FileContentResult(result, MediaTypeNames.Image.Jpeg);
        }

        /// <summary>
        /// Returning random image blurred according to Gaussian blur
        /// </summary>
        /// <returns>Blurred cat</returns>
        [HttpGet]
        [Route("Blurred")]
        public async Task<ActionResult> Blurred()
        {
            var result = await _catsService.GetBlurredAsync().ConfigureAwait(false);
            return new FileContentResult(result, MediaTypeNames.Image.Jpeg);
        }
    }
}
