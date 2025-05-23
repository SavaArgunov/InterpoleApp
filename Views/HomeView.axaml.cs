using InterpoleApp.ViewModels;
using Avalonia.Controls;
using InterpoleApp.Views;
using ReactiveUI;
using Avalonia.ReactiveUI;
using System;
using System.Reactive.Disposables;

namespace InterpoleApp.Views;

public partial class HomeView : ReactiveUserControl<HomeViewModel>
{
    public HomeView()
    {
        InitializeComponent();
        ViewModel = new HomeViewModel();
        this.WhenActivated((CompositeDisposable disposables) =>
        {
            // this.BindCommand(ViewModel, vm => vm.SayHelloCommand, v => v.HelloButton)
            //     .DisposeWith(disposables);
        });
    }
}