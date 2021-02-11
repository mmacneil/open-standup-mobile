using System;
using System.Collections.Generic;

namespace OpenStandup.Core.Interfaces.Services
{
    public interface IAppCenterWrapper
    {
        void Start();
        void TrackError(Exception exception, IDictionary<string, string> properties = null);
    }
}
