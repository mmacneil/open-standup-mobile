using System;
using System.Collections.Generic;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using OpenStandup.Core.Interfaces.Services;

namespace OpenStandup.Mobile.Infrastructure.Services
{
    public class AppCenterWrapper : IAppCenterWrapper
    {
        public void Start()
        {
            AppCenter.Start("android=ANDROID_KEY_SECRET_HERE;", typeof(Analytics), typeof(Crashes));
        }

        public void TrackError(Exception exception, IDictionary<string, string> properties = null)
        {
            Crashes.TrackError(exception, properties);
        }
    }
}
