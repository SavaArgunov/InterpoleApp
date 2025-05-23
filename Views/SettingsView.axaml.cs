using InterpoleApp.ViewModels;
using Avalonia.Controls;
using InterpoleApp.Views;
using ReactiveUI;
using Avalonia.ReactiveUI;
using System;
using System.Reactive.Disposables;

namespace InterpoleApp.Views;

public partial class SettingsView : ReactiveUserControl<SettingsViewModel>
{
    public SettingsView()
    {
        InitializeComponent();
        ViewModel = new SettingsViewModel();
        this.WhenActivated((CompositeDisposable disposables) =>
        {
            // Пример: подписка на свойство
            this.WhenAnyValue(x => x.ViewModel!.Title)
                .Subscribe(title => Console.WriteLine($"Title изменён: {title}"))
                .DisposeWith(disposables);
        });
    }
}