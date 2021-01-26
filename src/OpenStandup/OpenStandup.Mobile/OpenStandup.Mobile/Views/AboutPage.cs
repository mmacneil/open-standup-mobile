using Autofac;
using OpenStandup.Core.Interfaces;
using OpenStandup.Mobile.Helpers;
using Xamarin.CommunityToolkit.Effects;
using Xamarin.Essentials;
using Xamarin.Forms;


namespace OpenStandup.Mobile.Views
{
    public class AboutPage : BaseModalPage
    {
        private readonly IVersionInfo _versionInfo = App.Container.Resolve<IVersionInfo>();

        public AboutPage()
        {
            var gitHubLink = new Label
            {
                Style = ResourceDictionaryHelper.GetStyle("AboutIcon")
            };

            TouchEffect.SetNativeAnimation(gitHubLink, true);

            TouchEffect.SetCommand(gitHubLink, new Command(async () =>
            {
                await Launcher.OpenAsync("https://github.com/mmacneil/open-standup");
            }));

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
                                    FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label)),
                                    HorizontalTextAlignment = TextAlignment.Center,
                                    Text = $"Version {_versionInfo.CurrentVersion}",
                                    TextColor = ResourceDictionaryHelper.GetColor("Text")
                                },

                                gitHubLink
                            },
                            Margin = new Thickness(0, 15, 0, 0),
                            Spacing = 16
                        }
                    }
                }
            };
        }
    }
}
