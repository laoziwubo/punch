using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Laoziwubo.IDAL
{
    public interface IHeroD<T> : IDisposable
    {
        bool Insert(T model);
    }
}
