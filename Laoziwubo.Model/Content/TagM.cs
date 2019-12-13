using System;
using System.Collections.Generic;
using System.Text;
using Laoziwubo.Model.Attribute;

namespace Laoziwubo.Model.Content
{
    [DataTable("Tag")]
    public class TagM
    {
        [Identity(true)]
        [Field]
        public int Id { get; set; }

        [Field]
        public string Content { get; set; }

        [Field]
        public DateTime AddTime { get; set; }
    }
}
