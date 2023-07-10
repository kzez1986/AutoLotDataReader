using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data;
using System.Data.SqlClient;

using static System.Console;

namespace AutoLotDataReader
{
    class Program
    {
        static void Main(string[] args)
        {
            WriteLine("***** Fun with Data Readers *****");

            var cnStringBuilder = new SqlConnectionStringBuilder
            {
                InitialCatalog = "AutoLot",
                DataSource = @"(local)\",
                ConnectTimeout = 30,
                IntegratedSecurity = true
            };

            using (SqlConnection connection = new SqlConnection())
            {
                connection.ConnectionString = cnStringBuilder.ConnectionString;
                connection.Open();

                ShowConnectionStatus(connection);

                string sql = "Select * From Inventory;Select * from Customers";
                SqlCommand myCommand = new SqlCommand(sql, connection);

                using(SqlDataReader myDataReader = myCommand.ExecuteReader())
                {
                    do
                    {
                        while (myDataReader.Read())
                        {
                            WriteLine("***** Record *****");
                            for (int i = 0; i < myDataReader.FieldCount; i++)
                            {
                                WriteLine($"{myDataReader.GetName(i)} = {myDataReader.GetValue(i)}");
                            }
                            WriteLine();
                        }
                    } while (myDataReader.NextResult());
                }
            }
            ReadLine();
        }

        static void ShowConnectionStatus(SqlConnection connection)
        {
            WriteLine("***** Info about your connection");
            WriteLine($"Database location: {connection.DataSource}");
            WriteLine($"Database name: {connection.Database}");
            WriteLine($"Timeout: {connection.ConnectionTimeout}");
            WriteLine($"Connection state: {connection.State}");

        }
    }
}
