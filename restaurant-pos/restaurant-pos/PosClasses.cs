using System;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Xml.Linq;
using Microsoft.Data.Sqlite;

namespace Restaurant_pos_classes
{
    public class Database
    {
        private string username { get; set; }
        private string path { get; set; }
        private string filename { get; set; }
        private string fullpath { get; set; }
        public string connectionString { get; set; }
        public Database(string filename="database.db") 
        {
            this.username = System.Security.Principal.WindowsIdentity.GetCurrent().Name.Split("\\")[1];
            this.path = string.Format(@"C:\Users\{0}\Documents\restaurant-database", username);
            this.filename = filename;
            this.fullpath = path + @"\" + this.filename;

            connectionString = new SqliteConnectionStringBuilder()
            {
                Mode = SqliteOpenMode.ReadWriteCreate,
                DataSource = fullpath
            }.ToString();

            // Create directory if it doesn't exist when creating new object instance
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }

        public List<Product> GetProducts() 
        {
            List<Product> OutputList = new();
            // SELECT * FROM Products;
            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText =
                @"
                    SELECT p.id, p.price, p.name, p.description, t.value
                    FROM Products p 
	                    INNER JOIN Tax t ON ( t.id = p.taxID  )  
                ";

                using (var reader = command.ExecuteReader())
                {
                    using (DataTable datatable = new())
                    {
                        datatable.Load(reader);
                        foreach (DataRow row in datatable.Rows)
                        {
                            Int64 id = (Int64)row["id"];
                            decimal price = (decimal)row["price"];
                            string name = (string)row["name"];
                            string description = (string)row["description"];
                            decimal tax = (decimal)row["taxID"];
                            Console.WriteLine(id + price + name + description + tax);

                            Product product = new Product(id, name, description, price, 0.12m);
                            OutputList.Add(product);

                        }
                    }
                }
            connection.Close();
            }
            return OutputList;
        }

    }


    public class Cart
    {
        Int64 tableNumber;
        private List<Product> items = new List<Product>();

        public Cart(Int64 tableNumber)
        {
            this.tableNumber = tableNumber;
        }

        public decimal GetTotalPrice()
        {
            // Sums up the price of all products and returns the total 

            decimal total = 0;
            foreach (Product product in items)
            {
                total += product.GetPrice();
            }

            return total;
        }

        public void AddProduct(Int64 productID, Menu productMenu)
        {
            // Method to add a product from the menu to the cart

            // Loop over the menu
            foreach (Product entry in productMenu.GetMenu())
            {
                // If the product id that wants to be added is not the current item in the loop skip
                if (productID != entry.id) continue;

                this.items.Add(entry); // Add item to the cart
                Console.WriteLine("Added " + entry.id + " " + entry.name);
                break; // break out of the loop
            }
        }

        public void RemoveProduct(Int64 productID)
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

        public bool Pay()
        {
            // Returns if the payment went through or not 

            // In the future this could call a possible processPayment method

            bool isPaid = true;
            this.ClearCart();


            return isPaid;
        }

        public void ClearCart()
        {
            this.items.Clear();
        }

        public List<Product> GetCart()
        {
            return this.items;
        }
    }

    public class Product
    {
        public Int64 id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public decimal price { get; set; }
        public decimal tax { get; set; }

        public Product(Int64 id, string name, string description, decimal price, decimal tax)
        {
            this.id = id;
            this.name = name;
            this.description = description;
            this.price = price;
            this.tax = tax;
        }

        public decimal GetPrice()
        {
            // Calculates price of product with tax eg (100 * 1.25 -> 125 SEK)
            return this.price * (1.0m + this.tax);
        }

        public decimal GetPriceNoTax()
        {
            // Calculates price of product without tax
            return this.price;
        }

        public string GetStringPrice()
        {
            // Returns a string with the correct format of the price eg 250.00 SEK
            return string.Format("{0} SEK", GetPrice());
        }

    }

    public class Menu
    {
        private List<Product> menuItems = new List<Product>();

        public void AddProduct(string name, string description, decimal price, decimal tax)
        {
            // Add a product to the menu 

            // Gets id of last item in menu if menu has items and adds 1 to it. 
            // If the menu has no items, just set the id to 0
            Int64 last_avaliable_product_id = 0;
            if (menuItems.Count > 0)
            {
                Int64 last_product_id = menuItems.Last().id;
                last_avaliable_product_id = last_product_id + 1;
            }

            Product new_product =
                new Product(last_avaliable_product_id, name, description, price, tax);
            menuItems.Add(new_product);
        }

        public void RemoveProduct(Int64 id)
        {
            foreach (Product product in this.menuItems)
            {
                if (product.id != id) continue;

                menuItems.Remove(product);
                break;
            }
        }

        public List<Product> GetMenu()
        {
            return menuItems;
        }

    }

    public class Receipt
    {
        private long epochTime = DateTimeOffset.Now.ToUnixTimeSeconds();
        private DateTime currentDateTime = DateTime.Now;
        private List<string> receipt = new List<string>();

        // Path related variables
        private string username { get; set; }
        private string path { get; set; }
        private string filename { get; set; }
        private string fullpath { get; set; }

        public Receipt()
        {
            this.username = System.Security.Principal.WindowsIdentity.GetCurrent().Name.Split("\\")[1];
            this.path = string.Format(@"C:\Users\{0}\Documents\restaurant-receipts", username);
            this.filename = string.Format(@"receipt_{0}.txt", epochTime.ToString());
            this.fullpath = path + @"\" + filename;

            // Create directory if it doesn't exist when creating new object instance
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }

        public void CreateReceipt(Cart cart)
        {
            decimal vat25 = 0m;
            decimal vat12 = 0m;
            decimal vat0 = 0m;

            foreach (Product product in cart.GetCart())
            {
                switch (product.tax)
                {
                    case 0.25m:
                        vat25 += product.GetPrice() - product.GetPriceNoTax(); // 1.25m;
                        break;
                    case 0.12m:
                        vat12 += product.GetPrice() - product.GetPriceNoTax(); // 1.12m;
                        break;
                    default:
                        vat0 += product.GetPrice() - product.GetPriceNoTax();
                        break;
                }
            }

            decimal netPrice = vat25 + vat12 + vat0;
            decimal totalVat = (vat25 * 0.25m) + (vat12 * 0.12m);

            /*
                Contact information and receipt information
             */
            receipt.Add("Bengans Burgeria");
            receipt.Add("Fjällgatan 32H");
            receipt.Add("981 39 Jönköping\n");
            receipt.Add("Tel: (+46)63-055 55 55");
            receipt.Add("Mail: info.bengans@gmail.com");
            receipt.Add("Org. Nr.: 234567-8901\n");
            receipt.Add($"{currentDateTime.ToString("s").Replace("T", " ")}");
            receipt.Add($"Seller: {"Bengan"}");
            receipt.Add($"Receipt Nr.: {epochTime}");
            receipt.Add("-----------------------------------------------------\n");


            /*
                Product information and price
             */
            foreach (Product product in cart.GetCart())
            {
                receipt.Add("\n-----------------------------------------------------\n");
                receipt.Add("\t1x " + product.name + " " + product.GetStringPrice() + " (with " + product.tax * 100 + "% tax)");  ;
            }
            receipt.Add("\n-----------------------------------------------------\n");
            

            /*
               Vat basis and total
             */
            receipt.Add("-----------------------------------------------------\n");
            receipt.Add("VAT basis:");
            receipt.Add($"VAT 25%\t{vat25.ToString("0.00")} SEK");
            receipt.Add($"VAT 12%\t{vat12.ToString("0.00")} SEK");
            receipt.Add($"No VAT\t{vat0.ToString("0.00")} SEK\n");
            receipt.Add($"VAT total\t{netPrice} SEK\n");
            receipt.Add("-----------------------------------------------------\n");
            receipt.Add($"Total:\t\t{cart.GetTotalPrice()} SEK\n");
            receipt.Add("-----------------------------------------------------\n");


            // Write Receipt to file
            SaveReceiptToFile();
        }

        void SaveReceiptToFile()
        {
            // Open ReadWrite Stream
            using (StreamWriter sw = File.CreateText(fullpath))
            {
                // Loop over recipet stringparts and write to file
                foreach (string stringPart in receipt)
                {
                    sw.WriteLine(stringPart);
                }

                // Close Steam
                sw.Close();
            }
        }
    }
}
