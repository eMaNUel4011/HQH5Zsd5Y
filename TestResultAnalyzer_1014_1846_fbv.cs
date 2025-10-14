// 代码生成时间: 2025-10-14 18:46:41
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;

// 定义一个用于测试结果分析的类
public class TestResultAnalyzer
{
    // 构造函数，接收测试结果文件的路径
    public TestResultAnalyzer(string filePath)
    {
        if (string.IsNullOrEmpty(filePath))
            throw new ArgumentException("File path cannot be null or empty.", nameof(filePath));

        FilePath = filePath;
    }

    // 测试结果文件的路径
    public string FilePath { get; private set; }

    // 分析测试结果文件并返回分析结果
    public List<TestResult> AnalyzeResults()
    {
        try
        {
            // 读取文件内容
            string fileContent = File.ReadAllText(FilePath);

            // 使用正则表达式匹配测试结果
            var matches = Regex.Matches(fileContent, "Test: (?<testName>[^\s]+)\s+Result: (?<result>[^\s]+)");

            // 创建测试结果列表
            var results = new List<TestResult>();
            foreach (Match match in matches)
            {
                // 如果匹配成功，则创建测试结果对象并添加到列表中
                if (match.Success)
                {
                    results.Add(new TestResult
                    {
                        TestName = match.Groups["testName"].Value,
                        Result = match.Groups["result"].Value
                    });
                }
            }

            return results;
        }
        catch (Exception ex)
        {
            // 错误处理
            MessageBox.Show($"Error analyzing test results: {ex.Message}");
            return null;
        }
    }
}

// 定义测试结果类
public class TestResult
{
    // 测试名称
    public string TestName { get; set; }

    // 测试结果
    public string Result { get; set; }
}

// 定义WPF窗口类
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    private void AnalyzeResultsButton_Click(object sender, RoutedEventArgs e)
    {
        // 获取用户输入的文件路径
        string filePath = ResultsFilePathTextBox.Text;

        // 创建测试结果分析器实例
        TestResultAnalyzer analyzer = new TestResultAnalyzer(filePath);

        // 分析测试结果
        List<TestResult> results = analyzer.AnalyzeResults();

        if (results != null)
        {
            // 显示测试结果
            ResultsListBox.ItemsSource = results;
        }
    }
}