using System;
using System.Collections.Generic;
using System.Text;

namespace Laoziwubo.Model.Common
{
    public class GenericM
    {
        public string TableName { get; set; }
        public string Identity { get; set; }
        public Dictionary<string, object> Fields { get; set; }
    }
}
