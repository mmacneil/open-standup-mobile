using OpenStandup.Core.Interfaces;
using Xamarin.Essentials;

namespace OpenStandup.Mobile.Infrastructure.Services
{
    public class VersionInfo : IVersionInfo
    {
        public void Track()
        {
            VersionTracking.Track();
        }

        public string CurrentVersion => VersionTracking.CurrentVersion;
    }
}
