
using OpenStandup.Mobile.Helpers;
using Xamarin.Forms;

namespace OpenStandup.Mobile.Views
{
    public class AboutPage : BaseModalPage
    {
        public AboutPage()
        {
            Content = new Frame
            {
                Style = ResourceDictionaryHelper.GetStyle("ModalFrame"),
                Content = new StackLayout
                {
                    Children = { new Label {Text = "Version"}}
                }
            };
        }
    }
}
