using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassProject2.Data
{
    public class Database
    {
        public Database ( string connectionString )
        {
            _connectionString = connectionString;

            Products = new Products(this);
        }

        public Customers Customers { get; private set; } = new Customers();

        public Products Products { get; private set; }

        internal SqlConnection GetConnection ()
        {
            var conn = new SqlConnection(_connectionString);
            conn.Open();

            return conn;
        }

        private readonly string _connectionString;
    }
}
