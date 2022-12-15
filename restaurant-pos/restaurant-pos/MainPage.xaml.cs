namespace restaurant_pos;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
    }
    
    public float totalPrice = 0;
    public void AddItem(string item, float price, bool testing)
    {
        totalPrice += price;
        var newItem = new Label
        {
            Text = $"{item}: {price}kr",
            FontSize = 32,
            TextColor = Colors.White,
            Margin = 10
        };
        purchasedItems.Children.Add(newItem);
        Price.Text = $"{totalPrice}kr";
    }
    public void Pay(object sender, EventArgs e)
    {
        totalPrice = 0;
        Price.Text = $"{totalPrice}kr";
        purchasedItems.Children.Clear();
    }

    public void AddCoffee(object sender, EventArgs e)
    {
        // Price is up for discussion
        AddItem("Coffee", 15, false);
    }
    public void AddBun(object sender, EventArgs e)
    {
        // Price is up for discussion
        AddItem("Bun", 30, false);
    }
}