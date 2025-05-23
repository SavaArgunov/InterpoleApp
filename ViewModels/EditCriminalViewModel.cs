using System;
using System.Collections.ObjectModel;
using System.Reactive;
using InterpoleApp.Models;
using ReactiveUI;
using System.Reactive.Linq;
using InterpoleApp.Models.Enums;


namespace InterpoleApp.ViewModels;

public class EditCriminalViewModel : ReactiveObject
{
    public ObservableCollection<EyeColor> Categories { get; }
    public ObservableCollection<CrimeType> crimeCategories { get; }
    private readonly Criminal _criminal;
    private string _name;
    private string _secondName;
    private string _country;
    private int _dangerLevel;
    private int _size;
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
    public int Size
    {
        get => _size;
        set => this.RaiseAndSetIfChanged(ref _size, value);
    }
    public string Name
    {
        get => _name;
        set => this.RaiseAndSetIfChanged(ref _name, value);
    }

    public string SecondName
    {
        get => _secondName;
        set => this.RaiseAndSetIfChanged(ref _secondName, value);
    }
    public string Country
    {
        get => _country;
        set => this.RaiseAndSetIfChanged(ref _country, value);
    }

    public int DangerLevel
    {
        get => _dangerLevel;
        set => this.RaiseAndSetIfChanged(ref _dangerLevel, value);
    }

    public ReactiveCommand<Unit, Criminal> SaveCommand { get; }

    public EditCriminalViewModel(Criminal existingCriminal)
    {
        Categories = new ObservableCollection<EyeColor>(
            Enum.GetValues(typeof(EyeColor)) as EyeColor[]
        );
        crimeCategories = new ObservableCollection<CrimeType>(
            Enum.GetValues(typeof(CrimeType)) as CrimeType[]
        );
        _criminal = existingCriminal;

        EyeColor = _criminal.EyesColor;
        Name = _criminal.Name;
        SecondName = _criminal.secondName;
        Country = _criminal.Country;
        DangerLevel = _criminal.DangerLevel;
        Size = _criminal.Size;
        CrimeType = _criminal.CrimeType;

        SaveCommand = ReactiveCommand.Create(() =>
        {
            _criminal.Name = this.Name;
            _criminal.secondName = this.SecondName;
            _criminal.Country = this.Country;
            _criminal.DangerLevel = this.DangerLevel;
            _criminal.Size = this.Size;
            _criminal.EyesColor = this.EyeColor;
            _criminal.CrimeType = this.CrimeType;

            return _criminal;
        });
    }
}