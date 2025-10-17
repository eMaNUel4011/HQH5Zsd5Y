// 代码生成时间: 2025-10-17 19:22:49
using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows;

namespace UrlValidatorApp
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private async void ValidateButton_Click(object sender, RoutedEventArgs e)
        {
            // 获取输入的URL
            string url = UrlTextBox.Text;

            // 检查URL是否为空
            if (string.IsNullOrWhiteSpace(url))
            {
                MessageBox.Show("请输入URL", "验证失败", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                // 使用HttpClient验证URL的有效性
                using (HttpClient client = new HttpClient())
                {
                    HttpResponseMessage response = await client.GetAsync(url);

                    // 如果响应状态码为200，则URL有效
                    if (response.IsSuccessStatusCode)
                    {
                        MessageBox.Show("URL有效", "验证成功", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    else
                    {
                        MessageBox.Show("URL无效", "验证失败", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                }
            }
            catch (HttpRequestException ex)
            {
                // 处理请求异常
                MessageBox.Show($"请求异常: {ex.Message}", "验证失败", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                // 处理其他异常
                MessageBox.Show($"未知异常: {ex.Message}", "验证失败", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}