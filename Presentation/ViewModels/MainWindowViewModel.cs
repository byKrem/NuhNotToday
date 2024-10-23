using Presentation.Components;
using Presentation.Properties;
using System;
using System.Collections.ObjectModel;
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
            // TODO: Make a unified class for such type of information, so I don't need to manually set choosed resource in "every single file"
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

                FilePath.Add(folderBrowserDialog.SelectedPath);
            }
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
