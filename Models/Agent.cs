using System;
using InterpoleApp.Models.Enums;

namespace InterpoleApp.Models;

public class Agent : Iperson
{
    int id { get; set; }
    String Name { get; set; }
    String secondName { get; set; }
    public Rank rank { get; set; }
    public string GetName() => $"{Name} {secondName}";
}