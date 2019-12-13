using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Laoziwubo.Model.Common
{
    public class JsonM<T>
    {
        public bool Flag { get; set; }

        public string Message { get; set; }

        public List<T> Data { get; set; }

        readonly JsonSerializerSettings _setting = new JsonSerializerSettings()
        {
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
        };

        public string ToJson()
        {
            return JsonConvert.SerializeObject(this, _setting);
        }
    }

    public class JsonM
    {
        public bool Flag { get; set; }

        public string Message { get; set; }

        readonly JsonSerializerSettings _setting = new JsonSerializerSettings()
        {
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
        };

        public string ToJson()
        {
            return JsonConvert.SerializeObject(this, _setting);
        }
    }
}
