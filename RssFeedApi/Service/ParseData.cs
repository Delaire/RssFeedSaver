using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Xml;
using System.Xml.Serialization;
using HtmlAgilityPack;
using Newtonsoft.Json;
using RssFeedApi.Models;
using RssFeedApi.Models.Entities;


namespace RssFeedApi.Service
{
    public class ParseData
    {
        public static List<Article> ParseRssFeed(string result, RssFeedsToCall feedToCall)
        {
            HtmlDocument htmlDoc = new HtmlDocument();
            List<Article> postFeeds = new List<Article>();

            // There are various options, set as needed
            htmlDoc.OptionFixNestedTags = true;

            // filePath is a path to a file containing the html
            htmlDoc.LoadHtml(result);

            // Handle any parse errors as required
            if (htmlDoc.DocumentNode != null)
            {
                var elements = (from tr in htmlDoc.DocumentNode.Descendants("item")
                                select tr).ToList();

                //check to see if we have items in the element if not then the rss feed is not 
                //using the <item> tag
                if (elements.Count == 0)
                {
                    //testing with entry tag
                    elements = (from tr in htmlDoc.DocumentNode.Descendants("entry")
                                select tr).ToList();

                    if (elements.Count == 0)
                    {
                        //testing with article tag
                        elements = (from tr in htmlDoc.DocumentNode.Descendants("article")
                                    select tr).ToList();
                    }
                }

                if (!elements.Any())
                {
                    return null;
                }



                foreach (HtmlNode feedItem in elements)
                {
                    //TODO: to be customized for each FEED
                    //Quick Example for The Verge Feed:  http://www.theverge.com/rss/current

                    #region init prop
                    var title = "empty";
                    var dateTimeNow = System.DateTime.Now;

                    //TODO: this properpty must have the time and date of when the article was published
                    var pubDate = System.DateTime.Now;
                    var link = "empty";
                    var description = "empty";
                    bool saveItem = true;
                    #endregion


                    try
                    {
                        pubDate = DateTime.Parse(feedItem.Element("published").InnerHtml);

                        //making sure that we dont already have this article in the db
                        if (feedToCall.LastUpdatedArticlePubTime.HasValue
                            && feedToCall.LastUpdatedArticlePubTime > pubDate)
                        {
                            saveItem = false;
                        }
                    }
                    catch (Exception ex)
                    {

                    }

                    if (saveItem)
                    {
                        try
                        {
                            description = feedItem.Element("content").InnerHtml;
                        }
                        catch (Exception ex)
                        {
                            description = "Error: " + ex.Message;
                        }

                        try
                        {
                            title = feedItem.Element("title").InnerHtml;
                        }
                        catch (Exception ex)
                        {
                            title = "Error: " + ex.Message;
                        }

                        try
                        {
                            link = feedItem.Element("link").GetAttributeValue("href", "");
                        }
                        catch (Exception ex)
                        {
                            link = "Error: " + ex.Message;
                        }


                        //Todo: Need to parse the rest of the data
                        
                        postFeeds.Add(new Article()
                        {
                            Title = title,
                            Link = link,
                            Description = description,
                            Pubdate = pubDate,
                            CreateDate = dateTimeNow,
                            UrlId = feedToCall.ID,  //Matching the feedId with the Article Id
                            HtmlArticle = feedItem.OuterHtml,
                        });
                    }
                
                }

                return postFeeds;
            }

            return null;
        }
    }
}