using Newtonsoft.Json;

namespace NHSD.BuyingCatalogue.EmailClient.IntegrationTesting.Data
{
    /// <summary>
    /// Represents the attachments found in an e-mail response from the MailDev API.
    /// </summary>
    public sealed class EmailResponseAttachment
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EmailResponseAttachment"/> class
        /// with the specified <paramref name="contentType"/> and <paramref name="fileName"/>.
        /// </summary>
        /// <param name="contentType">The content type of the attachment.</param>
        /// <param name="fileName">The file name of the attachment.</param>
        public EmailResponseAttachment(string contentType, string fileName)
        {
            FileName = fileName;
            ContentType = contentType;
        }

        /// <summary>
        /// Gets the content type of the attachment.
        /// </summary>
        [JsonProperty("contentType")]
        public string ContentType { get; }

        /// <summary>
        /// Gets the file name of the attachment.
        /// </summary>
        [JsonProperty("fileName")]
        public string FileName { get; }
    }
}
