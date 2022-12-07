using System.Diagnostics;

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
        var flexLayout = new FlexLayout
        {

        };

		var newItem = new Label
		{
			Text = (sender as Button).Text
		};

        var newItemPrice = new Label
        {
            Text = (sender as Button).ClassId
        };

        flexLayout.Children.Add(newItem);
        flexLayout.Children.Add(newItemPrice);

        float itemPrice = float.Parse(newItemPrice.Text);
        newItemPrice.Text += "kr";
        totalPrice += itemPrice;
        Price.Text = $"{totalPrice}kr";

        count += 1;
        itemList.Add(newItem.Text);
    }
}

