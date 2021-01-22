using System;
using OpenStandup.Mobile.Helpers;
using Xamarin.CommunityToolkit.Effects;
using Xamarin.Forms;

namespace OpenStandup.Mobile.Controls
{
    public class DeleteLayout : StackLayout
    {
        public DeleteLayout(Action deleteHandler)
        {
            Children.Add(new Label { Style = ResourceDictionaryHelper.GetStyle("MetaIcon"), Text = IconFont.Delete });
            Children.Add(new Label { Text = "delete", Style = ResourceDictionaryHelper.GetStyle("MetaCommandText") });
            Style = ResourceDictionaryHelper.GetStyle("MetaCommandLayout");
            TouchEffect.SetNativeAnimation(this, true);
            TouchEffect.SetCommand(this, new Command(deleteHandler));
        }
    }
}
