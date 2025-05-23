using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using InterpoleApp.Database;
using InterpoleApp.Models;
using InterpoleApp.Models.Enums;
using ReactiveUI;
using InterpoleApp.Views;

namespace InterpoleApp.ViewModels;

public class ComplaintsViewModel : ReactiveObject
{
    
    public ObservableCollection<Complaint> Complaints { get; } = new();
    public ObservableCollection<Complaint> FilteredComplaints { get; } = new();

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
    public ComplaintsViewModel()
    {
        Criminal criminal = new Criminal
        {
            Name = "Яшув",
            secondName = "Жецкий",
            Country = "Польша",
            CrimeType = CrimeType.Вбивство,
            DangerLevel = 2,
            EyesColor = EyeColor.Зелений,
        };
    
        Complaint complaint = new Complaint
        {
            Country = "Польша",
            Department = "Policja",
            Title = "Жалоба на угрозы",
            Description = "Подозреваемый угрожал свидетелю...",
            Date = DateTime.Now,
            RelatedCriminal = criminal
        };
        Complaints.Add(complaint);
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
            //await _jsonService.SaveAllAsync(Criminals.ToList());
        }
    }
    public void UpdateList()
    {
        //Console.WriteLine("UpdateList() called " + DateTime.Now);
        FilteredComplaints.Clear();
        foreach (var item in Complaints)
        {
            FilteredComplaints.Add(item);
        }
    }
    // private void OpenCriminalCard(Criminal criminal)
    // {
    //     if (App.Current.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop &&
    //         desktop.MainWindow?.DataContext is MainViewModel main)
    //     {
    //         main.CurrentUser = new CriminalDetailView
    //         {
    //             DataContext = new CriminalDetailViewModel(criminal)
    //         };
    //         main.CurrentPage = null;
    //         main.RaisePropertyChanged(nameof(main.DisplayedContent));
    //     }
    // }
}