// See https://aka.ms/new-console-template for more information
using System.Diagnostics.CodeAnalysis;

class Cart
{
    public List<Product> items;

    decimal getTotalPrice()
    {
        decimal total = 0;
        foreach (Product product in items)
        {
            total += product.getPrice();
        }

        return total;

    }
    void addProduct(int productID, Menu productMenu)
    {
        //if (productID.ToString() in productMenu.menuItems)

        foreach (Product product in productMenu.items)
        {
            if (productID == product.id)
            {
                this.items.Add(productMenu.menuItems[productID.ToString()]);
                Console.WriteLine("Added " + product.id + " " + product.name);
                break;
            }
        }
    }

    void removeProduct(int productID) 
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
    void editProduct(int productID, Product newProduct) 
    {

    }

    bool pay()
    {
        /*
            Returns if the payment went through or not 
        */

        bool isPaid = true;

        this.clearCart();

        return isPaid;
    }

    void clearCart()
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

    decimal getPriceNoTax()
    {
        // Do more stuff without tax
        return this.price;
    }

    string getStringPrice()
    {
        return string.Format("{0} SEK", getPrice());
    }

}

class Menu
{
    public List<Product> items = new List<Product>();

    public Dictionary<String, Product> menuItems = new Dictionary<string, Product>();

}

class Program
{
    static int Main(string[] args)
    {
        Menu menu = new Menu();

        for (int i = 1; i < 11; i++)
        {
            Product new_product = new Product(i, $"Product {i}", $"Product {i} description", i * 10, 0.25m);
            //menu.items.Add(new_product);
            menu.menuItems.Add(i, new_product);
        }

        foreach (Product p in menu.items)
        {
            Console.WriteLine(p.name);
        }

        return 0;
    }
}