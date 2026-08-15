using DevmasterTrainingManagement.Infrastructure.Logging;

namespace DevmasterTrainingManagement.Infrastructure.Persistence;

/// <summary>
/// Service for handling backup and restore operations
/// </summary>
public class BackupService
{
    private readonly string _dataDirectory;
    private readonly string _backupDirectory;
    private readonly FileLogger _logger;

    public BackupService(string dataDirectory, FileLogger logger)
    {
        _dataDirectory = dataDirectory;
        _backupDirectory = Path.Combine(dataDirectory, "..", "Backup");
        _logger = logger;

        // Ensure backup directory exists
        if (!Directory.Exists(_backupDirectory))
            Directory.CreateDirectory(_backupDirectory);
    }

    /// <summary>
    /// Create a backup of all data files
    /// </summary>
    public void CreateBackup()
    {
        try
        {
            var timestamp = DateTime.Now.ToString("yyyy-MM-dd_HHmm");
            var backupPath = Path.Combine(_backupDirectory, $"backup_{timestamp}");

            if (!Directory.Exists(backupPath))
                Directory.CreateDirectory(backupPath);

            // Copy all JSON files
            var dataFiles = Directory.GetFiles(_dataDirectory, "*.json");
            foreach (var file in dataFiles)
            {
                var fileName = Path.GetFileName(file);
                File.Copy(file, Path.Combine(backupPath, fileName), true);
            }

            _logger.LogInfo($"Backup created successfully at: {backupPath}");
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error creating backup: {ex.Message}", ex);
            throw;
        }
    }

    /// <summary>
    /// Get list of available backups
    /// </summary>
    public List<BackupInfo> GetAvailableBackups()
    {
        var backups = new List<BackupInfo>();

        try
        {
            if (!Directory.Exists(_backupDirectory))
                return backups;

            var backupDirs = Directory.GetDirectories(_backupDirectory)
                .OrderByDescending(d => new DirectoryInfo(d).CreationTime)
                .ToList();

            foreach (var dir in backupDirs)
            {
                var dirName = Path.GetFileName(dir);
                var fileCount = Directory.GetFiles(dir, "*.json").Length;
                var createdDate = new DirectoryInfo(dir).CreationTime;

                backups.Add(new BackupInfo
                {
                    Name = dirName,
                    Path = dir,
                    FileCount = fileCount,
                    CreatedDate = createdDate
                });
            }

            return backups;
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error retrieving backups: {ex.Message}", ex);
            return backups;
        }
    }

    /// <summary>
    /// Restore data from a backup
    /// </summary>
    public void RestoreBackup(string backupPath)
    {
        try
        {
            if (!Directory.Exists(backupPath))
                throw new DirectoryNotFoundException($"Backup not found: {backupPath}");

            // Backup current data first
            CreateBackup();

            // Copy backup files back
            var backupFiles = Directory.GetFiles(backupPath, "*.json");
            foreach (var file in backupFiles)
            {
                var fileName = Path.GetFileName(file);
                File.Copy(file, Path.Combine(_dataDirectory, fileName), true);
            }

            _logger.LogInfo($"Backup restored successfully from: {backupPath}");
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error restoring backup: {ex.Message}", ex);
            throw;
        }
    }

    /// <summary>
    /// Delete a backup
    /// </summary>
    public void DeleteBackup(string backupPath)
    {
        try
        {
            if (Directory.Exists(backupPath))
            {
                Directory.Delete(backupPath, true);
                _logger.LogInfo($"Backup deleted: {backupPath}");
            }
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error deleting backup: {ex.Message}", ex);
            throw;
        }
    }
}

/// <summary>
/// Information about a backup
/// </summary>
public class BackupInfo
{
    public string Name { get; set; } = string.Empty;
    public string Path { get; set; } = string.Empty;
    public int FileCount { get; set; }
    public DateTime CreatedDate { get; set; }
}
