﻿namespace NHSD.BuyingCatalogue.EmailClient.Configuration
{
    /// <summary>
    /// SMTP server settings.
    /// </summary>
    public sealed class SmtpSettings
    {
        /// <summary>
        /// Gets the authentication settings for the SMTP server.
        /// </summary>
        public SmtpAuthenticationSettings Authentication { get; init; } = new();

        /// <summary>
        /// Gets the host name of the SMTP server.
        /// </summary>
        public string? Host { get; init; }

        /// <summary>
        /// Gets the port to use to connect to the SMTP server.
        /// </summary>
        public int Port { get; init; }

        /// <summary>
        /// Gets or sets a value indicating whether to allow connections
        /// to an SMTP server that does not present a valid/trusted certificate.
        /// </summary>
        /// <remarks>This should only be enabled in test environments.</remarks>
        public bool? AllowInvalidCertificate { get; set; }

        /// <summary>
        /// Gets the value used to prefix the subject in e-mails.
        /// </summary>
        public string? EmailSubjectPrefix { get; init; }
    }
}
