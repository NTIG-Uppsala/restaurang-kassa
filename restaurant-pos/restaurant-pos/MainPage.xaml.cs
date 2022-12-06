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

	public void AddItem(object item, EventArgs e)
	{
		var finfintNamn = new Label
		{
			Text = item.ToString()
		};

		parent.Add(finfintNamn);

		itemList.Add("agga");
		Debug.WriteLine("agga");
	}
}

