using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace Elastic.Models
{
    public static class ElasticQuery
    {
        public static string SearchAllQuery(string q)
        {
            return JsonConvert.SerializeObject(new
            {
                from = 0,
                size = 10,
                query = Query(q ?? "*")
            });
        }


        private static dynamic Query(string query)
        {
            return new 
            {
             
                query_string = new
                {
                    query = query.ToLower()
                }
                
            };
        }

        

        public static string Initialization()
        {
            return JsonConvert.SerializeObject(new
            {
                settings = new
                {
                    analysis = new
                    {
                        filter = new
                        {
                            swedish_stop = new
                            {
                                type = "stop",
                                stopwords = "_swedish_"
                            },
                            swedish_stemmer = new
                            {
                                type = "stemmer",
                                language = "swedish"
                            }
                        }
                        ,
                        analyzer = new
                        {
                            swedish = new
                           {
                                tokenizer = "standard",
                                filter = new[] { "lowercase", "swedish_stop", "swedish_stemmer" }
                            }
                        //    ,
                        //    synonym = new
                        //    {
                        //        tokenizer = "whitespace",
                        //        filter = new[] { "synonym" }
                        //    }
                        }
                    }
                },
                mappings = new
                {
                    page = new
                    {
                        _all = new
                        {
                            enabled = true,
                            analyzer = "swedish"
                        },
                        properties = PageProperties()
                    }
                }
            });
        }

        


        private static dynamic PageProperties()
        {
            return new
            {
                id = new
                {
                    type = "integer",
                    index = "not_analyzed"
                }
                ,
                header = new
                {
                    enabled = true,
                    type = "string",
                    store = true,
                    analyzer = "swedish"
                },
                ingress = new
                {
                    enabled = true,
                    type = "string",
                    store = true,
                    analyzer = "swedish"
                },
                body = new
                {
                    enabled = true,
                    type = "string",
                    store = true,
                    analyzer = "swedish"
                },
                url = new
                {
                    enabled = true,
                    type = "string",
                    store = true,
                    analyzer = "swedish"
                },
                pubdate = new
                {
                    type = "date"
                },
                sourcedate = new
                {
                    type = "date"
                },
                geotags = new
                {
                    type = "string",
                    fields = new
                    {
                        raw = new { type = "string", index = "not_analyzed" }
                    }
                },
                source = new
                {
                    type = "string",
                    fields = new
                    {
                        raw = new { type = "string", index = "not_analyzed" }
                    }
                }
            };
        }
    }
}