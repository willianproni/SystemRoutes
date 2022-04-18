using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Model;
using ReadFileExcel.Config;

namespace ReadFileExcel.Util
{
    public class RotasRepositorio : IRotasRepositorio
    {
        private string _connection;

        public RotasRepositorio()
        {
            _connection = DataBaseConfiguration.Get();
        }
        public bool Add(Route rota)
        {
            bool status = false;

            using (var dataBase = new SqlConnection(_connection))
            {
                dataBase.Open();
                dataBase.Execute(Route.INSERT, rota);
            }
            return status;
        }

        public List<Route> GetAll()
        {
            throw new NotImplementedException();
        }
    }
}
