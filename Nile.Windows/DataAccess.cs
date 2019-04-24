using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Data.OleDb;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
namespace Nile.Windows
{
    public class DataAccess
    {
        private string connectionString; // used to store the connection string
        private SqlConnection objConnection; // Database connection object
        public SqlCommand objSqlCommand=new SqlCommand();
        private string commandText;
        public DataTable objDataTable=new DataTable();
      
        public SqlConnection Connection
        {
            get { return this.objConnection; }
        }

        public DataAccess()
        {
            connectionString = ("server=CASHAMERICA;Trusted_Connection=yes;database=ProductsDatabase2; connection timeout=30"); // MUST change "server" to your SQL Server name
        }
        public string ConnectionString
        {
            get
            {
                return connectionString;
            }
            set
            {
                connectionString = value;
            }
        }

        public string CommandText
        {
            get
            {
                return commandText;
            }
            set
            {
                commandText = value;
            }
        }
        //Opens a new connectino with database
        public void OpenDBConnection()
        {
            objConnection = new SqlConnection(ConnectionString);
            objConnection.Open();
        }
        public void Parameters(string name, decimal val)
        {
            objSqlCommand.Parameters.AddWithValue(name, val);
        }
        
        public void Parameters(string name, string val)
       {
            objSqlCommand.Parameters.AddWithValue(name, val);
        }
        public void Parameters(string name, Boolean val)
        {
            objSqlCommand.Parameters.AddWithValue(name, val);
        }
        public object ExecuteNonQuery()
        {
            return objSqlCommand.ExecuteNonQuery();
        }
        //Creates a command object
        public void CreateCommandObject()
        {
            objSqlCommand = objConnection.CreateCommand();
            objSqlCommand.CommandText = commandText;
            objSqlCommand.CommandType = CommandType.Text;

        }       
        public DataTable FillDataTable()
        {    
            objDataTable = new DataTable();
            objSqlCommand.CommandText = commandText;
            objSqlCommand.Connection = objConnection;
            objSqlCommand.CommandType = CommandType.Text;
            objDataTable.Load(objSqlCommand.ExecuteReader());
            objConnection.Close();
            return objDataTable;
        }
        //Closes the Data Connection
        public void CloseConnection()
        {
            if (objConnection != null)
                objConnection.Close();
        }  
    }
}