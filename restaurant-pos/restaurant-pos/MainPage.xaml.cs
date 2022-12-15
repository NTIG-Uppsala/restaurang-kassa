using Microsoft.Maui.Controls;
using System.Diagnostics;
using System.Xml;

namespace restaurant_pos;

public partial class MainPage : ContentPage
{

    public float totalPrice = 0;

    public bool testing = false;
    public MainPage()
    {
        InitializeComponent();

        if (testing)
        {
            AddItemTest();
        }

    }

    // Function to add an item

    public void AddItem(object sender, EventArgs e)
    {

        var items = new Dictionary<string, (string, int)>(){
            {"Kaffe", ("Kaffe", 40)},
            {"Bulle", ("Bulle", 20)},
            {"Apelsinjuice", ("Apelsinjuice", 15)}
        };

        var clickedButton = sender as Button;
        var purchasedItem = items[clickedButton.ClassId].Item1;
        var itemPrice = items[clickedButton.ClassId].Item2;

        var newItem = new Label
        {
            Text = $"{purchasedItem}: {itemPrice}",
            FontSize = 32,
            TextColor = Colors.White,
            Margin = 10
        };

        purchasedItems.Children.Add(newItem);
        totalPrice += items[clickedButton.ClassId].Item2;
        Price.Text = $"{totalPrice}kr";
    }

    // Function to clear all items

    public void payItems(object sender, EventArgs e)
    {
        purchasedItems.Children.Clear();
        totalPrice = 0;
        Price.Text = $"{totalPrice}kr";
    }

    // Tests
    // Test to add item

    public void AddItemTest()
    {
        var itemLength = purchasedItems.Children.Count;

        Button b = this.FindByName<Button>("CounterBtn");
        b.SendClicked();

        if (purchasedItems.Count > itemLength)
        {
            Debug.WriteLine("Pass");
        }
        else
        {
            Debug.WriteLine("Fail");
        }

    }
}
