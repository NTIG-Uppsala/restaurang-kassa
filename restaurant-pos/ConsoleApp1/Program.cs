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
                if (Cart.itemList.Count == 0)
                {
                    Cart.itemList.Add("Coffee");
                    if (Cart.itemList[0] == "Coffee")
                    {
                        Console.WriteLine("TestCoffee succeeded");
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
            else if (option == "n")
            {
                string option2;
                Console.WriteLine("Available commands: \n'l', 'Add coffee");
                option2 = Console.ReadLine();
                if (option2 == "l")
                {

                    foreach (string item in Cart.ListContents())
                    {
                        Console.WriteLine(item);
                    }
                }
                else if (option2 == "Add coffee")
                {
                    Cart.itemList.Add("Coffee");
                    Console.WriteLine("Coffe added");

                    foreach (string item in Cart.ListContents())
                    {
                        Console.WriteLine(item);
                    }
                }
                else
                {
                    Console.WriteLine("Not valid option {0}", option2);
                }
            }
            else
            {
                Console.WriteLine("Not valid option {0}", option);
            }
        }
    }

    class Menu
    {
        public void Start()
        {

        }
    }

    class Cart
    {
        public List<string> itemList = new List<string>();
        public List<string> ListContents()
        {
            return itemList;
        }
    }

    class Checkout
    {
        public void Pay()
        {

        }
    }

    class Coffee
    {
        public void AddCoffee()
        {
            //it dosn't add Coffee ha ha :D

        }
    }

    class Buns
    {
        public void AddBun()
        {

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
            // To make sure the list is empty
            var CartTest = new Cart();
            var CoffeeTest = new Coffee();

        }
    }
}
