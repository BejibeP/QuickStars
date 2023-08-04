using QuickStars.MVApi.Domain.Configuration.Core;

namespace QuickStars.MVApi.Domain.Configuration
{
    /// <summary>
    /// Represents messaging settings for the application.
    /// </summary>
    public class MessagingSettings
    {
        /// <summary>
        /// The section name in the configuration that holds the messaging settings.
        /// </summary>
        public const string Messaging = "Messaging";

        /// <summary>
        /// Gets or sets RabbitMQ settings.
        /// </summary>
        public RabbitSettings RabbitMq { get; set; } = new RabbitSettings();

        /// <summary>
        /// Represents RabbitMQ settings.
        /// </summary>
        public class RabbitSettings : HostSettings
        {

        }
    }
}