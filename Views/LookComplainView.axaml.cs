using System;
using System.Linq;
using System.Reactive.Disposables;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Interactivity;
using Avalonia.ReactiveUI;
using InterpoleApp.Models;
using InterpoleApp.ViewModels;
using ReactiveUI;

namespace InterpoleApp.Views;

public partial class LookComplainView : ReactiveUserControl<LookComplainViewModel>
{
    public LookComplainView()
    {
        InitializeComponent();
    }

    private void MergeBtn_Click(object sender, RoutedEventArgs e)
    {
        Console.WriteLine("MergeBtn_Click");

        if (sender is Button btn && btn.Tag is Criminal criminal)
        {
            Console.WriteLine("Entered 1 MergeBtn_Click");
            Console.WriteLine(criminal.Name);

            if (App.Current.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop &&
                desktop.MainWindow?.DataContext is MainViewModel main)
            {
                var criminalListVM = main.CriminalListVM;
                
                    criminalListVM.Criminals.Add(criminal);
                    criminalListVM.jsonService.SaveAllAsync(criminalListVM.Criminals.ToList());
                    criminalListVM.UpdateList(); // если он делает фильтрацию или сортировку
                    
                    
                    var complaintVM = main.ComplaintsVM;

                    // Знайдемо скаргу, до якої був прив’язаний цей порушник
                    var toDelete = complaintVM.Complaints
                        .FirstOrDefault(c => c.RelatedCriminal?.id == criminal.id);

                    if (toDelete != null)
                    {
                        complaintVM.Complaints.Remove(toDelete);
                        complaintVM.jsonService.SaveAllAsync(complaintVM.Complaints.ToList());
                        complaintVM.UpdateList(); // якщо є фільтрація
                    }
            }
        }
        
    }
    private void LookBtn_Click(object? sender, RoutedEventArgs e)
    {
        Console.WriteLine("LookBtn_Click");
        if (sender is TextBlock tb && tb.Tag is Criminal criminal)
        {
            Console.WriteLine("Entred 1 LookBtn_Click");
            // Найдём MainViewModel через DisplayRoot
            if (DataContext is LookComplainViewModel && App.Current.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                Console.WriteLine("Entred 2 LookBtn_Click");
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
    private void BackButton_Click(object? sender, RoutedEventArgs e)
    {
        if (App.Current.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            if (desktop.MainWindow?.DataContext is MainViewModel main)
            {
                // Назначаем новый CriminalListView с нужным ViewModel
                var listView = new ComplaintsView
                {
                    DataContext = new ComplaintsViewModel()
                };

                main.CurrentUser = listView;
                main.CurrentPage = null;
                main.NotifyDisplayedContentChanged();
            }
        }
    }
    private void LookButton_Click(object? sender, RoutedEventArgs e)
    {
        if (sender is TextBlock tb && tb.DataContext is Complaint complaint)
        {
            // Найдём MainViewModel через DisplayRoot
            if (DataContext is CriminalListViewModel && App.Current.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                // Здесь предполагаем, что DataContext окна — это MainViewModel
                if (desktop.MainWindow?.DataContext is MainViewModel main)
                {
                    main.CurrentUser = new LookComplainView
                    {
                        DataContext = new LookComplainViewModel(complaint)
                    };
                    main.CurrentPage = null;
                    main.RaisePropertyChanged(nameof(main.DisplayedContent));
                }
            }
        }
    }
}