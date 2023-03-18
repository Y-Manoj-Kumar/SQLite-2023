using System;
using System.Globalization;
using Microsoft.Data.Sqlite;

class HTracker
{
    private string _connectionString = @"Data Source = H-Tracker.sqlite";

    public void Start()
    {
      var connection = new SqliteConnection(_connectionString); 
      connection.Open();
      var sqlcmd = connection.CreateCommand();
      sqlcmd.CommandText = @"CREATE TABLE IF NOT EXISTS keeping_hydrated(
        Id INTEGER  PRIMARY KEY AUTOINCREMENT,
        Date TEXT,
        Quantity INTEGER
      )";

      sqlcmd.ExecuteNonQuery();
      connection.Close();

      GetUserInput();
    }

    #region  GetUserInput
    void GetUserInput()
    {
      Console.Clear();
      bool closeApp = false;
      while (closeApp == false) // must be two equals..
      {
          Console.WriteLine("\n\nMAIN MENU");            
          Console.WriteLine("\nWhat would you like to do?");
          Console.WriteLine("\nType 0 to Close Application.");
          Console.WriteLine("Type 1 to View All Records.");
          Console.WriteLine("Type 2 to Insert Record.");
          Console.WriteLine("Type 3 to Delete Record.");
          Console.WriteLine("Type 4 to Update Record.");
          Console.WriteLine("------------------------------------------\n");

          string userInput = Console.ReadLine();
          switch (userInput)
          {
            case "0":
              Console.WriteLine("\nHave a GREAT DAY");
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
              Console.WriteLine("\nInvalid Command. Please type a number from 0 to 4.\n");
              break;
          }
      }
    }
    #endregion


    #region GetAllRecords
    private void GetAllRecords()
    {
       Console.Clear();
       var connection = new SqliteConnection(_connectionString);

       connection.Open();
       var sqlCmd = connection.CreateCommand();
       sqlCmd.CommandText = 
       @"SELECT * FROM keeping_hydrated";

       List<DrinkingWater> tableData = new(); //Used to create a list of all data in the table
       SqliteDataReader reader = sqlCmd.ExecuteReader();

       if (reader.HasRows)
       {
          while (reader.Read())
          {
                    tableData.Add(
                    new DrinkingWater
                    {
                        Id = reader.GetInt32(0),
                        Date = DateTime.ParseExact(reader.GetString(1), "dd-mm-yy", new CultureInfo("en-US")),
                        Quantity = reader.GetInt32(2)
                        
                    }); 
          }
       }
       else
       {
        Console.WriteLine("No rows found");
       }

       connection.Close();

       Console.WriteLine("..............................\n");

       foreach (var item in tableData)
       {
          Console.WriteLine($"{item.Id} - {item.Date.ToString("dd-mm-yy")} -Quantity: {item.Quantity}");
       }
       Console.WriteLine(".................................\n");

    }
    #endregion


    #region Insert
    private void Insert()
    {
       string date = GetDateInput();

       int quantity = GetNumberInput("\n\nPlease insert number of glasses you drink.(Decimals are not allowed)\n\n");

       var connection = new SqliteConnection(_connectionString);
        connection.Open();
        var sqlcmd = connection.CreateCommand();
        sqlcmd.CommandText = 
        $"INSERT INTO keeping_hydrated (date,quantity) VALUES ('{date}',{quantity})";

        sqlcmd.ExecuteNonQuery();
        connection.Close();

    }
    #endregion


    #region  Delete
    private void Delete()
    {
      Console.Clear();
      GetAllRecords();

      var recordId = GetNumberInput("\n\nEnter the record Id you want to Delete.");

      var connection = new SqliteConnection(_connectionString);
      connection.Open();
      var sqlCmd = connection.CreateCommand();
      sqlCmd.CommandText = 
      $" DELETE FROM keeping_hydrated WHERE Id='{recordId}'";

      int rowCount = sqlCmd.ExecuteNonQuery();
      if (rowCount == 0)
      {
        Console.WriteLine($"The Id which is '{recordId}' not exist");
      }

      Console.WriteLine($"The Id which is '{recordId}' is deleted");

      GetUserInput();

    }
    #endregion


    #region  Update
    private void Update()
    {
       
    }
    #endregion


    #region  GetDateInput
    string GetDateInput()
    {
      Console.WriteLine("\n\nEnter the date in the form of dd-mm-yy. Type 0 to enter Main Menu.\n\n");

      string dateInput = Console.ReadLine();

      if (dateInput == "0") GetUserInput();
      return dateInput;
    }
    #endregion


    #region GetNumberInput
    int GetNumberInput(string message)
    {
      Console.WriteLine(message);

      string numberInput = Console.ReadLine();
      if (numberInput == "0") GetUserInput();

      int finalInput = Convert.ToInt32(numberInput);
      return finalInput ;
    }
    #endregion


    #region properties

    public class DrinkingWater
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int Quantity{ get; set; }
    }

    #endregion

}


