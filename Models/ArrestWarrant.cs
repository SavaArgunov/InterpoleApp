namespace InterpoleApp.Models;

public class ArrestWarrant
{
    public int id { get; set; }
    public Criminal Target { get; set; }
    public bool IsActive { get; set; }    
}