using System.Threading.Tasks;

namespace OpenStandup.Core.Interfaces
{
    public interface IOutputPort<in T>
    {
        Task Handle(T response);
    }
}
