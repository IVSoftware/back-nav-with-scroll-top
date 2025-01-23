using System.Diagnostics;

namespace back_nav_with_scroll_top
{
    public partial class PageAView : ContentPage
    {
        public PageAView() => InitializeComponent();
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
        private async void OnFwdNavClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("///PageB");
        }
    }
}
