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
    public ObservableCollection<Evidence> Evidences => Criminal.Evidences;
    public LookCriminalViewModel(Criminal criminal)
    {
        Criminal = criminal;
        Console.WriteLine("Criminal has bid " + Criminal.Evidences.Count + " small has " + criminal.Evidences.Count);
    }
    public async Task AddEvidDialog(Window owner)
    {
        var _jsonService = new JsonStorageService();
        var dialog = new AddEvidenceView();
        var evid = await dialog.ShowDialogAsync(owner);

        if (evid != null && App.Current.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop &&
            desktop.MainWindow?.DataContext is MainViewModel main)
        {
            var target = main.CriminalListVM.Criminals.FirstOrDefault(c => c.id == Criminal.id);
            //evid.RelatedCriminal = Criminal;
            Console.WriteLine("Before add: Criminal in list?");
            if (target is not null)
            {
                target.Evidences.Add(evid);
                await main.CriminalListVM.SaveAllCriminals();
            }
            Console.WriteLine("After add:");
            await main.CriminalListVM.SaveAllCriminals();
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
}