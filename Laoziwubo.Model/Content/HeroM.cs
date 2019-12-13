using System;
using System.Collections.Generic;
using System.Text;
using Laoziwubo.Model.Attribute;

namespace Laoziwubo.Model.Content
{
    [DataTable("Hero")]
    public class HeroM
    {
        [Identity(true)]
        [Field]
        public int Id { get; set; }

        [Field]
        public string Name { get; set; }

        [Field]
        public string EnglishName { get; set; }

        [Field]
        public string Icon { get; set; }

        [Field]
        public string Description { get; set; }

        [Field]
        public string PrimaryAttributes { get; set; }

        [Field]
        public DateTime? UpdateTime { get; set; }
    }
}
