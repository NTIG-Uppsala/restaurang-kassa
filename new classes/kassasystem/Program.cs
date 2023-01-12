﻿namespace kassasystem
{
    class Cart
    {
        public List<Product> items = new List<Product>();

        public decimal getTotalPrice()
        {
            /*
                Sums up the price of all products and returns the total 
            */

            decimal total = 0;
            foreach (Product product in items)
            {
                total += product.getPrice();
            }

            return total;

        }

        public void addProduct(int productID, Menu productMenu)
        {
            /*
                Method to add a product from the menu to the cart
            */

            // Loop over the menu
            foreach (Product entry in productMenu.menuItems)
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
            /*
                Removes a product from the cart     
            */

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
            /*
                Returns if the payment went through or not 
            */

            // In the future this could call a possible processPayment method

            bool isPaid = true;
            this.clearCart();

            return isPaid;
        }

        public void clearCart()
        {
            this.items.Clear();
        }
    }

    class Product
    {
        public int id;
        public string name;
        public string description;
        public decimal price;
        public decimal tax;

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
            /*
                Returns a string with the correct format of the price eg 250.00 SEK
            */
            return string.Format("{0} SEK", getPrice());
        }

    }

    class Menu
    {
        public List<Product> menuItems = new List<Product>();

        public void addProduct(string name, string description, decimal price, decimal tax)
        {
            /*
                Add a product to the menu 
            */

            // Gets id of last item in menu if menu has items and adds 1 to it. 
            // If the menu has no items, just set the id to 0
            int last_avaliable_product_id = 0;
            if (menuItems.Count > 0)
            {
                int last_product_id = menuItems.Last().id;
                last_avaliable_product_id = last_product_id + 1;
            }

            Product new_product = new Product(last_avaliable_product_id, $"Product {name}", $"Product {description} description", price, tax);
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
    }
}