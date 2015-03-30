using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using RssFeedApi.Models.Entities;


namespace RssFeedApi.DbController
{
    public class ArticlesDbController : DbController
    {
        //Get Articles by UrlId, then => Pagination?
        public async Task<List<Article>> GetArticlesById(int id, int page = 0, int take = 10)
        {
            List<Article> article =
                await db.Articles.Where(a => a.UrlId == id)
                                 .OrderBy(a => a.Pubdate)  //allows us to order the article by date
                                 .Skip(page * take).Take(take).ToListAsync();

            //If nothing is found send error to user
            if (!article.Any())
            {
                throw new HttpResponseException(HttpStatusCode.NoContent);
            }

            return (article);
        }

        public async Task<List<Article>> SearchArticlesById(int id,string search, int page = 0, int take = 10)
        {
            List<Article> article =
                await db.Articles.Where(a => a.UrlId == id 
                                          && a.Title.Contains(search))
                                 .OrderBy(a => a.Pubdate)  //allows us to order the article by date
                                 .Skip(page * take).Take(take).ToListAsync();

            //If nothing is found send error to user
            if (!article.Any())
            {
                throw new HttpResponseException(HttpStatusCode.NoContent);
            }

            return (article);
        }
        //Get Articles by UrlId, and by date then => Pagination?

        public async Task<List<Article>> GetArticlesByIdAndDate(int id, DateTime date, int page = 0, int take = 10)
        {
            List<Article> article =
                await db.Articles.Where(a => a.UrlId == id && a.Pubdate >= date.Date)
                                 .OrderBy(a => a.Pubdate)  //allows us to order the article by date
                                 .Skip(page * take).Take(take).ToListAsync();

            //If nothing is found send error to user
            if (!article.Any())
            {
                throw new HttpResponseException(HttpStatusCode.NoContent);
            }

            return (article);
        }

        //Add Articles
        public async Task<bool> AddListArticle(List<Article> ArticlesParsed)
        {
            //Saving Rss Feed to the azure db
            foreach (var article in ArticlesParsed)
            {
                await AddArticle(article);
            }

            return true;
        }

        public async Task<bool> AddArticle(Article article)
        {
            try
            {
                db.Articles.Add(article);
                await db.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
    }
}
