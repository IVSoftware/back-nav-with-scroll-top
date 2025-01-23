namespace back_nav_with_scroll_top;

public partial class PageBView : ContentPage
{
	public PageBView()
	{
		InitializeComponent();
	}

    private async void OnBackButtonClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("///PageA");
    }
    protected override bool OnBackButtonPressed()
    {
        return base.OnBackButtonPressed();
    }
}