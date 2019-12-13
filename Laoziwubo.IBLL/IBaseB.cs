using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Laoziwubo.Model.Common;

namespace Laoziwubo.IBLL
{
    public interface IBaseB<T>
    {
        Task<IEnumerable<T>> GetListAsync(QueryM q);

        T GetEntityById(int id);

        bool Insert(T model);

        bool Update(T model);
    }
}
