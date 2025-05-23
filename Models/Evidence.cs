using System;
using System.Text.Json.Serialization;
using InterpoleApp.Models.Enums;

namespace InterpoleApp.Models;

public class Evidence
{
    public Guid id { get; set; } = Guid.NewGuid();
    public Guid RelatedCriminalId { get; set; }
    public string Title { get; set; }
    public string Description {get; set;}
    public EvidenceType EvidenceType { get; set; }
    
    [JsonIgnore]
    public Criminal? RelatedCriminal { get; set; }
}