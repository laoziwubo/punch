using Laoziwubo.Model.Attribute;
using System;

namespace Laoziwubo.Model.Content
{
    [DataTable("Punch")]
    public class PunchM
    {
        [Identity(true)]
        [Field]
        public int Id { get; set; }

        [Field]
        public DateTime Date { get; set; }

        [Field]
        public int Item1 { get; set; }

        [Field]
        public int Item2 { get; set; }

        [Field]
        public int Item3 { get; set; }

        [Field]
        public string Mood { get; set; }

        [Field]
        public DateTime AddTime { get; set; }
    }
}
