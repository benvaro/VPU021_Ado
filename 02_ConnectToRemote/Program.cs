using System;
using System.Data.SqlClient;

namespace _02_ConnectToRemote
{
    class Program
    {
        static void Main(string[] args)
        {
            string connectionString = @"Data Source=10.7.101.197;Initial Catalog=master;
                                        Integrated Security=false; User Id=test; Password=1";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    Console.WriteLine(connection.ConnectionString);
                    Console.WriteLine($"Database: {connection.Database}");
                    connection.ChangeDatabase("SportShop");
                    Console.WriteLine($"after change db: {connection.Database}");

                    //     string commandText = "create database TestAdoNet";

                    SqlCommand command = new SqlCommand();
                    //command.CommandText = commandText;
                    command.Connection = connection;

                    //        SqlCommand command = new SqlCommand(commandText, connection);
                    // ExecuteNonQuery - insert, update, delete, create
                    // ExecuteScalar - select min, max, avg, count, sum
                    // ExecuteReader - select
                    //       int result = command.ExecuteNonQuery();
                    //       Console.WriteLine("After create : " + result);

                    connection.ChangeDatabase("TestAdoNet");

                    // string createTableCommand = "create table Test(Id int identity primary key," +
                    //                                 "Name nvarchar(20))";
                    // command.CommandText = createTableCommand;
                    // command.ExecuteNonQuery();
                    //Console.WriteLine("Table created");

                    // command.CommandText = "Insert into Test values(N'Alex'), " +
                    //                                            "(N'Katia')";
                    //int rows = command.ExecuteNonQuery();
                    //command.CommandText = "Insert into Test values(N'Valia'), " +
                    //                                            "(N'Katia')";
                    //rows = command.ExecuteNonQuery();
                    //Console.WriteLine($"{rows} rows where added");

                    /// Read from table
                    /// 
                    command.CommandText = "select * from Test";
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            //object id = reader[0];
                            //object name = reader[1];

                            object id = reader["Id"];
                            object name = reader["Name"];

                            Console.WriteLine($"{id}\t\t{name}\n");
                        }
                    }
                    reader.Close();

                }
                catch (SqlException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
    }
}
