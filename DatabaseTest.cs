using System;
using Microsoft.Data.Sqlite;

class DatabaseTest
{
    public void Start()
    {
        Console.WriteLine("DatabaseTest.Start");

        string connectionString = @"Data Source=DataSqllite.sqlite";
        using (var connection = new SqliteConnection(connectionString))
        {
            connection.Open();

            var sqlCmd = connection.CreateCommand();
            sqlCmd.CommandText =
                @" CREATE TABLE IF NOT EXISTS drinking_water (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    Date TEXT,
                    Quantity INTEGER
                )";
            sqlCmd.ExecuteNonQuery();

            connection.Close();

        }


    }
}