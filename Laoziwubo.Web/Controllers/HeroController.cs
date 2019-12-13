using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Laoziwubo.IBLL;
using Laoziwubo.Model.Common;
using Laoziwubo.Model.Content;
using Microsoft.AspNetCore.Mvc;

namespace Laoziwubo.Web.Controllers
{
    public class HeroController : Controller
    {
        private readonly IBaseB<HeroM> _baseB;
        public HeroController(IBaseB<HeroM> baseB)
        {
            _baseB = baseB;
        }

        public async Task<IActionResult> Index()
        {
            var q = new QueryM()
            {
                PageSize = 120,
                PageNum = 1
            };
            var list = await _baseB.GetListAsync(q);
            return View(list);
        }

        public IActionResult Detail(int id)
        {
            var model = _baseB.GetEntityById(id);
            return View(model);
        }
    }
}