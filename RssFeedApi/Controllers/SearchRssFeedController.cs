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
    public class SearchRssFeedController : Controller
    {
        RssFeedDbController feedController = new RssFeedDbController();
        ArticlesDbController ArticlesDbController = new ArticlesDbController();
        ApiRoot api = new ApiRoot();

        // GET /api/SearchRssFeed?rssName=YourMainUrlName&search=World&page=0
        // Searchs articles titles that are linked to your feed
        public async Task<List<Article>> Get(string rssName, string search, int page)
        {
             //Getting the feed url to call for the azure db
            var feedToCall = feedController.GetFeedRssByShortName(rssName);

            //Making sure element is not null
            if (feedToCall != null)
            {
                return await ArticlesDbController.SearchArticlesById(feedToCall.ID, search, page);
            }

            throw new HttpResponseException(HttpStatusCode.NotFound); ;
        }

    }
}