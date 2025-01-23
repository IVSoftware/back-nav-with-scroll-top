Doing this is a bit tricky in terms of timing, because the layout needs to be fully calculated before you can do something like `scrollView.ScrollToAsync(0, 0, false)`. But by triggering on the `NavigatedTo` event and then using scheduling the action at the end of the existing message queue, you should see reliable timing. A fallback method, involving a "magic delay" of a few milliseconds, is also shown but we try and avoid this kind of thing when we can.

I'll post in the comments the full code I used to test this answer and you can make sure it works on your end.

~~~
public partial class PageAView : ContentPage
{
    public PageAView() => InitializeComponent();

    private async void OnFwdNavClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("///PageB");
    }

    protected async override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);
#if true
        Dispatcher.Dispatch(async () =>
        {
            await scrollView.ScrollToAsync(0, 0, false);
        });
#else
        // AVOID "MAGIC DELAYS" WHEN YOU CAN
        await Task.Delay(1);
        await scrollView.ScrollToAsync(0, 0, false);
#endif
    }
}
~~~
___

A couple of suggestions.

- First, having a UI dependency in the ViewModel is not strictly speaking an MVVM approach. `Shell.Current.GoToAsync(...)` is something that actually belongs in the code-behind, not the VM.

- Second, since a platform like Android is going to have a system "Back Navigation" button, you should get it involved and make it do what you want it to do.


~~~
public partial class PageBView : ContentPage
{
	public PageBView() => InitializeComponent();

    private async void OnBackButtonClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("///PageA");
    }

    protected override bool OnBackButtonPressed()
    {
        _ = Shell.Current.GoToAsync("///PageA");
        return true;
    }
}
~~~

[![system back nav][1]][1]


  [1]: https://i.sstatic.net/C2qq3wrk.png