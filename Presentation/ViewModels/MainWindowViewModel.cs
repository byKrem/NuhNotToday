using Presentation.Components;
using Presentation.Properties;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Input;

namespace Presentation.ViewModels
{
    internal class MainWindowViewModel : ViewModelBase
    {
        private ObservableCollection<string> _filePath;
        public ObservableCollection<string> FilePath
        {
            get => _filePath;
            set => Set(ref _filePath, ref value);
        }

        private ICommand _shutdownCommand;
        private ICommand _hideToTrayCommand;
        private ICommand _selectFilePathCommand;
        private ICommand _selectFolderPathCommand;

        public ICommand ShutdownCommand
        {
            get => _shutdownCommand;
            set => Set(ref _shutdownCommand, ref value);
        }
        public ICommand HideToTrayCommand
        {
            get => _hideToTrayCommand;
            set => Set(ref _hideToTrayCommand, ref value);
        }
        public ICommand SelectFilePathCommand
        {
            get => _selectFilePathCommand;
            set => Set(ref _selectFilePathCommand, ref value);
        }
        public ICommand SelectFolderPathCommand
        {
            get => _selectFolderPathCommand;
            set => Set(ref _selectFolderPathCommand, ref value);
        }

        public MainWindowViewModel() 
        {
            ShutdownCommand = new RelayCommand(Shutdown);
            HideToTrayCommand = new RelayCommand(HideToTray);
            SelectFilePathCommand = new RelayCommand(SelectFilePath);
            SelectFolderPathCommand = new RelayCommand(SelectFolderPath);
            FilePath = new ObservableCollection<string>();
        }

        private void Shutdown(object parameter)
        {
            App.Current.Shutdown();
        }

        private void HideToTray(object parameter)
        {
            App.Current.MainWindow.Hide();
            NotifyIcon ni = new NotifyIcon();
            // TODO: Replace a placeholder with an actual icon.
            // TODO: Make a separate class for such type of information, so I don't need to manually set choosed resource in "every single file"
            ni.Icon = Resources.placeholder;
            ni.Visible = true;
            ni.DoubleClick +=
                delegate (object sender, EventArgs args)
                {
                    App.Current.MainWindow.Show();
                    ni.Visible = false;
                };
        }

        private void SelectFolderPath(object parameter)
        {
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog()
            {
                RootFolder = Environment.SpecialFolder.MyComputer
            };

            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                foreach(string path in TreeSearch(folderBrowserDialog.SelectedPath))
                {
                    FilePath.Add(path);
                }
            }
        }

        // TODO: Too slow... It takes almost a minute on my PC to scan 3_107 folders and 123_663 files... Maybe I can make it faster?
        // TODO: Also I need to somehow make it not blocking UI thread
        private string[] TreeSearch(string folderPath)
        {
            if (string.IsNullOrWhiteSpace(folderPath))
            {
                return new string[0];
            }

            DirectoryInfo folderInfo = new DirectoryInfo(folderPath);

            return folderInfo.GetFiles("*.exe", SearchOption.AllDirectories).AsParallel().Select(x => x.FullName).ToArray();
        }

        private void SelectFilePath(object parameter)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog() 
            {
                InitialDirectory = @"C:\",
                Multiselect = true,
                Filter = "Executable files (*.exe)|*.exe"
            };

            if(openFileDialog.ShowDialog() == DialogResult.OK)
            {
                foreach (var file in openFileDialog.FileNames)
                {
                    FilePath.Add(file);
                }
            }
        }
    }
}
