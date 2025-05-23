using System;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Interactivity;
using Avalonia.ReactiveUI;
using InterpoleApp.Models;
using InterpoleApp.ViewModels;

namespace InterpoleApp.Views;

public partial class LookCriminalView : ReactiveUserControl<LookCriminalViewModel>
{
    public LookCriminalView()
    {
        Console.WriteLine("LookCriminalView loaded");
        InitializeComponent(); 
    }

    private void BackButton_Click(object? sender, RoutedEventArgs e)
    {
        if (App.Current.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            if (desktop.MainWindow?.DataContext is MainViewModel main)
            {
                // Назначаем новый CriminalListView с нужным ViewModel
                var listView = new CriminalListView
                {
                    DataContext = new CriminalListViewModel() 
                };
                main.CriminalListVM.SaveAllCriminals();
                main.CurrentUser = listView;
                main.CurrentPage = null;
                main.NotifyDisplayedContentChanged();
            }
        }
    }

    private void AddEvidenceBtn_Click(object? sender, RoutedEventArgs e)
    {
        if (ViewModel is { } vm && VisualRoot is Window owner)
        {
             vm.AddEvidDialog(owner);
        }
    }

    private void EditEvidenceBtn_Click(object? sender, RoutedEventArgs e)
    {
        if (ViewModel is { } vm && VisualRoot is Window owner)
        {
            if (sender is TextBlock tb && tb.DataContext is Evidence evidence)
            {
                Console.WriteLine("EditEvidenceBtn_Click " + evidence.Title);
                vm.EditViaDialog(owner,evidence);
            }
            
        }
    }
}