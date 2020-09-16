namespace Domain.Models
{
    /// <summary>
    /// Class to hold Application settings
    /// </summary>
    public class ApplicationSetting
    {
        public string AuthenticationTokenSecret { get; set; }
    }

    /// <summary>
    /// Class to hold connection string
    /// </summary>
    public class ConnectionSettings
    {
        public string DefaultConnection { get; set; }
    }

    /// <summary>
    /// Class to hold logging configuration
    /// </summary>
    public class Logging
    {
        public LogLevel LogLevel { get; set; }
    }

    public class LogLevel
    {
        public string Default { get; set; }
    }
}
