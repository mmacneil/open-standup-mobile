using Xamarin.Forms;

namespace OpenStandup.Mobile.Controls
{
    public class RoundImage : StackLayout
    {
        public RoundImage(Image image, int height = 100, int width = 100, int cornerRadius = 50)
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


