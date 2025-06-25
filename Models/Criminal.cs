using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using InterpoleApp.Models.Enums;

namespace InterpoleApp.Models;

public class Criminal : Iperson
{
    public Guid id { get; set; } = Guid.NewGuid();
    public string Name { get; set; }
    public string secondName { get; set; }
    public string FullName => $"{Name} {secondName}";
    public int DangerLevel { get; set; }
    public int Size { get; set; }
    public EyeColor EyesColor { get; set; }
    public CrimeType CrimeType { get; set; }
    public string Country { get; set; }


    public string GetName() => $"{Name} {secondName}";
    public string GetCountry() => $"{Country}";
}