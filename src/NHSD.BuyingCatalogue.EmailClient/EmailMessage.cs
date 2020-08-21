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
        private readonly List<EmailAddress> _recipients = new List<EmailAddress>();

        /// <summary>
        /// Initializes a new instance of the <see cref="EmailMessage"/> class
        /// using the provided <paramref name="template"/>.
        /// </summary>
        /// <param name="template">The <see cref="MessageTemplate"/> to use to initialize
        /// the message.</param>
        public EmailMessage(MessageTemplate template)
        {
            if (template is null)
                throw new ArgumentNullException(nameof(template));

            Sender = template.Sender ?? throw new ArgumentException(
                $"{nameof(MessageTemplate.Sender)} must be provided.",
                nameof(template));

            _recipients.AddRange(template.Recipients);
            Subject = template.Subject;
            HtmlBody = template.HtmlBody;
            TextBody = template.TextBody;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EmailMessage"/> class
        /// with the specified <paramref name="recipients"/>.
        /// </summary>
        /// <param name="sender">The address of the sender of the message.</param>
        /// <param name="recipients">The recipients of the message.</param>
        public EmailMessage(EmailAddress sender, params EmailAddress[] recipients)
        {
            Sender = sender;
            _recipients.AddRange(recipients);
        }

        /// <summary>
        /// Gets or sets the sender (from address) of the message.
        /// </summary>
        public EmailAddress Sender { get; }

        /// <summary>
        /// Gets the recipients of the message.
        /// </summary>
        public IReadOnlyList<EmailAddress> Recipients => _recipients;

        /// <summary>
        /// Gets or sets the subject of the message.
        /// </summary>
        public string? Subject { get; set; }

        /// <summary>
        /// Gets or sets the HTML version of the body.
        /// </summary>
        public EmailMessageBody? HtmlBody { get; set; }

        /// <summary>
        /// Gets or sets the plain text version of the body.
        /// </summary>
        public EmailMessageBody? TextBody { get; set; }

        /// <summary>
        /// Gets the collection of message attachments.
        /// </summary>
        public IList<EmailAttachment> Attachments { get; } = new List<EmailAttachment>();

        /// <summary>
        /// Gets a value indicating whether the message has an attachment.
        /// </summary>
        public bool HasAttachments => Attachments.Any();

        /// <summary>
        /// Adds format items to both the HTML and text body parts.
        /// </summary>
        /// <param name="formatItems">One or more format items to format the message with.</param>
        /// <exception cref="ArgumentNullException"><paramref name="formatItems"/> is <see langref="null"/>.</exception>
        public void AddFormatItems(params object[] formatItems)
        {
            if (formatItems is null)
                throw new ArgumentNullException(nameof(formatItems));

            HtmlBody?.AddFormatItems(formatItems);
            TextBody?.AddFormatItems(formatItems);
        }

        /// <summary>
        /// Adds a recipient with the provided <paramref name="address"/> and
        /// optional display name to the list of recipients.
        /// </summary>
        /// <param name="address">The e-mail address of the recipient.</param>
        /// <param name="displayName">An optional display name for the recipient.</param>
        /// <exception cref="ArgumentNullException"><paramref name="address"/> is <see langref="null"/>.</exception>
        /// <exception cref="ArgumentException"><paramref name="address"/> is empty or consists only of white space.</exception>
        public void AddRecipient(string address, string? displayName = null)
        {
            if (address is null)
                throw new ArgumentNullException(nameof(address));

            if (string.IsNullOrWhiteSpace(address))
                throw new ArgumentException($"{nameof(address)} cannot be empty or white space.", nameof(address));

            _recipients.Add(new EmailAddress(address, displayName));
        }

        /// <summary>
        /// Adds a recipient with the provided <paramref name="address"/> to the list of recipients.
        /// </summary>
        /// <param name="address">The e-mail address of the recipient.</param>
        /// <exception cref="ArgumentNullException"><paramref name="address"/> is <see langref="null"/>.</exception>
        public void AddRecipient(EmailAddress address)
        {
            if (address is null)
                throw new ArgumentNullException(nameof(address));

            _recipients.Add(address);
        }
    }
}
