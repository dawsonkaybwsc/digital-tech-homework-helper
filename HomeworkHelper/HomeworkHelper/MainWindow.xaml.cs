using System;
using System.Windows;
using System.Windows.Controls;
using System.Collections.Generic;
using System.Windows.Output;

namespace HomeworkHelper
{
    public partial class MainWindow : Window
    {
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
