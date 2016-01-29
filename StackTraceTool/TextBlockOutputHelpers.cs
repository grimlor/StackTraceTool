namespace StackTraceTool
{
    using Windows.UI;
    using Windows.UI.Text;
    using Windows.UI.Xaml.Controls;
    using Windows.UI.Xaml.Documents;
    using Windows.UI.Xaml.Media;

    /// <summary>
    /// Makes outputting to a TextBlock with formatting easier.
    /// </summary>
    public static class TextBlockOutputHelpers
    {
        /// <summary>
        /// Outputs text with formating.
        /// </summary>
        /// <param name="target">The target <see cref="TextBlock"/>.</param>
        /// <param name="toOutput">String to output.</param>
        /// <param name="fontStyle">The font style.</param>
        /// <param name="fontWeight">The font weight.</param>
        /// <param name="fontColor">Color of the font.</param>
        public static void OutputWithFormat(this TextBlock target, string toOutput,
            FontStyle fontStyle = FontStyle.Normal,
            FontWeight fontWeight = default(FontWeight),
            Color fontColor = default(Color))
        {
            var formatted = new Run
            {
                Text = toOutput,
                FontStyle = fontStyle,
                FontWeight = fontWeight.Equals(default(FontWeight)) ? FontWeights.Normal : fontWeight,
                Foreground = fontColor.Equals(default(Color)) ? new SolidColorBrush(Colors.Black) : new SolidColorBrush(fontColor)
            };
            target.Inlines.Add(formatted);
        }

        /// <summary>
        /// Unformatted output.
        /// </summary>
        /// <param name="target">The target <see cref="TextBlock"/>.</param>
        /// <param name="toOutput">String to output.</param>
        public static void Output(this TextBlock target, string toOutput)
        {
            target.OutputWithFormat(toOutput);
        }

        /// <summary>
        /// Outputs in italics.
        /// </summary>
        /// <param name="target">The target <see cref="TextBlock"/>.</param>
        /// <param name="toOutput">String to output.</param>
        public static void OutputItalic(this TextBlock target, string toOutput)
        {
            target.OutputWithFormat(toOutput, FontStyle.Italic);
        }

        /// <summary>
        /// Outputs in bold.
        /// </summary>
        /// <param name="target">The target <see cref="TextBlock"/>.</param>
        /// <param name="toOutput">String to output.</param>
        public static void OutputBold(this TextBlock target, string toOutput)
        {
            target.OutputWithFormat(toOutput, fontWeight: FontWeights.Bold);
        }
    }
}