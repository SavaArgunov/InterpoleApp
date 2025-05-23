using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using ReactiveUI;
using System.Reactive;
using Avalonia.Controls;
using InterpoleApp.Database;
using InterpoleApp.ViewModels;
using InterpoleApp.Views;
using InterpoleApp.Models;
using System.IO;
using System.Threading.Tasks;
using Avalonia.Data.Converters;
using InterpoleApp.Systems;

namespace InterpoleApp.ViewModels;

public class CriminalListViewModel : ViewModelBase
{
    private readonly MainViewModel _main;
    public List<int?> DangerLevels { get; } = new() { null, 1, 2, 3, 4, 5 };
    private int? _selectedDangerLevel;

    public int? SelectedDangerLevel
    {
        get => _selectedDangerLevel;
        set
        {
            
            this.RaiseAndSetIfChanged(ref _selectedDangerLevel, value);
            FilteringByDanger();
        }
    }
    private string _searchQuery = "";
    public string SearchQuery
    {
        get => _searchQuery;
        set
        {
            this.RaiseAndSetIfChanged(ref _searchQuery, value);
            ApplySearch();
        }
    }
    public ObservableCollection<Criminal> Criminals { get; set; } = new();
    public ObservableCollection<Criminal> FilteredCriminals { get; set; } = new();
    public ReactiveCommand<Unit, Unit> AddCriminalCommand { get; }
    public ReactiveCommand<Unit, Unit> DeleteCommand { get; }
    public ReactiveCommand<Unit, Unit> LookCommand { get; }
    public JsonStorageService jsonService = new JsonStorageService();
    private async Task InitBase()
    {
        
        var loaded = await jsonService.GetAllAsync<Criminal>();

        // Копируем в ObservableCollection
        this.Criminals.Clear();
        foreach (var c in loaded)
            this.Criminals.Add(c);
        
        
        await jsonService.SaveAllAsync(this.Criminals.ToList());
        Console.WriteLine("Сохраняем в: " + Path.GetFullPath("Database/criminals.json"));
        UpdateList();
    }
    
    public CriminalListViewModel()
    {
        Console.WriteLine($"Created CriminalListViewModel с ID: {this.GetHashCode()}");
        //_main = main;
        var jsonService = new JsonStorageService();
        Load();
    }

    private void DeleteCriminal(Criminal toDelete)
    {
        Criminals.Remove(toDelete);
        jsonService.SaveAllAsync(Criminals.ToList());
        UpdateList();
    }

    private async void Load()
    {
        await InitBase();
        Console.WriteLine("Loaded: " + Criminals.Count + " criminals"); 
        Console.WriteLine("FilteredCriminals: " + FilteredCriminals.Count);
    }
    public async Task AddViaDialog(Window owner)
    {
        //int newId = Criminals.Any() ? Criminals.Max(c => c.id) + 1 : 1;
        var _jsonService = new JsonStorageService();
        var dialog = new AddCriminalView();
        var criminal = await dialog.ShowDialogAsync(owner);

        if (criminal != null)
        {
            //criminal.id = newId;
            Criminals.Add(criminal);
            UpdateList();
            await _jsonService.SaveAllAsync(Criminals.ToList());
        }
    }
    public async Task EditViaDialog(Window owner, Criminal crimi)
    {
        var _jsonService = new JsonStorageService();
        var dialog = new EditCriminalView();
        var criminal = await dialog.ShowEdiotDialog(owner, crimi);
        UpdateList();
        await _jsonService.SaveAllAsync(Criminals.ToList());
    }

    public async Task Look(Window owner, Criminal criminal)
    {
        Console.WriteLine("Details");
        _main.ShowCriminalDetails(criminal);
        var _jsonService = new JsonStorageService();
    }
    private void ApplySearch()
    {
        var filtered = Levenshtein.ApplySearch(
            Criminals,
            c => c.Name,
            SearchQuery
        );

        FilteredCriminals.Clear();
        foreach (var c in filtered)
            FilteredCriminals.Add(c);
    }

    private void FilteringByDanger()
    {
        // Console.WriteLine("_selectedDangLev " + _selectedDangerLevel);
        // Console.WriteLine("SelectedDangLev " + SelectedDangerLevel);
        IEnumerable<Criminal> baseList = Criminals;

        if (SelectedDangerLevel is not null)
            baseList = baseList.Where(c => c.DangerLevel == SelectedDangerLevel);

        FilteredCriminals.Clear(); // ← сначала очищаем
        foreach (var c in baseList.ToList())
        {
            Console.WriteLine(c.Name);
            FilteredCriminals.Add(c);
        }
    }

    public void UpdateList()
    {
        FilteredCriminals.Clear();
        foreach (var item in Criminals)
        {
            FilteredCriminals.Add(item);
        }
    }

    public async Task SaveAllCriminals()
    {
        await jsonService.SaveAllAsync(Criminals.ToList());
    }
}