using CacheManager.Core;
using Day18.Models;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Web;
using System.Web.Mvc;

namespace Day18.Controllers
{
    public class RegController : Controller
    {

        string strPath = string.Empty;
        // GET: Reg
        public ActionResult Index()
        {
            var cache = CacheFactory.Build<string>(settings =>
            {
                settings
                    //.WithSystemRuntimeCacheHandle()
                    //.And
                    .WithRedisConfiguration("redis", config =>
                    {
                        config.WithAllowAdmin()
                            .WithDatabase(0)
                            .WithEndpoint("localhost", 6379);
                    })
                    .WithMaxRetries(1000)
                    .WithRetryTimeout(100)
                    .WithRedisBackplane("redis")
                    .WithRedisCacheHandle("redis", true);
            });
            // registerModel
            var result = cache.Get("registerModel");
            RegisterModel model = JsonConvert.DeserializeObject<RegisterModel>(result);
            return View(model);
        }

        [HttpPost]
        public ActionResult Register(RegisterModel registerModel, HttpPostedFileBase headImg)
        {
            UploadHeadImg(headImg);
            registerModel.ImagePath = strPath;
            registerModel.Fav = Request.Form["Fav"];
            RedisSample(registerModel);

            return Redirect("Index");
        }

        [HttpPost]
        public void UploadHeadImg(HttpPostedFileBase headImg)
        {
            //bool isSuccess = true;
            try
            {
                var dateDir = DateTime.Now.ToString("yyyy-MM-dd");
                var spath = Path.Combine(HttpContext.Server.MapPath("~/"), dateDir);
                //var path = HttpContext.Server.MapPath(spath);
                if (!Directory.Exists(spath))
                {
                    Directory.CreateDirectory(spath);
                }

                strPath = Path.Combine(spath, headImg.FileName);
                headImg.SaveAs(strPath);
            }
            catch (Exception ex)
            {
                //isSuccess = false;
            }

            //return isSuccess;
        }


        private static void RedisSample(RegisterModel registerModel)
        {
            var cache = CacheFactory.Build<string>(settings =>
            {
                settings
                    //.WithSystemRuntimeCacheHandle()
                    //.And
                    .WithRedisConfiguration("redis", config =>
                    {
                        config.WithAllowAdmin()
                            .WithDatabase(0)
                            .WithEndpoint("localhost", 6379);
                    })
                    .WithMaxRetries(1000)
                    .WithRetryTimeout(100)
                    .WithRedisBackplane("redis")
                    .WithRedisCacheHandle("redis", true);
            });

            //cache.Add("test", 123456);
            cache.Clear();
            cache.Add(nameof(registerModel), JsonConvert.SerializeObject(registerModel));

            //cache.Update("test", p => p + 1);


        }
    }
}