namespace restaurant_pos;

public partial class MainPage : ContentPage
{
	public int count = 0;
    public List<string> itemList = new List<string>();

    public MainPage()
	{
		InitializeComponent();
	}

	private void OnCounterClicked(object sender, EventArgs e)
	{
		count++;
        
		if (count == 1)
			CounterBtn.Text = $"Clicked {count} time";
		else
			CounterBtn.Text = $"Clicked {count} times";

		SemanticScreenReader.Announce(CounterBtn.Text);
	}
	public void AddItem()
	{ 
		
	}
}

