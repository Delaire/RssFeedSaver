using System;

namespace RssFeedApi.Models
{
    public class Item
    {
        public string title { get; set; }
        public string link { get; set; }
        public string description { get; set; }
        public Guid guid { get; set; }
        public string pubDate { get; set; }
        public string FullArticle { get; set; }
    }

    public class Entry
    {
        public string parsererror { get; set; }
        public string published { get; set; }
        public string updated { get; set; }
        public string title { get; set; }
        public string content { get; set; }
    }
}