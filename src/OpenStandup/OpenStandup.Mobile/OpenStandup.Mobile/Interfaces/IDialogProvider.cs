using System.Threading.Tasks;

namespace OpenStandup.Mobile.Interfaces
{
    public interface IDialogProvider
    {
        Task DisplayAlert(string title, string message, string cancel);
        Task<bool> DisplayAlert(string title, string message, string accept, string cancel);
        Task<string> DisplayActionSheet(string title, string cancel, string destruction, params string[] buttons);
    }
}
