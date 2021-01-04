using System;
using Android.Content;
using Android.Graphics;
using OpenStandup.Mobile.Droid.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;


[assembly: ExportRenderer(typeof(WebView), typeof(AutoHeightWebViewRenderer))]
namespace OpenStandup.Mobile.Droid.Renderers
{
    public class AutoHeightWebViewRenderer : WebViewRenderer
    {
        public AutoHeightWebViewRenderer(Context context) : base(context) { }

        protected override void OnElementChanged(ElementChangedEventArgs<WebView> e)
        {
            base.OnElementChanged(e);

            if (!(e.NewElement is { } webViewControl)) return;
            if (e.OldElement == null)
            {
                Control.SetWebViewClient(new ExtendedWebViewClient(webViewControl, this));
            }
        }

        private class ExtendedWebViewClient : Android.Webkit.WebViewClient
        {
            private readonly WebView _control;
            private readonly AutoHeightWebViewRenderer _renderer;

            public ExtendedWebViewClient(WebView control, AutoHeightWebViewRenderer renderer)
            {
                _control = control;
                _renderer = renderer;
            }

            public override async void OnPageFinished(Android.Webkit.WebView view, string url)
            {
                if (_control == null) return;

                try
                {
                    var i = 10;
                    while (view.ContentHeight == 0 && i-- > 0) // wait here till content is rendered
                    {
                        await System.Threading.Tasks.Task.Delay(100);
                    }

                    _control.HeightRequest = view.ContentHeight;
                }
                catch (ObjectDisposedException)
                {
                    // Fix me: very frequent "System.ObjectDisposedException: Cannot access a disposed object. Android.Webkit.WebView"
                }
            }

            public override void OnPageStarted(Android.Webkit.WebView view, string url, Bitmap favicon)
            {
                if (url == "file:///android_asset/")
                {
                    return;
                }

                base.OnPageStarted(view, url, favicon);
                var args = new WebNavigatingEventArgs(WebNavigationEvent.NewPage, new UrlWebViewSource { Url = url }, url);
                _renderer.ElementController.SendNavigating(args);
            }
        }
    }
}

