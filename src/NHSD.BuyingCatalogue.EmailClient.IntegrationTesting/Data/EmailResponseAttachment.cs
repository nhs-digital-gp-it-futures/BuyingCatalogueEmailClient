using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace NHSD.BuyingCatalogue.EmailClient.IntegrationTesting.Data
{
    /// <summary>
    /// EmailResponseAttachment represents the attachments found in a email response from the MailDev API.
    /// </summary>
    public class EmailResponseAttachment
    {
        /// <summary>
        /// The Content Type associated with the attachment.
        /// </summary>
        [JsonProperty("contentType")]
        public string? ContentType { get; set; }
        /// <summary>
        /// The file name of the attachment.
        /// </summary>
        [JsonProperty("fileName")]
        public string? FileName { get; set; }
        /// <summary>
        /// Represents metadata found in the Attachment section of a MailDev Email api response.
        /// </summary>
        /// <param name="contentType">The content type of the attachment.</param>
        /// <param name="fileName">the file name of the attachment</param>
        public EmailResponseAttachment(string contentType, string fileName)
        {
            FileName = fileName;
            ContentType = contentType;
        }
    }
}
