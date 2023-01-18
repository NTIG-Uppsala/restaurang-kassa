using Restaurant_pos_classes;
using System.Diagnostics;
using System.Xml.Linq;

namespace Restaurant_pos_program
{
    class Program
    {
        static void Main()
        {
            PosProgram program = new();
            program.Loop();
        }
    }

    public class PosProgram
    {

        bool isRunning = true;
        Menu menu;
        Cart cart;
        Database database;

        public PosProgram()
        {
            menu = new();
            cart = new(1);
            database = new();
            LoadDatabaseProducts();
        }

        public void Loop()
        {
            while (isRunning)
            {
                
                switch (GetInput("\nChoose an option (h for help) : "))
                {
                    case "a": // Add item to cart
                    case "add":
                        add(menu, cart);
                        break;
                    case "l":
                    case "list": // List items in cart
                        PrintCart(cart);
                        break;
                    case "h":
                    case "help": // show commands
                        Console.WriteLine("Options:");
                        Console.WriteLine("- a to add to cart\n- l to list items in cart\n- p to pay\n- m to show menu\n- q to exit program");
                        break;
                    case "p":
                    case "pay": // pay
                        Pay(cart);

                        break;
                    case "m":
                    case "menu": // show menu
                        PrintMenu(menu);
                        break;
                    case "q":
                    case "quit": // exit program
                        isRunning = false;
                        break;
                    default:
                        Console.WriteLine("Not a valid option");
                        break;
                }
            }
        }
        void LoadDatabaseProducts()
        {
            List<Product> productsFromDatabase = database.GetProducts();

            foreach (Product product in productsFromDatabase)
            {
                menu.AddProduct(product.name, product.description, product.price, product.tax);
            }
        }

        void add(Menu menu, Cart cart)
        {
            // If there is no food in menu
            if (menu.GetMenu().Count == 0)
            {
                Console.WriteLine("Sorry, the menu is empty");
                return;
            }
            PrintMenu(menu);

            int itemId = GetInputInt("What to add? (Type the number)");

            if (itemId == -1) return;

            cart.AddProduct(itemId, menu);
            Console.WriteLine("New total price: " + cart.GetTotalPrice());
            return;
        }
        void Pay(Cart cart)
        {
            if (cart.GetCart().Count == 0)
            {
                Console.WriteLine("Cart is empty");
                return;
            }
            PrintCart(cart);
            string paymentAccept = GetInput("Accept? (y/n)");
            if (paymentAccept == "y" || paymentAccept == "yes")
            {
                Receipt receipt = new();
                receipt.CreateReceipt(cart);
                cart.Pay();
                Console.WriteLine("Paid");
            }
            else
            {
                Console.WriteLine("Canceled");
            }
        }

        void PrintCart(Cart cart)
        {
            Console.WriteLine("Cart:");

            foreach (Product product in cart.GetCart())
            {
                Console.WriteLine("\t1x " + product.name + " " + product.GetPrice() + " kr (with " + product.tax * 100 + "% tax)");
            }
            Console.WriteLine("Total: " + cart.GetTotalPrice() + " kr");
        }

        void PrintMenu(Menu menu)
        {
            Console.WriteLine("Menu:");

            foreach (Product product in menu.GetMenu())
            {
                Console.WriteLine("\t" + product.id + " " + product.name + " " + product.GetPrice() + " kr (with " + product.tax * 100 + "% tax)");
            }
            Console.WriteLine("");
        }

        string GetInput(string question)
        {
            Console.WriteLine(question);
            var input = Console.ReadLine();
            Console.WriteLine("");
            return (input is not null) ? input : "";
        }
        int GetInputInt(string question)
        {
            Console.WriteLine(question);
            try
            {
                string input = Console.ReadLine(); // not null
                
                if (input == "q" || input == "quit") return -1;

                int inputInt = Convert.ToInt32(input);
                Console.WriteLine("");
                return inputInt;
            }
            catch(Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                Console.WriteLine("Please give a number");
                return GetInputInt(question);
            }
        }
    }
}
