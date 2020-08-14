using Autofac;
using CleanXF.Mobile;
using CleanXF.Mobile.ViewModels;
using ShellLogin.ViewModels;
using Xamarin.Forms;

namespace CleanXF.Mobile.Views
{
    public class LoadingPage : ContentPage
    {
        public LoadingPage()
        {           
            Content = new StackLayout
            {               
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.Center,
                Children = { new ActivityIndicator { IsRunning = true }, new Label { Text = "Starting up..." } }
            };
        }
             
        private readonly LoadingViewModel _viewModel = App.Container.Resolve<LoadingViewModel>();

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.Init();
        }
    }
}


/*    <ContentPage.Content>
        <StackLayout VerticalOptions="Center" HorizontalOptions="Center">
            <ActivityIndicator Color="Accent"
                               IsRunning="True"
                               IsVisible="True" />
            <Label Text="Loading ..." />
        </StackLayout>
    </ContentPage.Content>
*/