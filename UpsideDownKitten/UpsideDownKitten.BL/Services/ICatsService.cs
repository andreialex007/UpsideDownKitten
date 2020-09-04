using System.Threading.Tasks;

namespace UpsideDownKitten.BL.Services
{
    public interface ICatsService
    {
        Task<byte[]> GetRotatedAsync();
        Task<byte[]> GetBlurredAsync();
        Task<byte[]> GetBlackWhiteAsync();
    }
}