// 代码生成时间: 2025-09-23 19:11:55
using System;
using System.IO;
using System.Linq;
using System.Windows;

namespace FolderOrganizerApp
{
    // MainWindow.xaml 的代码后台
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        // 整理文件夹结构的方法
        private void OrganizeFolders()
        {
            try
            {
                string sourcePath = txtSourcePath.Text; // 用户输入的源路径
                if (string.IsNullOrEmpty(sourcePath))
                {
                    MessageBox.Show("Please provide a valid source path.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                if (!Directory.Exists(sourcePath))
                {
                    MessageBox.Show(\$"The directory '{sourcePath}' does not exist.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                // 获取源路径下的所有文件和目录
                var allItems = Directory.EnumerateFileSystemEntries(sourcePath, "*", SearchOption.AllDirectories);

                // 进行文件夹结构整理
                foreach (var item in allItems)
                {
                    // 根据文件或目录的类型进行不同的处理
                    if (File.Exists(item))
                    {
                        // 移动文件到对应的子目录，这里以文件扩展名作为子目录名称
                        string extension = Path.GetExtension(item).TrimStart('.');
                        string destination = Path.Combine(sourcePath, extension);
                        Directory.CreateDirectory(destination); // 确保目标目录存在
                        string destinationFilePath = Path.Combine(destination, Path.GetFileName(item));
                        File.Move(item, destinationFilePath);
                    }
                    else if (Directory.Exists(item))
                    {
                        // 递归处理子目录
                        OrganizeFoldersInDirectory(item);
                    }
                }

                MessageBox.Show("Folder organization completed.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(\$"An error occurred: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // 递归整理目录的方法
        private void OrganizeFoldersInDirectory(string directoryPath)
        {
            try
            {
                var files = Directory.GetFiles(directoryPath);
                foreach (var file in files)
                {
                    string extension = Path.GetExtension(file).TrimStart('.');
                    string destination = Path.Combine(directoryPath, extension);
                    Directory.CreateDirectory(destination); // 确保目标目录存在
                    string destinationFilePath = Path.Combine(destination, Path.GetFileName(file));
                    File.Move(file, destinationFilePath);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(\$"An error occurred while organizing '{directoryPath}': {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
