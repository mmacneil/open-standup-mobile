using System;


namespace OpenStandup.SharedKernel.Utilities
{
    public static class Url
    {
        public static bool IsValidUrl(string value)
        {
            return Uri.TryCreate(value, UriKind.Absolute, out var uriResult)
                   && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
        }
    }
}
