using System;

namespace NHSD.BuyingCatalogue.EmailClient.IntegrationTesting.Drivers
{
    /// <summary>
    /// Defines the settings required by the <see cref="EmailServerDriver"/>.
    /// </summary>
    public sealed class EmailServiceDriverSettings
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EmailServiceDriverSettings"/> class
        /// with the specified URL.
        /// </summary>
        /// <param name="smtpServerApiBaseUrl">The URL for the SMTP server's REST API.</param>
        public EmailServiceDriverSettings(Uri smtpServerApiBaseUrl)
        {
            SmtpServerApiBaseUrl = smtpServerApiBaseUrl;
        }

        /// <summary>
        /// Gets the URL for the SMTP server's REST API.
        /// </summary>
        public Uri SmtpServerApiBaseUrl { get; }
    }
}
