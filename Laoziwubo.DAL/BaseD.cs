using System;
using Dapper;
using Laoziwubo.IDAL;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Laoziwubo.Model.Common;
using Laoziwubo.Model.Attribute;

namespace Laoziwubo.DAL
{
    public class BaseD<T> : IBaseD<T> where T : new()
    {
        private IDbConnection _conn;
        private readonly GenericM _genericM;
        public BaseD(Connection dbConnection, GenericM genericM, Dictionary<string, object> dic)
        {
            _conn = dbConnection.GetSqlConnection();
            _genericM = genericM;
            _genericM.Fields = dic;

            var type = typeof(T);

            //表名
            var dataTableAttribute = (DataTableAttribute)type.GetCustomAttribute(typeof(DataTableAttribute));
            _genericM.TableName = dataTableAttribute == null ? typeof(T).Name : dataTableAttribute.Name;

            //字段处理
            var properties = type.GetTypeInfo().GetProperties();
            foreach (var p in properties)
            {
                if (p.GetCustomAttribute(typeof(FieldAttribute)) is FieldAttribute)
                {
                    _genericM.Fields.Add(p.Name, null);
                }
                if (p.GetCustomAttribute(typeof(IdentityAttribute)) is IdentityAttribute)
                {
                    _genericM.Identity = p.Name;
                }
            }
        }

        public async Task<IEnumerable<T>> GetEntitysAsync(QueryM q)
        {
            try
            {
                var orderBy = q.OrderBy ?? _genericM.Identity + " DESC";
                var where = string.IsNullOrEmpty(q.Where) ? "" : " WHERE " + q.Where;
                var str = $@"SELECT {string.Join(",", _genericM.Fields.Keys)} 
                            FROM {_genericM.TableName + where}
                            ORDER BY {orderBy}
                            LIMIT {(q.PageNum - 1) * q.PageSize} , {q.PageSize};";
                return await _conn.QueryAsync<T>(str);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public T GetEntityById(int id)
        {
            try
            {
                var str = $@"SELECT {string.Join(",", _genericM.Fields.Keys)} FROM {_genericM.TableName} WHERE {_genericM.Identity} = @{_genericM.Identity};";
                var p = new DynamicParameters();
                p.Add($"@{_genericM.Identity}", id);
                return _conn.QueryFirstOrDefault<T>(str, p);
            }
            catch (Exception)
            {
                return new T();
            }
        }

        public bool Insert(T model)
        {
            try
            {
                if (_genericM.Fields.ContainsKey(_genericM.Identity))
                {
                    _genericM.Fields.Remove(_genericM.Identity);
                }
                var str = $@" INSERT INTO {_genericM.TableName} ({string.Join(",", _genericM.Fields.Keys)}) 
                              VALUES (@{string.Join(",@", _genericM.Fields.Keys)});";
                _conn.Execute(str, model);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool Update(T model)
        {
            try
            {
                if (_genericM.Fields.ContainsKey(_genericM.Identity))
                {
                    _genericM.Fields.Remove(_genericM.Identity);
                }
                var fields = string.Empty;
                foreach (var f in _genericM.Fields.Keys)
                {
                    fields += f + "=@" + f + ",";
                }
                fields = fields.TrimEnd(',');
                var str = $@" UPDATE {_genericM.TableName} SET {fields}
                              WHERE {_genericM.Identity} = @{_genericM.Identity};";
                _conn.Execute(str, model);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected void Dispose(bool disposing)
        {
            if (disposing)
            {
                _conn.Close();
                _conn = null;
            }
        }

        ~BaseD()
        {
            Dispose(false);
        }
    }
}
