using System;
using System.Collections.Generic;
using System.Linq;

namespace NHSD.BuyingCatalogue.EmailClient
{
    /// <summary>
    /// An e-mail message.
    /// </summary>
    public sealed class EmailMessage
    {
        private readonly List<EmailAttachment> _attachments = new List<EmailAttachment>();
        private readonly List<EmailAddress> _recipients = new List<EmailAddress>();

        /// <summary>
        /// Initializes a new instance of the <see cref="EmailMessage"/> class
        /// using the provided <paramref name="template"/>.
        /// </summary>
        /// <param name="template">The <see cref="EmailMessageTemplate"/> to use to initialize
        /// the message.</param>
        /// <param name="recipients">The recipient(s) of the e-mail.</param>
        /// <param name="attachments">Any attachments to the e-mail.</param>
        /// <param name="formatItems">Any format items to format the content with.</param>
        /// <exception cref="ArgumentNullException"><paramref name="template"/> is <see langref="null"/>.</exception>
        public EmailMessage(
            EmailMessageTemplate template,
            ICollection<EmailAddress> recipients,
            IEnumerable<EmailAttachment>? attachments = null,
            params object[] formatItems)
        {
            if (template is null)
                throw new ArgumentNullException(nameof(template));

            Sender = template.Sender ?? throw new ArgumentException(
                $"{nameof(EmailMessageTemplate.Sender)} must be provided.",
                nameof(template));

            if (recipients is null)
                throw new ArgumentNullException(nameof(recipients));

            if (recipients.Count == 0)
                throw new ArgumentException("At least one recipient must be specified.", nameof(recipients));

            if (formatItems is null)
                throw new ArgumentNullException(nameof(formatItems));

            _recipients.AddRange(recipients);
            Subject = template.Subject;
            HtmlBody = new EmailMessageBody(template.HtmlContent, formatItems);
            TextBody = new EmailMessageBody(template.PlainTextContent, formatItems);

            if (attachments != null)
                _attachments.AddRange(attachments);
        }

        /// <summary>
        /// Gets the sender (from address) of the message.
        /// </summary>
        public EmailAddress Sender { get; }

        /// <summary>
        /// Gets the recipients of the message.
        /// </summary>
        public IReadOnlyList<EmailAddress> Recipients => _recipients;

        /// <summary>
        /// Gets the subject of the message.
        /// </summary>
        public string? Subject { get; }

        /// <summary>
        /// Gets the HTML version of the body.
        /// </summary>
        public EmailMessageBody? HtmlBody { get; }

        /// <summary>
        /// Gets the plain text version of the body.
        /// </summary>
        public EmailMessageBody? TextBody { get; }

        /// <summary>
        /// Gets the collection of message attachments.
        /// </summary>
        public IReadOnlyList<EmailAttachment> Attachments => _attachments;

        /// <summary>
        /// Gets a value indicating whether the message has an attachment.
        /// </summary>
        public bool HasAttachments => Attachments.Any();
    }
}
