namespace Backend
{
    class Application
    {
        public static void Main(string[] args)
        {
            string option;
            var Cart = new Cart();
            // comment added
            Console.Write("Run Tests? (y/n)");
            option = Console.ReadLine();
            if (option == "y")
            {
                Console.WriteLine("Running TESTS...");
                // Testing Cart gives an empty list
                if (Cart.itemList.Count == 0)
                {
                    // Test to add "Coffee"
                    Cart.AddItem("Coffee", 15);
                    if (Cart.itemList[0] == "Coffee")
                    {
                        Console.WriteLine("TestCoffee succeeded");
                        // Test to add "Bun"
                        Cart.AddItem("Bun", 150);
                        if (Cart.itemList[1] == "Bun")
                        {
                            Console.WriteLine("TestBun succeeded");
                            // Testing paying
                            if (Cart.totalPrice == Cart.Pay())
                            {
                                Cart.Pay();
                                if (Cart.itemList.Count == 0)
                                {
                                    Console.WriteLine("TestPay succeeded");
                                }
                                else
                                {
                                    Console.WriteLine("TestPay failed, itemList.Count not equal to 0");
                                }
                            }
                            else
                            {
                                Console.WriteLine("TestPay failed, 'totalPrice' not equal to return of 'Cart.Pay()'");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Bun test failed, 'Bun' not added");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Coffee test failed, 'Coffee' not added");
                    }
                }
                else
                {
                    Console.WriteLine("Coffee test failed, 'itemList' not empty");
                }
                Console.WriteLine("Tests complete.");
            }
            // comment added
            else if (option == "n")
            {
                bool programLoop = true;
                while (programLoop)
                {
                    Console.WriteLine("\nAvailable commands: \n'l', 'a', 'x'");
                    option = Console.ReadLine();
                    if (option == "l")
                    {

                        foreach (string item in Cart.ListContents())
                        {
                            Console.WriteLine(item);
                        }
                    }
                    else if (option == "a")
                    {
                        Console.WriteLine("\nAvailable commands: \n'Coffee', 'Bun'");
                        option = Console.ReadLine();
                        if (option == "Coffee")
                        {
                            Cart.AddItem("Coffee", 15);
                            // Goes through each item in the function Cart.ListContents()
                            // and that function returns a list
                            foreach (string item in Cart.ListContents())
                            {
                                Console.WriteLine(item);
                            }
                        }
                        else if (option == "Bun")
                        {
                            Cart.AddItem("Bun", 150);
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
                    }
                    else if(option == "x")
                    {
                        programLoop = false;
                    }
                    else
                    {
                        Console.WriteLine("Not valid option {0}", option);
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
        public void AddItem(string item, float price) 
        {
            itemList.Add(item);
            totalPrice += price;
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
                Console.WriteLine("Printing contents of cart...");
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

    class Tests
    {
        public void TestCart()
        {
            var CartTest = new Cart();
            if (CartTest.itemList.Count == 0)
            {
                CartTest.itemList.Add("Coffee");
                if (CartTest.itemList[0] == "Coffee")
                {
                    Console.WriteLine("TestCart succeeded");
                }
                else
                {
                    Console.WriteLine("TestCart failed to return expected value");
                    Console.WriteLine("itemList.ToString() = {0}", CartTest.ListContents().ToString());
                }
            }
            else
            {
                Console.WriteLine("TestCart failed, itemList did not start empty");
            }
        }

        public void TestCoffee()
        {
            var CartTest = new Cart();

        }
    }
}
