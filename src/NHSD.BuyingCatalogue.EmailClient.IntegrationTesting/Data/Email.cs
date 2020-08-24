using System.Collections.Generic;

namespace NHSD.BuyingCatalogue.EmailClient.IntegrationTesting.Data
{
    /// <summary>
    /// Represents the contents of an Email message.
    /// </summary>
    public sealed class Email
    {
        /// <summary>
        /// Gets or sets From.
        /// </summary>
        public List<EmailAddress> From { get; } = new List<EmailAddress>();

        /// <summary>
        /// Gets or sets To.
        /// </summary>
        public List<EmailAddress> To { get;} = new List<EmailAddress>();

        /// <summary>
        /// Gets or sets Subject.
        /// </summary>
        public string? Subject { get; set; }

        /// <summary>
        /// Gets or sets PlainTextBody.
        /// </summary>
        public string? PlainTextBody { get; set; }

        /// <summary>
        /// Gets or sets HtmlBody.
        /// </summary>
        public string? HtmlBody { get; set; }

        /// <summary>
        /// Gets or sets Attachment
        /// </summary>
        public List<EmailAttachmentData> Attachments { get; } = new List<EmailAttachmentData>();
    }
}
