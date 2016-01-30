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
            var chunkToProcess = jsonToOutput;
            var chunkToOutput = String.Empty;
            var indentLevel = 0;
            for (var i = 0; i < jsonToOutput.Length; i++)
            {
                if (jsonToOutput[i] != '\"')
                {
                    if (jsonToOutput[i] == '{')
                    {
                        chunkToOutput += jsonToOutput[i] + Environment.NewLine;
                        indentLevel++;
                        for (int j = 0; j < indentLevel; j++)
                        {
                            chunkToOutput += '\t';
                        }
                    }
                    else if (jsonToOutput[i] == '}')
                    {
                        indentLevel--;
                        for (int j = 0; j < indentLevel; j++)
                        {
                            chunkToOutput += '\t';
                        }
                        chunkToOutput += Environment.NewLine + jsonToOutput[i];
                    }
                    else
                    {
                        chunkToOutput += jsonToOutput[i];
                        continue;
                    }
                    target.Output(chunkToOutput);
                    chunkToOutput = String.Empty;
                    chunkToProcess = jsonToOutput.Substring(i + 1);
                }
                else
                {
                    target.Output(chunkToOutput);
                    chunkToOutput = String.Empty;
                    i = i + OutputAndRemoveDataPoint(target, chunkToProcess);
                    chunkToProcess = jsonToOutput.Substring(i);
                }
            }
        }

        private static int OutputAndRemoveDataPoint(TextBlock target, string toOutput)
        {
            var pattern = @"(¤(\\u[a-zA-Z0-9]{4}|\\[^u]|[^\\¤])*¤(\s*:)?|\b(true|false|null)\b|-?\d+(?:\.\d*)?(?:[eE][+\-]?\d+)?)".Replace('¤', '"');
            var matches = Regex.Matches(toOutput, pattern);
            if (Regex.IsMatch(matches[0].Value, @"^¤".Replace('¤', '"')))
            {
                if (Regex.IsMatch(matches[0].Value, ":$"))
                {
                    // key
                    target.OutputBold(matches[0].Value.Replace('\"', '\0') + "\t");
                }
                else
                {
                    // string
                    target.OutputWithFormat(matches[0].Value + Environment.NewLine, fontColor: Colors.DarkGreen);
                }
            }
            else if (Regex.IsMatch(matches[0].Value, "true|false"))
            {
                // boolean
                target.OutputWithFormat(matches[0].Value.Replace('\"', '\0') + Environment.NewLine, fontColor: Colors.DarkBlue);
            }
            else if (Regex.IsMatch(matches[0].Value, "null"))
            {
                // null
                target.OutputWithFormat(matches[0].Value.Replace('\"', '\0') + Environment.NewLine, fontColor: Colors.DarkBlue);
            }
            else
            {
                // number
                target.OutputWithFormat(matches[0].Value.Replace('\"', '\0') + Environment.NewLine, fontColor: Colors.DarkCyan);
            }

            return matches[0].Length;
        }
    }
}