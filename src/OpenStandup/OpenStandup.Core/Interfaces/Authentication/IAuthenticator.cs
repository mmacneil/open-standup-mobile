using OpenStandup.SharedKernel;
using System.Threading.Tasks;

namespace OpenStandup.Core.Interfaces.Authentication
{
    public interface IAuthenticator
    {
        Task<Result<string>> Authenticate();
    }
}
