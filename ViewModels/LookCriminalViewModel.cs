using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using InterpoleApp.Database;
using InterpoleApp.Models;
using InterpoleApp.Models.Enums;
using InterpoleApp.Views;
using InterpoleApp.ViewModels;
using Evidence = InterpoleApp.Models.Evidence;

namespace InterpoleApp.ViewModels;

public class LookCriminalViewModel : ViewModelBase
{
    public Guid Id { get; } = Guid.NewGuid();
    public Criminal Criminal { get;}
    public string fullName { get; set; } = "";
    public string country { get; set; } = "";
    public int DangerLevel { get; set; } = 0;
    public int size { get; set; }
    public EyeColor EyeColor { get; set; }
    public CrimeType CrimeType { get; set; }
    public ObservableCollection<Evidence> Evidences { get; set; } = new();
    public JsonStorageService EvidenceStorageService = new JsonStorageService();
    public LookCriminalViewModel(Criminal criminal)
    {
        InitBase();
        Criminal = criminal;
        
    }
    public async Task AddEvidDialog(Window owner, Criminal criminal)
    {
        var _jsonService = new JsonStorageService();
        var dialog = new AddEvidenceView();
        var evid = await dialog.ShowDialogAsync(owner, criminal);
        if (evid != null && App.Current.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop &&
            desktop.MainWindow?.DataContext is MainViewModel main)
        {
            Console.WriteLine("Before add: Criminal in list?");
            Evidences.Add(evid);
            EvidenceStorageService.SaveAllAsync<Evidence>(Evidences.ToList());
        }
    }
    public async Task EditViaDialog(Window owner, Evidence evidence)
    {
        if (App.Current.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop &&
            desktop.MainWindow?.DataContext is MainViewModel main)
        {
            Console.WriteLine("evidtype before " + evidence.EvidenceType);
            var _jsonService = new JsonStorageService();
            var dialog = new EditEvidenceView();
            var evid = await dialog.ShowEdiotDialog(owner, evidence);
            Console.WriteLine("evidtype after " + evidence.EvidenceType);
            var index = Evidences.IndexOf(evidence);
            if (index >= 0 && evid != null)
            {
                Evidences[index] = evid;
            }
            await main.CriminalListVM.SaveAllCriminals();
        }
    }

    async Task InitBase()
    {
        var loaded = await EvidenceStorageService.GetAllAsync<Evidence>();
        Evidences.Clear();
        foreach (var c in loaded)
        {
            Evidences.Add(c);
        }
    }
}