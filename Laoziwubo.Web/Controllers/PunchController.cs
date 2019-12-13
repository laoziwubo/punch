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
    public class PunchController : Controller
    {
        private readonly IBaseB<PunchM> _baseB;
        public PunchController(IBaseB<PunchM> baseB)
        {
            _baseB = baseB;
        }

        public async Task<IActionResult> Index()
        {
            var q = new QueryM()
            {
                PageSize = 5,
                PageNum = 1
            };
            var list = await _baseB.GetListAsync(q) as List<PunchM>;

            if (list == null || list.Count == 0)
            {
                return View(list);
            }
            var pre = list.OrderByDescending(e => e.Id).FirstOrDefault().Date;
            if (pre.ToString("d") != DateTime.Now.ToString("d"))
            {
                pre = Convert.ToDateTime(DateTime.Now.ToString("d"));
            }
            var listMirror = list.OrderByDescending(e => e.Id).ToList();
            foreach (var l in listMirror)
            {
                var days = (pre - l.Date).Days - 1;
                if (days > 0)
                {
                    for (int i = 0; i < days; i++)
                    {
                        var block = new PunchM()
                        {
                            Date = l.Date.AddDays(i + 1),
                            Id = 0
                        };
                        list.Add(block);
                    }
                }
                pre = l.Date;
            }
            return View(list);
        }

        [HttpPost]
        public async Task<JsonResult> GetList(int p)
        {
            var q = new QueryM()
            {
                PageSize = 5,
                PageNum = p
            };
            var list = await _baseB.GetListAsync(q) as List<PunchM>;
            var r = new JsonM<PunchM>
            {
                Flag = list != null && list.Any()
            };

            if (r.Flag)
            {
                r.Message = "加载成功！";

                var pre = list.OrderByDescending(e => e.Id).FirstOrDefault().Date;
                var listMirror = list.OrderByDescending(e => e.Id).ToList();
                foreach (var l in listMirror)
                {
                    var days = (pre - l.Date).Days - 1;
                    if (days > 0)
                    {
                        for (int i = 0; i < days; i++)
                        {
                            var block = new PunchM()
                            {
                                Date = l.Date.AddDays(i + 1),
                                Id = 0
                            };
                            list.Add(block);
                        }
                    }
                    pre = l.Date;
                }
                r.Data = list.OrderByDescending(e => e.Date).ToList();
            }
            else
            {
                r.Message = "没有数据了！";
                r.Data = null;
            }
            return Json(r.ToJson());
        }

        [HttpPost]
        public JsonResult Add(PunchM model)
        {
            model.Date = Convert.ToDateTime(DateTime.Now.ToString("d"));
            model.AddTime = DateTime.Now;

            var r = new JsonM();
            if (model.Id != 0)
            {
                r.Flag = _baseB.Update(model);
            }
            else
            {
                r.Flag = _baseB.Insert(model);
            }
            r.Message = r.Flag ? "今日打卡成功！" : "打卡失败，请稍后重试！";
            return Json(r.ToJson());
        }
    }
}