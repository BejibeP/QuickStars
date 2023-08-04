namespace QuickStars.MVApi.Domain.Configuration.Core
{
    /// <summary>
    /// Represents common host settings for various services.
    /// </summary>
    public abstract class HostSettings
    {
        /// <summary>
        /// Gets or sets the host address of the service.
        /// </summary>
        public string Host { get; set; } = string.Empty;
        /// <summary>
        /// Gets or sets the port number of the service.
        /// </summary>
        public int Port { get; set; } = 0;
        /// <summary>
        /// Gets or sets the user name for connecting to the service.
        /// </summary>
        public string User { get; set; } = string.Empty;
        /// <summary>
        /// Gets or sets the password for connecting to the service.
        /// </summary>
        public string Password { get; set; } = string.Empty;
    }
}