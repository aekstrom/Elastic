using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Elastic.Models
{
    public class HttpElasticClient
    {
        public T HttpResult<T>(string baseUrl, Func<HttpClient, Task<HttpResponseMessage>> Do)
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                
                    httpClient.BaseAddress = new Uri(baseUrl);
                    
                    var result = Do(httpClient);
                    if (!result.Result.StatusCode.Equals(HttpStatusCode.NotFound))
                        result.Result.EnsureSuccessStatusCode();

                    var res = result.Result.Content.ReadAsStringAsync().Result;

                    return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(res);
                }
            }
            catch (Exception ex)
            {
                return default(T);
            }
        }

        public static StringContent JsonContent(string content)
        {
            return new StringContent(content, Encoding.UTF8, "application/json");
        }
    }
}