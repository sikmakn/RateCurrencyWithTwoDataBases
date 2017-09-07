using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace BusinessLogic.RateUpdate
{
    public class Reader
    {
        public static async Task<string> HttpClientRead(string url)
        {
            HttpClientHandler handler = new HttpClientHandler()
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
