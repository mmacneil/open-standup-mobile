using Xamarin.Forms;

namespace CleanXF.Mobile.Services
{
    public class Navigator : INavigator
    {
        private readonly Application _app;
        public Navigator(Application app)
        {
            _app = app;
        }

        public void LoadShell()
        {
            _app.MainPage = new AppShell();
        }
    }
}
