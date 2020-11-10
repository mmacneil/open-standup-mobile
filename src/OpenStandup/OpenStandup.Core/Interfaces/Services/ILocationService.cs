using System;
using System.Threading;
using System.Threading.Tasks;

namespace OpenStandup.Core.Interfaces.Services
{
    public interface ILocationService
    {
        Task<Tuple<double, double>> GetCurrentLocation(CancellationTokenSource cts);
    }
}
