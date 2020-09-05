using System.Threading.Tasks;
using UpsideDownKitten.BL.Clients;
using UpsideDownKitten.BL.Common;
using UpsideDownKitten.BL.Services.Interfaces;

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
            var result = await _catsClient.GetCatAsync().ConfigureAwait(false);
            return ImagesProcessor.Rotate(result);
        }

        public async Task<byte[]> GetBlurredAsync()
        {
            var result = await _catsClient.GetCatAsync().ConfigureAwait(false);
            return ImagesProcessor.Blur(result);
        }

        public async Task<byte[]> GetBlackWhiteAsync()
        {
            var result = await _catsClient.GetCatAsync().ConfigureAwait(false);
            return ImagesProcessor.BlackWhite(result);
        }
    }
}
