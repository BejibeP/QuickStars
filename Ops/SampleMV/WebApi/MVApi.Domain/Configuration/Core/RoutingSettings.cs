namespace QuickStars.MVApi.Domain.Configuration.Core
{
    /// <summary>
    /// Represents the routing settings for the API.
    /// </summary>
    public class RoutingSettings
    {
        /// <summary>
        /// Gets or sets whether routing is enabled for the API.
        /// Defaults to false if not specified.
        /// </summary>
        public bool Enabled { get; set; } = false;
        /// <summary>
        /// Gets or sets the routing prefix for the API.
        /// Defaults to an empty string if not specified.
        /// </summary>
        public string Prefix { get; set; } = string.Empty;
    }
}