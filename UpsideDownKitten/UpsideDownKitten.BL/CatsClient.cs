using System.Net.Http;
using System.Threading.Tasks;

namespace UpsideDownKitten.BL
{
    public class CatsClient
    {
        public async Task<byte[]> GetCatAsync()
        {
            using (var client = new HttpClient())
            {
                using (var requestMessage = new HttpRequestMessage(HttpMethod.Get, "https://cataas.com/cat"))
                {
                    var result = await client.SendAsync(requestMessage);
                    return await result.Content.ReadAsByteArrayAsync();
                }
            }
        }
    }
}
