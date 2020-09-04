using System.Threading.Tasks;

namespace UpsideDownKitten.BL.Services
{
    public interface ICatsService
    {
        Task<byte[]> GetRotated();
        Task<byte[]> GetBlurred();
        Task<byte[]> GetBlackWhite();
    }
}