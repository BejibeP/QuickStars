using QuickStars.MVApi.Domain.Configuration.Core;

namespace QuickStars.MVApi.Domain.Configuration
{
    /// <summary>
    /// Represents settings related to external APIs.
    /// </summary>
    public class ExternalApiSettings
    {
        /// <summary>
        /// The section name in the configuration that holds the external API settings.
        /// </summary>
        public const string ExternalAPIs = "ExternalAPIs";

        /// <summary>
        /// Gets or sets Weather PyApi settings.
        /// </summary>
        public ApiSettings Weather { get; set; } = new ApiSettings();

        /// <summary>
        /// Represents configuration settings for an external API.
        /// </summary>
        public class ApiSettings : HostSettings
        {
            /// <summary>
            /// Gets or sets the protocol for connecting to the API (e.g., http, https).
            /// Defaults to "http" if not specified.
            /// </summary>
            public string Protocol { get; set; } = "http";
            /// <summary>
            /// Gets or sets routing settings for the API.
            /// </summary>
            public RoutingSettings Routing { get; set; } = new RoutingSettings();
        }
    }
}