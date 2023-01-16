using Restaurant_pos_classes;
namespace Restaurant_pos_program
{
    class Program
    {
        static void Main(string[] args)
        {
            PosProgram program = new PosProgram();
            program.Loop();
        }
    }

    public class PosProgram
    {

        bool isRunning = true;
        public void Loop()
        {
            Menu menu = new();
            Cart cart = new(1);
            // To see that you can add items to cart
            menu.addProduct("BUN","It maybe a 'bulle'", 10, 0.12m);
            menu.addProduct("COFFE", "COFFE contains coffee", 10, 0.12m);
            while (isRunning)
            {
                
                switch (getInput("\nChoose an option (h for help) : "))
                {
                    case "a": // Add item to cart
                    case "add":
                        add(menu, cart);
                        break;
                    case "l":
                    case "list": // List items in cart
                        printCart(cart);
                        break;
                    case "h":
                    case "help": // show commands
                        Console.WriteLine("Options:");
                        Console.WriteLine("- a to add to cart\n- l to list items in cart\n- p to pay\n- m to show menu\n- q to exit program");
                        break;
                    case "p":
                    case "pay": // pay
                        pay(cart);

                        break;
                    case "m":
                    case "menu": // show menu
                        printMenu(menu);
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

        void add(Menu menu, Cart cart)
        {
            // If there is no food in menu
            if (menu.getMenu().Count == 0)
            {
                Console.WriteLine("Sorry, the menu is empty");
                return;
            }
            printMenu(menu);

            int itemId = getInputInt("What to add? (Type the number)");

            if (itemId == -1) return;

            cart.addProduct(itemId, menu);
            Console.WriteLine("New total price: " + cart.getTotalPrice());
            return;
        }
        void pay(Cart cart)
        {
            if (cart.getCart().Count == 0)
            {
                Console.WriteLine("Cart is empty");
                return;
            }
            printCart(cart);
            string paymentAccept = getInput("Accept? (y/n)");
            if (paymentAccept == "y" || paymentAccept == "yes")
            {
                cart.pay();
                Console.WriteLine("Paid");
            }
            else
            {
                Console.WriteLine("Canceled");
            }
        }

        void printCart(Cart cart)
        {
            Console.WriteLine("Cart:");

            foreach (Product product in cart.getCart())
            {
                Console.WriteLine("\t1x " + product.name + " " + product.getPrice() + " kr (with " + product.tax * 100 + "% tax)");
            }
            Console.WriteLine("Total: " + cart.getTotalPrice() + " kr");
        }

        void printMenu(Menu menu)
        {
            Console.WriteLine("Menu:");

            foreach (Product product in menu.getMenu())
            {
                Console.WriteLine("\t" + product.id + " " + product.name + " " + product.getPrice() + " kr (with " + product.tax * 100 + "% tax)");
            }
            Console.WriteLine("");
        }

        string getInput(string question)
        {
            Console.WriteLine(question);
            var input = Console.ReadLine();
            Console.WriteLine("");
            return (input is not null) ? input : "";
        }
        int getInputInt(string question)
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
                return getInputInt(question);
            }
        }
    }
}
