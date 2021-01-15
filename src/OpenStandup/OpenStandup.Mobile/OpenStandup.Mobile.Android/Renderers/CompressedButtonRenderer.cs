using Android.Content;
using OpenStandup.Mobile.Controls;
using OpenStandup.Mobile.Droid.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;


[assembly: ExportRenderer(typeof(Button), typeof(CompressedButtonRenderer), new[] { typeof(CustomVisual) })]
namespace OpenStandup.Mobile.Droid.Renderers
{
    public class CompressedButtonRenderer : Xamarin.Forms.Platform.Android.AppCompat.ButtonRenderer
    {
        public CompressedButtonRenderer(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Button> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement != null)
            {
                // Cleanup
            }

            if (e.NewElement != null)
            {
                Control.SetPadding(0, 0, 0, 0);
            }
        }
    }
}