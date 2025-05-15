using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebScrappingECommerece
{
    public class DatabaseHelper
    {
        private readonly string connectionString;

        public DatabaseHelper(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public void InsertProduct(string name, string price, string rating, string description)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand("SPAddProduct", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Name", name ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@Price", price ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@Rating", rating ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@Description", description ?? (object)DBNull.Value);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}

