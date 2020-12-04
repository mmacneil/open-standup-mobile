using Xamarin.Forms;

namespace OpenStandup.Mobile.Interfaces
{
    public interface IIndicatorPageService
    {
        void InitIndicatorPage(ContentPage indicatorPage);

        void ShowIndicatorPage();

        void HideIndicatorPage();
    }
}
