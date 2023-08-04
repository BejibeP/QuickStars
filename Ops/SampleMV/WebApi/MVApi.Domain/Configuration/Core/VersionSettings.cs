namespace QuickStars.MVApi.Domain.Configuration.Core
{
    /// <summary>
    /// Represents the versioning settings for the API.
    /// </summary>
    public class VersionSettings
    {
        /// <summary>
        /// Gets or sets whether versioning is enabled for the API.
        /// Defaults to false if not specified.
        /// </summary>
        public bool Enabled { get; set; } = false;
        /// <summary>
        /// Gets or sets the API version.
        /// Defaults to "1" if not specified.
        /// </summary>
        public string Version { get; set; } = "1";
    }
}