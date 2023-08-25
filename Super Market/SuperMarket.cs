using System.Globalization;
using Microsoft.Data.Sqlite;

public class SuperMarket
{
    private string connectionString = @"Data Source = Inventory.sqlite";
    private string tableName = "Warehouse";
    #region  Start
    public void Start()
    {
        var connection = new SqliteConnection(connectionString);

        connection.Open();
        var sqlCmd = connection.CreateCommand();
        sqlCmd.CommandText =
        @"CREATE TABLE IF NOT EXISTS "+ tableName +@" (
        Product_Id INTEGER PRIMARY KEY AUTOINCREMENT,
        Product_Name TEXT,
        Product_Price INTEGER,
        In_Stock INTEGER
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
          Console.WriteLine("Type 5 to buy the Products");
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
            Update();
            break;

            case "5":
            print_bill();
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
        $"SELECT * FROM {tableName} ";

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
                        product_price = reader.GetInt32(2),
                        product_instock = reader.GetInt32(3)
                        
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

        Console.WriteLine("product_Id\tproduct_Name\tproduct_price\tIn_Stock");
        Console.WriteLine("....................................................");
        foreach (var item in tabledata)
        {
            Console.WriteLine($"{item.product_id}\t\t{item.product_name}\t\t{item.product_price}\t\t{item.product_instock}");        
        }

    }

    #endregion


    #region Insert
    private void Insert()
    {
        int product_id = GetProductIdInput();
        string product_name = GetProductNameInput();
        int product_price = GetProductPriceInput();
        int product_instock = GetProductInStock();

        var connection = new SqliteConnection(connectionString);
        connection.Open();

        var sqlCmd = connection.CreateCommand();
        sqlCmd.CommandText =
        $"INSERT INTO {tableName} (product_id,product_name,product_price,in_stock) VALUES('{product_id}','{product_name}','{product_price}','{product_instock}')";

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
        $"DELETE FROM {tableName} WHERE Product_Id='{productid}'";

        int rowCount = sqlCmd.ExecuteNonQuery();
        if (rowCount == 0)
        {
            Console.WriteLine($"\nEntered productId which is {productid} is not found");
        }

        Console.WriteLine($"\nEntered productId which is {productid} is deleted successfully");
        
    }
    #endregion


    #region  Update

    public void Update()
    {
        InventoryList();
        
        var recordId = GetNumberInput("\n\nPlease type Id of the record would like to update. Type 0 to return to main manu.\n\n");
        
       using( var connection = new SqliteConnection(connectionString))
        {
            connection.Open();

            var checkCmd = connection.CreateCommand();
            checkCmd.CommandText = $"SELECT EXISTS(SELECT 1 FROM {tableName} WHERE Id ={recordId})";
            int checkQuery = Convert.ToInt32(checkCmd.ExecuteScalar());

            if (checkQuery == 0)
            {
                Console.WriteLine($"\n\nRecord with Id {recordId} doesn't exist.\n\n");
                connection.Close();
                Update();
            }

             int  product_id = GetProductIdInput();
             string product_name = GetProductNameInput();
             int product_price = GetProductPriceInput();
             int product_instock = GetProductInStock();

             var sqlcmd = connection.CreateCommand();
             sqlcmd.CommandText = $"UPDATE {tableName} SET Product_Id ='{product_id}', Product_Name = '{product_name}', Product_Price ='{product_price}',In_Stock='{product_instock}'     ";

             sqlcmd.ExecuteNonQuery();

                connection.Close();
        }

    }

    #endregion


    #region  GetProductIdInput
    int GetProductIdInput()
    {
        Console.WriteLine("\nEnter ProductId.");
        string productId = Console.ReadLine();

        if (productId == "0")
        
            GetUserInput();

        int Id = Convert.ToInt32(productId);

        return Id;
    }
    #endregion


    #region GetProductInStock


    int GetProductInStock()
    {
        Console.WriteLine("\nEnter present stock of Product.");
        string productinstock = Console.ReadLine();

        if (productinstock == "0")
       
            GetUserInput();
       
        int instock = Convert.ToInt32(productinstock);

        return instock;
    }

    #endregion


    #region GetProductNameInput
    string GetProductNameInput()
    {
        Console.WriteLine("\nEnter ProductName.");
        string productName = Console.ReadLine();

        if (productName == "0")
            
            GetUserInput();

        return productName;
    }
    #endregion


    #region GetProductPrice
    int GetProductPriceInput()
    {
        Console.WriteLine("\nEnter Product Price.");
        string productPrice = Console.ReadLine();

        if (productPrice == "0")
            
            GetUserInput();

        int price = Convert.ToInt32(productPrice);

        return price;
    }
    #endregion


    #region  GetNumberInput
    int GetNumberInput(string message)
    {
      Console.WriteLine(message);

      string numberInput = Console.ReadLine();
      if (numberInput == "0") 
        
        GetUserInput();

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
        public int product_instock {get; set;}
    }
    #endregion


//                   .........................BILLING SECTION.............................

    int sum = 0; // Initialize the total bill sum
    private object tablename;
    private string p_name;
    int p_price;
    List<string> purchasedItems = new List<string>();

    DateTime currentDateTime = DateTime.Now;


    public void print_bill()
    {
        bool closeApp = false;
        while (!closeApp)
        {
            Console.WriteLine("\nEnter 0 to buy products. ");
            Console.WriteLine("Enter 1 to checkout.");
            Console.WriteLine("Enter 2 to Exit.\n");

            string userInput = Console.ReadLine();

            switch (userInput)
            {
                case "0":
                    InventoryList();
                    Console.WriteLine("\nEnter product_Id from above Inventory List to buy.");
                    string id = Console.ReadLine();
                    int enteredId = Convert.ToInt32(id);

                    Console.WriteLine("\nEnter quantity of product respectively.");
                    string quantity = Console.ReadLine();
                    int enteredQuantity = Convert.ToInt32(quantity);

                    int price = CalculatePrice(enteredId, enteredQuantity);
                    if (price != -1)
                    {

                        string purchasedItem = $"{enteredQuantity} x {p_name}        ₹{price}";
                        purchasedItems.Add(purchasedItem);

                        Console.WriteLine($"\nPrice of {p_name} {p_price}*{quantity} : ₹{price}");
                        sum = sum+price;
                    }
                    break;

                case "1":
                    Console.WriteLine("Payment thorough cash or card");
                    string pay_mode = Console.ReadLine();
                    Console.WriteLine("\n\n**********************************");
                    Console.WriteLine("              RECEIPT          ");
                    Console.WriteLine("**********************************");
                    Console.WriteLine("SandyStores    " + currentDateTime +"\n");
                    foreach (string item in purchasedItems)
                    {
                        Console.WriteLine(item);
                    }
                    Console.WriteLine("----------------------------------");
                    Console.WriteLine("Total Bill   : " +"₹"+sum);
                    Console.WriteLine("Payment done :"+pay_mode);
                    Console.WriteLine("----------------------------------");
                    Console.WriteLine(" \n        THANK YOU!               ");

                    Console.WriteLine("VISIT AGAIN HAVE A GREAT DAY     ");

                    Console.WriteLine("**********************************");
                    break;
                    

                case "2":
                    closeApp = true;
                    Console.WriteLine("HAVE A GREAT DAY :)");
                    break;
            }
        }
    }

    int CalculatePrice(int enteredId, int quantity)
    {
        int price = -1; // Default value for invalid product IDs
        

        switch (enteredId)
        {
            case 109264 :
                p_name="KisanJam";
                p_price=39;
                price = quantity * p_price;
                // Console.WriteLine($"Price of Kitkat :  {price} * {quantity} = {sum} " );
                break;
            case 435631:
                p_name="GoodDay";
                p_price=30;
                price = quantity * p_price;
                break;
            case 836153:
                p_name="Snikers";
                p_price=70;
                price = quantity * p_price;
                break;
            case 863416:
                p_name="KitKat";
                p_price=30;
                price = quantity * p_price;
                break;
            case 887542:
                p_name="MilkBread";
                p_price=45;
                price = quantity * p_price;
                break;
            case 998125:
                p_name="BadamMilk";
                p_price=60;
                price = quantity * p_price;
                break;
            default:
                Console.WriteLine("Enter Valid Product Id");
                break;
        }
            const int MaxLength = 6;

             var name = p_name;
             if (name.Length > MaxLength)
               name = name.Substring(0, MaxLength);

             p_name = name;

        return price;
    }





}


