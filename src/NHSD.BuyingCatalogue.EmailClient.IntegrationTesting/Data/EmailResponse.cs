using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace NHSD.BuyingCatalogue.EmailClient.IntegrationTesting.Data
{
    public sealed class EmailResponse
    {
        [JsonProperty("id")]
        public string? Id { get; set; }
        [JsonProperty("text")]
        public string? Text { get; set; }
        [JsonProperty("html")]
        public string? Html { get; set; }
        [JsonProperty("subject")]
        public string? Subject { get; set; }
        [JsonProperty("from")]
        public List<EmailAddress> From { get; set; } = new List<EmailAddress>();
        [JsonProperty("to")]
        public List<EmailAddress> To { get; set; } = new List<EmailAddress>();
        [JsonProperty("attachments")]
        public List<EmailResponseAttachment> Attachments { get; set; } = new List<EmailResponseAttachment>();
    }
}
