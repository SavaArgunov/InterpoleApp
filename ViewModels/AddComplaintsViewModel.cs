using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reactive;
using System.Threading.Tasks;
using Avalonia.Controls;
using InterpoleApp.Database;
using InterpoleApp.Models;
using ReactiveUI;
using InterpoleApp.ViewModels;
using InterpoleApp.Views;

namespace InterpoleApp.ViewModels;

public class AddComplaintsViewModel : ReactiveObject
{
    public string Title { get; set; } = "";
    public string Department { get; set; } = "";
    public string Country { get; set; } = "";
    public string Description { get; set; } = "";
    public Criminal? RelatedCriminal { get; set; }
    public ReactiveCommand<Unit, Complaint> SaveCommand { get; }
    public ReactiveCommand<Unit, Criminal> AddNewCriminalCommand { get;}
    public ObservableCollection<Criminal> CriminalsList { get; set; } = new();
    public JsonStorageService jsonService = new JsonStorageService();
    public AddComplaintsViewModel()
    {
        Load();
        SaveCommand = ReactiveCommand.Create(() => new Complaint
        {
            Title = Title,
            Department = Department,
            Country = Country,
            Description = Description,
            RelatedCriminal = RelatedCriminal
        });
    }

    private async void Load()
    {
        await InitBase();
        Console.WriteLine("Loaded: " + CriminalsList.Count + " criminals"); 
    }
    private async Task InitBase()
    {
        
        var loaded = await jsonService.GetAllAsync<Criminal>();

        // Копируем в ObservableCollection
        this.CriminalsList.Clear();
        foreach (var c in loaded)
        {
            Console.WriteLine(c.Name);
            this.CriminalsList.Add(c);
        }
        await jsonService.SaveAllAsync(this.CriminalsList.ToList());
        Console.WriteLine("Сохраняем в: " + Path.GetFullPath("Database/criminals.json"));
    }

    public async Task AddNewCriminal(Window owner)
    {
        var win = new AddCriminalView(); // не DataContext вручную, всё уже делается внутри ShowDialogAsync
        Criminal? result = await win.ShowDialogAsync(owner);

        if (result is not null)
        {
            CriminalsList.Add(result);
            RelatedCriminal = result;
        }
    }
}