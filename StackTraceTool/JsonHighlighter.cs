namespace StackTraceTool
{
    using System;
    using System.Text.RegularExpressions;

    using Windows.UI;
    using Windows.UI.Xaml.Controls;

    public static class JsonHighlighter
    {
        public static void OutputSyntaxHighlightedJson(this TextBlock target, string jsonToOutput)
        {
            var pattern = @"(¤(\\u[a-zA-Z0-9]{4}|\\[^u]|[^\\¤])*¤(\s*:)?|\b(true|false|null)\b|-?\d+(?:\.\d*)?(?:[eE][+\-]?\d+)?)".Replace('¤', '"');
            var matches = Regex.Matches(jsonToOutput, pattern);

            foreach (Match match in matches)
            {
                if (Regex.IsMatch(match.Value, @"^¤".Replace('¤', '"')))
                {
                    if (Regex.IsMatch(match.Value, ":$"))
                    {
                        // key
                        target.OutputBold(match.Value.Replace('\"', '\0'));
                    }
                    else
                    {
                        // string
                        target.OutputWithFormat(match.Value + Environment.NewLine, fontColor: Colors.DarkGreen);
                    }
                }
                else if (Regex.IsMatch(match.Value, "true|false"))
                {
                    // boolean
                    target.OutputWithFormat(match.Value.Replace('\"', '\0') + Environment.NewLine, fontColor: Colors.DarkBlue);
                }
                else if (Regex.IsMatch(match.Value, "null"))
                {
                    // null
                    target.OutputWithFormat(match.Value.Replace('\"', '\0') + Environment.NewLine, fontColor: Colors.DarkBlue);
                }
                else
                {
                    // number
                    target.OutputWithFormat(match.Value.Replace('\"', '\0') + Environment.NewLine, fontColor: Colors.DarkCyan);
                }
            }
        }
    }
}