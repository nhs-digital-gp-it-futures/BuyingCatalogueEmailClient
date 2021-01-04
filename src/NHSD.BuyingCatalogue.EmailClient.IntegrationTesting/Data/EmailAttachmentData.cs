using System;
using System.Collections.Generic;
using System.Net.Mime;

namespace NHSD.BuyingCatalogue.EmailClient.IntegrationTesting.Data
{
    /// <summary>
    /// Represents the contents of an attachment.
    /// </summary>
    public sealed class EmailAttachmentData
    {
        private readonly List<byte> attachmentData = new();

        /// <summary>
        /// Initializes a new instance of the <see cref="EmailAttachmentData"/> class
        /// with the provided data and metadata.
        /// </summary>
        /// <param name="data">The attachment data as a byte array.</param>
        /// <param name="fileName">The filename of the attachment.</param>
        /// <param name="mediaType">The media type of the attachment.</param>
        public EmailAttachmentData(IEnumerable<byte> data, string? fileName, ContentType mediaType)
        {
            if (fileName is null)
            {
                throw new ArgumentNullException(nameof(fileName));
            }

            attachmentData.AddRange(data);
            FileName = fileName;
            ContentType = mediaType;
        }

        /// <summary>
        /// Gets the content of the attachment in a list of bytes.
        /// </summary>
        public IReadOnlyList<byte> AttachmentData => attachmentData;

        /// <summary>
        /// Gets the file name of the attachment.
        /// </summary>
        public string FileName { get; }

        /// <summary>
        /// Gets the content type associated with the attachment.
        /// </summary>
        public ContentType ContentType { get; }
    }
}
