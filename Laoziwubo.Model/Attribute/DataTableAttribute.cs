using System;
using System.Collections.Generic;
using System.Text;

namespace Laoziwubo.Model.Attribute
{
    [AttributeUsage(AttributeTargets.Class)]
    public class DataTableAttribute : System.Attribute
    {
        public readonly string Name;
        public DataTableAttribute(string name)
        {
            Name = name;
        }
    }
}
