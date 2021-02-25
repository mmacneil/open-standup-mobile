using Autofac;
using OpenStandup.Mobile.Helpers;
using Rg.Plugins.Popup.Contracts;
using Xamarin.CommunityToolkit.Effects;
using Xamarin.Forms;

namespace OpenStandup.Mobile.Views
{
    public class FormattingHelpPage : BaseModalPage
    {
        private readonly IPopupNavigation _popupNavigation = App.Container.Resolve<IPopupNavigation>();

        public FormattingHelpPage()
        {
            var close = new Label
            {
                Text = "Close",
                Style = ResourceDictionaryHelper.GetStyle("CloseLabel"),
                Margin = new Thickness(0, 15, 0, 0)
            };

            TouchEffect.SetCommand(close, new Command(async () =>
            {
                await _popupNavigation.PopAsync();
            }));

            Content = new Frame
            {
                Style = ResourceDictionaryHelper.GetStyle("ModalFrame"),
                VerticalOptions = LayoutOptions.Center,
                Content = new StackLayout
                {
                    Children =
                    {
                        new Label { Style = ResourceDictionaryHelper.GetStyle("Title"), Text = "Tips"},
                        new Label { Style = ResourceDictionaryHelper.GetStyle("ContentLabel"), Text = "• Your public repo names will be converted to hyperlinks in your post"},
                        new Label { Style = ResourceDictionaryHelper.GetStyle("ContentLabel"), Text = "• Markdown links are supported i.e.\n[link text](https://site.com)" },
                        close
                    },
                    Spacing = 12,
                    Padding = new Thickness(10, 25)
                }
            };
        }
    }
}
