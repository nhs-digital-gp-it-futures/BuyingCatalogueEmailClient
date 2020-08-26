using System;
using System.Collections.Generic;
using System.Net.Mime;

namespace NHSD.BuyingCatalogue.EmailClient.IntegrationTesting.Data
{
    /// <summary>
    /// Reads and stores the contents of an Attachment from a stream.
    /// </summary>
    public sealed class EmailAttachmentData
    {
        /// <summary>
        /// The content of the attachment in a list of bytes.
        /// </summary>
        public List<byte> AttachmentData { get; } = new List<byte>();

        /// <summary>
        /// The file name of the attachment.
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// The content type associated with the attachment 
        /// </summary>
        public ContentType ContentType { get; set; }

        /// <summary>
        /// Reads the content of an attachment and stores it as an encode string. Also stores the attachment filename. 
        /// </summary>
        /// <param name="data">The attachment data downloaded as a byte array</param>
        /// <param name="fileName">The filename of the attachment.</param>
        /// <param name="mediaType">type of encoding used when storing the attachment.</param>
        public EmailAttachmentData(Byte[] data, string? fileName, ContentType mediaType)
        {
            if (fileName is null)
            {
                throw new ArgumentNullException(nameof(fileName));
            }
            AttachmentData.AddRange(data);
            FileName = fileName;
            ContentType = mediaType;
        }
    }
}
