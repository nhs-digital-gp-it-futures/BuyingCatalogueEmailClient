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
        /// The content of the attachment encoded as a string.
        /// </summary>
        public List<byte> AttachmentData { get; } = new List<byte>();

        /// <summary>
        /// The Id of the email associated with the attachment.
        /// </summary>
        public string Id { get; set; }

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
        /// <param name="id">Id of the email the attachment comes from</param>
        /// <param name="fileName">The filename of the attachment.</param>
        /// <param name="mediaType">type of encoding used when storing the attachment.</param>
        public EmailAttachmentData(string id, string fileName, ContentType mediaType)
        {
            Id = id;
            FileName = fileName;
            ContentType = mediaType;
        }
    }
}
