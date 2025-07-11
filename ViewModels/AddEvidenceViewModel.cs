﻿using System;
using System.Collections.ObjectModel;
using System.Reactive;
using InterpoleApp.Models;
using InterpoleApp.Models.Enums;
using ReactiveUI;

namespace InterpoleApp.ViewModels;

public class AddEvidenceViewModel : ReactiveObject
{
    public ObservableCollection<EvidenceType> Categories { get; }
    public string title { get; set; } = "";
    public string description { get; set; } = "";
    public Guid relatedCriminalID { get; set; } = new();
    private EvidenceType _evidenceType;
    public EvidenceType evidenceType
    {
        get => _evidenceType;
        set => this.RaiseAndSetIfChanged(ref _evidenceType, value);
    }
    public ReactiveCommand<Unit, Evidence> SaveCommand { get; }
    public AddEvidenceViewModel(Criminal criminal)
    {
        relatedCriminalID = criminal.id;
        Categories = new ObservableCollection<EvidenceType>(
            Enum.GetValues(typeof(EvidenceType)) as EvidenceType[]
        );
        SaveCommand = ReactiveCommand.Create(() => new Evidence
        {
            Title = title,
            Description = description,
            RelatedCriminalId = relatedCriminalID,
            EvidenceType = evidenceType,
        });
    }
}