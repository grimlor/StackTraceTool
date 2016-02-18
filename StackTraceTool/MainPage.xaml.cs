using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace StackTraceTool
{
    public sealed partial class MainPage : Page
    {
        public MainPage(Frame frame)
        {
            this.InitializeComponent();
            this.ShellSplitView.Content = frame;
        }

        private void OnMenuButtonClicked(object sender, RoutedEventArgs e)
        {
            ShellSplitView.IsPaneOpen = !ShellSplitView.IsPaneOpen;
            ((RadioButton)sender).IsChecked = false;
        }

        private void OnCleanupButtonChecked(object sender, RoutedEventArgs e)
        {
            NavigateTo<CleanupPage>(e);
        }

        private void OnParseJsonButtonChecked(object sender, RoutedEventArgs e)
        {
            NavigateTo<JsonPresenterPage>(e);
        }

        private void NavigateTo<T>(RoutedEventArgs e) where T : Page
        {
            ShellSplitView.IsPaneOpen = false;
            if (ShellSplitView.Content != null)
                ((Frame)ShellSplitView.Content).Navigate(typeof(T), e);
        }
    }
}
