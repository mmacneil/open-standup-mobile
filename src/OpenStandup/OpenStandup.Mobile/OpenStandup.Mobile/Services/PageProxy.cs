using System;
using System.Threading.Tasks;
using OpenStandup.Mobile.Interfaces;
using Xamarin.Forms;

namespace OpenStandup.Mobile.Services
{
    public class PageProxy : IDialogProvider
    {
        private readonly Func<Page> _pageResolver;

        public PageProxy(Func<Page> pageResolver)
        {
            _pageResolver = pageResolver;
        }

        public async Task DisplayAlert(string title, string message, string cancel)
        {
            await _pageResolver().DisplayAlert(title, message, cancel);
        }

        public async Task<bool> DisplayAlert(string title, string message, string accept, string cancel)
        {
            return await _pageResolver().DisplayAlert(title, message, accept, cancel);
        }

        public async Task<string> DisplayActionSheet(string title, string cancel, string destruction, params string[] buttons)
        {
            return await _pageResolver().DisplayActionSheet(title, cancel, destruction, buttons);
        }
    }
}
