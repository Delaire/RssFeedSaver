using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Helpers;
using RssFeedApi.DbController;
using RssFeedApi.Models.Entities;


namespace RssFeedApi.Service
{
    public static class ServiceApi
    {
        public static RssFeedDbController UrlsDbController = new RssFeedDbController();
        public static ArticlesDbController ArticlesDbController = new ArticlesDbController();
        public static ApiRoot api = new ApiRoot();

        public async static Task<List<Article>> GetArticles(string RssName)
        {
            //Getting url to call and check to see if the last time it has been updated was more then an hour ago
            var UrlToCall = UrlsDbController.GetUrlToCallsByShortName(RssName);
            List<Article> ArticlesToSend = null;
            //Making sure element is not null
            if (UrlToCall != null)
            {
                var date = DateTime.Now;
#pragma warning disable 1587
                /// checking to see when UrlToCall was it last updated
                ///if TimeLastCalled was last updated less then one hour ago then dont call the url and get the 
                ///article directly from the Azure DB if not get the lastest articles from the site 
                ///update the Azure DB and then call the Azure Db to get the last 10 articles of the site
#pragma warning restore 1587

                // if Call to the site has been made more then 1 hour ago fetching then fetch data from the rss feed
                
                if (UrlToCall.TimeLastCalled <= date.Date.AddMinutes(-60))
                {
                    //data is too old, getting the lastest articles from the Rss Feed
                    //Saving them to the Azure DB and getting the lastest Article from the Azure DB

                    var ArticlesToParse = await api.GetUrlResult(UrlToCall.Url);
                    var ArticlesParsed = ParseData.ParseRssFeed(ArticlesToParse);




                }

                //getting articles form the Azure DB
                ArticlesToSend = await ArticlesDbController.GetArticlesById(UrlToCall.ID);

                return ArticlesToSend;

            }




            return null;
        }

    }
}