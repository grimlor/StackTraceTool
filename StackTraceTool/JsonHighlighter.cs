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

            var indentLevel = 0;
            for (var i = 0; i < matches.Count; ++i)
            {
                if (Regex.IsMatch(matches[i].Value, @"^¤".Replace('¤', '"')))
                {
                    if (Regex.IsMatch(matches[i].Value, ":$"))
                    {
                        // key
                        for (var j = 0; j < indentLevel; j++)
                        {
                            target.Output("\t");
                        }
                        target.OutputBold(matches[i].Value.Replace('\"', '\0') + "\t");
                        if (i < matches.Count && Regex.IsMatch(matches[i + 1].Value, ":$"))
                        {
                            target.Output(Environment.NewLine);
                            indentLevel++;
                        }
                    }
                    else
                    {
                        // string
                        target.OutputWithFormat(matches[i].Value + Environment.NewLine, fontColor: Colors.DarkGreen);
                    }
                }
                else if (Regex.IsMatch(matches[i].Value, "true|false"))
                {
                    // boolean
                    target.OutputWithFormat(matches[i].Value.Replace('\"', '\0') + Environment.NewLine, fontColor: Colors.DarkBlue);
                }
                else if (Regex.IsMatch(matches[i].Value, "null"))
                {
                    // null
                    target.OutputWithFormat(matches[i].Value.Replace('\"', '\0') + Environment.NewLine, fontColor: Colors.DarkBlue);
                }
                else
                {
                    // number
                    target.OutputWithFormat(matches[i].Value.Replace('\"', '\0') + Environment.NewLine, fontColor: Colors.DarkCyan);
                }
            }
        }
    }
}