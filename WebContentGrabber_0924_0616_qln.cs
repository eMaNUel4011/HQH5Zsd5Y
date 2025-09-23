// 代码生成时间: 2025-09-24 06:16:17
 * Features:
 * - Fetch web content from a specified URL.
 * - Display the fetched content in a WPF UI.
 * - Error handling for common web request exceptions.
 */
# TODO: 优化性能

using System;
# NOTE: 重要实现细节
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Windows;
using HtmlAgilityPack;

namespace WebContentGrabber
{
    public partial class MainWindow : Window
    {
        private HttpClient httpClient;

        public MainWindow()
        {
# 扩展功能模块
            InitializeComponent();
            httpClient = new HttpClient();
        }

        // Fetches web content from the specified URL and updates the UI with the fetched content.
        private async void FetchContentButton_Click(object sender, RoutedEventArgs e)
        {
# FIXME: 处理边界情况
            try
            {
                string url = UrlTextBox.Text;
                if (string.IsNullOrWhiteSpace(url))
                {
                    MessageBox.Show("Please enter a URL.");
                    return;
                }

                HttpResponseMessage response = await httpClient.GetAsync(url);
                response.EnsureSuccessStatusCode();
                string content = await response.Content.ReadAsStringAsync();

                // Parse HTML content and extract the body.
# 添加错误处理
                HtmlDocument doc = new HtmlDocument();
                doc.LoadHtml(content);
                string bodyContent = doc.DocumentNode.SelectSingleNode("//body")?.InnerHtml;

                // Display the fetched content in the UI.
# 扩展功能模块
                ContentTextBox.Text = bodyContent;
            }
            catch (HttpRequestException ex)
            {
                MessageBox.Show($"Error fetching content: {ex.Message}");
# 添加错误处理
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An unexpected error occurred: {ex.Message}");
            }
        }
    }
}

/*
 * Note: This code assumes that the MainWindow.xaml contains the following XAML structure:
 * 
 * <Window x:Class="WebContentGrabber.MainWindow"
 *         xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
 *         xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
 *         Title="Web Content Grabber" Height="450" Width="800">
 *     <Grid>
 *         <Grid.RowDefinitions>
 *             <RowDefinition Height="Auto"/>
 *             <RowDefinition Height="*"/>
 *         </Grid.RowDefinitions>
 *         <TextBox x:Name="UrlTextBox" Margin="10" />
 *         <Button Content="Fetch Content" Click="FetchContentButton_Click" Margin="10" />
 *         <TextBox x:Name="ContentTextBox" Grid.Row="1" Margin="10" TextWrapping="Wrap" AcceptsReturn="True" IsReadOnly="True" />
 *     </Grid>
# 扩展功能模块
 * </Window>
# FIXME: 处理边界情况
 */