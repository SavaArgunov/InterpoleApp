using System;
using ReactiveUI;
using System.Reactive;
using System.Threading.Tasks;
using Avalonia.Controls;
using InterpoleApp.Models;
using InterpoleApp.Systems;
using InterpoleApp.ViewModels;
using InterpoleApp.Views;

namespace InterpoleApp.ViewModels;

public class MainViewModel : ReactiveObject
{
    public CriminalListViewModel CriminalListVM { get; } = new CriminalListViewModel();
    public ComplaintsViewModel ComplaintsVM { get; } = new ComplaintsViewModel();
    public object DisplayedContent
    {
        get
        {
            Console.WriteLine("DisplayedContent вызван: " + (CurrentUser?.GetType().Name ?? CurrentPage?.GetType().Name));
            return (object?)CurrentUser ?? CurrentPage;
        }
    }
    private UserControl? _currentUser;
    public UserControl CurrentUser
    {
        get => _currentUser;
        set => this.RaiseAndSetIfChanged(ref _currentUser, value);
    }
    private ReactiveObject _currentPage;
    public ReactiveObject CurrentPage
    {
        get => _currentPage;
        set => this.RaiseAndSetIfChanged(ref _currentPage, value);
    }

    public ReactiveCommand<Unit, Unit> ShowHomeCommand { get; }
    public ReactiveCommand<Unit, Unit> ShowCriminalListCommand { get; }
    public ReactiveCommand<Unit, Unit> ShowComplaintsCommand { get; }

    public void NotifyDisplayedContentChanged()
    {
        this.RaisePropertyChanged(nameof(DisplayedContent));
    }
    public MainViewModel()
    {
        InitializeAsync();
        this.WhenAnyValue(x => x.CurrentUser, x => x.CurrentPage)
            .Subscribe(_ => this.RaisePropertyChanged(nameof(DisplayedContent)));
        ShowHomeCommand = ReactiveCommand.Create(() =>
        {
            CurrentUser = new HomeView
            {
                DataContext = new HomeViewModel()
            };
            CurrentPage = null;
            this.RaisePropertyChanged(nameof(DisplayedContent));
        });

        ShowCriminalListCommand = ReactiveCommand.Create(() =>
        {
            CurrentUser = new CriminalListView
            {
                DataContext = new CriminalListViewModel()
            };
            CurrentPage = null;
            this.RaisePropertyChanged(nameof(DisplayedContent));
        });
        ShowComplaintsCommand = ReactiveCommand.Create(() =>
        {
            CurrentUser = new ComplaintsView
            {
                DataContext = new ComplaintsViewModel()
            };
            CurrentPage = null;
            this.RaisePropertyChanged(nameof(DisplayedContent));
        });
        
        // Показываем главную по умолчанию
        CurrentPage = new CriminalListViewModel();
    }
    public void ShowCriminalDetails(Criminal criminal)
    {
        CurrentUser = new CriminalListView();
        CurrentPage = null;
        this.RaisePropertyChanged(nameof(CurrentUser));
    }
    public async Task InitializeAsync()
    {
        var service = new EvidenceService();
        await service.LoadAllAsync();
    }    
}