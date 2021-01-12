using OpenStandup.Mobile.Controls;
using OpenStandup.Mobile.Helpers;
using OpenStandup.Mobile.ViewModels;
using Xamarin.Forms;

namespace OpenStandup.Mobile.Views
{
    public class ModalProfilePage : BaseModalPage
    {
        private ProfileViewModel _viewModel;

        private readonly ProfileLayout _profileLayout;

        public ModalProfilePage()
        {
            _profileLayout = new ProfileLayout
            {
                Margin = new Thickness(0, 20, 0, 0)
            };

            Content = new Frame
            {
                Style = ResourceDictionaryHelper.GetStyle("ModalFrame"),
                Content = _profileLayout
            };
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            _viewModel = (ProfileViewModel)BindingContext;
            await _viewModel.Initialize();
            _profileLayout.BindingContext = _viewModel;
            _profileLayout.BindStats(_viewModel.StatModels);
        }
    }
}
