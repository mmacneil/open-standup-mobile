using System;
using System.Threading;
using System.Threading.Tasks;
using OpenStandup.Core.Interfaces.Services;
using Xamarin.Essentials;

namespace OpenStandup.Mobile.Infrastructure.Services
{
    public class LocationService : ILocationService
    {
        public async Task<Tuple<double, double>> GetCurrentLocation(CancellationTokenSource cts)
        {
            try
            {
                var request = new GeolocationRequest(GeolocationAccuracy.Medium, TimeSpan.FromSeconds(10));
                var location = await Geolocation.GetLocationAsync(request, cts.Token);
                return new Tuple<double, double>(location.Latitude, location.Longitude);
            }
            catch (Exception ex) when (
                ex is FeatureNotSupportedException
                || ex is FeatureNotEnabledException
                || ex is PermissionException)
            {
                return new Tuple<double, double>(0, 0);
            }
        }
    }
}
