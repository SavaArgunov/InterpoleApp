using System;
using System.Runtime.CompilerServices;
using InterpoleApp.Models;
using InterpoleApp.Models.Enums;

namespace InterpoleApp.ViewModels;

public class LookComplainViewModel : ViewModelBase
{
    public Guid Id { get; } = Guid.NewGuid();
    public string title { get; set; } = "";
    public string country { get; set; } = "";
    public string department { get; set; } = "";
    public string description { get; set; } = "";
    public Criminal criminal { get; set; }
    public LookComplainViewModel(Complaint complaint)
    {
        title = complaint.Title;
        country = complaint.Country;
        department = complaint.Department;
        description = complaint.Description;
        criminal = complaint.RelatedCriminal;
    }
}