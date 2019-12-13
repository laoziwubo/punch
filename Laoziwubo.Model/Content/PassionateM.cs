using System;
using System.Collections.Generic;
using System.Text;
using Laoziwubo.Model.Attribute;

namespace Laoziwubo.Model.Content
{
    [DataTable("Passionate")]
    public class PassionateM
    {
        [Identity(true)]
        [Field]
        public int Id { get; set; }

        [Field]
        public string Title { get; set; }

        [Field]
        public int Days { get; set; }

        [Field]
        public string Keywords { get; set; }

        [Field]
        public string Resources { get; set; }

        [Field]
        public string Completion { get; set; }

        [Field]
        public string Who { get; set; }

        [Field]
        public string Message { get; set; }

        [Field]
        public int Status { get; set; }

        [Field]
        public DateTime AddTime { get; set; }

        [Field]
        public DateTime UpdateTime { get; set; }

        public Dictionary<string, int> CompletionDict { get; set; }

        public List<string> ResourcesList { get; set; }
    }
}
