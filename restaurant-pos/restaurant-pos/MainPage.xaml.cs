using Microsoft.Maui.Controls;
using System.Diagnostics;
using System.Xml;

namespace restaurant_pos;

public partial class MainPage : ContentPage
{
    public int count = 0;
    public float totalPrice = 0;
    public List<string> itemList = new List<string>();

    public MainPage()
	{
		InitializeComponent();
	}

	public void AddItem(object sender, EventArgs e)
	{
        var newItem = new Label
        {
            Text = $"{(sender as Button).Text}: {(sender as Button).ClassId}kr",
            FontSize = 32,
            TextColor = Colors.White,
            Margin = 10
        };

        var newItemPrice = new Label
        {
            Text = (sender as Button).ClassId,
            FontSize = 32,
            TextColor = Colors.White,
        };

        purchasedItems.Children.Add(newItem);

        float itemPrice = float.Parse(newItemPrice.Text);
        newItemPrice.Text += "kr";
        totalPrice += itemPrice;
        Price.Text = $"{totalPrice}kr";

        count += 1;
        itemList.Add(newItem.Text);
    }

    public void payItems(object sender, EventArgs e)
    {
        purchasedItems.Children.Clear();
        totalPrice = 0;
        Price.Text = $"{totalPrice}kr";
    }

}

