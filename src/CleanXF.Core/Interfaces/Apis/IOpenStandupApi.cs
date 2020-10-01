using System.Threading.Tasks;

namespace CleanXF.Core.Interfaces.Apis
{
    public interface IOpenStandupApi
    {
        Task<bool> SaveProfile();
    }
}
