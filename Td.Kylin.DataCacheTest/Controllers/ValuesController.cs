using Microsoft.AspNet.Mvc;
using System;
using System.Diagnostics;
using Td.Kylin.DataCache;

namespace Td.Kylin.DataCacheTest.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        [HttpGet("systemarea")]
        public IActionResult GetSystemArea()
        {
            Stopwatch watch = new Stopwatch();

            watch.Start();

            var data = CacheCollection.SystemAreaCache.Value();

            watch.Stop();

            return Ok(new
            {
                Time = string.Format("总运行时：{0}毫秒", watch.Elapsed.TotalMilliseconds),
                Data = data
            });
        }

        [HttpGet("openarea")]
        public IActionResult GetOpenArea()
        {
            Stopwatch watch = new Stopwatch();

            watch.Start();

            var data = CacheCollection.OpenAreaCache.Value();

            watch.Stop();

            return Ok(new
            {
                Time = string.Format("总运行时：{0}毫秒", watch.Elapsed.TotalMilliseconds),
                Data = data
            });
        }

        [HttpGet("areaforum")]
        public IActionResult GetAreaForum()
        {
            Stopwatch watch = new Stopwatch();

            watch.Start();

            var data = CacheCollection.AreaForumCache.Value();

            watch.Stop();

            return Ok(new
            {
                Time = string.Format("总运行时：{0}毫秒", watch.Elapsed.TotalMilliseconds),
                Data = data
            });
        }

        [HttpGet("arearecommendindustry")]
        public IActionResult GetAreaRecommendIndustry()
        {
            Stopwatch watch = new Stopwatch();

            watch.Start();

            var data = CacheCollection.AreaRecommendIndustryCache.Value();

            watch.Stop();

            return Ok(new
            {
                Time = string.Format("总运行时：{0}毫秒", watch.Elapsed.TotalMilliseconds),
                Data = data
            });
        }

        [HttpGet("b2cproductcategory")]
        public IActionResult GetB2CProductCategory()
        {
            Stopwatch watch = new Stopwatch();

            watch.Start();

            var data = CacheCollection.B2CProductCategoryCache.Value();

            watch.Stop();

            return Ok(new
            {
                Time = string.Format("总运行时：{0}毫秒", watch.Elapsed.TotalMilliseconds),
                Data = data
            });
        }

        [HttpGet("b2cproductcategorytag")]
        public IActionResult GetB2CProductCategoryTag()
        {
            Stopwatch watch = new Stopwatch();

            watch.Start();

            var data = CacheCollection.B2CProductCategoryTagCache.Value();

            watch.Stop();

            return Ok(new
            {
                Time = string.Format("总运行时：{0}毫秒", watch.Elapsed.TotalMilliseconds),
                Data = data
            });
        }

        [HttpGet("businessservice")]
        public IActionResult GetBusinessService()
        {
            Stopwatch watch = new Stopwatch();

            watch.Start();

            var data = CacheCollection.BusinessServiceCache.Value();

            watch.Stop();

            return Ok(new
            {
                Time = string.Format("总运行时：{0}毫秒", watch.Elapsed.TotalMilliseconds),
                Data = data
            });
        }

        [HttpGet("forumcategory")]
        public IActionResult GetForumCategory()
        {
            Stopwatch watch = new Stopwatch();

            watch.Start();

            var data = CacheCollection.ForumCategoryCache.Value();

            watch.Stop();

            return Ok(new
            {
                Time = string.Format("总运行时：{0}毫秒", watch.Elapsed.TotalMilliseconds),
                Data = data
            });
        }

        [HttpGet("forumcircle")]
        public IActionResult GetForumCircle()
        {
            Stopwatch watch = new Stopwatch();

            watch.Start();

            var data = CacheCollection.ForumCircleCache.Value();

            watch.Stop();

            return Ok(new
            {
                Time = string.Format("总运行时：{0}毫秒", watch.Elapsed.TotalMilliseconds),
                Data = data
            });
        }

        [HttpGet("merchantindustry")]
        public IActionResult GetMerchantIndustry()
        {
            Stopwatch watch = new Stopwatch();

            watch.Start();

            var data = CacheCollection.MerchantIndustryCache.Value();

            watch.Stop();

            return Ok(new
            {
                Time = string.Format("总运行时：{0}毫秒", watch.Elapsed.TotalMilliseconds),
                Data = data
            });
        }

        [HttpGet("systemgolbalconfig")]
        public IActionResult GetSystemGolbalConfig()
        {
            Stopwatch watch = new Stopwatch();

            watch.Start();

            var data = CacheCollection.SystemGolbalConfigCache.Value();

            watch.Stop();

            return Ok(new
            {
                Time = string.Format("总运行时：{0}毫秒", watch.Elapsed.TotalMilliseconds),
                Data = data
            });
        }

        [HttpGet("userempiricalconfig")]
        public IActionResult GetUserEmpiricalConfig()
        {
            Stopwatch watch = new Stopwatch();

            watch.Start();

            var data = CacheCollection.UserEmpiricalConfigCache.Value();

            watch.Stop();

            return Ok(new
            {
                Time = string.Format("总运行时：{0}毫秒", watch.Elapsed.TotalMilliseconds),
                Data = data
            });
        }

        [HttpGet("userlevelconfig")]
        public IActionResult GetUserLevelConfig()
        {
            Stopwatch watch = new Stopwatch();

            watch.Start();

            var data = CacheCollection.UserLevelConfigCache.Value();

            watch.Stop();

            return Ok(new
            {
                Time = string.Format("总运行时：{0}毫秒", watch.Elapsed.TotalMilliseconds),
                Data = data
            });
        }

        [HttpGet("userpointsconfig")]
        public IActionResult GetUserPointsConfig()
        {
            Stopwatch watch = new Stopwatch();

            watch.Start();

            var data = CacheCollection.UserPointsConfigCache.Value();

            watch.Stop();

            return Ok(new
            {
                Time = string.Format("总运行时：{0}毫秒", watch.Elapsed.TotalMilliseconds),
                Data = data
            });
        }

        [HttpGet("merchantproductsystemcategory")]
        public IActionResult GetMerchantProductSystemCategory()
        {
            Stopwatch watch = new Stopwatch();

            watch.Start();

            var data = CacheCollection.MerchantProductSystemCategoryCache.Value();

            watch.Stop();

            return Ok(new
            {
                Time = string.Format("总运行时：{0}毫秒", watch.Elapsed.TotalMilliseconds),
                Data = data
            });
        }

        [HttpGet("jobcategory")]
        public IActionResult GetJobCategory()
        {
            Stopwatch watch = new Stopwatch();

            watch.Start();

            var data = CacheCollection.JobCategoryCache.Value();

            watch.Stop();

            return Ok(new
            {
                Time = string.Format("总运行时：{0}毫秒", watch.Elapsed.TotalMilliseconds),
                Data = data
            });
        }

        [HttpGet("test")]
        public IActionResult Test()
        {
            CacheCollection.SystemAreaCache.Update(new DataCache.CacheModel.SystemAreaCacheModel { AreaID = 110000, AreaName = "北京" });

            var data = CacheCollection.SystemAreaCache.Value();

            return Ok(data);
        }

        [HttpGet("getarea/{areaID}")]
        public IActionResult GetArea(int areaID)
        {
            var area = CacheCollection.SystemAreaCache.Get(areaID);

            return Ok(area);
        }

        [HttpGet("update/{level:int}")]
        public IActionResult GetUpdateLevel(int level)
        {
            var cacheLevel = (CacheLevel)Enum.Parse(typeof(CacheLevel), level.ToString());
            CacheCollection.Update(cacheLevel);

            var config = CacheCollection.SystemGolbalConfigCache.Value();

            return Ok(config);
        }
    }
}
