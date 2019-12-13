using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Laoziwubo.IBLL;
using Laoziwubo.Model.Common;
using Laoziwubo.Model.Content;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Laoziwubo.Web.Controllers
{
    public class IndexController : Controller
    {
        private readonly IBaseB<StickerM> _baseB;
        public IndexController(IBaseB<StickerM> baseB)
        {
            _baseB = baseB;
        }

        public async Task<IActionResult> Index()
        {
            var q = new QueryM()
            {
                PageSize = 10,
                PageNum = 1
            };
            var list = await _baseB.GetListAsync(q);
            return View(list);
        }
    }
}
