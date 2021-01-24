using Autofac;
using OpenStandup.Mobile.Factories;
using OpenStandup.Mobile.Helpers;
using OpenStandup.Mobile.ViewModels;
using Rg.Plugins.Popup.Contracts;
using Rg.Plugins.Popup.Pages;
using Xamarin.CommunityToolkit.Effects;
using Xamarin.Forms;

namespace OpenStandup.Mobile.Controls
{
    public class UserMetaLayout : StackLayout
    {
        private readonly IPageFactory _pageFactory = App.Container.Resolve<IPageFactory>();
        private readonly IPopupNavigation _popupNavigation = App.Container.Resolve<IPopupNavigation>();

        public static BindableProperty AvatarSourceProperty = BindableProperty.Create(
            nameof(AvatarSource),
            typeof(string),
            typeof(UserMetaLayout), null, BindingMode.Default, null, AvatarSourceChanged);

        public static BindableProperty GitHubIdProperty = BindableProperty.Create(
            nameof(GitHubId),
            typeof(string),
            typeof(UserMetaLayout), null, BindingMode.Default);

        public static BindableProperty LoginProperty = BindableProperty.Create(
            nameof(Login),
            typeof(string),
            typeof(UserMetaLayout), null, BindingMode.Default, null, LoginChanged);

        public static BindableProperty ModifiedProperty = BindableProperty.Create(
            nameof(Modified),
            typeof(string),
            typeof(UserMetaLayout), null, BindingMode.Default, null, ModifiedChanged);

        public string AvatarSource
        {
            get => (string)GetValue(AvatarSourceProperty);
            set => SetValue(AvatarSourceProperty, value);
        }

        public string GitHubId
        {
            get => (string)GetValue(GitHubIdProperty);
            set => SetValue(GitHubIdProperty, value);
        }

        public string Login
        {
            get => (string)GetValue(LoginProperty);
            set => SetValue(LoginProperty, value);
        }

        public string Modified
        {
            get => (string)GetValue(ModifiedProperty);
            set => SetValue(ModifiedProperty, value);
        }

        private static void AvatarSourceChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (!(bindable is UserMetaLayout @this)) return;

            if (newValue is string source)
            {
                @this._avatar.Source = source;
            }
        }

        private static void LoginChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (!(bindable is UserMetaLayout @this)) return;

            if (newValue is string text)
            {
                @this._loginLabel.Text = text;
            }
        }

        private static void ModifiedChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (!(bindable is UserMetaLayout @this)) return;

            if (newValue is string text)
            {
                @this._modifiedLabel.Text = text;
            }
        }

        private readonly Image _avatar = new Image { Aspect = Aspect.AspectFill };
        private readonly Label _loginLabel = new Label { Style = ResourceDictionaryHelper.GetStyle("LinkLabel"), VerticalOptions = LayoutOptions.Center };
        private readonly Label _modifiedLabel = new Label { Style = ResourceDictionaryHelper.GetStyle("MetaLabel"), VerticalOptions = LayoutOptions.Center };

        public UserMetaLayout(int avatarHeight = 35, int avatarWidth = 35, int avatarCornerRadius = 20)
        {
            TouchEffect.SetNativeAnimation(_loginLabel, true);
            TouchEffect.SetCommand(_loginLabel, new Command(async () =>
            {
                await _popupNavigation.PushAsync(_pageFactory.Resolve<ProfileViewModel>(vm =>
                {
                    vm.SelectedGitHubId = GitHubId;
                    vm.SelectedLogin = _loginLabel.Text;
                }) as PopupPage);
            }));

            Children.Add(new RoundImage(_avatar, avatarHeight, avatarWidth, avatarCornerRadius));
            Children.Add(_loginLabel);
            Children.Add(_modifiedLabel);
            HorizontalOptions = LayoutOptions.Start;
            Orientation = StackOrientation.Horizontal;
            Padding = new Thickness(13, 0);
        }
    }
}