namespace DevmasterTrainingManagement.Infrastructure.Logging;

/// <summary>
/// Simple file-based logger
/// </summary>
public class FileLogger
{
    private readonly string _logFilePath;
    private static readonly object _lock = new object();

    public FileLogger(string logDirectory = "Logs")
    {
        // Ensure log directory exists
        if (!Directory.Exists(logDirectory))
            Directory.CreateDirectory(logDirectory);

        _logFilePath = Path.Combine(logDirectory, "app.log");
    }

    /// <summary>
    /// Log information message
    /// </summary>
    public void LogInfo(string message)
    {
        Log("INFO", message);
    }

    /// <summary>
    /// Log warning message
    /// </summary>
    public void LogWarning(string message)
    {
        Log("WARNING", message);
    }

    /// <summary>
    /// Log error message
    /// </summary>
    public void LogError(string message, Exception? ex = null)
    {
        var fullMessage = ex != null ? $"{message}\n{ex}" : message;
        Log("ERROR", fullMessage);
    }

    /// <summary>
    /// Log debug message
    /// </summary>
    public void LogDebug(string message)
    {
        Log("DEBUG", message);
    }

    private void Log(string level, string message)
    {
        lock (_lock)
        {
            try
            {
                var timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                var logEntry = $"[{timestamp}] [{level}] {message}";
                
                File.AppendAllText(_logFilePath, logEntry + Environment.NewLine);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to write log: {ex.Message}");
            }
        }
    }

    /// <summary>
    /// Clear log file
    /// </summary>
    public void ClearLog()
    {
        try
        {
            if (File.Exists(_logFilePath))
                File.Delete(_logFilePath);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to clear log: {ex.Message}");
        }
    }

    /// <summary>
    /// Get log file path
    /// </summary>
    public string GetLogFilePath() => _logFilePath;
}
