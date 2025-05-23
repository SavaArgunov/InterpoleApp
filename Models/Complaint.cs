using System;

namespace InterpoleApp.Models;

public class Complaint
{
    public int  ComplaintId { get; set; }
    public string  Country { get; set; }
    public string Department { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime Date { get; set; }
    
    public Criminal? RelatedCriminal  { get; set; }
    
    
}