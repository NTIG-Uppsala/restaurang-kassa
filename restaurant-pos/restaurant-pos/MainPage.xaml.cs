using System.Diagnostics;

namespace restaurant_pos;

public partial class MainPage : ContentPage
{
    public float count = 0;
    public MainPage()
	{
		InitializeComponent();
	}

	public void AddItem(object sender, EventArgs e)
	{
		var newItem = new Label
		{
			Text = (sender as Button).Text
		};

        var newItem2 = new Label
        {
            Text = (sender as Button).ClassId
        };

        parent.Add(newItem);
        parent.Add(newItem2);


        float priceVariable = float.Parse(newItem2.Text);
        newItem2.Text += "kr";
        count += priceVariable;
        Price.Text = $"{count}kr";

    }
}

