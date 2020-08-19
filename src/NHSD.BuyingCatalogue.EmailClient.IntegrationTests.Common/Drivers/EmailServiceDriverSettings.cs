using System;
using System.Collections.Generic;
using System.Text;

namespace NHSD.BuyingCatalogue.EmailClient.IntegrationTests.Common.Drivers
{
    /// <summary>
    /// 
    /// </summary>
    public class EmailServiceDriverSettings
    {
        /// <summary>
        /// SmtpServerApiBaseUrl
        /// </summary>
        public string SmtpServerApiBaseUrl { get; set; }

        /// <summary>
        /// EmailServiceDriverSettings
        /// </summary>
        /// <param name="smtpServerApiBaseUrl">EmailServiceDriverSettings</param>
        public EmailServiceDriverSettings(string smtpServerApiBaseUrl)
        {
            SmtpServerApiBaseUrl = smtpServerApiBaseUrl;
        }
    }
}
