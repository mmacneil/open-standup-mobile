using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows.Input;
using OpenStandup.Mobile.Helpers;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace OpenStandup.Mobile.Converters
{
    public class HtmlLabelConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var formatted = new FormattedString();

            foreach (var item in ProcessString((string)value))
            {
                formatted.Spans.Add(CreateSpan(item));
            }

            return formatted;
        }

        private Span CreateSpan(StringSection section)
        {
            var span = new Span
            {
                Text = section.Text
            };

            if (string.IsNullOrEmpty(section.Link)) return span;

            span.GestureRecognizers.Add(new TapGestureRecognizer
            {
                Command = _navigationCommand,
                CommandParameter = section.Link
            });

            span.TextColor = ResourceDictionaryHelper.GetColor("Link");
            span.TextDecorations = TextDecorations.Underline;
            return span;
        }

        public static IList<StringSection> ProcessString(string rawText)
        {
            const string spanPattern = @"(<a.*?>.*?</a>)";

            var collection = Regex.Matches(rawText, spanPattern, RegexOptions.Singleline);

            var sections = new List<StringSection>();

            var lastIndex = 0;

            foreach (Match item in collection)
            {
                sections.Add(new StringSection { Text = rawText.Substring(lastIndex, item.Index) });
                lastIndex += item.Index + item.Length;

                // Get HTML href 
                var html = new StringSection
                {
                    Link = Regex.Match(item.Value, "(?<=href=\\\")[\\S]+(?=\\\")").Value,
                    Text = Regex.Replace(item.Value, "<.*?>", string.Empty)
                };

                sections.Add(html);
            }

            sections.Add(new StringSection { Text = rawText.Substring(lastIndex) });
            return sections;
        }

        public class StringSection
        {
            public string Text { get; set; }
            public string Link { get; set; }
        }

        private readonly ICommand _navigationCommand = new Command<string>(async (url) =>
        {
            await Launcher.OpenAsync(url);
        });

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
