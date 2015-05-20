using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Elastic.Models
{


    public class ResultModel
    {
        public int? took { get; set; }
        public bool? timed_out { get; set; }
        public Shards _shards { get; set; }
        public Hits hits { get; set; }
        //public Aggregations aggregations { get; set; }


        public class Shards
        {
            public int? total { get; set; }
            public int? successful { get; set; }
            public int? failed { get; set; }
        }

        public class Hits
        {
            public int? total { get; set; }
            public double? max_score { get; set; }
            public IEnumerable<Hit> hits { get; set; }
        }

        public class Hit
        {
            public string _index { get; set; }
            public string _type { get; set; }
            public string _id { get; set; }
            public double? _score { get; set; }
            public dynamic _source { get; set; }
            //public dynamic highlight { get; set; }
        }

        //public class Aggregations
        //{
        //    public Tags tags { get; set; }
        //    public Sources sources { get; set; }
        //    public PageTypes pagetypes { get; set; }
        //    public GeoTags geotags { get; set; }
        //}

        public class Bucket
        {
            public string key { get; set; }
            public int? doc_count { get; set; }
        }
       
    }
}