using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Laoziwubo.IDAL;
using Laoziwubo.Model.Content;

namespace Laoziwubo.DAL
{
    public class HeroD<T> : IHeroD<T> where T : HeroM
    {
        private IDbConnection _conn;
        public HeroD(Connection dbConnection)
        {
            _conn = dbConnection.GetSqlConnection();
        }

        public bool Insert(T model)
        {
            try
            {
                model.Description = model.Description.Replace("'", "''");
                var str = $@" INSERT INTO [db_laoziwubo].[dbo].[Hero] ( [Name],[EnglishName],[Icon],[Description],[PrimaryAttributes],[UpdateTime])
                   VALUES ('{model.Name}', '{model.EnglishName}', '{model.Icon}','{model.Description}', '',GETDATE());";
                _conn.Execute(str);
                return true;
            }
            catch (Exception)
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

        ~HeroD()
        {
            Dispose(false);
        }
    }
}
