using System;
using System.Collections.Generic;
using System.Text;

namespace Laoziwubo.Model.Common
{
   public class QueryM
    {
        public int PageNum { get; set; }
        public int PageSize { get; set; }
        public string OrderBy { get; set; }
        public string Where { get; set; }
    }
}
