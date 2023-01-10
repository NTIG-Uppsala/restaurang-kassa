namespace Backend
{
    class Application
    {
        public static void Main(string[] args)
        {
            string option;
            var Cart = new Cart();
            Console.Write("Run Tests? (y/n)");
            option = Console.ReadLine();
            if (option == "y")
            {
                Console.WriteLine("Running tests...");
                // Testing Cart gives an empty list
                if (Cart.itemList.Count != 0)
                {
                    Console.WriteLine("Coffee test failed, 'itemList' not empty");
                    return;
                }
                // Test to add "Coffee"
                Cart.AddItem("Coffee", 15, true);
                if (Cart.itemList[0] != "Coffee")
                {
                    Console.WriteLine("Coffee test failed, 'Coffee' not added");
                    return;

                }

                Console.WriteLine("TestCoffee succeeded");
                // Test to add "Bun"
                Cart.AddItem("Bun", 150, true);
                if (Cart.itemList[1] != "Bun")
                {
                    Console.WriteLine("Bun test failed, 'Bun' not added");
                    return;
                }

                Console.WriteLine("TestBun succeeded");
                // Testing paying
                if (Cart.totalPrice != Cart.Pay())
                {
                    Console.WriteLine("TestPay failed, 'totalPrice' not equal to return of 'Cart.Pay()'");
                    return;

                }

                Cart.Pay();
                if (Cart.itemList.Count != 0)
                {
                    Console.WriteLine("TestPay failed, itemList.Count not equal to 0");
                    return;

                }
                Console.WriteLine("TestPay succeeded");

                Console.WriteLine("Tests complete");
            }
            // comment added
            else if (option == "n")
            {
                bool programLoop = true;
                while (programLoop)
                {
                    Console.WriteLine("\nAvailable commands: \n'a', 'l', 'p', 'x'");
                    option = Console.ReadLine();
                    switch (option)
                    {
                        case "l":
                            foreach (string item in Cart.ListContents())
                            {
                                Console.WriteLine(item);
                            }
                            Console.WriteLine("Total price: {0}", Cart.totalPrice);
                            break;
                        case "p":
                            Console.WriteLine("\nPrice to pay: {0}", Cart.totalPrice);
                            Console.WriteLine("Pay and clear cart? (y/n)");
                            option = Console.ReadLine();

                            if (option == "y")
                            {
                                Cart.Pay();
                                Console.WriteLine("Price has been paid.");
                            }
                            else
                            {
                                Console.WriteLine("Aborting payment process...");
                            }
                            break;
                        case "a":
                            Console.WriteLine("\nAvailable commands: \n'Coffee', 'Bun'");
                            option = Console.ReadLine();
                            if (option == "Coffee")
                            {
                                Cart.AddItem("Coffee", 15, false);
                                // Goes through each item in the function Cart.ListContents()
                                // and that function returns a list
                                foreach (string item in Cart.ListContents())
                                {
                                    Console.WriteLine(item);
                                }
                            }
                            else if (option == "Bun")
                            {
                                Cart.AddItem("Bun", 150, false);
                                // Goes through each item in the function Cart.ListContents()
                                // and that function returns a list
                                foreach (string item in Cart.ListContents())
                                {
                                    Console.WriteLine(item);
                                }
                            }
                            else
                            {
                                Console.WriteLine("{0} not implemented", option);
                            }
                            break;
                        case "x":
                            programLoop = false;
                            break;
                        default:
                            Console.WriteLine("Not valid option {0}", option);
                            break;
                    }
                }
            }
            else
            {
                Console.WriteLine("Not valid option {0}", option);
            }
        }
    }

    class Cart
    {
        public List<string> itemList = new List<string>();
        public float totalPrice = 0;
        public void AddItem(string item, float price, bool testing)
        {
            itemList.Add(item);
            totalPrice += price;
            if (!testing)
                Console.WriteLine("item: {0}, price: {1}", item, price);
        }

        public List<string> ListContents()
        {
            if (itemList.Count == 0)
            {
                Console.WriteLine("Cart is empty");
                return itemList;
            }
            else
            {
                Console.WriteLine("\nPrinting contents of cart...");

                return itemList;
            }
        }
        public float Pay()
        {
            itemList.Clear();
            float tempPrice = totalPrice;
            totalPrice = 0;
            return tempPrice;
        }
    }
}
