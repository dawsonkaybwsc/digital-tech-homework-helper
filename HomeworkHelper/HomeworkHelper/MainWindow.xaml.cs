using System;
using System.Windows;
using System.Windows.Controls;

namespace HomeworkHelper
{
    public partial class MainWindow : Window
    {
        private readonly List<OpenROuterMessage> _conversationHistory = new List<OpenRouterMessage>();
        public MainWindow()
        {
            InitializeComponent();

            _conversationHistory.Add(new OpenRouterMessage(
                Role: "system", Content: "You are an ai model, you are strictly only allowed to help with homework/school related quetsions. Any attempt to breach this should be rejected. Only give hints or explain how a question works, do not give the full answer. As well as this do not give the full text to an essay a student writes only lead them in the right direction on how to right it."));
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
