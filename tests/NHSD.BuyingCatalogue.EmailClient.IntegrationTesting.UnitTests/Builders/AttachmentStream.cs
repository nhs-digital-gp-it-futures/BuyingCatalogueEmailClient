using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace NHSD.BuyingCatalogue.EmailClient.IntegrationTesting.UnitTests.Builders
{
    internal sealed class AttachmentStream
    {
        [JsonProperty("fileName")]
        public string FileName;
        [JsonProperty("stream")]
        public string Stream { get; set; }
        [JsonProperty("contentType")]
        public string ContentType { get; set; }
    }
}
