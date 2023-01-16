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

                        // If there is no food in menu
                        if (menu.getMenu().Count == 0)
                        {
                            Console.WriteLine("Sorry, the menu is empty");
                            break;
                        }
                        foreach (Product product in menu.getMenu()) 
                        {
                            Console.WriteLine(product.id);
                        }
                        // TODO: Make so you can choice what to add
                        int itemId = getInputInt("What to add? (Type the number)");
                        cart.addProduct(itemId,menu);
                        Console.WriteLine("To pay: " + cart.getTotalPrice());
                        break;
                    case "l":
                    case "list": // List items in cart
                        foreach (Product product in cart.getCart())
                        { 
                            Console.WriteLine(product.id + product.name + product.getPrice() + product.tax + " kr");
                        }
                        break;
                    case "h":
                    case "help": // show commands
                        Console.WriteLine("Options:");
                        Console.WriteLine("a to add to cart, l to list items in cart, p to pay, m to show menu, q exit program");
                        break;
                    case "p":
                    case "pay": // pay
                        break;
                    case "m":
                    case "menu": // show menu
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
        string getInput(string question)
        {
            Console.WriteLine(question);
            var input = Console.ReadLine();
            return (input is not null) ? input : "";
            // if (input == null){input = ""}
        }
        int getInputInt(string question)
        {
            Console.WriteLine(question);
            try
            {
                return Convert.ToInt32(Console.ReadLine());
            }
            catch(Exception ex)
            {
                Console.WriteLine("Please give a number");
                return getInputInt(question);
            }
        }
    }
}
