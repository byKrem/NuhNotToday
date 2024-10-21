using Presentation.Components;
using System;
using System.Collections.ObjectModel;
using System.Windows.Forms;
using System.Windows.Input;

namespace Presentation.ViewModels
{
    internal class MainWindowViewModel
    {
        public ObservableCollection<string> FilePath;

        public ICommand ShutdownCommand;
        public ICommand HideToTrayCommand;
        public ICommand SelectFilePathCommand;
        public ICommand SelectFolderPathCommand;

        public MainWindowViewModel() 
        {
            ShutdownCommand = new RelayCommand(Shutdown);
            HideToTrayCommand = new RelayCommand(HideToTray);
            SelectFilePathCommand = new RelayCommand(SelectFilePath);
            SelectFolderPathCommand = new RelayCommand(SelectFolderPath);
        }

        private void Shutdown(object parameter)
        {
            App.Current.Shutdown();
        }

        private void HideToTray(object parameter)
        {
            App.Current.MainWindow.Hide();
            NotifyIcon ni = new NotifyIcon();
            ni.Icon = new System.Drawing.Icon("Main.ico");
            ni.Visible = true;
            ni.DoubleClick +=
                delegate (object sender, EventArgs args)
                {
                    App.Current.MainWindow.Show();
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
                Filter = "*.exe"
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
