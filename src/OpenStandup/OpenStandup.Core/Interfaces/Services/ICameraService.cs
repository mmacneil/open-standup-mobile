using System.Threading.Tasks;

namespace OpenStandup.Core.Interfaces.Services
{
    public interface ICameraService
    {
        Task TakePhotoAsync();
    }
}
