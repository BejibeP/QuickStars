using QuickStars.MVApi.Domain.Configuration.Core;

namespace QuickStars.MVApi.Domain.Configuration
{
    /// <summary>
    /// Represents application-wide settings for the entire application.
    /// </summary>
    public class AppSettings
    {
        /// <summary>
        /// The section name in the configuration that holds the application settings.
        /// </summary>
        public const string App = "App";

        /// <summary>
        /// Gets or sets whether Swagger is enabled.
        /// Defaults to false if not specified.
        /// </summary>
        public bool SwaggerEnabled { get; set; } = false;
        /// <summary>
        /// Gets or sets the title of the application.
        /// </summary>
        public string Title { get; set; } = string.Empty;
        /// <summary>
        /// Gets or sets the description of the application.
        /// </summary>
        public string Description { get; set; } = string.Empty;
        /// <summary>
        /// Gets or sets the name of the contact person for the application.
        /// </summary>
        public string ContactName { get; set; } = string.Empty;
        /// <summary>
        /// Gets or sets the email address of the contact person for the application.
        /// </summary>
        public string ContactEmail { get; set; } = string.Empty;
        /// <summary>
        /// Gets or sets the URL of the contact person or contact page for the application.
        /// </summary>
        public string ContactUrl { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets routing settings for the application.
        /// </summary>
        public RoutingSettings Routing { get; set; } = new RoutingSettings();
        /// <summary>
        /// Gets or sets versioning settings for the application.
        /// </summary>
        public VersionSettings Versionning { get; set; } = new VersionSettings();
        /// <summary>
        /// Gets or sets health check settings for the application.
        /// </summary>
        public HealthCheckSettings HealthChecks { get; set; } = new HealthCheckSettings();
        /// <summary>
        /// Gets or sets security settings for the application.
        /// </summary>
        public SecuritySettings Security { get; set; } = new SecuritySettings();
    }
}