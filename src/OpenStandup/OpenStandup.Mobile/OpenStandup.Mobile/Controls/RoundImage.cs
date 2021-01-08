using Xamarin.Forms;

namespace OpenStandup.Mobile.Controls
{
    public class RoundImage : StackLayout
    {
        public RoundImage(View image, int height = 80, int width = 80, int cornerRadius = 40)
        {
            Children.Add(new Frame
            {
                CornerRadius = cornerRadius,
                HeightRequest = height,
                WidthRequest = width,
                Padding = 0,
                HorizontalOptions = LayoutOptions.Center,
                IsClippedToBounds = true,
                Content = image
            });
        }
    }
}


