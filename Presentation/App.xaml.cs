using Presentation.ViewModels;
using Presentation.Views;
using System.Windows;

namespace Presentation
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            MainWindow window = new MainWindow();
            window.DataContext = new MainWindowViewModel();

            window.Show();
        }
    }
}
