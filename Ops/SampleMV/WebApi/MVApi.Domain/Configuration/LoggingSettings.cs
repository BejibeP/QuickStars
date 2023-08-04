namespace QuickStars.MVApi.Domain.Configuration
{
    /// <summary>
    /// Represents settings related to logging for the application.
    /// </summary>
    public class LoggingSettings
    {
        /// <summary>
        /// The section name in the configuration that holds the logging settings.
        /// </summary>
        public const string Logging = "Logging";

        /// <summary>
        /// Gets or sets the log level settings for different parts of the application.
        /// </summary>
        public LevelSettings LogLevel { get; set; } = new LevelSettings();

        /// <summary>
        /// Represents log level settings for different parts of the application.
        /// </summary>
        public class LevelSettings
        {
            /// <summary>
            /// Gets or sets the default log level.
            /// Defaults to "Debug" if not specified.
            /// </summary>
            public string Default { get; set; } = "Debug";
            /// <summary>
            /// Gets or sets the log level for ASP.NET Core components.
            /// Defaults to "Debug" if not specified.
            /// </summary>
            public string AspNetCore { get; set; } = "Debug";
        }
    }
}