// 代码生成时间: 2025-10-20 11:51:05
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ApiResponseFormatterTool
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Handles the 'Format Response' button click event.
        /// </summary>
        private void FormatResponseButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Get the API response from the input TextBox
                string apiResponse = ApiResponseInputTextBox.Text;

                // Parse the API response to a JSON object
                JObject jsonResponse = JObject.Parse(apiResponse);

                // Format the JSON object to a more readable string
                string formattedResponse = jsonResponse.ToString(Formatting.Indented);

                // Display the formatted response in the output TextBox
                FormattedResponseOutputTextBox.Text = formattedResponse;
            }
            catch (JsonReaderException ex)
            {
                // Handle JSON parsing errors
                MessageBox.Show($"Error parsing JSON: {ex.Message}", "Error");
            }
            catch (Exception ex)
            {
                // Handle any other unexpected errors
                MessageBox.Show($"An unexpected error occurred: {ex.Message}", "Error");
            }
        }
    }
}
