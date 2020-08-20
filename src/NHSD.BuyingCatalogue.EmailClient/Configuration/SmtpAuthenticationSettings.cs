namespace NHSD.BuyingCatalogue.EmailClient.Configuration
{
    /// <summary>
    /// SMTP authentication settings.
    /// </summary>
    public sealed class SmtpAuthenticationSettings
    {
        /// <summary>
        /// Gets or sets a value indicating whether the SMTP server requires authentication.
        /// </summary>
        public bool IsRequired { get; set; }

        /// <summary>
        /// Gets or sets the user name to authenticate with.
        /// </summary>
        public string? UserName { get; set; }

        /// <summary>
        /// Gets or sets the password for authentication.
        /// </summary>
        public string? Password { get; set; }
    }
}
