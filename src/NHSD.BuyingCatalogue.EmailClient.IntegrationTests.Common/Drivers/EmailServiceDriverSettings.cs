using System;
using System.Collections.Generic;
using System.Text;

namespace NHSD.BuyingCatalogue.EmailClient.IntegrationTesting.Drivers
{
    /// <summary>
    /// An object used to pass SmtpServerApiBaseUrl to the EmailServiceDriver.
    /// </summary>
    public sealed class EmailServiceDriverSettings
    {
        /// <summary>
        /// SmtpServerApiBaseUrl is the URL used to connect SMTP Server.
        /// </summary>
        public string SmtpServerApiBaseUrl { get; }

        /// <summary>
        ///  EmailServiceDriverSettings contains the configuration information used by the EmailServiceDriver.
        /// </summary>
        /// <param name="smtpServerApiBaseUrl">EmailServiceDriverSettings</param>
        public EmailServiceDriverSettings(string smtpServerApiBaseUrl)
        {
            SmtpServerApiBaseUrl = smtpServerApiBaseUrl;
        }
    }
}
