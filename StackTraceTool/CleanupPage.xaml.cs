namespace StackTraceTool
{
    using System;
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Controls;

    public sealed partial class CleanupPage : Page
    {
        public CleanupPage()
        {
            this.InitializeComponent();
        }

        private void inputButton_Click(object sender, RoutedEventArgs e)
        {
            stackTraceOutput.Text = stackTraceInput.Text.Replace("\\r\\n", Environment.NewLine);
            stackTraceOutput.Text = stackTraceInput.Text.Replace("\\n\\r", Environment.NewLine);
            stackTraceOutput.Text = stackTraceInput.Text.Replace("\\r", Environment.NewLine);
            stackTraceOutput.Text = stackTraceInput.Text.Replace("\\n", Environment.NewLine);
            stackTraceInput.Text = string.Empty;
            inputPanel.Visibility = Visibility.Collapsed;
            outputPanel.Visibility = Visibility.Visible;
        }

        private void resetButton_Click(object sender, RoutedEventArgs e)
        {
            stackTraceOutput.Text = string.Empty;
            outputPanel.Visibility = Visibility.Collapsed;
            inputPanel.Visibility = Visibility.Visible;
        }
    }
}
