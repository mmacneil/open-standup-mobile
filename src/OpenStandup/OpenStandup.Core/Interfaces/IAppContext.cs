using System.Threading.Tasks;
using OpenStandup.Core.Domain.Entities;

namespace OpenStandup.Core.Interfaces
{
    public interface IAppContext
    {
        GitHubUser User { get; }
        Task Refresh();
    }
}
