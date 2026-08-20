using System;
using System.Windows;
using System.Windows.Controls;

namespace HomeworkHelper
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private async void Submit_Click(object sender, RoutedEventArgs e)
        {
            string question = InputTextBox.Text?.Trim() ?? string.Empty;
            if (string.IsNullOrWhiteSpace(question))
            {
                MessageBox.Show("Please type in a question before pressing submit.", "Input Required", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            string selectedModel = "google/gemini 2.5 flash";
            if (ModelComboBox.SelectedItem is ComboBoxItem selectedItem && selectedItem.Tag is string modelTag)
            {
                selectedModel = modelTag;
            }

            Submit.IsEnabled = false;
            InputTextBox.IsEnabled = false;
            ModelComboBox.IsEnabled = false;
            OutputTextBox.Text = "Thinking, wait please";

            try
            {
                var service = new OpenRouterApi();
                string answer = await service.GetCompletionAsync(question, selectedModel);
                OutputTextBox.Text = answer;
            }
            catch (Exception ex)
            {
                OutputTextBox.Text = $"A unexpected error has occurred:\n{ex.Message}";
            }
            finally
            {
                Submit.IsEnabled = true;
                InputTextBox.IsEnabled = true;
                ModelComboBox.IsEnabled = true;
            }
        }
    }
}
