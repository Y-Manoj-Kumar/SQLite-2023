using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.Data.Sqlite;

class HTracker
{

    private string _connectionstring = @"Data Source = Habit-Tracker.sqlite";

    public void Start()
    {
      using(var connection = new SqliteConnection(_connectionstring))
      {
        connection.Open();
        var tablecmd = connection.CreateCommand();

        tablecmd.CommandText = 
        @"CREATE TABLE IF NOT EXISTS drinking_water
        (Id INTEGER PRIMARY KEY AUTOINCREMENT,
        Name TEXT,
        Date INTEGER
        )";
        tablecmd.ExecuteNonQuery();

        connection.Close();
      }

      GetUserInput();
    }


    void GetUserInput()
    {

      Console.Clear();
      bool closeApp = false;

      while(closeApp=false)
      {
        Console.WriteLine("\n\nMAIN MENU");
        Console.WriteLine("\nWhat would you like to do");
        Console.WriteLine("\nType 0 to Close Application");
        Console.WriteLine("Type 1 to view All Records.");
        Console.WriteLine("Type 2 to Insert Record.");
        Console.WriteLine("Type 3 to Delete Record.");
        Console.WriteLine("Type 4 to UpdATE Record.");

        string inputcommand = Console.ReadLine();

        switch (inputcommand)
        {
          case "0":
              Console.WriteLine("Have a Great day");
              closeApp = true;
              Environment.Exit(0);
              break;

          case "1":
              GetAllRecords();
              break;

          case "2":
              Insert();
              break;

          case "3":
              Delete();
              break;

            case "4":
                Update();
                break;
          default:
              Console.WriteLine("Invalid Command. Please type number from 0 to 4. ");
              break;
        };

      }

    }


    private void GetAllRecords()
    {

    }


    private void Insert()
    {

    }

    private void Delete()
    {

    }

    private void Update()
    {

    }



}