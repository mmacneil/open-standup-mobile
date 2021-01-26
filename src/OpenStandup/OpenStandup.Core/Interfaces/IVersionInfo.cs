

namespace OpenStandup.Core.Interfaces
{
    public interface IVersionInfo
    {
        void Track();
        string CurrentVersion { get; }
    }
}
