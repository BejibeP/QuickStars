using QuickStars.MVApi.Domain.Configuration.Core;

namespace QuickStars.MVApi.Domain.Configuration
{
    /// <summary>
    /// Represents settings related to database connections.
    /// </summary>
    public class DatabaseSettings
    {
        /// <summary>
        /// The section name in the configuration that holds the database connection settings.
        /// </summary>
        public const string DatabaseConnections = "DatabaseConnections";

        /// <summary>
        /// Gets or sets the default database connection settings.
        /// </summary>
        public DatabaseConnection Default { get; set; } = new DatabaseConnection();

        /// <summary>
        /// Represents a specific database connection configuration.
        /// </summary>
        public class DatabaseConnection : HostSettings
        {
            /// <summary>
            /// Gets or sets the database provider (e.g., SQLServer, MySQL, etc.).
            /// Defaults to "SQLServer" if not specified.
            /// </summary>
            public string Provider { get; set; } = "SQLServer";
        }
    }
}