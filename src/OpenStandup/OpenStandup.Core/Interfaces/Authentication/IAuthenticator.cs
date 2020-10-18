using OpenStandup.SharedKernel;
using System.Threading.Tasks;

namespace OpenStandup.Core.Interfaces.Authentication
{
    public interface IAuthenticator
    {
        Task<OperationResponse<string>> Authenticate();
    }
}
