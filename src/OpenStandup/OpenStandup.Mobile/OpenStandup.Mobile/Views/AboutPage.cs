using Autofac;
using OpenStandup.Core.Interfaces;
using OpenStandup.Mobile.Controls;
using OpenStandup.Mobile.Helpers;
using Xamarin.Forms;


namespace OpenStandup.Mobile.Views
{
    public class AboutPage : BaseModalPage
    {
        private readonly IVersionInfo _versionInfo = App.Container.Resolve<IVersionInfo>();

        public AboutPage()
        {
            Content = new Frame
            {
                Style = ResourceDictionaryHelper.GetStyle("ModalFrame"),
                Content = new StackLayout
                {
                    Children =
                    {
                        new Image {HeightRequest = 150, Source = "logo_black.png"},
                        new StackLayout
                        {
                            Children =
                            {
                                new Label 
                                {
                                    FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
                                    HorizontalTextAlignment = TextAlignment.Center, 
                                    Text = $"Version {_versionInfo.CurrentVersion}",
                                    TextColor = ResourceDictionaryHelper.GetColor("Text")
                                },
                                new IconItem(IconFont.Github) {Text = "https://github.com/mmacneil/open-standup"}
                            },
                            Margin = new Thickness(0, 15, 0, 0)
                        }
                    }
                }
            };
        }
    }
}
