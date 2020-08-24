using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace NHSD.BuyingCatalogue.EmailClient.IntegrationTesting.Data
{
    public class EmailResponseAttachment
    {
        [JsonProperty("contentType")]
        public string? ContentType { get; set; }
        [JsonProperty("fileName")]
        public string? FileName { get; set; }

        public EmailResponseAttachment(string contentType, string fileName)
        {
            FileName = fileName;
            ContentType = contentType;
        }
    }
}
