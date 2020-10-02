

namespace CleanXF.Mobile.Infrastructure
{
    public static class Configuration
    {
#if DEBUG
        public const string ServiceEndpoint = "http://10.0.2.2:5000";
        public const string AppSecret = "1b128dec-fd50-4ee3-91c1-bf9c1b4a8d95";
#else
        public const string ServiceEndpoint = "https://andrewhoefling.com/service";
#endif
    }
}
