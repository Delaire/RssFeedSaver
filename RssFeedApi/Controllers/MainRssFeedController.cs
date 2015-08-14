using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using RssFeedApi.DbController;
using RssFeedApi.Models.Entities;
using RssFeedApi.Service;
using WebApi.OutputCache.V2;

namespace RssFeedApi.Controllers
{
    public class MainRssFeedController : ApiController
    {
        RssFeedDbController feedController = new RssFeedDbController();
        ArticlesDbController ArticlesDbController = new ArticlesDbController();
        ApiRoot api = new ApiRoot();

        // GET: api/MainRssFeed
        public string Get()
        {
            return "Welcome to Damien Delaire Rss Api saver";
        }

        //// GET: api/MainRssFeed/5
        //public string Get(int id)
        //{
        //    return "value";
        //}

        // GET /api/MainRssFeed?rssName=YourMainUrlName&page=0
        //Example url: http://www.YourMainUrlName.com/rss/current

        ////Possible improvement use a Cache:
        ////Cache for 10s on the server, inform the client that response is valid for 1s  (1 seconds)
        [CacheOutput(ClientTimeSpan = 1, ServerTimeSpan = 1)]
       
        public async Task<List<Article>> Get(string rssName, int page)
        {
            //Getting the feed url to call for the azure db
            var feedToCall = feedController.GetFeedRssByShortName(rssName);

            //Making sure element is not null
            if (feedToCall != null)
            {
                var date = DateTime.Now;

                //if Call to the site has been made more then 1 hour ago fetching then fetch data from the rss feed
                // checking to see when UrlToCall was it last updated
                if (feedToCall.TimeLastCalled <= date.Date.AddMinutes(-60))
                {
                    //data is too old, getting the lastest articles from the Rss Feed
                    //Saving them to the Azure DB and getting the lastest Article from the Azure DB

                    //Getting Rss Feed
                    var ArticlesToParse = await api.GetUrlResult(feedToCall.Url);

                    //Parsing Rss Feed
                    //TODO: how to make sure that you dont have any duplicate?
                    List<Article> ArticlesParsed = ParseData.ParseRssFeed(ArticlesToParse, feedToCall);

                    //making sure that we have items in our list
                    if (ArticlesParsed.Any())
                    {
                        //Saving Rss Feed to the azure db
                        await ArticlesDbController.AddListArticle(ArticlesParsed);

                        var newestArticleAdded = ArticlesParsed.OrderBy(a => a.Pubdate).First();
                        //updating the time
                        feedController.UpdateTimeAndLastArticleAdded(feedToCall, newestArticleAdded);
                    }
                }

                //getting articles form the Azure DB
                return await ArticlesDbController.GetArticlesById(feedToCall.ID, page);
            }

            throw new HttpResponseException(HttpStatusCode.NotFound); ;
        }


       
        //// POST: api/RssFeed
        //public void Post([FromBody]string value)
        //{
        //}

        //// PUT: api/RssFeed/5
        //public void Put(int id, [FromBody]string value)
        //{
        //}

        //// DELETE: api/RssFeed/5
        //public void Delete(int id)
        //{
        //}
    }
}
