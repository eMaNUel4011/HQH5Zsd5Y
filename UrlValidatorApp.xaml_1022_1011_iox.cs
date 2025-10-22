// 代码生成时间: 2025-10-22 10:11:59
using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;

namespace UrlValidatorApp
{
    /// <summary>
    /// Interaction logic for UrlValidatorApp.xaml
    /// </summary>
    public partial class UrlValidatorApp : Window
    {
        public UrlValidatorApp()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Validates the URL entered by the user.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">The event arguments.</param>
        private async void ValidateUrlButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(urlTextBox.Text))
            {
                MessageBox.Show("Please enter a URL.");
                return;
            }

            try
            {
                Uri uriResult;
                if (!Uri.TryCreate(urlTextBox.Text, UriKind.Absolute, out uriResult)
                    || (uriResult.Scheme != Uri.UriSchemeHttp && uriResult.Scheme != Uri.UriSchemeHttps))
                {
                    MessageBox.Show("Invalid URL. Please enter a valid HTTP or HTTPS URL.");
                    return;
                }

                // Use HttpWebRequest to check if the URL is reachable
                using (var client = new WebClient())
                {
                    string response = await client.DownloadStringTaskAsync(uriResult);
                    MessageBox.Show("URL is valid and reachable.");
                }
            }
            catch (WebException ex)
            {
                // Handle cases where the URL is not reachable or other web-related errors occur
                MessageBox.Show($"WebException occurred: {ex.Message}");
            }
            catch (Exception ex)
            {
                // Handle non-web related exceptions
                MessageBox.Show($"An unexpected error occurred: {ex.Message}");
            }
        }
    }
}