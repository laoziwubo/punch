using System;
using System.Collections.Generic;
using System.Text;
using Laoziwubo.Model.Attribute;

namespace Laoziwubo.Model.Content
{
    [DataTable("Sticker")]
    public class StickerM
    {
        [Identity(true)]
        [Field]
        public int Id { get; set; }

        [Field]
        public string Author { get; set; }

        [Field]
        public string Path { get; set; }

        [Field]
        public string Tag { get; set; }

        [Field]//赞赏次数
        public int Appreciate { get; set; }

        [Field]//访问量
        public int View { get; set; }

        [Field]//是否模板
        public bool IsTemplet { get; set; }

        [Field]
        public DateTime AddTime { get; set; }
    }
}
