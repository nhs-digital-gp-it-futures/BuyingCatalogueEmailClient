using System.IO;
using System.Net.Mail;
using System.Net.Mime;

namespace NHSD.BuyingCatalogue.EmailClient.IntegrationTesting.Utils
{
    /// <summary>
    /// Reads and stores the contents of an Attachment from a stream.
    /// </summary>
    public sealed class EmailAttachment : Attachment
    {
        /// <summary>
        /// The content of the attachment encoded as a string.
        /// </summary>
        public string ContentAsString { get; set; }
        /// <summary>
        /// Reades the content of an attachment and stores it as an encode string. Also stores the attachment filename. 
        /// </summary>
        /// <param name="contentStream">The stream containing the attachment.</param>
        /// <param name="fileName">The filename of the attachment.</param>
        /// <param name="mediaType">type of encoding used when storing the attachment.</param>
        public EmailAttachment(Stream contentStream, string fileName, ContentType mediaType) : base(contentStream, mediaType)
        {
            using (StreamReader sr = new StreamReader(contentStream))
            {
                this.ContentAsString = sr.ReadToEnd();
            }
            this.Name = fileName;
        }
    }
}
