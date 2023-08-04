namespace QuickStars.MVApi.Domain.Configuration.Core
{
    /// <summary>
    /// Represents settings related to security for the application.
    /// </summary>
    public class SecuritySettings
    {
        /// <summary>
        /// Gets or sets whether SSL (Secure Sockets Layer) is required for connections.
        /// Defaults to false if not specified.
        /// </summary>
        public bool SSLRequired { get; set; } = false;
        /// <summary>
        /// Gets or sets the allowed hosts for the application.
        /// Defaults to "*" (allow all hosts) if not specified.
        /// </summary>
        public string AllowedHosts { get; set; } = "*";
        /// <summary>
        /// Gets or sets the audience for JWT (JSON Web Token) validation.
        /// Defaults to "*" if not specified.
        /// </summary>
        public string Audience { get; set; } = "*";
        /// <summary>
        /// Gets or sets the issuer of JWT (JSON Web Token).
        /// Defaults to "http://localhost:7081" if not specified.
        /// </summary>
        public string Issuer { get; set; } = "http://locahost:7081";
        /// <summary>
        /// Gets or sets the secret key used for JWT (JSON Web Token) generation and validation.
        /// Defaults to a predefined value if not specified.
        /// </summary>
        public string Secret { get; set; } = "thisIsAJWTToken";
        /// <summary>
        /// Gets or sets the expiration time for JWT (JSON Web Token) in minutes.
        /// Defaults to 60 minutes if not specified.
        /// </summary>
        public int ExpirationInMinutes { get; set; } = 60;
    }
}