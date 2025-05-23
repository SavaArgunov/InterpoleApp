using System;
using System.Collections.ObjectModel;
using System.Reactive;
using InterpoleApp.Models;
using ReactiveUI;
using System.Reactive.Linq;
using InterpoleApp.Models.Enums;


namespace InterpoleApp.ViewModels;

public class EditEvidenceViewModel : ReactiveObject
{
    public ObservableCollection<EvidenceType> Categories { get; }
    private string _title;
    private string _description;
    private string _criminalName;
    private EvidenceType _evidenceType;

    public string title
    {
        get => _title;
        set => this.RaiseAndSetIfChanged(ref _title, value);
    }
    public string description
    {
        get => _description;
        set => this.RaiseAndSetIfChanged(ref _description, value);
    }
    public string criminalName
    {
        get => _criminalName;
        set => this.RaiseAndSetIfChanged(ref _criminalName, value);
    }
    public EvidenceType evidenceType
    {
        get => _evidenceType;
        set => this.RaiseAndSetIfChanged(ref _evidenceType, value);
    }
    private readonly Evidence _evidence;
    public ReactiveCommand<Unit, Evidence> SaveCommand { get; }

    public EditEvidenceViewModel(Evidence existingEvidence)
    {
        _evidence = existingEvidence;
        Console.WriteLine("EditEvidenceViewModel " + _evidence.Title);
        Categories = new ObservableCollection<EvidenceType>(
            Enum.GetValues(typeof(EvidenceType)) as EvidenceType[]
        );
        title = existingEvidence.Title;
        description = existingEvidence.Description;
        //criminalName = existingEvidence.RelatedCriminal.FullName;
        SaveCommand = ReactiveCommand.Create(() =>
        {
            _evidence.Title = this.title;
            _evidence.Description = this.description;
            _evidence.EvidenceType = this.evidenceType;
            //_evidence.RelatedCriminal.Name = this.criminalName;
            return _evidence;
        });
    }
}