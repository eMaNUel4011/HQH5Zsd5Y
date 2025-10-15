// 代码生成时间: 2025-10-15 19:14:57
using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace FileSplitMergeTool
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

        private void SplitFileButton_Click(object sender, RoutedEventArgs e)
        {
            // Open file dialog to select the file to split
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "All files (*.*)|*.*";
            if (openFileDialog.ShowDialog() == true)
            {
                string filePath = openFileDialog.FileName;
                string fileName = Path.GetFileNameWithoutExtension(filePath);
                string fileExtension = Path.GetExtension(filePath);
                int partsCount = 0;

                // Get the number of parts from user input
                if (!int.TryParse(SplitPartsTextBox.Text, out partsCount) || partsCount <= 0)
                {
                    MessageBox.Show("Please enter a valid number of parts.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                SplitFile(filePath, fileName, fileExtension, partsCount);
            }
        }

        private void MergeFilesButton_Click(object sender, RoutedEventArgs e)
        {
            // Open folder dialog to select the directory with parts to merge
            var dialog = new Ookii.Dialogs.Wpf.VistaFolderBrowserDialog();
            if (dialog.ShowDialog() == true)
            {
                string directoryPath = dialog.SelectedPath;
                string fileName = MergeFileNameTextBox.Text;
                string fileExtension = Path.GetExtension(fileName);

                if (string.IsNullOrWhiteSpace(fileName))
                {
                    MessageBox.Show("Please enter a valid file name for the merged file.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                MergeFiles(directoryPath, fileName, fileExtension);
            }
        }

        private void SplitFile(string filePath, string fileName, string fileExtension, int partsCount)
        {
            try
            {
                using (FileStream fileStream = File.OpenRead(filePath))
                {
                    long fileSize = fileStream.Length;
                    long partSize = fileSize / partsCount;

                    for (int i = 0; i < partsCount; i++)
                    {
                        string partFilePath = Path.Combine(Path.GetDirectoryName(filePath), $