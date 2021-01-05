using System.Collections.Generic;
using Newtonsoft.Json;

namespace NHSD.BuyingCatalogue.EmailClient.IntegrationTesting.Data
{
    /// <summary>
    /// Represents the response from calls to the MailDev API.
    /// </summary>
    public sealed class EmailResponse
    {
#pragma warning disable IDE0044 // Add readonly modifier (must be mutable for deserialization)

        [JsonProperty("from")]
        private List<EmailAddress> from = new();

        [JsonProperty("to")]
        private List<EmailAddress> to = new();

        [JsonProperty("attachments")]
        private List<EmailResponseAttachment> attachments = new();

#pragma warning restore IDE0044 // Add readonly modifier

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
        [JsonIgnore]
        public IReadOnlyList<EmailAddress> From => from;

        /// <summary>
        /// Gets the list of To addresses of the email.
        /// </summary>
        [JsonIgnore]
        public IReadOnlyList<EmailAddress> To => to;

        /// <summary>
        /// Gets a list of the attachment metadata.
        /// </summary>
        [JsonIgnore]
        public IReadOnlyList<EmailResponseAttachment> Attachments => attachments;
    }
}
