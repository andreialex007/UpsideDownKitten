using System.Threading.Tasks;
using UpsideDownKitten.BL.Clients;
using UpsideDownKitten.BL.Utils;

namespace UpsideDownKitten.BL.Services
{
    public class CatsService : ICatsService
    {
        private readonly ICatsClient _catsClient;

        public CatsService(ICatsClient catsClient)
        {
            _catsClient = catsClient;
        }

        public async Task<byte[]> GetRotatedAsync()
        {
            var result = await _catsClient.GetCatAsync();
            return ImagesProcessor.Rotate(result);
        }

        public async Task<byte[]> GetBlurredAsync()
        {
            var result = await _catsClient.GetCatAsync();
            return ImagesProcessor.Blur(result);
        }

        public async Task<byte[]> GetBlackWhiteAsync()
        {
            var result = await _catsClient.GetCatAsync();
            return ImagesProcessor.BlackWhite(result);
        }
    }
}
