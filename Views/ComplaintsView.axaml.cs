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

public partial class ComplaintsView : ReactiveUserControl<ComplaintsViewModel>
{
    public ComplaintsView()
    {
        InitializeComponent();
    }
    private async void AddButton_Click(object? sender, RoutedEventArgs e)
    {
        if (ViewModel is { } vm && VisualRoot is Window owner)
        {
            await vm.AddViaDialog(owner);
        }
    }
}