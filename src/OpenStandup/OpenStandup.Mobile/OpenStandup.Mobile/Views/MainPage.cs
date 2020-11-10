using System;
using Xamarin.Forms;

namespace OpenStandup.Mobile.Views
{
    public class MainPage : ContentPage
    {
        public MainPage()
        {
            Title = "My News Feed";

            var layout = new AbsoluteLayout();
            var scrollView = new ScrollView();
            AbsoluteLayout.SetLayoutBounds(scrollView, new Rectangle(0, 0, 1, 1));
            AbsoluteLayout.SetLayoutFlags(scrollView, AbsoluteLayoutFlags.SizeProportional);

            var stackLayout = new StackLayout
            {
                Children =
                {
                    new Label
                    {
                        BackgroundColor = Color.LightGray,
                        FontSize = 38,
                        HorizontalOptions = LayoutOptions.Fill,
                        HorizontalTextAlignment = TextAlignment.Center,
                        Text = "Welcome to OpenStandup!",
                        VerticalOptions = LayoutOptions.Start
                    },
                    new Label
                    {
                        BackgroundColor = Color.LightPink,
                        FontSize = 38,
                        HorizontalOptions = LayoutOptions.Fill,
                        HorizontalTextAlignment = TextAlignment.Center,
                        Text = "Welcome to OpenStandup!",
                        VerticalOptions = LayoutOptions.Start
                    },
                    new Label
                    {
                        BackgroundColor = Color.LightCyan,
                        FontSize = 38,
                        HorizontalOptions = LayoutOptions.Fill,
                        HorizontalTextAlignment = TextAlignment.Center,
                        Text = "Welcome to OpenStandup!",
                        VerticalOptions = LayoutOptions.Start
                    },
                    new Label
                    {
                        BackgroundColor = Color.LightGreen,
                        FontSize = 38,
                        HorizontalOptions = LayoutOptions.Fill,
                        HorizontalTextAlignment = TextAlignment.Center,
                        Text = "Welcome to OpenStandup!",
                        VerticalOptions = LayoutOptions.Start
                    },
                    new Label
                    {
                        BackgroundColor = Color.LightCyan,
                        FontSize = 38,
                        HorizontalOptions = LayoutOptions.Fill,
                        HorizontalTextAlignment = TextAlignment.Center,
                        Text = "Welcome to OpenStandup!",
                        VerticalOptions = LayoutOptions.Start
                    },
                    new Label
                    {
                        BackgroundColor = Color.LightGreen,
                        FontSize = 38,
                        HorizontalOptions = LayoutOptions.Fill,
                        HorizontalTextAlignment = TextAlignment.Center,
                        Text = "Welcome to OpenStandup!",
                        VerticalOptions = LayoutOptions.Start
                    },
                    new Label
                    {
                        BackgroundColor = Color.LightCyan,
                        FontSize = 38,
                        HorizontalOptions = LayoutOptions.Fill,
                        HorizontalTextAlignment = TextAlignment.Center,
                        Text = "Welcome to OpenStandup!",
                        VerticalOptions = LayoutOptions.Start
                    },
                    new Label
                    {
                        BackgroundColor = Color.LightGreen,
                        FontSize = 38,
                        HorizontalOptions = LayoutOptions.Fill,
                        HorizontalTextAlignment = TextAlignment.Center,
                        Text = "Welcome to OpenStandup!",
                        VerticalOptions = LayoutOptions.Start
                    },
                    new Label
                    {
                        BackgroundColor = Color.LightCyan,
                        FontSize = 38,
                        HorizontalOptions = LayoutOptions.Fill,
                        HorizontalTextAlignment = TextAlignment.Center,
                        Text = "Welcome to OpenStandup!",
                        VerticalOptions = LayoutOptions.Start
                    },
                    new Label
                    {
                        BackgroundColor = Color.LightGreen,
                        FontSize = 38,
                        HorizontalOptions = LayoutOptions.Fill,
                        HorizontalTextAlignment = TextAlignment.Center,
                        Text = "Welcome to OpenStandup!",
                        VerticalOptions = LayoutOptions.Start
                    }
                }
            };

            scrollView.Content = stackLayout;

            var fab = new ImageButton
            {
                Style = (Style)Application.Current.Resources["FloatingActionButton"],
                Source = "ic_edit.png"
            };

            fab.Clicked += OnImageButtonClicked;

            AbsoluteLayout.SetLayoutBounds(fab, new Rectangle(.95, .95, 55, 55));
            AbsoluteLayout.SetLayoutFlags(fab, AbsoluteLayoutFlags.PositionProportional);

            layout.Children.Add(scrollView);
            layout.Children.Add(fab);

            Content = layout;
        }

        private async void OnImageButtonClicked(object sender, EventArgs e)
        {
            // Page appearance not animated
            await Navigation.PushAsync(new EditPostPage());
        }
    }
}

