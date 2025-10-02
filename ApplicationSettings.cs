namespace DesignPatternsShowcase.Common.Configuration;

/// <summary>
/// Application configuration settings loaded from appsettings.json
/// </summary>
public class ApplicationSettings
{
    public int MaxRetryAttempts { get; set; } = 3;
    public int RetryDelayMs { get; set; } = 1000;
    public int SessionTimeoutMinutes { get; set; } = 30;
    public bool EnablePerformanceMetrics { get; set; } = true;
    public string LogLevel { get; set; } = "Information";
}
