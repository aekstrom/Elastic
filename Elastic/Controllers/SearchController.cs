using System.Net;
using System.Net.Http;
using System.Web.Http;
using Elastic.Models;

namespace Elastic.Controllers
{
    public class SearchController : ApiController
    {
        public HttpResponseMessage Get(string q = null)
        {
 
            var client = new SearchClient(new HttpElasticClient(), "http://localhost:9700");
            var result = client.Get(q);
            return Request.CreateResponse(HttpStatusCode.OK, result, Request.GetConfiguration());
        }

        [HttpGet]
        public string Test()
        {
            var _client = new HttpElasticClient();

            const string url = "/labindex/page/_search";

            var res =
                _client.HttpResult<ResultModel>("http://localhost:9700",
                client => client.PostAsync(url, HttpElasticClient.JsonContent(ElasticQuery.SearchAllQuery("*"))));

            return Newtonsoft.Json.JsonConvert.SerializeObject(res);
        }

        [HttpGet]
        public HttpResponseMessage Create(string index)
        {
            var client = new SearchClient(new HttpElasticClient(), "http://localhost:9700");
            var result = client.InitializeIndex(index);
            return Request.CreateResponse(HttpStatusCode.OK, result, Request.GetConfiguration());
        }

    }
}
