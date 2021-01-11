using System.Collections.Generic;
using Xamarin.Forms;

namespace OpenStandup.Mobile.Helpers
{
    public static class ResourceDictionaryHelper
    {
        // Was using indexer before but discovered potential bug when using multiple standalone resource dictionaries
        // and indexer syntax i.e. Application.Current.Resources["MetaLabel"] received KeyNotFound exception. Switching the lookup
        // from indexer to Resources.TryGetValue works with this setup. There is an open issue for this here: https://github.com/xamarin/Xamarin.Forms/issues/12592

        public static Color GetColor(string key)
        {
            return GetValue(key) is Color color ? color : default;
        }

        public static Style GetStyle(string key)
        {
            return GetValue(key) is Style style ? style : default;
        }

        private static object GetValue(string key)
        {
            if (Application.Current.Resources.TryGetValue(key, out var value))
            {
                return value;
            }

            throw new KeyNotFoundException($"{key} missing from resource dictionary");
        }
    }
}
