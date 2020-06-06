using CleanXF.SharedKernel;
using System.Threading.Tasks;

namespace CleanXF.Core.Interfaces.Authentication
{
    public interface IAuthenticator
    {
        Task<OperationResponse<string>> Authenticate();
    }
}
