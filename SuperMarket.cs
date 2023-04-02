using System.Globalization;
using Microsoft.Data.Sqlite;

public class SuperMarket
{
    private string connectionString = @"Data Source = Inventory.sqlite";

    #region  Start
    public void Start()
    {
        var connection = new SqliteConnection(connectionString);

        connection.Open();
        var sqlCmd = connection.CreateCommand();
        sqlCmd.CommandText =
        @"CREATE TABLE IF NOT EXISTS Inventory(
        Product_Id INTEGER PRIMARY KEY AUTOINCREMENT,
        Product_Name TEXT,
        Product_Price INTEGER
        )";
                    
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
          Console.WriteLine("\nType 0 to Close Application.");
          Console.WriteLine("Type 1 to View InventoryList.");
          Console.WriteLine("Type 2 to Insert ProductDetails.");
          Console.WriteLine("Type 3 to Delete ProductDetails.");
          Console.WriteLine("Type 4 to Update ProductDetails.");
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
            InventoryList();
            break;

            case "2":
            Insert();
            break;

            case "3":
            Delete();
            break;

            case "4":
            // Update();
            break;

            default:
            Console.WriteLine("\n\n Command Invalid.Please enter a valid one");
            break;
          }


        }
    }
    #endregion


    #region InventoryList

    public void InventoryList()
    {

        Console.Clear();
        var connection = new SqliteConnection(connectionString);

        connection.Open();
        var sqlCmd = connection.CreateCommand();
        sqlCmd.CommandText =
        @"SELECT * FROM Inventory";

        List<ProductDetails> tabledata = new();
        SqliteDataReader reader = sqlCmd.ExecuteReader();

        if (reader.HasRows)
        {
            while (reader.Read())
            {
                tabledata.Add
                (
                    new ProductDetails
                    {
                        product_id = reader.GetInt32(0),
                        product_name = reader.GetString(1),
                        product_price = reader.GetInt32(2)
                        
                    }
                );
            }   
        }
        else
        {
            Console.WriteLine("No Rows are found");
        }
        connection.Close();

        Console.WriteLine("........INVENTORY LIST.........\n");

        Console.WriteLine("product_Id   product_Name    product_price");
        Console.WriteLine("\n");
        foreach (var item in tabledata)
        {
            Console.WriteLine($"{item.product_id}            {item.product_name}        {item.product_price}");
        }

    }

    #endregion


    #region Insert
    private void Insert()
    {
        int product_id = GetProductIdInput();
        string product_name = GetProductNameInput();
        int product_price = GetProductPriceInput();

        var connection = new SqliteConnection(connectionString);
        connection.Open();

        var sqlCmd = connection.CreateCommand();
        sqlCmd.CommandText =
        $"INSERT INTO Inventory (product_id,product_name,product_price) VALUES('{product_id}','{product_name}','{product_price}')";

        sqlCmd.ExecuteNonQuery();
        connection.Close();
    }
    #endregion


    #region Delete

    private void Delete()
    {
        Console.Clear();
        InventoryList();

        var productid = GetNumberInput("\n\nEnter the productId you want to Delete");
        var connection  = new SqliteConnection(connectionString);

        connection.Open();
        var sqlCmd = connection.CreateCommand();
        sqlCmd.CommandText = 
        $"DELETE FROM Inventory WHERE Product_Id='{productid}'";

        int rowCount = sqlCmd.ExecuteNonQuery();
        if (rowCount == 0)
        {
            Console.WriteLine($"\nEntered productId which is {productid} is not found");
        }

        Console.WriteLine($"\nEntered productId which is {productid} is deleted successfully");
        
    }
    #endregion

    #region  GetProductIdInput
    int GetProductIdInput()
    {
        Console.WriteLine("\nEnter ProductId.");
        string productId = Console.ReadLine();

        if (productId == "0")GetUserInput();

        int Id = Convert.ToInt32(productId);

        return Id;
    }
    #endregion


    #region GetProductNameInput
    string GetProductNameInput()
    {
        Console.WriteLine("\nEnter ProductName.");
        string productName = Console.ReadLine();

        if (productName == "0")GetUserInput();

        return productName;
    }
    #endregion


    #region GetProductPrice
    int GetProductPriceInput()
    {
        Console.WriteLine("\nEnter Product Price.");
        string productPrice = Console.ReadLine();

        if (productPrice == "0")GetUserInput();

        int price = Convert.ToInt32(productPrice);

        return price;
    }
    #endregion


    #region  GetNumberInput
    int GetNumberInput(string message)
    {
      Console.WriteLine(message);

      string numberInput = Console.ReadLine();
      if (numberInput == "0") GetUserInput();

      int finalInput = Convert.ToInt32(numberInput);
      return finalInput ;
    }
    #endregion

    #region GetSetters of ProductDetails
    public class ProductDetails
    {
        public int product_id { get; set; }
        public string product_name { get; set; }
        public int product_price { get; set; }
    }
    #endregion

}