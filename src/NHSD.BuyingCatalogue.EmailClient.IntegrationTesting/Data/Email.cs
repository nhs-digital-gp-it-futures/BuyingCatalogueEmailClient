using System.Collections.Generic;

namespace NHSD.BuyingCatalogue.EmailClient.IntegrationTesting.Data
{
    /// <summary>
    /// Represents the contents of an <see cref="Email"/> message.
    /// </summary>
    public sealed class Email
    {
        /// <summary>
        /// Gets the list of senders' addresses.
        /// </summary>
        public List<EmailAddress> From { get; } = new();

        /// <summary>
        /// Gets the list of To (recipient's) address.
        /// </summary>
        public List<EmailAddress> To { get; } = new();

        /// <summary>
        /// Gets or sets the subject of the e-mail.
        /// </summary>
        public string? Subject { get; init; } = null!;

        /// <summary>
        /// Gets or sets the plain-text body of the e-mail.
        /// </summary>
        public string? Text { get; init; } = null!;

        /// <summary>
        /// Gets or sets the HTML body of the e-mail.
        /// </summary>
        public string? Html { get; init; } = null!;

        /// <summary>
        /// Gets the list of attachments.
        /// </summary>
        public List<EmailAttachmentData> Attachments { get; } = new();
    }
}
