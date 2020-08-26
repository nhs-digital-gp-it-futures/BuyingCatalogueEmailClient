using System;

namespace NHSD.BuyingCatalogue.EmailClient.IntegrationTesting.Drivers
{
    /// <summary>
    /// An object used to pass SmtpServerApiBaseUrl to the EmailServiceDriver.
    /// </summary>
    public sealed class EmailServerDriverSettings
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EmailServerDriverSettings"/> class
        /// with the specified <paramref name="smtpServerApiBaseUrl"/>.
        /// </summary>
        /// <param name="smtpServerApiBaseUrl">The URL for the SMTP server's REST API.</param>
        public EmailServerDriverSettings(Uri smtpServerApiBaseUrl)
        {
            SmtpServerApiBaseUrl = smtpServerApiBaseUrl ?? throw new ArgumentNullException(nameof(smtpServerApiBaseUrl));
        }

        /// <summary>
        /// Gets the URL used to connect SMTP Server.
        /// </summary>
        public Uri SmtpServerApiBaseUrl { get; }
    }
}
