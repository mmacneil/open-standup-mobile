using System.Threading.Tasks;
using OpenStandup.Core.Domain.Entities;

namespace OpenStandup.Core.Interfaces.Data.Repositories
{
    public interface IPostRepository
    {
        Task<bool> InsertOrReplace(Post post);
    }
}
