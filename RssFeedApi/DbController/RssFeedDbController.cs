using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using RssFeedApi.Models.Entities;


namespace RssFeedApi.DbController
{
    public class RssFeedDbController : DbController
    {
        //Get UrlToCall by UrlId 
        public async Task<RssFeedsToCall> GetUrlToCallsById(int id)
        {
            var UrlsToCalls =
                await db.RssFeedsToCalls.FindAsync(id);

            return (UrlsToCalls);
        }

        //Get UrlToCall by Shortname
        public RssFeedsToCall GetFeedRssByShortName(string Name)
        {
            try
            {
                var UrlsToCalls =
                     db.RssFeedsToCalls.FirstOrDefault(a => a.Name == Name);

                if (UrlsToCalls == null)
                {
                    throw new HttpResponseException(HttpStatusCode.NoContent); ;
                }

                return (UrlsToCalls);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        //Get list  UrlToCall by Site name
        public async Task<List<RssFeedsToCall>> GetListOfUrlToCallsBySiteName(string SiteName)
        {
            var UrlsToCalls =
                await db.RssFeedsToCalls.Where(a => a.SiteName == SiteName).ToListAsync();

            return (UrlsToCalls);
        }



        //Get list  UrlToCall by Shortname
        public async Task<List<RssFeedsToCall>> GetListOfUrlToCallsByShortName(string Name)
        {
            var UrlsToCalls =
                await db.RssFeedsToCalls.Where(a => a.Name == Name).ToListAsync();

            return (UrlsToCalls);
        }

        //Update date of UrlToCall
        public async Task<bool> UpdateTimeAndLastArticleAdded(RssFeedsToCall feedToCall, Article article)
        {
            feedToCall.LastUpdatedArticlePubTime = article.Pubdate;
            feedToCall.TimeLastCalled = DateTime.Now;
            Update(feedToCall);

            return true;
        }
        public async Task<bool> Update(RssFeedsToCall feedToCall)
        {
            db.Entry(feedToCall).State = EntityState.Modified;
            await db.SaveChangesAsync();
            return true;
        }


    }
}