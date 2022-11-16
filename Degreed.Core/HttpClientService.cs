using System;
using System.Net.Http;
using System.Net.Http.Headers;

namespace Degreed.Core
{
    public class HttpClientService
    {
        /// <summary>
        /// This method will connect the external api and will get response data from api
        /// </summary>
        /// <param name="baseUrl"></param>
        /// <param name="mediaType"></param>
        /// <returns></returns>
        public static string ConsumeHttpGetServiceAsync(string baseUrl, string mediaType)
        {
            using (var client = new System.Net.Http.HttpClient())
            {
                using (var request = new HttpRequestMessage())
                {
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    request.Method = HttpMethod.Get;
                    request.RequestUri = new Uri(baseUrl);
                    var response = client.GetAsync(baseUrl).Result;
                    response.EnsureSuccessStatusCode();
                    return response.Content.ReadAsStringAsync().Result;
                } 
            }
        }
    }
}
