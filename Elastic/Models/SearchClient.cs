using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.UI.WebControls;
using Newtonsoft.Json.Linq;

namespace Elastic.Models
{
    public class SearchClient
    {
        private HttpElasticClient _httpClient;
        private string _elasticSearchUrl;

        public SearchClient(HttpElasticClient httpClient, string elasticSearchUrl)
        {
            _elasticSearchUrl = elasticSearchUrl;
            _httpClient = httpClient;
        }

        public ResultModel Get(string q)
        {
            const string url = "/labindex/page/_search";

            var res =
                _httpClient.HttpResult<ResultModel>(_elasticSearchUrl, client => client.PostAsync(url, HttpElasticClient.JsonContent(ElasticQuery.SearchAllQuery(q))));

            return res;
        }

        public ResultModel InitializeIndex(string index)
        {
            string url = "/" + index;

            var res =
                _httpClient.HttpResult<ResultModel>(_elasticSearchUrl, client => client.PostAsync(url, HttpElasticClient.JsonContent(ElasticQuery.Initialization())));

            return res;
        }
        

        
    }
}