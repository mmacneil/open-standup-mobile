using System.Threading.Tasks;

namespace CleanXF.Mobile.Services
{
    public interface INavigator
    {
        Task GoTo(string route);
    }
}
