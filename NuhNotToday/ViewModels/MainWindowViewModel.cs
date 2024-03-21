using ReactiveUI;
using Avalonia.Controls;
using Avalonia.Platform.Storage;
using System.Threading.Tasks;
using System.Diagnostics;

namespace NuhNotToday.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
#pragma warning disable CA1822 // Mark members as static
    public string Greeting => "Welcome to Avalonia!";
#pragma warning restore CA1822 // Mark members as static

    public ReactiveCommand<Window, Task> ChooseFolderCommand {get;}

    public MainWindowViewModel()
    {
        ChooseFolderCommand = ReactiveCommand.Create(async (Window window) => {
            
            FolderPickerOpenOptions pickerOptions = new()
            {
                Title = "Choose folders",
                AllowMultiple = true
            };

            var result = await window.StorageProvider.OpenFolderPickerAsync(pickerOptions);

            Debug.Write(result);
        });
    }

}
