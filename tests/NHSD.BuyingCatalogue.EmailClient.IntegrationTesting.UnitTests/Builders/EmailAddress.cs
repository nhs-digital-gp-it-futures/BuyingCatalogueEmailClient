using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace NHSD.BuyingCatalogue.EmailClient.IntegrationTesting.UnitTests.Builders
{
    internal sealed class EmailAddress
    {
        public EmailAddress(string address)
        {
            this.Address = address;
        }
        [JsonProperty("address")]
        public string Address { get; }
    }
}
