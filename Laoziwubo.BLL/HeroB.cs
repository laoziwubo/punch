using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;
using HttpCode.Core;
using Laoziwubo.IBLL;
using Laoziwubo.IDAL;
using Laoziwubo.Model.Content;

namespace Laoziwubo.BLL
{
    public class HeroB<T> : IHeroB<T> where T : HeroM, new()
    {
        private readonly IHeroD<T> _heroD;
        public HeroB(IHeroD<T> heroD)
        {
            _heroD = heroD;
        }

        public string InsertHeroes()
        {
            var res = "";
            var counter = 0;
            var httpHelpers = new HttpHelpers();
            var items = new HttpItems
            {
                Url = "https://dota2.gamepedia.com/Heroes",
                Method = "Get"
            };
            var hr = httpHelpers.GetHtml(items);

            var doc = new HtmlDocument();
            doc.LoadHtml(hr.Html);

            items.Url = "https://dota2-zh.gamepedia.com/%E8%8B%B1%E9%9B%84";
            hr = httpHelpers.GetHtml(items);
            var docCn = new HtmlDocument();
            docCn.LoadHtml(hr.Html);
            var trNodesCn = docCn.DocumentNode.SelectNodes("//table[@class='wikitable sortable']/tr");

            var trNodes = doc.DocumentNode.SelectNodes("//table[@class='wikitable sortable']/tr");
            foreach (var item in trNodes.Take(trNodes.Count))
            {
                if (item.LastChild.Name == "th")
                {
                    continue;
                }
                var a = item.SelectSingleNode("td[1]/div/div/a");
                if (a == null)
                {
                    continue;
                }
                var heroEn = a.GetAttributeValue("title", "");
                var heroCn = "";
                switch (heroEn)
                {
                    case "Pangolier":
                        heroCn = "石鳞剑士";
                        break;
                    case "Dark Willow":
                        heroCn = "邪影芳灵";
                        break;
                    default:
                        heroCn = trNodesCn[trNodes.IndexOf(item)].SelectSingleNode("td[1]/div/div/a").GetAttributeValue("title", "");
                        break;
                }
                var img = a.SelectSingleNode("img");
                var src = img.GetAttributeValue("src", "");
                var description = item.SelectSingleNode("td[last()]").InnerText;
                counter += 1;
                res += $"<p>{heroEn} - {heroCn}</p><p>{src}</p><div>{description}</div></br>";

                var heroM = new T()
                {
                    EnglishName = heroEn,
                    Name = heroCn,
                    Icon = src,
                    Description = description
                };
                //Insert(heroM);
            }
            return $"<p>共计{counter}位</p>{res}";
        }

        public bool Insert(T model)
        {
            return _heroD.Insert(model);
        }
    }
}
