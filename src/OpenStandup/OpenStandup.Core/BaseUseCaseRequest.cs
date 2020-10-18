using OpenStandup.Core.Interfaces;

namespace OpenStandup.Core
{
    public abstract class BaseUseCaseRequest<TUseCaseResponse>
    {
        public IOutputPort<TUseCaseResponse> OutputPort { get; }

        protected BaseUseCaseRequest(IOutputPort<TUseCaseResponse> outputPort = null)
        {
            OutputPort = outputPort;
        }
    }
}
