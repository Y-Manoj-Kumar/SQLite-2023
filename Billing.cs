using System;
using System.Linq; 


class Billing
{
    int price = 0;
    public void print_bill()
    {
 
        bool closeApp =false;
        while (closeApp == false)
        {
            Console.WriteLine("\nEnter 0 to buy products. ");
            Console.WriteLine("Enter 1 to checkout.");
            Console.WriteLine("Enter 2 to Exit.\n");

            string userInput = Console.ReadLine();

            switch (userInput)
            {
                case "0":
                Console.WriteLine("\nEnter product_Id from above Inventory List to buy.");
                string Id =  Console.ReadLine();
                int entered_id = Convert.ToInt32(Id);

                Console.WriteLine("\nEnter quantity of product respectively.");
                string quantity = Console.ReadLine();
                int entered_quantity = Convert.ToInt32(quantity);
                
                if (entered_id == 11)
                {
                    int price = (entered_quantity*10);
                    Console.WriteLine("Price of Id 11 is: " +price);
                }    
                else if(entered_id == 12)
                {
                    int price = (entered_quantity*12);
                    Console.WriteLine("Price of Id 12 is: " +price);
                }
                else if(entered_id == 13)
                {
                    int price = (entered_quantity*20);
                    Console.WriteLine("Price of Id 13 is: " +price);
                }
                else if(entered_id == 14)
                {
                    int price = (entered_quantity*30);
                    Console.WriteLine("Price of Id 14 is: " +price);
                }
                else if(entered_id == 15)
                {
                    int price = (entered_quantity*50);
                    Console.WriteLine("Price of Id 15 is: " +price);
                }
                else 
                {
                    Console.WriteLine("Enter Valid Product Id");
                }
                break;

                case "1":
                // check_out();
                Console.WriteLine("\nIm from checkout");
                break;

                case "2":
                closeApp = true;
                Console.WriteLine("HAVE A GREAT DAY :)");
                Environment.Exit(2);
                break;   
            }
        }
    }


    public void check_out()
    {
        
    }


}