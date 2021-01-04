using System;

namespace NHSD.BuyingCatalogue.EmailClient
{
    /// <summary>
    /// Defines a template that can be used to initialize a new <see cref="EmailMessage"/>.
    /// </summary>
    public sealed class EmailMessageTemplate
    {
        private EmailAddressTemplate? _sender;

        /// <summary>
        /// Initializes a new instance of the <see cref="EmailMessageTemplate"/> class.
        /// </summary>
        /// <remarks>Required for deserialization.</remarks>
        public EmailMessageTemplate()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EmailMessageTemplate"/> class
        /// with the specified <paramref name="sender"/>.
        /// </summary>
        /// <param name="sender">The sender of the message.</param>
        public EmailMessageTemplate(EmailAddressTemplate sender)
        {
            Sender = sender;
        }

        /// <summary>
        /// Gets or sets the sender (from address) of the message.
        /// </summary>
        /// <exception cref="ArgumentNullException"><paramref name="value"/> is <see langword="null"/>.</exception>
        public EmailAddressTemplate? Sender
        {
            get => _sender;
            set
            {
                _sender = value ?? throw new ArgumentNullException(nameof(value));
            }
        }

        /// <summary>
        /// Gets or sets the subject of the message.
        /// </summary>
        public string? Subject { get; set; }

        /// <summary>
        /// Gets or sets the HTML body.
        /// </summary>
        public string? HtmlContent { get; set; }

        /// <summary>
        /// Gets or sets the plain text body.
        /// </summary>
        public string? PlainTextContent { get; set; }

        /// <summary>
        /// Returns the <see cref="EmailAddress"/> representation of <see cref="Sender"/>.
        /// </summary>
        /// <returns>The <see cref="EmailAddress"/> representation of <see cref="Sender"/>.</returns>
        public EmailAddress? GetSenderAsEmailAddress() => Sender?.AsEmailAddress();
    }
}
