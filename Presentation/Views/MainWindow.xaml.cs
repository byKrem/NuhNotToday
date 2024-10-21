using System.Windows;
using System.Windows.Input;

namespace Presentation.Views
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void MoveWindow(object sender, MouseButtonEventArgs e)
        {
            if(MouseButtonState.Pressed == e.LeftButton)
            {
                this.DragMove();
            }
        }
    }
}
