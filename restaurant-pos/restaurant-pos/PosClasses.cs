using System.Diagnostics;

namespace Restaurant_pos_classes
{
    public class Cart
    {
        int tableNumber;
        private List<Product> items = new List<Product>();

        public Cart(int tableNumber)
        {
            this.tableNumber = tableNumber;
        }

        public decimal getTotalPrice()
        {
            // Sums up the price of all products and returns the total 

            decimal total = 0;
            foreach (Product product in items)
            {
                total += product.getPrice();
            }

            return total;
        }

        public void addProduct(int productID, Menu productMenu)
        {
            // Method to add a product from the menu to the cart

            // Loop over the menu
            foreach (Product entry in productMenu.getMenu())
            {
                // If the product id that wants to be added is not the current item in the loop skip
                if (productID != entry.id) continue;

                this.items.Add(entry); // Add item to the cart
                Console.WriteLine("Added " + entry.id + " " + entry.name);
                break; // break out of the loop
            }
        }

        public void removeProduct(int productID)
        {
            // Removes a product from the cart     

            foreach (Product product in this.items)
            {

                if (productID != product.id) continue;

                this.items.Remove(product);
                Console.WriteLine("Removed " + product.id + " " + product.name);
                break;

            }
        }

        public bool pay()
        {
            // Returns if the payment went through or not 

            // In the future this could call a possible processPayment method

            bool isPaid = true;
            this.clearCart();


            return isPaid;
        }

        public void clearCart()
        {
            this.items.Clear();
        }

        public List<Product> getCart()
        {
            return this.items;
        }
    }

    public class Product
    {
        public int id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public decimal price { get; set; }
        public decimal tax { get; set; }

        public Product(int id, string name, string description, decimal price, decimal tax)
        {
            this.id = id;
            this.name = name;
            this.description = description;
            this.price = price;
            this.tax = tax;
        }

        public decimal getPrice()
        {
            // Calculates price of product with tax eg (100 * 1.25 -> 125 SEK)
            return this.price * (1.0m + this.tax);
        }

        public decimal getPriceNoTax()
        {
            // Calculates price of product without tax
            return this.price;
        }

        public string getStringPrice()
        {
            // Returns a string with the correct format of the price eg 250.00 SEK
            return string.Format("{0} SEK", getPrice());
        }

    }

    public class Menu
    {
        private List<Product> menuItems = new List<Product>();

        public void addProduct(string name, string description, decimal price, decimal tax)
        {
            // Add a product to the menu 

            // Gets id of last item in menu if menu has items and adds 1 to it. 
            // If the menu has no items, just set the id to 0
            int last_avaliable_product_id = 0;
            if (menuItems.Count > 0)
            {
                int last_product_id = menuItems.Last().id;
                last_avaliable_product_id = last_product_id + 1;
            }

            Product new_product =
                new Product(last_avaliable_product_id, name, description, price, tax);
            menuItems.Add(new_product);
        }

        public void removeProduct(int id)
        {
            foreach (Product product in this.menuItems)
            {
                if (product.id != id) continue;

                menuItems.Remove(product);
                break;
            }
        }

        public List<Product> getMenu()
        {
            return menuItems;
        }

    }

    public class Receipt
    {
        long epochTime = DateTimeOffset.Now.ToUnixTimeSeconds();
        DateTime currentDateTime = DateTime.Now;
        public void createReceipt(Cart cart)
        {
            string userName = System.Security.Principal.WindowsIdentity.GetCurrent().Name.Split("\\")[1];

            string filename = string.Format(@"C:\Users\{0}\Documents\restaurant-receipts\receipt_{1}.txt", userName, epochTime.ToString());

            if (!Directory.Exists(string.Format(@"c:\Users\{0}\Documents\restaurant-receipts\", userName)))
            {
                Directory.CreateDirectory(string.Format(@"c:\Users\{0}\Documents\restaurant-receipts\", userName));
            }

            List<string> receipt = new List<string>()
            {
                "Bengans Burgeria",
                "Fjällgatan 32H",
                "981 39 Jönköping\n",
                "Tel: (+46)63-055 55 55",
                "Mail: info.bengans@gmail.com",
                "Org. Nr: 234567-8901\n",
                $"{currentDateTime.ToString("s").Replace("T", " ")}",
                $"Säljare: {"Bengan"}",
                $"Kvitto Nr: {epochTime}",
                "-----------------------------------------------------\n",
            };


            decimal vat25 = 0m;
            decimal vat12 = 0m;
            decimal vat0 = 0m;

            foreach (Product product in cart.getCart())
            {
                switch (product.tax)
                {
                    case 0.25m:
                        vat25 += product.getPrice() - product.getPriceNoTax(); // 1.25m;
                        break;
                    case 0.12m:
                        vat12 += product.getPrice() - product.getPriceNoTax(); // 1.12m;
                        break;
                    default:
                        vat0 += product.getPrice() - product.getPriceNoTax();
                        break;
                }
            }

            decimal netPrice = vat25 + vat12 + vat0;
            decimal totalVat = (vat25 * 0.25m) + (vat12 * 0.12m);

            foreach (Product product in cart.getCart())
            {
                receipt.Add("\n-----------------------------------------------------\n");
                receipt.Add("\t1x " + product.name + " " + product.getStringPrice() + " (with " + product.tax * 100 + "% tax)");  ;
            }
            receipt.Add("\n-----------------------------------------------------\n");

            List<string> receitPart2 = new List<string>()
            {
                "-----------------------------------------------------\n",
                "VAT basis:",
                $"VAT 25%\t{vat25.ToString("0.00")} SEK",
                $"VAT 12%\t{vat12.ToString("0.00")} SEK",
                $"No VAT\t{vat0.ToString("0.00")} SEK\n",
                $"VAT total\t{netPrice} SEK\n",

                "-----------------------------------------------------\n",
                $"Total:\t\t{cart.getTotalPrice()} SEK\n",
            };
            receipt.AddRange(receitPart2);

            using (StreamWriter sw = File.CreateText(filename))
            { 
                foreach (string stringPart in receipt)
                { 
                    sw.WriteLine(stringPart); 
                }
                sw.Close(); 
            }
        }
    }
}
