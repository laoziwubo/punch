using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Laoziwubo.IBLL;
using Laoziwubo.IDAL;
using Laoziwubo.Model.Common;

namespace Laoziwubo.BLL
{
    public class BaseB<T> : IBaseB<T>
    {
        private readonly IBaseD<T> _dao;
        public BaseB(IBaseD<T> dao)
        {
            _dao = dao;
        }

        public Task<IEnumerable<T>> GetListAsync(QueryM q)
        {
            return _dao.GetEntitysAsync(q);
        }

        public T GetEntityById(int id)
        {
            return _dao.GetEntityById(id);
        }

        public bool Insert(T model)
        {
            return _dao.Insert(model);
        }

        public bool Update(T model)
        {
            return _dao.Update(model);
        }
    }
}
