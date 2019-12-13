using System;
using System.Collections.Generic;
using System.Text;

namespace Laoziwubo.Model.Attribute
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public class FieldAttribute : System.Attribute
    {

    }
}
