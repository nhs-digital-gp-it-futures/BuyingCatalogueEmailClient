using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace NHSD.BuyingCatalogue.EmailClient.IntegrationTesting.Data
{
    /// <summary>
    /// Represents the EmailResponse from calls to MailDev API
    /// </summary>
    public sealed class EmailResponse
    {
        /// <summary>
        /// Gets or sets the email Id of the message
        /// </summary>
        [JsonProperty("id")]
        public string? Id { get; set; }
        /// <summary>
        /// Gets or sets the text of the email.
        /// </summary>
        [JsonProperty("text")]
        public string? Text { get; set; }
        /// <summary>
        /// Gets or sets the HTML of the email.
        /// </summary>
        [JsonProperty("html")]
        public string? Html { get; set; }
        /// <summary>
        /// Gets or sets the Subject of the email
        /// </summary>
        [JsonProperty("subject")]
        public string? Subject { get; set; }
        /// <summary>
        /// Gets or sets the From addresses of the email
        /// </summary>
        [JsonProperty("from")]
        public List<EmailAddress> From { get; } = new List<EmailAddress>();
        /// <summary>
        /// Gets or sets the To addresses of the email
        /// </summary>
        [JsonProperty("to")]
        public List<EmailAddress> To { get; } = new List<EmailAddress>();
        /// <summary>
        /// Gets or sets the attachment meta data found on the email
        /// </summary>
        [JsonProperty("attachments")]
        public List<EmailResponseAttachment> Attachments { get; } = new List<EmailResponseAttachment>();
    }
}
