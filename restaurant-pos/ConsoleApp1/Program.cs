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
                var Test = new Tests();
                Test.TestCart();
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
            Console.WriteLine("Available commands: \n'l'");
            option = Console.ReadLine();
            if (option == "l")
            {
                var Cart = new Cart();
                Cart.ListContents();
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
    }
}