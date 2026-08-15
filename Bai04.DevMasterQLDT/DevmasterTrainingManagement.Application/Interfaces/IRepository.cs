namespace DevmasterTrainingManagement.Application.Interfaces;

/// <summary>
/// Generic repository interface for CRUD operations
/// </summary>
/// <typeparam name="T">The entity type</typeparam>
public interface IRepository<T> where T : class
{
    /// <summary>
    /// Add a new entity
    /// </summary>
    Task AddAsync(T entity);

    /// <summary>
    /// Update an existing entity
    /// </summary>
    Task<bool> UpdateAsync(T entity);

    /// <summary>
    /// Delete an entity by ID
    /// </summary>
    Task<bool> DeleteAsync(string id);

    /// <summary>
    /// Get an entity by ID
    /// </summary>
    Task<T?> GetByIdAsync(string id);

    /// <summary>
    /// Get all entities
    /// </summary>
    Task<IReadOnlyList<T>> GetAllAsync();

    /// <summary>
    /// Check if an entity exists by ID
    /// </summary>
    Task<bool> ExistsAsync(string id);
}
