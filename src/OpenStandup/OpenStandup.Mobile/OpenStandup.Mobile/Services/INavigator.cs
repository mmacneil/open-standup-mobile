using System.Threading.Tasks;

namespace OpenStandup.Mobile.Services
{
    public interface INavigator
    {
        Task GoTo(string route);
    }
}
