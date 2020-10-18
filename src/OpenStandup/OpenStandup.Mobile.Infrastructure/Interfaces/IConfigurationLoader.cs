using System.Threading.Tasks;

namespace OpenStandup.Mobile.Infrastructure.Interfaces
{
    public interface IConfigurationLoader
    {
        Task<bool> TryLoad();
    }
}
