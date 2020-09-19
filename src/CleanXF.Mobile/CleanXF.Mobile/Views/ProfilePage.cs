using Autofac;
using CleanXF.Mobile.ViewModels;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.NetworkInformation;
using CleanXF.Mobile.Controls;
using CleanXF.Mobile.Models;
using Xamarin.Forms;


namespace CleanXF.Mobile.Views
{
    public class ProfilePage : ContentPage
    {
        private readonly ProfileViewModel _viewModel = App.Container.Resolve<ProfileViewModel>();

        private FlexLayout _statsLayout = new FlexLayout();

        public ProfilePage()
        {
            Padding = new Thickness(5, 20);

            BindingContext = _viewModel;

            Title = "My Profile";

            var header = new Grid
            {
                ColumnDefinitions =
                {
                    new ColumnDefinition { Width = new GridLength(30, GridUnitType.Star) },
                    new ColumnDefinition { Width = new GridLength(70, GridUnitType.Star) },
                }
            };

            var image = new Image
            {
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center
            };

            var imageLayout = new StackLayout
            {
                Children =
                {
                    new Frame
                    {
                        CornerRadius = 50,
                        HeightRequest = 100,
                        WidthRequest = 100,
                        Padding = 0,
                        HorizontalOptions = LayoutOptions.Center,
                        IsClippedToBounds = true,
                        Content = image
                    }
                }
            };

            var login = new Label { Style = (Style)Application.Current.Resources["Title"] };
            login.SetBinding(Label.TextProperty, nameof(ProfileViewModel.Login));

            var location = new Label { Style = (Style)Application.Current.Resources["SubTitle"] };
            location.SetBinding(Label.TextProperty, nameof(ProfileViewModel.Location));

            var joined = new Label { Style = (Style)Application.Current.Resources["MicroSubTitle"] };
            joined.SetBinding(Label.TextProperty, nameof(ProfileViewModel.Joined));

            header.Children.Add(imageLayout);
            header.Children.Add(new StackLayout
            {
                Children =
                {
                    login,
                    location,
                    joined
                }
            }, 1, 0);

            _statsLayout = new FlexLayout
            {
                Wrap = FlexWrap.Wrap,
                JustifyContent = FlexJustify.SpaceAround
            };

            BindableLayout.SetItemTemplate(_statsLayout, new DataTemplate(() => new StatCell()));

            image.SetBinding(Image.SourceProperty, nameof(ProfileViewModel.AvatarUrl));

            var rootLayout = new StackLayout
            {
                Children =
                {
                    header,
                    new BoxView { HorizontalOptions = LayoutOptions.FillAndExpand, HeightRequest = 1, Color = Color.FromHex("#1690F4") },
                    _statsLayout
                }
            };

            Content = rootLayout;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await _viewModel.Initialize(); //.ConfigureAwait(false);
            BindableLayout.SetItemsSource(_statsLayout, _viewModel.StatModels); // Do standard property bindings or ObservableCollections work with SetItemSource? Seems the data source must be hydrated before calling this to render the data
        }
    }
}

