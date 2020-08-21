using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace NHSD.BuyingCatalogue.EmailClient.IntegrationTesting.UnitTests.Builders
{
    internal sealed class EmailServerDriverResponse
    {
        [JsonProperty("to")]
        public List<EmailAddress> To { get; set; }
        [JsonProperty("from")]
        public List<EmailAddress> From { get; set; }
        [JsonProperty("subject")]
        public string Subject { get; set; }
        [JsonProperty("html")]
        public string Html { get; set; }
        [JsonProperty("text")]
        public string Text { get; set; }
        [JsonProperty("attachment")]
        public AttachmentStream Attachment { get; set; }
    }
}
