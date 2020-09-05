using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace UpsideDownKitten.BL.Clients
{
    public class CatsClient : ICatsClient
    {
        private readonly string _serviceUrl;

        public CatsClient(IConfiguration configuration)
        {
            _serviceUrl = configuration.GetValue<string>("AppSettings:CatsServiceUrl");
        }

        public async Task<byte[]> GetCatAsync()
        {
            using (var client = new HttpClient())
            {
                using (var requestMessage = new HttpRequestMessage(HttpMethod.Get, $"{_serviceUrl}/cat"))
                {
                    var result = await client.SendAsync(requestMessage).ConfigureAwait(false);
                    return await result.Content.ReadAsByteArrayAsync().ConfigureAwait(false);
                }
            }
        }

    }
}
