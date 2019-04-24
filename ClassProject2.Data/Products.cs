using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassProject2.Data
{
    /// <summary>Represents a table of products.</summary>
    public class Products
    {        
        internal Products ( Database database )
        {
            _database = database;
        }

        private readonly Database _database;
        
        public void Add ( Product product )
        {
            Validator.ValidateObject(product, new ValidationContext(product));

            using (var conn = _database.GetConnection())
            {
                //"INSERT INTO Products (Name,UnitPrice,Discontinued) VALUES ('" + product.Name + "')";
                var cmd = conn.CreateCommand();
                cmd.CommandText = "INSERT INTO Products (Name,UnitPrice,Discontinued) " +
                                  "VALUES (@name, @price, @discontinued)";
                cmd.Parameters.AddWithValue("@name", product.Name);
                cmd.Parameters.AddWithValue("@price", product.UnitPrice);
                cmd.Parameters.AddWithValue("@discontinued", product.IsDiscontinued);

                cmd.ExecuteNonQuery();
            };
        }

        /// <summary>Gets a specific product.</summary>
        /// <param name="id">The ID.</param>
        /// <returns>The product, if any.</returns>
        public Product Get ( int id )
        {
            using (var conn = _database.GetConnection())
            {
                var cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT Id, Name, Discontinued, UnitPrice " +
                                  "FROM Products WHERE Id = @id";
                cmd.Parameters.AddWithValue("@id", id);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        return new Product(reader.GetInt32(0))
                        {
                            Name = reader.GetString(1),
                            IsDiscontinued = reader.GetBoolean(2),
                            UnitPrice = reader.GetDecimal(3)
                        };
                    };
                };
            };

            return null;
        }

        /// <summary>Gets all the products.</summary>
        /// <returns>The list of products.</returns>
        public IEnumerable<Product> GetAll()
        {
            var items = new List<Product>();

            using (var conn = _database.GetConnection())
            {
                SqlCommand objSqlCommand=new SqlCommand();
                //var cmd = conn.CreateCommand();
                objSqlCommand.CommandText ="SELECT Id, Name, Discontinued, UnitPrice FROM Products";
                objSqlCommand.Connection = conn;
                //objSqlCommand.CommandType = CommandType.StoredProcedure;

                using (var reader = objSqlCommand.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        //var ordinal = reader.GetOrdinal("Id");
                        //reader.GetFieldValue<int>(ordinal);
                        //reader.GetInt32(ordinal);
                        var product = new Product(reader.GetInt32(0))
                        {
                            Name = reader.GetString(1),
                            IsDiscontinued = reader.GetBoolean(2),
                            UnitPrice = reader.GetDecimal(3)
                        };
                        items.Add(product);
                    };
                };
            };

            return items;
        }

        //public IEnumerable<Product> GetAll()
        //{
        //    using (var conn = _database.GetConnection())
        //    {
        //        //var cmd = new SqlCommand("", conn);
        //        var cmd = conn.CreateCommand();
        //        cmd.CommandText = "SELECT * FROM Products";
        //        cmd.CommandType = System.Data.CommandType.Text;
                                
        //        var data = new SqlDataAdapter(cmd);
                
        //        var ds = new DataSet();

        //        data.Fill(ds);

        //        var items = new List<Product>();
        //        foreach (var row in ds.Tables[0].RowsAsEnumerable())
        //        {
        //            items.Add(new Product(row.Field<int>("Id"))
        //            {
        //                Name = row.Field<string>("Name"),
        //                IsDiscontinued = row.Field<bool>("Discontinued"),
        //                UnitPrice = row.Field<decimal>("UnitPrice")
        //            });
        //        };

        //        return items;

        //    };
        //}     
        
        public void Update ( Product product )
        {
            Validator.ValidateObject(product, new ValidationContext(product));

            using (var conn = _database.GetConnection())
            {
                var cmd = conn.CreateCommand();
                cmd.CommandText = "UPDATE Products " +
                                  "SET Name = @name, " +
                                  "UnitPrice = @price, " +
                                  "Discontinued = @discontinued" +
                                  " WHERE Id = @id";
                cmd.Parameters.AddWithValue("@id", product.Id);
                cmd.Parameters.AddWithValue("@name", product.Name);
                cmd.Parameters.AddWithValue("@price", product.UnitPrice);
                cmd.Parameters.AddWithValue("@discontinued", product.IsDiscontinued);

                cmd.ExecuteNonQuery();
            };
            ////Find existing product
            //var existing = _items.FirstOrDefault(i => i.Id == product.Id);
            //if (existing == null)
            //    throw new Exception("Product not found");

            ////Name must be unique
            //if (_items.Any(i => i.Id != product.Id && 
            //            String.Compare(i.Name, product.Name, true) == 0))
            //    throw new Exception("Name must be unique");

            ////Update
            //existing.Name = product.Name;
            //existing.IsDiscontinued = product.IsDiscontinued;
            //existing.UnitPrice = product.UnitPrice;
        }           
        
        private readonly Sequence _ids = new Sequence();
    }
}
