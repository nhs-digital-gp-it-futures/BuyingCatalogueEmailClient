using System.Collections.Generic;

namespace NHSD.BuyingCatalogue.EmailClient.IntegrationTesting.Data
{
    /// <summary>
    /// Represents the contents of an <see cref="Email"/> message.
    /// </summary>
    public sealed class Email
    {
        private readonly List<EmailAttachmentData> attachments = new();
        private readonly List<EmailAddress> from = new();
        private readonly List<EmailAddress> to = new();

        /// <summary>
        /// Gets the list of senders' addresses.
        /// </summary>
        public IReadOnlyList<EmailAddress> From => from;

        /// <summary>
        /// Gets the list of To (recipient's) address.
        /// </summary>
        public IReadOnlyList<EmailAddress> To => to;

        /// <summary>
        /// Gets the subject of the e-mail.
        /// </summary>
        public string? Subject { get; init; } = null!;

        /// <summary>
        /// Gets the plain-text body of the e-mail.
        /// </summary>
        public string? Text { get; init; } = null!;

        /// <summary>
        /// Gets the HTML body of the e-mail.
        /// </summary>
        public string? Html { get; init; } = null!;

        /// <summary>
        /// Gets the list of attachments.
        /// </summary>
        public IReadOnlyList<EmailAttachmentData> Attachments => attachments;

        /// <summary>
        /// Add <paramref name="senders"/> to the <see cref="From"/> collection.
        /// </summary>
        /// <param name="senders">The senders to add.</param>
        internal void AddSenders(IEnumerable<EmailAddress> senders) => AddRange(from, senders);

        /// <summary>
        /// Add <paramref name="recipients"/> to the <see cref="To"/> collection.
        /// </summary>
        /// <param name="recipients">The recipients to add.</param>
        internal void AddRecipients(IEnumerable<EmailAddress> recipients) => AddRange(to, recipients);

        /// <summary>
        /// Adds attachments to the <see cref="Attachments"/> collection.
        /// </summary>
        /// <param name="attachmentData">The attachments to add.</param>
        internal void AddAttachments(IEnumerable<EmailAttachmentData> attachmentData) =>
            AddRange(attachments, attachmentData);

        private static void AddRange<T>(List<T> list, IEnumerable<T> values) => list.AddRange(values);
    }
}
