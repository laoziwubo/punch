using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Laoziwubo.IBLL;
using Laoziwubo.Model.Common;
using Laoziwubo.Model.Content;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;

namespace Laoziwubo.Web.Controllers
{
    public class StickerController : Controller
    {
        private readonly IBaseB<StickerM> _baseB;
        public StickerController(IBaseB<StickerM> baseB)
        {
            _baseB = baseB;
        }

        public IActionResult Tag()
        {
            return View();
        }

        [HttpPost]
        public async Task<JsonResult> Upload(ICollection<IFormFile> files)
        {
            var r = new JsonM<string>();
            foreach (var file in files)
            {
                if (file.Length > 10485760)//大小限制为10M
                {
                    r.Flag = false;
                    r.Message = $"文件[{file.FileName}]大小为[{Math.Round(Convert.ToDouble(file.Length) / (1024 * 1024), 2)}]MB，超出限定大小，请检查后重试！";
                    return Json(r);
                }

                var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.ToString().Trim('"');

                var extension = fileName.Split(".").LastOrDefault()?.ToLower();
                var picExtensions = new[] { "png", "jpg", "jpeg"};
                if (!picExtensions.Contains(extension))
                {
                    r.Flag = false;
                    r.Message = $"暂时支持jpg/jpeg/png格式";
                    return Json(r);
                }

                //var filePath = _env.WebRootPath + @"\sticker\";
                var filePath = "/home/laoziwubo/wwwroot/sticker/";

                if (!Directory.Exists(filePath))
                {
                    Directory.CreateDirectory(filePath);
                }

                //重写文件名
                fileName = Guid.NewGuid() + "." + extension;
                //包含路径的文件名
                var fileFullName = filePath + fileName;

                using (var fs = System.IO.File.Create(fileFullName))
                {
                    await file.CopyToAsync(fs);
                    fs.Flush();

                    //保存
                    var model = new StickerM
                    {
                        AddTime = DateTime.Now,
                        Appreciate = 0,
                        Author = "管理员",
                        IsTemplet = false,
                        Path = "/sticker/" + fileName,
                        Tag = "",
                        View = 0
                    };
                    r.Flag = _baseB.Insert(model);
                    r.Message = r.Flag ? $"上传成功!|{"/sticker/" + fileName}" : $"文件[{file.FileName}]保存时出错，请重试~";
                }
            }
            return Json(r);
        }
    }
}

