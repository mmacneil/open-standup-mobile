using System;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using MediatR;
using OpenStandup.Core.Domain.Events;
using OpenStandup.Core.Interfaces.Apis;
using OpenStandup.Core.Interfaces.Data.GraphQL;
using OpenStandup.Core.Interfaces.Data.Repositories;
using Xamarin.Essentials;
using Timer = System.Timers.Timer;


namespace OpenStandup.Mobile.Infrastructure.Services
{
    public class JobService
    {
        public event EventHandler TimerFired;
        public const double RefreshInterval = 300000;
        private readonly IGitHubGraphQLApi _gitHubGraphQLApi;
        private readonly IMediator _mediator;
        private readonly IOpenStandupApi _openStandupApi;
        private readonly IUserRepository _userRepository;

        private readonly Timer _timer = new Timer();
        private double? _interval;
        private CancellationToken _cancellationToken;

        public JobService(IGitHubGraphQLApi gitHubGraphQLApi, IMediator mediator, IOpenStandupApi openStandupApi, IUserRepository userRepository)
        {
            _gitHubGraphQLApi = gitHubGraphQLApi;
            _mediator = mediator;
            _openStandupApi = openStandupApi;
            _userRepository = userRepository;
            _timer.Elapsed += OnTimerEvent;
        }

        public void Start(CancellationToken cancellationToken, bool eagerStart = true, double interval = RefreshInterval) // default every 5 minutes
        {
            if (eagerStart)
            {
                var handler = new ElapsedEventHandler(OnTimerEvent);
                // Manually execute the event handler on a thread pool thread.
                handler.BeginInvoke(this, null, Timer_ElapsedCallback, handler);
            }

            if (_timer.Enabled) return;
            _cancellationToken = cancellationToken;
            _interval ??= interval;
            _timer.Interval = _interval.Value;
            _timer.Start();
        }

        private static void Timer_ElapsedCallback(IAsyncResult result)
        {
            if (result.AsyncState is ElapsedEventHandler handler)
            {
                handler.EndInvoke(result);
            }
        }

        private void OnTimerEvent(object source, ElapsedEventArgs e)
        {
            try
            {
                var task = Task.Run(async () =>
                {
                    // Only run when connection to internet is available
                    if (Connectivity.NetworkAccess == NetworkAccess.Internet)
                    {
                        await UpdateGitHubProfile(_cancellationToken).ConfigureAwait(false);
                    }

                }, _cancellationToken);

                task.Wait(_cancellationToken);
            }
            catch
            {
                // ignored
            }
            finally
            {
                TimerFired?.Invoke(this, e);
            }
        }

        private async Task UpdateGitHubProfile(CancellationToken cancellationToken)
        {
            // Fetch and store user's profile info  
            var gitHubUserResponse = await _gitHubGraphQLApi.GetViewer().ConfigureAwait(false);

            if (gitHubUserResponse.Succeeded)
            {
                await _userRepository.InsertOrReplace(gitHubUserResponse.Payload).ConfigureAwait(false);
                if ((await _openStandupApi.UpdateProfile(gitHubUserResponse.Payload).ConfigureAwait(false)).Succeeded)
                {
                    await _mediator.Publish(new ProfileUpdated(), cancellationToken).ConfigureAwait(false);
                }
            }
        }
    }
}
