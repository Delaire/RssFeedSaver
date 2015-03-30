using System;
using System.Threading;
using System.Web.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RssFeedApi.Controllers;

namespace RssFeedApi.Tests.Controllers
{
    [TestClass]
    public class MainRssFeedControllerTest
    {
        MainRssFeedController feedController = new MainRssFeedController();

        //[SetUp]
        //public void init()
        //{
        //    Thread.CurrentPrincipal = null;

        //    _cache = new Mock<IApiOutputCache>();
        //    _keyGenerator = new Mock<ICacheKeyGenerator>();

        //    var conf = new HttpConfiguration();

        //    var builder = new ContainerBuilder();
        //    builder.RegisterInstance(_cache.Object);

        //    conf.DependencyResolver = new AutofacWebApiDependencyResolver(builder.Build());
        //    conf.Routes.MapHttpRoute(
        //        name: "DefaultApi",
        //        routeTemplate: "api/{controller}/{action}/{id}",
        //        defaults: new { id = RouteParameter.Optional }
        //        );

        //    _server = new HttpServer(conf);
        //}

        [TestMethod]
        public void GetApi()
        {
			//TODO:create Test
            // var data = feedController.Get("FeedMain", 0);
            // Assert.IsNotNull(data);
        }
    }
}
