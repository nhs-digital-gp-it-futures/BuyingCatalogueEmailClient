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
        public byte[]? AttachmentData { get; set; }

        public string Id { get; set; }

        public string FileName { get; set; }

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
