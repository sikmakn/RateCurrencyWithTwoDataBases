using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using BusinessLogic.Helpers.Interfacies;

namespace BusinessLogic.Helpers
{
    public class Reader: IReader
    {
        public async Task<string> HttpClientRead(string url)
        {
            var handler = new HttpClientHandler()
            {
                AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate
            };
            using (var client = new HttpClient(handler))
            {
                using (var r = await client.GetAsync(new Uri(url)))
                {
                    var result = await r.Content.ReadAsStringAsync();
                    return result;
                }
            }
        }
    }
}
