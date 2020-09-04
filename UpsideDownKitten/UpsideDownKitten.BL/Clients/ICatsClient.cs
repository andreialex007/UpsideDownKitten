using System.Threading.Tasks;

namespace UpsideDownKitten.BL.Clients
{
    public interface ICatsClient
    {
        Task<byte[]> GetCatAsync();
    }
}