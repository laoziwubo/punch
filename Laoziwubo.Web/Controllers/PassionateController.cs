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
    [Route("[controller]")]
    public class PassionateController : Controller
    {
        private readonly IBaseB<PassionateM> _baseB;
        public PassionateController(IBaseB<PassionateM> baseB)
        {
            _baseB = baseB;
        }

        [Route("{who?}")]
        [Route("[action]/{who?}")]
        public async Task<IActionResult> Index(string who)
        {
            var userAccess = new List<string>() { "niuer", "ball" };
            if (string.IsNullOrEmpty(who) || !userAccess.Contains(who))
            {
                return View("WhoAmI");
            }
            var q = new QueryM()
            {
                PageSize = 20,
                PageNum = 1,
                Where = $"Who='{who}'"
            };
            var list = await _baseB.GetListAsync(q) as List<PassionateM>;
            ViewBag.Who = who;

            if (list == null || list.Count == 0)
            {
                return View();
            }
            foreach (var l in list)
            {
                l.CompletionDict = new Dictionary<string, int>();
                l.ResourcesList = new List<string>();
                if (l.Keywords.Contains(","))
                {
                    var kArray = l.Keywords.Split(",");
                    var cArray = string.IsNullOrEmpty(l.Completion) ? null : l.Completion.Split("|");
                    for (var i = 0; i < kArray.Length; i++)
                    {
                        l.CompletionDict.Add(kArray[i], string.IsNullOrEmpty(l.Completion) ? 0 : Convert.ToInt32(cArray[i]));
                    }
                }
                else
                {
                    l.CompletionDict.Add(l.Keywords, string.IsNullOrEmpty(l.Completion) ? 0 : Convert.ToInt32(l.Completion));
                }
                if (!string.IsNullOrEmpty(l.Resources))
                {
                    if (l.Resources.Contains("|"))
                    {
                        foreach (var r in l.Resources.Split("|"))
                        {
                            l.ResourcesList.Add(r);
                        }
                    }
                    else
                    {
                        l.ResourcesList.Add(l.Resources);
                    }
                }
            }
            return View(list);
        }

        [HttpPost]
        public JsonResult Edit(PassionateM model)
        {
            var r = new JsonM();

            if (model.Status != 1 && model.Status != 2)
            {
                r.Flag = false;
                r.Message = "用户输入状态异常！";
                return Json(r.ToJson());
            }

            if (model.Id != 0)
            {
                var m = _baseB.GetEntityById(model.Id);
                m.Resources += model.Resources;
                if (!string.IsNullOrEmpty(m.Resources))
                {
                    m.Resources = m.Resources.TrimStart('|');
                }
                m.Keywords = model.Keywords.TrimStart(',');
                m.Completion = model.Completion.TrimStart('|');
                m.Message = model.Message;
                m.UpdateTime = DateTime.Now;
                m.Status = model.Status;
                r.Flag = _baseB.Update(m);
                r.Message = r.Flag ? "目标更新成功！" : "目标更新失败，请稍后重试！";
            }
            else
            {
                model.AddTime = DateTime.Now;
                model.UpdateTime = model.AddTime;
                model.Keywords = model.Keywords.TrimStart(',');
                if (!string.IsNullOrEmpty(model.Resources))
                {
                    model.Resources = model.Resources.TrimStart('|');
                }
                r.Flag = _baseB.Insert(model);
                r.Message = r.Flag ? "目标创建成功！" : "目标创建失败，请稍后重试！";
            }
            return Json(r.ToJson());
        }
    }
}