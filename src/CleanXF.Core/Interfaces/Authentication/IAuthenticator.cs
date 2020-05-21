using System.Threading.Tasks;

namespace CleanXF.Core.Interfaces.Authentication
{
    public interface IAuthenticator
    {
        Task Authenticate();
    }
}
