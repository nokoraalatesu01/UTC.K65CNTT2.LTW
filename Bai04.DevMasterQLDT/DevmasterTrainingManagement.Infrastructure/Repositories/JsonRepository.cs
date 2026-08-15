using System.Text.Json;
using DevmasterTrainingManagement.Application.Interfaces;

namespace DevmasterTrainingManagement.Infrastructure.Repositories;

/// <summary>
/// JSON-based repository for generic CRUD operations
/// </summary>
/// <typeparam name="T">The entity type</typeparam>
public class JsonRepository<T> : IRepository<T> where T : class
{
    private readonly string _filePath;
    private List<T> _data = new();

    public JsonRepository(string dataDirectory)
    {
        var entityName = typeof(T).Name.ToLowerInvariant();
        _filePath = Path.Combine(dataDirectory, $"{entityName}s.json");
        
        // Ensure data directory exists
        if (!Directory.Exists(dataDirectory))
            Directory.CreateDirectory(dataDirectory);
    }

    /// <summary>
    /// Load data from JSON file
    /// </summary>
    public async Task LoadAsync()
    {
        try
        {
            if (File.Exists(_filePath))
            {
                var json = await File.ReadAllTextAsync(_filePath);
                _data = JsonSerializer.Deserialize<List<T>>(json) ?? new List<T>();
            }
            else
            {
                _data = new List<T>();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error loading data from {_filePath}: {ex.Message}");
            _data = new List<T>();
        }
    }

    /// <summary>
    /// Save data to JSON file
    /// </summary>
    private async Task SaveAsync()
    {
        try
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            var json = JsonSerializer.Serialize(_data, options);
            await File.WriteAllTextAsync(_filePath, json);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error saving data to {_filePath}: {ex.Message}");
            throw;
        }
    }

    public async Task AddAsync(T entity)
    {
        if (entity == null)
            throw new ArgumentNullException(nameof(entity));

        await LoadAsync();
        _data.Add(entity);
        await SaveAsync();
    }

    public async Task<bool> UpdateAsync(T entity)
    {
        if (entity == null)
            throw new ArgumentNullException(nameof(entity));

        await LoadAsync();
        
        // Get the Id property
        var idProperty = typeof(T).GetProperty("Id");
        if (idProperty == null)
            throw new InvalidOperationException($"Entity {typeof(T).Name} must have an Id property");

        var entityId = idProperty.GetValue(entity)?.ToString();
        var existingIndex = _data.FindIndex(x => 
            idProperty.GetValue(x)?.ToString() == entityId);

        if (existingIndex < 0)
            return false;

        _data[existingIndex] = entity;
        await SaveAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(string id)
    {
        if (string.IsNullOrEmpty(id))
            return false;

        await LoadAsync();
        
        var idProperty = typeof(T).GetProperty("Id");
        if (idProperty == null)
            throw new InvalidOperationException($"Entity {typeof(T).Name} must have an Id property");

        var existingIndex = _data.FindIndex(x => 
            idProperty.GetValue(x)?.ToString() == id);

        if (existingIndex < 0)
            return false;

        _data.RemoveAt(existingIndex);
        await SaveAsync();
        return true;
    }

    public async Task<T?> GetByIdAsync(string id)
    {
        if (string.IsNullOrEmpty(id))
            return null;

        await LoadAsync();
        
        var idProperty = typeof(T).GetProperty("Id");
        if (idProperty == null)
            throw new InvalidOperationException($"Entity {typeof(T).Name} must have an Id property");

        return _data.FirstOrDefault(x => 
            idProperty.GetValue(x)?.ToString() == id);
    }

    public async Task<IReadOnlyList<T>> GetAllAsync()
    {
        await LoadAsync();
        return _data.AsReadOnly();
    }

    public async Task<bool> ExistsAsync(string id)
    {
        return await GetByIdAsync(id) != null;
    }
}
