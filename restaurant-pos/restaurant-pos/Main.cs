namespace restaurant_pos
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Main program = new();
            program.Loop();
        }
    }

    internal class Main
    {
        bool isRunning = true;
        Dictionary<int, Cart> carts = new Dictionary<int, Cart>(); // table_id, cart
        public void Loop()
        {
            int tableNumber;
            while (isRunning)
            { 
                switch (getInput("Choose an option (h for help) : "))
                {
                    case "a": // Add item to cart
                    case "add":
                        tableNumber = Convert.ToInt32(getInput("What table is the cart associated with? "));
                        break;
                    case "newcart":
                        tableNumber = Convert.ToInt32(getInput("What table is the cart associated with? "));
                        carts.Add(tableNumber, new Cart(tableNumber));
                        break;
                    case "l":
                    case "list": // List item to cart
                        break;
                    case "h":
                    case "help": // show commands
                        printOptions();
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

        void printOptions()
        {
            Console.WriteLine("Options:");
        }

        string getInput(string question)
        {
            Console.WriteLine(question);
            var input = Console.ReadLine();
            return (input is not null) ? input : "";
        }
    }
}
