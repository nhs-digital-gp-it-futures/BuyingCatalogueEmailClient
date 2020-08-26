using System;

namespace NHSD.BuyingCatalogue.EmailClient.IntegrationTesting.Drivers
{
    /// <summary>
    /// An object used to pass SmtpServerApiBaseUrl to the EmailServiceDriver.
    /// </summary>
    public sealed class EmailServerDriverSettings
    {
        /// <summary>
        ///  EmailServerDriverSettings contains the configuration information used by the EmailServiceDriver.
        /// </summary>
        /// <param name="smtpServerApiBaseUrl">EmailServerDriverSettings</param>
        public EmailServerDriverSettings(Uri smtpServerApiBaseUrl)
        {
            SmtpServerApiBaseUrl = smtpServerApiBaseUrl ?? throw new ArgumentNullException(nameof(smtpServerApiBaseUrl));
        }

        /// <summary>
        /// SmtpServerApiBaseUrl is the URL used to connect SMTP Server.
        /// </summary>
        public Uri SmtpServerApiBaseUrl { get; }
    }
}
