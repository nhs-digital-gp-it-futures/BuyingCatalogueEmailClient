using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace NHSD.BuyingCatalogue.EmailClient.IntegrationTesting.Data
{
    public sealed class EmailAddress
    {
        [JsonProperty("address")]
        public string? Address { get; set; }
        [JsonProperty("name")]
        public string? Name { get; set; }

        public EmailAddress(string name , string address)
        {
            Name = name;
            Address = address;
        }
    }
}
