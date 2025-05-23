using System.Collections.Generic;
using System;
using System.Threading.Tasks;
using InterpoleApp.Models;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Collections.Concurrent;
using System.IO;

namespace InterpoleApp.Database;

public class JsonStorageService
{
    private readonly JsonSerializerOptions _jsonOptions = new()
    {
        WriteIndented = true,
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        ReferenceHandler = ReferenceHandler.IgnoreCycles
    };

    private readonly ConcurrentDictionary<Type, object> _cache = new();

    public async Task<List<T>> GetAllAsync<T>() where T : class
    {
        //Проверяем есть ли уже эти данные в кэше,что бы второй раз не читать
        if (_cache.TryGetValue(typeof(T), out var cached) && cached is List<T> cachedList)
            return cachedList;
        
        string file = GetFilePath<T>();
        
        
        if (!File.Exists(file))
        {
            var empty = new List<T>();
            _cache[typeof(T)] = empty;
            return empty;
        }

        var json = await File.ReadAllTextAsync(file);
        var items = JsonSerializer.Deserialize<List<T>>(json, _jsonOptions) ?? new List<T>();

        _cache[typeof(T)] = items;
        return items;
    }

    public async Task SaveAllAsync<T>(List<T> items) where T : class
    {
        _cache[typeof(T)] = items;

        string file = GetFilePath<T>();
        Directory.CreateDirectory(Path.GetDirectoryName(file)!); // на всякий случай

        var json = JsonSerializer.Serialize(items, _jsonOptions);
        await File.WriteAllTextAsync(file, json);
    }

    
    private static string ProjectRoot =>
        Path.Combine(AppContext.BaseDirectory, @"C:\Users\Сава\dotnet\RiderProjects\InterpoleApp\InterpoleApp");
    private string GetFilePath<T>()
    {
        var name = typeof(T).Name.ToLowerInvariant();
        return Path.Combine(ProjectRoot, "Database", $"{name}s.json");
    }
}