namespace StackTraceTool
{
    using System;
    using System.Linq;
    using System.Text.RegularExpressions;

    using Windows.UI;
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Controls;

    using Newtonsoft.Json;

    public sealed partial class JsonPresenterPage : Page
    {
        public JsonPresenterPage()
        {
            this.InitializeComponent();
        }

        private void inputButton_Click(object sender, RoutedEventArgs e)
        {
            var countOpen = jsonInput.Text.Count(t => t == '{');
            var closeCount = jsonInput.Text.Count(t => t == '}');
            if (countOpen != closeCount)
            {
                var message = "Braces are mismatched." + Environment.NewLine;
                jsonOutput.OutputWithFormat(message, fontColor: Colors.Red);
            }
            else
            {
                string sanitizationErrors;
                var toParse = SanitizeInput(jsonInput.Text, out sanitizationErrors);
                try
                {
                    //var exception = JsonConvert.DeserializeObject<ParsedExceptionJson>(toParse);
                    if (!string.IsNullOrEmpty(sanitizationErrors))
                    {
                        jsonOutput.OutputItalic(sanitizationErrors + Environment.NewLine);
                    }
                    jsonOutput.OutputSyntaxHighlightedJson(toParse);

                    //OutputException(exception);
                }
                catch (JsonReaderException)
                {
                    var message = "It's dead, Jim. (Psst: check your JSON as it's probably malformed.)";
                    jsonOutput.OutputWithFormat(message, fontColor: Colors.Red);
                }
            }
            jsonInput.Text = string.Empty;
            inputPanel.Visibility = Visibility.Collapsed;
            outputPanel.Visibility = Visibility.Visible;
        }

        private void OutputException(ParsedExceptionJson exception)
        {
            jsonOutput.OutputBold("ErrorMessage:" + Environment.NewLine);
            jsonOutput.Output(exception.ErrorDetails.ErrorMessage + Environment.NewLine);
            jsonOutput.OutputBold("ExceptionType:" + Environment.NewLine);
            jsonOutput.Output(exception.ErrorDetails.ExceptionType + Environment.NewLine);
            jsonOutput.OutputBold("StackTrace:" + Environment.NewLine);
            jsonOutput.Output(exception.ErrorDetails.StackTrace + Environment.NewLine);
            jsonOutput.OutputBold("InnerExceptionDetail:" + Environment.NewLine);
            jsonOutput.Output(exception.ErrorDetails.InnerExceptionDetail + Environment.NewLine);
            jsonOutput.OutputBold("UnexpectedException:" + Environment.NewLine);
            jsonOutput.Output(exception.ErrorDetails.UnexpectedException + Environment.NewLine);
        }

        private static string SanitizeInput(string input, out string errors)
        {
            string output = input;
            bool missingBraces = false, 
                hasQuotes = false;

            errors = string.Empty;

            if (!input.StartsWith("{"))
            {
                missingBraces = true;
                output = "{" + input + "}";
            }

            var internalQuotes = "(?<![:, {])\"(?![:, \r\n])";
            var replacement = "#";
            var rgx = new Regex(internalQuotes);
            if (rgx.IsMatch(output))
            {
                hasQuotes = true;
                output = rgx.Replace(output, replacement);
            }

            if (missingBraces)
            {
                errors = "You were missing encapsulating braces but I handled that for you." +
                         Environment.NewLine;
            }
            if (hasQuotes)
            {
                errors += "There were quotes in your JSON but I handled that by replacing them with hashmarks." +
                         Environment.NewLine;
            }

            return output;
        }

        private void resetButton_Click(object sender, RoutedEventArgs e)
        {
            jsonOutput.Text = string.Empty;
            outputPanel.Visibility = Visibility.Collapsed;
            inputPanel.Visibility = Visibility.Visible;
        }
    }
}
