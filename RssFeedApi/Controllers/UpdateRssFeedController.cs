using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using RssFeedApi.DbController;
using RssFeedApi.Models.Entities;
using RssFeedApi.Service;

namespace RssFeedApi.Controllers
{
    public class UpdateRssFeedController : Controller
    {
        RssFeedDbController feedController = new RssFeedDbController();
        ArticlesDbController ArticlesDbController = new ArticlesDbController();
        ApiRoot api = new ApiRoot();

        // GET: UpdateRssFeed
        public async Task<bool> Get(string rssName)
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
                    List<Article> ArticlesParsed = ParseData.ParseRssFeed(ArticlesToParse, feedToCall);

                   //Saving Rss Feed to the azure db
                    await ArticlesDbController.AddListArticle(ArticlesParsed);

                    var newestArticleAdded = ArticlesParsed.OrderBy(a => a.Pubdate).First();
                    //updating the time
                    feedController.UpdateTimeAndLastArticleAdded(feedToCall, newestArticleAdded);
                }

                //getting articles form the Azure DB
                return true;
            }

            throw new HttpResponseException(HttpStatusCode.NotFound); ;
        }
    }
}