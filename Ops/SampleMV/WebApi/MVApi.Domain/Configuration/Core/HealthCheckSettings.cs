namespace QuickStars.MVApi.Domain.Configuration.Core
{
    /// <summary>
    /// Represents settings related to health checks for the application.
    /// </summary>
    public class HealthCheckSettings
    {
        /// <summary>
        /// Gets or sets whether health checks are enabled for the API.
        /// Defaults to false if not specified.
        /// </summary>
        public bool Enabled { get; set; } = false;
        /// <summary>
        /// Gets or sets the route where health checks are exposed.
        /// Defaults to an empty string if not specified.
        /// </summary>
        public string Route { get; set; } = string.Empty;
    }
}