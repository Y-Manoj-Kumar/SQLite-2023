using System.Globalization;
using Microsoft.Data.Sqlite;

class RMK_MoniFlat
{

    private string connectionString = @"Data Source = RMK_MoniFlat.sqlite";

    #region  Start
    public void Start()
    {
        var connection = new SqliteConnection(connectionString);

        connection.Open();
        var sqlCmd = connection.CreateCommand();
        sqlCmd.CommandText =
        @"CREATE TABLE IF NOT EXISTS Moni_Flat_Members(
        Id INTEGER PRIMARY KEY AUTOINCREMENT,
        Name TEXT,
        Age INTEGER,
        Address TEXT,
        Mobile_Number INTEGER,
        Months_Stayed INTEGER)";

        sqlCmd.ExecuteNonQuery();
        connection.Close();

        GetUserInput();
    } 
    #endregion

    #region  GetUserInput
    private void GetUserInput()
    {
        Console.Clear();
        bool closeApp = false;
        while (closeApp == false)
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
            closeApp = true;
            Console.WriteLine("\nHave a Great Day\n");
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
            Console.WriteLine("\n\n Command Invalid.Please enter correctly");
            break;
          }


        }
    }
    #endregion

    #region  GetAllRecords
    private void GetAllRecords()
    {
        Console.Clear();
        var connection = new SqliteConnection(connectionString);

        connection.Open();
        var sqlCmd =connection.CreateCommand();
        sqlCmd.CommandText = 
        @"SELECT * FROM Moni_Flat_Members";

        List<FlatMembers> tabledata = new();
        SqliteDataReader reader = sqlCmd.ExecuteReader();

        if (reader.HasRows)
        {
            while (reader.Read())
            {
                tabledata.Add(
                new FlatMembers
                {
                    Id = reader.GetInt32(0),
                    Name = reader.GetString(1),
                    Age = reader.GetInt32(2),
                    Address =reader.GetString(3),
                    Mobile_Number = reader.GetInt64(4),
                    Months_Stayed = reader.GetInt32(5)
                });
            }
        }
        else
        {
            Console.WriteLine("No Rows Found.\n");
        }

        connection.Close();
        

        Console.WriteLine("..............................\n");

        foreach (var item in tabledata)
        {
            Console.WriteLine($"{item.Id} \nName: {item.Name} \nAge: {item.Age} \nAddress: {item.Address} \nMobile_Number: {item.Mobile_Number} \nMonths_Stayed: {item.Months_Stayed}");
        }

        Console.WriteLine("..............................\n");
    }
    #endregion
    
    #region Insert
    private void Insert()
    {
         string Name = GetNameInput();
         int Age = GetAgeInput();
         string Address = GetAddressInput();
         long Mobile_Number = GetMobileNumber();
         int Months_Stayed = GetMonthsStayed();

         var connection = new SqliteConnection(connectionString);
         connection.Open();

         var sqlCmd = connection.CreateCommand();
         sqlCmd.CommandText =
         $"INSERT  INTO  Moni_Flat_Members (Name,Age,Address,Mobile_Number,Months_Stayed) VALUES('{Name}','{Age}','{Address}','{Mobile_Number}','{Months_Stayed}')";

        sqlCmd.ExecuteNonQuery();
        connection.Close();
    }
    #endregion

    #region Update
    private void Update()
    {
        GetAllRecords();

        var recordId = GetNumberInput("\n\nPlease type Id of the record would like to update. Type 0 to return to main manu.\n\n");

        var connection = new SqliteConnection(connectionString);
        
            connection.Open();

            var checkCmd = connection.CreateCommand();
            checkCmd.CommandText = $"SELECT EXISTS(SELECT 1 FROM Moni_Flat_Members WHERE Id = {recordId})";
            int checkQuery = Convert.ToInt32(checkCmd.ExecuteScalar());

            if (checkQuery == 0)
            {
                Console.WriteLine($"\n\nRecord with Id {recordId} doesn't exist.\n\n");
                connection.Close();
                Update();
            }

         string name = GetNameInput();
         int Age = GetAgeInput();
         string Address = GetAddressInput();
         long Mobile_Number = GetMobileNumber();
         int Months_Stayed = GetMonthsStayed();

         var sqlCmd = connection.CreateCommand();
         sqlCmd.CommandText = $"UPDATE Moni_Flat_Members SET Name = '{name}', Age = {Age}, Address '{Address}', Mobile_Number = '{Mobile_Number}', Months_Stayed = '{Months_Stayed} WHERE Id = {recordId}";

         sqlCmd.ExecuteNonQuery();

         connection.Close();
    
    }
    #endregion

    #region Delete
    private void Delete()
    {
        Console.Clear();
        GetAllRecords();

        var recordId = GetNumberInput("Enter the Id you want to delete.");
        var connection = new SqliteConnection(connectionString);

        connection.Open();
        var sqlCmd = connection.CreateCommand();
        sqlCmd.CommandText =
        $"DELETE FROM Moni_Flat_Members WHERE Id='{recordId}'";

        int rowCount = sqlCmd.ExecuteNonQuery();
        if (rowCount == 0)
        {
            Console.WriteLine($"Entered recordId which is '{recordId}' is not found");
        }

        Console.WriteLine($"The Id which is '{recordId}' is deleted successfully.");
        
    }
    #endregion

    #region GetNameInput
    string GetNameInput()
    {
        Console.WriteLine("Enter your Full Name.");
        string enteredName = Console.ReadLine();

        if (enteredName == "0")GetUserInput();

        return enteredName;
    }
    #endregion

    #region GetAgeInput
    int GetAgeInput()
    {
        Console.WriteLine("\nEnter your Age.");
        string enteredAge = Console.ReadLine();

        if (enteredAge == "0")GetUserInput();

        int age = Convert.ToInt32(enteredAge);

        return age;
    }
    #endregion

    #region GetAddressInput
    string GetAddressInput()
    {
        Console.WriteLine("\nEnter Your Address.");
        string enteredAddress = Console.ReadLine();

        if (enteredAddress == "0")GetUserInput();

        return enteredAddress;
    }
    #endregion

    #region  GetMobileNumber
    long GetMobileNumber()
    {
        Console.WriteLine("\nEnter your MobileNumber.");
        string enteredMobilenumber = Console.ReadLine();

        if (enteredMobilenumber == "0")GetUserInput();

        long Mobile_Number = Convert.ToInt64(enteredMobilenumber);

        return Mobile_Number;
    }
    #endregion

    #region  GetMonthsStayed
    int GetMonthsStayed()
    {
        Console.WriteLine("\nEnter how many MonthsStayed.");
        string enteredMonthsStayed = Console.ReadLine();

        if (enteredMonthsStayed == "0")GetUserInput();

        int Months_Stayed = Convert.ToInt32(enteredMonthsStayed);

        return Months_Stayed;
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

    #region FlatMembers
    public class FlatMembers
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string Address { get; set; }
        public long Mobile_Number { get; set; }
        public int Months_Stayed { get; set; }
    }
    #endregion

}