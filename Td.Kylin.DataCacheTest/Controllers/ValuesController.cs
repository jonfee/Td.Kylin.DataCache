using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using Td.Kylin.DataCache;
using System.Threading;
using System.Diagnostics;

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

            var data = CacheCollection.Items.SystemArea;

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

            var data = CacheCollection.Items.OpenArea;

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

            var data = CacheCollection.Items.AreaForum;

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

            var data = CacheCollection.Items.AreaRecommendIndustry;

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

            var data = CacheCollection.Items.B2CProductCategory;

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

            var data = CacheCollection.Items.B2CProductCategoryTag;

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

            var data = CacheCollection.Items.BusinessService;

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

            var data = CacheCollection.Items.ForumCategory;

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

            var data = CacheCollection.Items.ForumCircle;

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

            var data = CacheCollection.Items.MerchantIndustry;

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

            var data = CacheCollection.Items.SystemGolbalConfig;

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

            var data = CacheCollection.Items.UserEmpiricalConfig;

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

            var data = CacheCollection.Items.UserLevelConfig;

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

            var data = CacheCollection.Items.UserPointsConfig;

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
            CacheCollection.Items.Update(CacheItemType.SystemArea);

            var data = CacheCollection.Items.SystemArea;

            return Ok(data);
        }
    }
}
