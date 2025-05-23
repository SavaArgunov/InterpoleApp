using System;
using System.Collections.ObjectModel;
using System.Reactive;
using InterpoleApp.Models;
using InterpoleApp.Models.Enums;
using ReactiveUI;

namespace InterpoleApp.ViewModels;

public class AddCriminalViewModel : ReactiveObject
{
    public ObservableCollection<EyeColor> Categories { get; }
    public ObservableCollection<CrimeType> crimeCategories { get; }
    public string Name { get; set; } = "";
    public string secondName { get; set; } = "";
    public string Country { get; set; } = "";
    public int DangerLevel { get; set; } = 1;
    public int size { get; set; }
    private EyeColor _eyeColor;
    public EyeColor EyeColor
    {
        get => _eyeColor;
        set => this.RaiseAndSetIfChanged(ref _eyeColor, value);
    }
    private CrimeType _crimeType;
    public CrimeType CrimeType
    {
        get => _crimeType;
        set => this.RaiseAndSetIfChanged(ref _crimeType, value);
    }
    public ReactiveCommand<Unit, Criminal> SaveCommand { get; }
    public AddCriminalViewModel()
    {
        Categories = new ObservableCollection<EyeColor>(
            Enum.GetValues(typeof(EyeColor)) as EyeColor[]
        );
        crimeCategories = new ObservableCollection<CrimeType>(
            Enum.GetValues(typeof(CrimeType)) as CrimeType[]
        );
        SaveCommand = ReactiveCommand.Create(() => new Criminal
        {
            EyesColor = EyeColor,
            CrimeType = CrimeType,
            Name = Name,
            secondName = secondName,
            Country = Country,
            DangerLevel = DangerLevel,
            Size = size
        });
    }
}