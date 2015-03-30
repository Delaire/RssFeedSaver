namespace RssFeedApi.Models
{
    public class RssFeedToCall
    {
        public string UrlItem { get; set; }
    }

    public class RssFeedToCallRoot
    {
        public RssFeedToCall RssFeedToCall { get; set; }
    }

}
