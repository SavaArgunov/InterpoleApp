using Avalonia.Controls;
using InterpoleApp.ViewModels;
using ReactiveUI;
using Avalonia.ReactiveUI;

namespace InterpoleApp.Views;

public partial class MainWindow : ReactiveWindow<MainViewModel>
{
    public MainWindow()
    {
        InitializeComponent();
        DataContext = new MainViewModel();
        this.WhenActivated(disposables =>
        {
            // Привязки, если нужны
        }); 
    }
}