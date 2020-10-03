using System.Threading.Tasks;

namespace CleanXF.Mobile.Infrastructure.Interfaces
{
    public interface IConfigurationLoader
    {
        Task<bool> TryLoad();
    }
}
