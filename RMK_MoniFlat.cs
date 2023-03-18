using System.Globalization;
using Microsoft.Data.Sqlite;

class RMK_MoniFlat
{

    private string connectionString = @"Data Source = RMK_MoniFlat.sqlite";

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


    private void GetAllRecords()
    {
        Console.Clear();
        var connection = new SqliteConnection(connectionString);

        connection.Open();
        var sqlCmd =connection.CreateCommand();
        sqlCmd.CommandText = 
        @"SELECT * FROM RMK_MoniFlat";

        List<FlatMembers> tabledata = new();
        SqliteDataReader reader = sqlCmd.ExecuteReader();

        if (reader.HasRows)
        {
            while (reader.Read())
            {
                tabledata.Add(
                new FlatMembers
                {
                    // Id = reader.GetInt32(0);
                    // Name = reader.GetString(1);
                    // Age = reader.GetInt32(2);
                    // Address =reader.GetString(3);
                    // Mobile_Number = reader.GetInt64(4);
                    // Months_Stayed = reader.GetInt32(5);


                    
                 
                });
            }
        }
        else
        {
            Console.WriteLine("No Rows Found.");
        }
    }

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

    private void Update()
    {
        Console.WriteLine("Im From Upadte");
    }

    private void Delete()
    {
        Console.WriteLine("Im From Delete");
    }

    string GetNameInput()
    {
        Console.WriteLine("Enter your Full Name.");
        string enteredName = Console.ReadLine();

        if (enteredName == "0")GetUserInput();

        return enteredName;
    }

    int GetAgeInput()
    {
        Console.WriteLine("\nEnter your Age.");
        string enteredAge = Console.ReadLine();

        if (enteredAge == "0")GetUserInput();

        int age = Convert.ToInt32(enteredAge);

        return age;
    }

    string GetAddressInput()
    {
        Console.WriteLine("\nEnter Your Address.");
        string enteredAddress = Console.ReadLine();

        if (enteredAddress == "0")GetUserInput();

        return enteredAddress;
    }

    long GetMobileNumber()
    {
        Console.WriteLine("\nEnter your MobileNumber.");
        string enteredMobilenumber = Console.ReadLine();

        if (enteredMobilenumber == "0")GetUserInput();

        long Mobile_Number = Convert.ToInt64(enteredMobilenumber);

        return Mobile_Number;
    }

    int GetMonthsStayed()
    {
        Console.WriteLine("\nEnter how many MonthsStayed.");
        string enteredMonthsStayed = Console.ReadLine();

        if (enteredMonthsStayed == "0")GetUserInput();

        int Months_Stayed = Convert.ToInt32(enteredMonthsStayed);

        return Months_Stayed;
        
    }


    public class FlatMembers
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string Address { get; set; }
        public long Mobile_Number { get; set; }
        public int Months_Stayed { get; set; }
    }

    
}