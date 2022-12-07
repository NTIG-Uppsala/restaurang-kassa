using System.Diagnostics;

namespace restaurant_pos;

public partial class MainPage : ContentPage
{
    public int count = 0;
    public List<string> itemList = new List<string>();

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
		parent.Add(newItem);
    }
}

