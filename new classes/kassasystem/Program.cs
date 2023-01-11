// See https://aka.ms/new-console-template for more information


using System.Xml;

class Cart
{
    public List<Product> items;

    public decimal getTotalPrice()
    {
        decimal total = 0;
        foreach (Product product in items)
        {
            total += product.getPrice();
        }

        return total;

    }
    public void addProduct(int productID, Menu productMenu)
    {
        //if (productID.ToString() in productMenu.menuItems)

        foreach (Product entry in productMenu.menuItems)
        {
            if (productID == entry.id)
            {
                this.items.Add(entry);
                Console.WriteLine("Added " + entry.id + " " + entry.name);
                break;
            }
        }
    }

    public void removeProduct(int productID) 
    {
        foreach (Product product in this.items)
        {

           if (productID == product.id)
           {
                this.items.Remove(product);
                Console.WriteLine("Removed " + product.id + " " + product.name);
                break;
           }
        }
    }

    public bool pay()
    {
        /*
            Returns if the payment went through or not 
        */

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
        // Do Stuff
        return this.price * (1 + this.tax);
    }

    public decimal getPriceNoTax()
    {
        // Do more stuff without tax
        return this.price;
    }

    public string getStringPrice()
    {
        return string.Format("{0} SEK", getPrice());
    }

}

class Menu
{
    public List<Product> menuItems = new List<Product>();

    public void addProduct(string name, string description, decimal price, decimal tax)
    {
        int id = 0;
        if (menuItems.Count > 0) 
        { 
            id = menuItems.Last().id+1; 
        }
        
        Product new_product = new Product(id, $"Product {name}", $"Product {description} description",  price, 0.25m);
        menuItems.Add(new_product);
    }

    //public void removeProduct(int id) 
    //{
    //    menuItems.Remove(id);
    //}
}

class Program
{
    static int Main(string[] args)
    {
        Menu menu = new Menu();

        menu.addProduct("Bun", "Soft and nice", 15, 0.12m);
        menu.addProduct("Coffee", "Black", 25, 0.12m);

        foreach (Product entry in menu.menuItems)
        {
            Console.Write(entry.name + ", " + entry.getPrice() + ", ID: " + entry.id + "\n");
        }

        return 0;
    }
}