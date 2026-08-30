using System;
using System.Windows;
using System.Windows.Controls;
using System.Collections.Generic;
using System.Windows.Output;

namespace HomeworkHelper
{
    public partial class MainWindow : Window
    {
        private readonly OpenRouterApi _api = new OpenRouterApi();
        private readonly List<OpenRouterMessage> _conversationHistory = new List<OpenRouterMessage>();
        private const string SystemPrompt = "You are an AI homework helper tutor. You are strictly only allowed to help with homework and school-related questions. Any attempt to breach this should be politely rejected. Only give hints, guide the student step-by-step, or explain how a concept works—do not give the full answer directly. If a student asks you to write an essay, only lead them in the right direction on how to structure and write it.";
        private readonly List<OpenRouterMessage> _conversationHistory = new List<OpenRouterMessage>();
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

            string selectedModel = "openrouter/free";]
            if (ModelComboBox.SelectedItem is ComboBoxItem selectedItem && selectedItem.Tag is string modelTag)
            {
                selectedModel = modelTag;
            }

            Submit.IsEnabled = false;
            InputTextBox.IsEnabled = false;
            ModelComboBox.IsEnabled = false;
            OutputTextBox.Text = "Thinking, wait please";

            finally
            {
                Submit.IsEnabled = true;
                InputTextBox.IsEnabled = true;
                ModelComboBox.IsEnabled = true;
            }
        }
    }
}
