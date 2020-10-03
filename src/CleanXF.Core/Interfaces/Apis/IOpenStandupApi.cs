using System.Threading.Tasks;
using CleanXF.Core.Dto.Api;
using CleanXF.SharedKernel;

namespace CleanXF.Core.Interfaces.Apis
{
    public interface IOpenStandupApi
    {
        Task<bool> SaveProfile();
        Task<OperationResponse<AppConfigDto>> GetConfiguration();
    }
}
