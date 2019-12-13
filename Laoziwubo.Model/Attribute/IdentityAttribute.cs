using System;
using System.Collections.Generic;
using System.Text;

namespace Laoziwubo.Model.Attribute
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public class IdentityAttribute : System.Attribute
    {
        public readonly bool IsIdentity;
        public IdentityAttribute(bool isIdentity)
        {
            IsIdentity = isIdentity;
        }
    }
}
