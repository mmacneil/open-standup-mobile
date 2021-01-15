using OpenStandup.Common.Dto;
using Xamarin.Forms;

namespace OpenStandup.Mobile.DataTemplates
{
    public class PostSummaryDataTemplateSelector : DataTemplateSelector
    {
        public DataTemplate PostWithImage { get; set; }
        public DataTemplate PostWithoutImage { get; set; }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            return string.IsNullOrEmpty(((PostDto)item).ImageName) ? PostWithoutImage : PostWithImage;
        }
    }
}


