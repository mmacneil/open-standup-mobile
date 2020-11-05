using System.Threading.Tasks;
using Vessel;

namespace OpenStandup.Core.Interfaces.Authentication
{
    public interface IAuthenticator
    {
        Task<Dto<string>> Authenticate();
    }
}
