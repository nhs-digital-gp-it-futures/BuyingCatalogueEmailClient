using System.Collections.Generic;
using Newtonsoft.Json;

namespace NHSD.BuyingCatalogue.EmailClient.IntegrationTesting.Data
{
    /// <summary>
    /// Represents the response from calls to the MailDev API.
    /// </summary>
    public sealed class EmailResponse
    {
        /// <summary>
        /// Gets or sets the ID of the message.
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
        /// Gets or sets the subject of the email.
        /// </summary>
        [JsonProperty("subject")]
        public string? Subject { get; set; }

        /// <summary>
        /// Gets the list of From addresses of the email.
        /// </summary>
        [JsonProperty("from")]
        public List<EmailAddress> From { get; } = new();

        /// <summary>
        /// Gets the list of To addresses of the email.
        /// </summary>
        [JsonProperty("to")]
        public List<EmailAddress> To { get; } = new();

        /// <summary>
        /// Gets a list of the attachment metadata.
        /// </summary>
        [JsonProperty("attachments")]
        public List<EmailResponseAttachment> Attachments { get; } = new();
    }
}
