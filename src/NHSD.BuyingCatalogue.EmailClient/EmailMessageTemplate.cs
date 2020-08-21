using System.Collections.Generic;

namespace NHSD.BuyingCatalogue.EmailClient
{
    /// <summary>
    /// Defines a template that can be used to initialize a new <see cref="EmailMessage"/>.
    /// </summary>
    public sealed class EmailMessageTemplate
    {
        /// <summary>
        /// Gets or sets the sender (from address) of the message.
        /// </summary>
        public EmailAddress? Sender { get; set; }

        /// <summary>
        /// Gets or sets the recipients of the message.
        /// </summary>
        public IList<EmailAddress> Recipients { get; } = new List<EmailAddress>();

        /// <summary>
        /// Gets or sets the subject of the message.
        /// </summary>
        public string? Subject { get; set; }

        /// <summary>
        /// Gets or sets the HTML body.
        /// </summary>
        public EmailMessageBody? HtmlBody { get; set; }

        /// <summary>
        /// Gets or sets the plain text body.
        /// </summary>
        public EmailMessageBody? TextBody { get; set; }
    }
}
