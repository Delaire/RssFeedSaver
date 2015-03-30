using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace RssFeedApi.Service
{
    public class ApiRoot
    {
        private async Task<string> CallUrl(string pageUrl)
        {
            var req = new HttpClient();
            var message = new HttpRequestMessage(HttpMethod.Get, pageUrl);

            var response = await req.SendAsync(message);
            return await response.Content.ReadAsStringAsync();
        }

        public async Task<string> GetUrlResult(string pageUrl)
        {
            HttpResponseMessage httpResponse = new HttpResponseMessage();
            string ResultData = "";
            try
            {
                var req = new HttpClient();
                var message = new HttpRequestMessage(HttpMethod.Get, pageUrl);

                httpResponse = await req.SendAsync(message);
                ResultData = await httpResponse.Content.ReadAsStringAsync();
            }
            catch (HttpException ex)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
                ResultData = "error:" + ex;
            }
            catch (Exception ex)
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
                ResultData = "error:" + ex;
            }
            finally
            {
                //needs to be disposed for both feeds
               httpResponse.Dispose();
            }

            return ResultData;
        }


        //public async static Task<object> GetLatestArticles()
        //{
        //  //  RssFeedModel.Model.Channel.

        //    return null;
        //}


        //public async static Task<object> GetLatestUrlDate()
        //{


        //    return null;
        //}

    }
}