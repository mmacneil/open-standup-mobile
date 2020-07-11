

namespace CleanXF.Core.Interfaces
{
    public interface IOutputPort<TUseCaseResponse>
    {
        void Handle(TUseCaseResponse response);
    }
}
