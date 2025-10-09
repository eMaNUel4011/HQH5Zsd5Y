// 代码生成时间: 2025-10-09 22:21:39
using System;
using System.IO;
using System.Windows;

namespace FileWatcherApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private FileSystemWatcher _fileWatcher;
        private string _watchedPath;
        private string _filter;

        public MainWindow()
        {
            InitializeComponent();
            _watchedPath = @"C:\path	o\directory"; // Set the directory to watch
            _filter = "*.txt"; // Set the file filter
            InitializeWatcher();
        }

        /// <summary>
        /// Initializes the file system watcher.
        /// </summary>
        private void InitializeWatcher()
        {
            _fileWatcher = new FileSystemWatcher(_watchedPath, _filter)
            {
                NotifyFilter = NotifyFilters.LastWrite | NotifyFilters.FileName | NotifyFilters.DirectoryName,
                IncludeSubdirectories = true
            };

            // Event handlers
            _fileWatcher.Changed += OnChanged;
            _fileWatcher.Created += OnChanged;
            _fileWatcher.Deleted += OnChanged;
            _fileWatcher.Renamed += OnRenamed;
            _fileWatcher.Error += OnError;

            _fileWatcher.EnableRaisingEvents = true;
        }

        /// <summary>
        /// Event handler for file changes.
        /// </summary>
        /// <param name="source">The source of the event.</param>
        /// <param name="e">The event arguments.</param>
        private void OnChanged(object source, FileSystemEventArgs e)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                MessageBox.Show($"File: {e.FullPath} has been changed");
            });
        }

        /// <summary>
        /// Event handler for file renames.
        /// </summary>
        /// <param name="source">The source of the event.</param>
        /// <param name="e">The event arguments.</param>
        private void OnRenamed(object source, RenamedEventArgs e)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                MessageBox.Show($"File: {e.OldFullPath} has been renamed to {e.FullPath}");
            });
        }

        /// <summary>
        /// Event handler for errors.
        /// </summary>
        /// <param name="source">The source of the event.</param>
        /// <param name="e">The event arguments.</param>
        private void OnError(object source, ErrorEventArgs e)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                MessageBox.Show($"Error: {e.GetException().Message}");
            });
        }
    }
}
