// 代码生成时间: 2025-10-05 18:54:30
using System;
using System.Windows;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace WpfRestfulApiApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private HttpClient httpClient;

        public MainWindow()
        {
            InitializeComponent();
            httpClient = new HttpClient();
        }

        private async void GetButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                HttpResponseMessage response = await httpClient.GetAsync(