using InterpoleApp.ViewModels;
using Avalonia.Controls;
using InterpoleApp.Views;
using ReactiveUI;
using Avalonia.ReactiveUI;
using System;
using System.Linq;
using System.Reactive.Disposables;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Interactivity;
using InterpoleApp.Models;
using InterpoleApp.ViewModels;

namespace InterpoleApp.Views;

public partial class CriminalListView : ReactiveUserControl<CriminalListViewModel>
{
    public CriminalListView()
    {
        InitializeComponent();
        ViewModel = new CriminalListViewModel();
        DataContext = ViewModel;
        ViewModel.UpdateList();
        this.WhenActivated((CompositeDisposable disposables) =>
        {
            this.BindCommand(ViewModel, vm => vm.AddCriminalCommand, v => v.AddButton)
                .DisposeWith(disposables);
        });
    }
    private async void AddButton_Click(object? sender, RoutedEventArgs e)
    {
        if (ViewModel is { } vm && VisualRoot is Window owner)
        {
            await vm.AddViaDialog(owner);
        }
    }

    private void LookButton_Click(object? sender, RoutedEventArgs e)
    {
        if (sender is TextBlock tb && tb.DataContext is Criminal criminal)
        {
            // Найдём MainViewModel через DisplayRoot
            if (DataContext is CriminalListViewModel && App.Current.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                // Здесь предполагаем, что DataContext окна — это MainViewModel
                if (desktop.MainWindow?.DataContext is MainViewModel main)
                {
                    main.CurrentUser = new LookCriminalView
                    {
                        DataContext = new LookCriminalViewModel(criminal)
                    };
                    main.CurrentPage = null;
                    main.RaisePropertyChanged(nameof(main.DisplayedContent));
                }
            }
        }
    }
    private async void EditButton_Click(object? sender, RoutedEventArgs e)
    {
        Console.WriteLine("Edit button clicked");
        if (sender is Button btn && btn.DataContext is Criminal criminal &&
            ViewModel is { } vm && VisualRoot is Window owner)
        {
                await vm.EditViaDialog(owner, criminal);
        }
    }
    private async void OnDeleteClick(object? sender, RoutedEventArgs e)
    {
        if (sender is Button button && button.DataContext is Criminal criminal)
        {
            ViewModel?.Criminals.Remove(criminal);
            await ViewModel!.jsonService.SaveAllAsync(ViewModel.Criminals.ToList());
            ViewModel?.UpdateList();
        }
    }
}