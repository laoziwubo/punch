using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;
using System.Text;
using Laoziwubo.Model.Attribute;

namespace Laoziwubo.Model.Content
{
    [DataTable("User")]
    public class UserM
    {
        [Identity(true)]
        [Field]
        public int Id { get; set; }

        [Field]
        public string UserName { get; set; }

        [Field]
        public DateTime AddTime { get; set; }

        [Display(Name = "是否抢先会员")]
        public bool IsEarly => Id < 10;
    }
}
