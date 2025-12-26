using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WMS.Util
{
    public class DatabaseHelper
    {
        // 连接字符串示例
        private string ConnectionString { get; set; }

        public DatabaseHelper(string connStr)
        {
            ConnectionString = connStr;
        }

        public DataTable ExecuteQuery(string sqlQuery)
        {
            DataTable resultTable = new DataTable();

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                try
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            resultTable.Load(reader);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"数据库操作出错: {ex.Message}");
                    throw;
                }
            }

            return resultTable;
        }
    }
}
