using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.Data.Sqlite;

class HTracker
{

    private string _connectionString = @"Data Source=habit-Tracker.sqlite";

    public void Start()
    {
      using(var connection = new SqliteConnection(_connectionString))
      {
            connection.Open();
            var tableCmd = connection.CreateCommand();

            tableCmd.CommandText=
            @"CREATE TABLE IF NOT EXISTS drinking_water(
                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                Date TEXT,
                Quantity INTEGER
            )";

            tableCmd.ExecuteNonQuery();

            connection.Close();
      }  

    //   GetUserInput();
    }


}