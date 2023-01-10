// See https://aka.ms/new-console-template for more information
class Cart
{
    public List<Product> items;

    decimal getTotalPrice() { return 0; }
    void addProduct(int productID) 
    {
        
    }
    void removeProduct(int productID) { }
    void editProduct(int productID) { }

    void pay() { }
    void clearCart()
    {
        this.items = new List<Product>();
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

    decimal getPrice()
    {
        // Do Stuff
        return this.price * (1+this.tax);
    }

    decimal getPriceNoTax()
    {
        // Doo more stuff without tax
        return this.price;
    }
}

class Menu
{
    public List<Product> items = new List<Product>();

}

class Program
{
    static int Main(string[] args)
    {
        Menu menu = new Menu();
        
        for (int i = 1; i < 11; i++)
        {
            Product new_product = new Product(i, $"Product {i}", $"Product {i} description", i * 10, 0.25m);
            menu.items.Add(new_product);
        }

        foreach(Product p in menu.items)
        {
            Console.WriteLine(p.name);
        }

        return 0;
    }
}