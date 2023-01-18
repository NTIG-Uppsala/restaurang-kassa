namespace Restaurant_pos_program
{
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

}
