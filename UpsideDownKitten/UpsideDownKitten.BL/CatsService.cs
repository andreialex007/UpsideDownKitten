using System.Threading.Tasks;

namespace UpsideDownKitten.BL
{
    public interface ICatsService
    {
        Task<byte[]> GetRotated();
        Task<byte[]> GetBlurred();
        Task<byte[]> GetBlackWhite();
    }

    public class CatsService : ICatsService
    {
        private readonly ICatsClient _catsClient;

        public CatsService(ICatsClient catsClient)
        {
            _catsClient = catsClient;
        }

        public async Task<byte[]> GetRotated()
        {
            var result = await _catsClient.GetCatAsync().ConfigureAwait(false);
            return ImagesProcessor.Rotate(result);
        }

        public async Task<byte[]> GetBlurred()
        {
            var result = await _catsClient.GetCatAsync().ConfigureAwait(false);
            return ImagesProcessor.Blur(result);
        }

        public async Task<byte[]> GetBlackWhite()
        {
            var result = await _catsClient.GetCatAsync().ConfigureAwait(false);
            return ImagesProcessor.BlackWhite(result);
        }
    }
}
