using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InterpoleApp.Database;
using InterpoleApp.Models;
using InterpoleApp.Database;

namespace InterpoleApp.Systems;

public class EvidenceService
{
    private readonly JsonStorageService _jsonService = new();
    public List<Evidence> Evidences { get; private set; } = new();

    public async Task LoadAllAsync()
    {
        Evidences = await _jsonService.GetAllAsync<Evidence>() ?? new();
    }

    public async Task SaveAllAsync()
    {
        await _jsonService.SaveAllAsync<Evidence>(Evidences);
    }

    public List<Evidence> GetForCriminal(Guid criminalId)
    {
        return Evidences.Where(e => e.RelatedCriminalId == criminalId).ToList();
    }

    public void DeleteForCriminal(Guid criminalId)
    {
        Evidences.RemoveAll(e => e.RelatedCriminalId == criminalId);
    }

    public void Add(Evidence evidence)
    {
        Evidences.Add(evidence);
    }
}
