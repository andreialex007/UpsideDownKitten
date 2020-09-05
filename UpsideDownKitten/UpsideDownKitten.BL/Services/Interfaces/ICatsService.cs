using System.Threading.Tasks;

namespace UpsideDownKitten.BL.Services.Interfaces
{
    public interface ICatsService
    {
        Task<byte[]> GetRotatedAsync();
        Task<byte[]> GetBlurredAsync();
        Task<byte[]> GetBlackWhiteAsync();
    }
}