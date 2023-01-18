namespace Restaurant_pos_program
{
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

}
