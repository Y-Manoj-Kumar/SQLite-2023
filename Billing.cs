using System.Globalization;
using Microsoft.Data.Sqlite;

class Billing
{

    public void print_bill()
    {
        Console.WriteLine("\nSelect product_Id to buy products");
        string Id =  Console.ReadLine();
        int entered_id = Convert.ToInt32(Id);

        Console.WriteLine("\nEnter quantity of product respectively.");
        string quantity = Console.ReadLine();
        int entered_quantity = Convert.ToInt32(quantity);

        // switch (entered_id)
        // {
        //     case "0":
        //     break;
            
        //     // default:
        // }

        if (entered_id == 11)
        {
            // int finalprice;

            int price = (entered_quantity*10);

            Console.WriteLine("Total price of entered product_Id is " + price);
        }


    }

    public void calculate_bill()
    {

    }




}