using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Presentation.Components
{
    internal class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public void Set<T>(ref T oldValue, ref T newValue, [CallerMemberName] string prop = "")
        {
            oldValue = newValue;
            OnPropertyChanged(prop);
        }

        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
