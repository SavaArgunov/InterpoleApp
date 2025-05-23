using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
//using Avalonia.Controls.Shapes;
using InterpoleApp.Database;
using InterpoleApp.Models;
using InterpoleApp.Models.Enums;
using InterpoleApp.Systems;
using ReactiveUI;
using InterpoleApp.Views;

namespace InterpoleApp.ViewModels;

public class ComplaintsViewModel : ReactiveObject
{
    
    public ObservableCollection<Complaint> Complaints { get; } = new();
    public ObservableCollection<Complaint> FilteredComplaints { get; } = new();
    public JsonStorageService jsonService = new JsonStorageService();
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

    private Complaint? _selectedComplaint;
    public Complaint? SelectedComplaint
    {
        get => _selectedComplaint;
        set
        {
            this.RaiseAndSetIfChanged(ref _selectedComplaint, value);
            if (value?.RelatedCriminal != null)
            {
                // OpenCriminalCard(value.RelatedCriminal);
            }
        }
    }
    private async void Load()
    {
        await InitBase();
    }
    private async Task InitBase()
    {
        
        var loaded = await jsonService.GetAllAsync<Complaint>();

        // Копируем в ObservableCollection
        this.Complaints.Clear();
        foreach (var c in loaded)
            this.Complaints.Add(c);
        
        
        await jsonService.SaveAllAsync(this.Complaints.ToList());
        Console.WriteLine("Сохраняем в: " + Path.GetFullPath("Database/complaints.json"));
        UpdateList();
    }
    public ComplaintsViewModel()
    {
        Load();
        Console.WriteLine(Complaints.Count);
        UpdateList();
    }
    public async Task AddViaDialog(Window owner)
    {
        int newId = Complaints.Any() ? Complaints.Max(c => c.ComplaintId) + 1 : 1;
        var _jsonService = new JsonStorageService();
        var dialog = new AddComplaintsView();
        var complaint = await dialog.ShowDialogAsync(owner);

        if (complaint != null)
        {
            complaint.ComplaintId = newId;
            Console.WriteLine("Adding new complaint " + complaint.Title);
            Complaints.Add(complaint);
            UpdateList();
            await _jsonService.SaveAllAsync(Complaints.ToList());
        }
    }
    private void ApplySearch()
    {
        var filtered = Levenshtein.ApplySearch(
            Complaints,
            c => c.Title,
            SearchQuery
        );

        FilteredComplaints.Clear();
        foreach (var c in filtered)
            FilteredComplaints.Add(c);
    }
    public void UpdateList()
    {
        FilteredComplaints.Clear();
        foreach (var item in Complaints)
        {
            FilteredComplaints.Add(item);
        }
    }
}