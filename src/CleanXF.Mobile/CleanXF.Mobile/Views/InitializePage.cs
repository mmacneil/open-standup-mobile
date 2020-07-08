using System.Collections.Generic;
using Xamarin.Forms;

namespace CleanXF.Mobile.Views
{
    public class InitializePage : ContentPage
    {
        public InitializePage()
        {
            Shell.SetNavBarIsVisible(this, false);

            var rootLayout = new StackLayout
            {
               VerticalOptions = LayoutOptions.Center                            
            };               

            rootLayout.Children.Add(new ActivityIndicator { IsRunning = true  });
            rootLayout.Children.Add(new Label { HorizontalTextAlignment = TextAlignment.Center, Text = "Starting up..." });

            Content = rootLayout;
        }
    }
}


/*<?xml version="1.0" encoding="utf-8" ?>
<ContentPage xmlns="http://xamarin.com/schemas/2014/forms"
             xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
             xmlns:d="http://xamarin.com/schemas/2014/forms/design"
             xmlns:mc="http://schemas.openxmlformats.org/markup-compatibility/2006"
             mc:Ignorable="d"
             Shell.NavBarIsVisible="False"
             x:Class="ShellLogin.Views.LoadingPage">
    <ContentPage.Content>
        <StackLayout VerticalOptions="Center" HorizontalOptions="Center">
            <ActivityIndicator Color="Accent"
                               IsRunning="True"
                               IsVisible="True" />
            <Label Text="Loading ..." />
        </StackLayout>
    </ContentPage.Content>
</ContentPage>
*/