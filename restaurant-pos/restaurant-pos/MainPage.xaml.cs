using Microsoft.Maui.Controls;
using System.Diagnostics;
using System.Diagnostics.Metrics;
using System.Reflection;
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
            Debug.WriteLine("-------------");
            AddItemTest();
            PricingTest();
            PayItemsTest();
            Debug.WriteLine("-------------");
        }

    }

    // Function to add an item

    public void AddItem(object sender, EventArgs e)
    {

        var items = new Dictionary<string, (string, int)>(){
            {"Kaffe", ("Kaffe", 40)},
            {"Bulle", ("Bulle", 20)},
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

    // Test to add all items
    public void AddItemTest()
    {

        var items = new Dictionary<string, (string, int)>(){
            {"Kaffe", ("Kaffe", 40)},
            {"Bulle", ("Bulle", 20)},
        };

        int counter = 1;

        foreach (var item in items)
        {
            Button b = this.FindByName<Button>($"CounterBtn{counter}");
            b.SendClicked();
            counter += 1;    
        }

        var itemLength = purchasedItems.Children.Count;

        if (itemLength == items.Count)
        {
            Debug.WriteLine("Add Item Test: Passed");
        }
        else
        {
            throw new Exception("Add Item Test failed");
        }
    }

    // Test to check if price is correct
    public void PricingTest()
    {
        var items = new Dictionary<string, (string, int)>(){
            {"Kaffe", ("Kaffe", 40)},
            {"Bulle", ("Bulle", 20)},
        };

        var price = 0;

        foreach (var item in items)
        {
            price += item.Value.Item2;
        }

        if (totalPrice == price)
        {
            Debug.WriteLine("Pricing Test: Passed");
        }
        else
        {
            throw new Exception("Pricing Test failed");
        }
    }

    // Test to check if pay function works

    public void PayItemsTest()
    {
        Button b = this.FindByName<Button>("payButton");
        b.SendClicked();

        if (totalPrice == 0 && purchasedItems.Children.Count == 0)
        {
            Debug.WriteLine("Pay Items Test: Passed");
        }
        else
        {
            throw new Exception("Pricing Test failed");
        }

    }

}
