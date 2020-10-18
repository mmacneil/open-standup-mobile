using Xamarin.Forms;

namespace OpenStandup.Mobile.Views
{
    public class MainPage : ContentPage
    {
        public MainPage()
        {
            Title = "My News Feed";
            Content = new StackLayout
            {
                Children = { 
                    new Label { Text = "Welcome", VerticalOptions = LayoutOptions.CenterAndExpand, HorizontalOptions = LayoutOptions.CenterAndExpand }}
            };
        }
    }
}

