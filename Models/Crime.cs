using System.Collections.Generic;
using InterpoleApp.Models.Enums;

namespace InterpoleApp.Models;

public class Crime
{
    public int id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public Country Location { get; set; }
    public Agent leadAgent { get; set; }
    public CrimeType CrimeType { get; set; }
    public List<Criminal> Suspects { get; set; }
    List<Evidence> Evidences { get; set; }
}