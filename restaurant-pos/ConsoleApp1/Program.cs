namespace Backend
{
    public class Application
    {
        public static void Main()
        {
            Cart shoppingCart = new Cart();
            Menu.Start(shoppingCart);
        }

        public static void OldMain(string[] args)
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

    public class Menu

    {
        
        
        public static void Start(Cart shoppingCart)
        {
            string option;
            Console.WriteLine("Available commands: \n'l', 'Add coffee");
            option = Console.ReadLine();
            if (option == "l")
            {
                foreach (string item in shoppingCart.ListContents())
                {
                    Console.WriteLine(item);
                }
            }
            else if (option == "Add coffee")
            {
                Console.WriteLine("Coffe added");
                shoppingCart.AddCoffee();
                foreach (string item in shoppingCart.ListContents())
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

    public class Cart
    {
        public List<string> itemList = new List<string>();
        public List<string> ListContents()
        {
            return itemList;
        }

        public void AddCoffee()
        {
            //it won't add Coffee
            itemList.Add("Coffee");
        }

        public void AddBun()
        {
            
        }

        public void Pay()
        {

        }
    }  

    public class Tests
    {
        public void TestCart()
        {
            if (shoppingCart.itemList.Count == 0)
            {
                shoppingCart.itemList.Add("Coffee");
                if (shoppingCart.itemList[0] == "Coffee")
                {
                    Console.WriteLine("TestCart succeeded");
                }
                else
                {
                    Console.WriteLine("TestCart failed to return expected value");
                    Console.WriteLine("itemList.ToString() = {0}", shoppingCart.ListContents().ToString());
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
            if (shoppingCart.itemList.Count == 0)
            {
                shoppingCart.AddCoffee();
                if (shoppingCart.itemList[0] == "Coffee")
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
