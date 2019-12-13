using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Laoziwubo.IBLL;
using Laoziwubo.Model.Content;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Laoziwubo.Web.Controllers
{
    public class CrawlerController : Controller
    {
        private readonly IHeroB<HeroM> _heroB;
        public CrawlerController(IHeroB<HeroM> heroB)
        {
            _heroB = heroB;
        }

        public IActionResult Heroes()
        {
            var html = _heroB.InsertHeroes();
            ViewBag.Html = html;
            return View();
        }
    }
}
