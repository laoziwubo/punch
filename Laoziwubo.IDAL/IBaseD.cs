using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Laoziwubo.Model.Common;

namespace Laoziwubo.IDAL
{
    public interface IBaseD<T> : IDisposable
    {
        /// <summary>
        /// 异步获取所有实体
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<T>> GetEntitysAsync(QueryM q);

        /// <summary>
        /// 根据主键Id获取一个实体
        /// </summary>
        /// <param name="id">主键Id</param>
        /// <returns></returns>
        T GetEntityById(int id);

        /// <summary>
        /// 插入一条信息
        /// </summary>
        bool Insert(T model);

        /// <summary>
        /// 更新一条信息
        /// </summary>
        bool Update(T model);
    }
}
