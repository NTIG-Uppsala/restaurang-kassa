namespace Backend
{
    class Application
    {
        public static void Main(string[] args)
        {
            string option;
            Console.Write("Run Tests? (y/n)");
            option = Console.ReadLine();
            if (option == "y")
            {
                var TestCart = new Tests();
                TestCart.TestCart();
                var TestCoffe = new Tests();
                TestCoffe.TestCoffee();
                Console.WriteLine("Tests complete.");
            }
            else if (option == "n")
            {
                var menu = new Menu();
                menu.Start();
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
            string option;
            Console.WriteLine("Available commands: \n'l', 'Add coffee");
            option = Console.ReadLine();
            if (option == "l")
            {
                var Cart = new Cart();
                foreach (string item in Cart.ListContents())
                {
                    Console.WriteLine(item);
                }
            }
            else if (option == "Add coffee")
            {
                Console.WriteLine("Coffe added");
                var Coffee = new Coffee();
                Coffee.AddCoffee();
                var Cart = new Cart();
                foreach (string item in Cart.ListContents())
                {
                    Console.WriteLine(item);
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
            //it won't add Coffee
            var Cart = new Cart();
            Cart.itemList.Add("Coffee");
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
            if (CartTest.itemList.Count == 0)
            {
                CoffeeTest.AddCoffee();
                if (CartTest.itemList[0] == "Coffee")
                {
                    Console.WriteLine("TestCoffee succeeded");
                }
                else
                {
                    Console.WriteLine("TestCoffee failed, 'Coffee' not added");
                }
            }
            else
            {
                Console.WriteLine("TestCoffee failed, 'itemList' not empty");
            }
        }
    }
}
