using System.Collections.Generic;

namespace RssFeedApi.Models
{
    public class Channel
    {
        public string title { get; set; }
        public string link { get; set; }
        public string description { get; set; }
        public string language { get; set; }
        public string copyright { get; set; }
        public string lastBuildDate { get; set; }
        public string managingEditor { get; set; }
        public string webMaster { get; set; }
        public string image { get; set; }
        public List<Item> item { get; set; }
    }
}