using System;
using System.IO;

namespace NHSD.BuyingCatalogue.EmailClient
{
    /// <summary>
    /// Represents an attachment to an e-mail.
    /// </summary>
    public sealed class EmailAttachment
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EmailAttachment"/> class
        /// with the given file name and content.
        /// </summary>
        /// <param name="fileName">The file name of the attachment.</param>
        /// <param name="content">The content of the attachment.</param>
        /// <exception cref="ArgumentNullException"><paramref name="fileName"/> is <see langref="null"/>.</exception>
        /// <exception cref="ArgumentException"><paramref name="fileName"/> is empty or consists only of white space.</exception>
        /// <exception cref="ArgumentNullException"><paramref name="content"/> is <see langref="null"/>.</exception>
        public EmailAttachment(string fileName, Stream content)
        {
            FileName = fileName ?? throw new ArgumentNullException(nameof(fileName));
            if (string.IsNullOrWhiteSpace(fileName))
                throw new ArgumentException($"{nameof(fileName)} is required.", nameof(fileName));

            Content = content ?? throw new ArgumentNullException(nameof(content));
        }

        /// <summary>
        /// Gets the file name of the attachment.
        /// </summary>
        public string FileName { get; }

        /// <summary>
        /// Gets the content of the attachment.
        /// </summary>
        public Stream Content { get; }
    }
}
