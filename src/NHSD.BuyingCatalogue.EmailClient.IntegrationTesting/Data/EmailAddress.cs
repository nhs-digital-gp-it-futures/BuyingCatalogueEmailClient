using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace NHSD.BuyingCatalogue.EmailClient.IntegrationTesting.Data
{
    /// <summary>
    /// The Name and Address used to send and receive emails
    /// </summary>
    public sealed class EmailAddress
    {
        /// <summary>
        /// The Address.
        /// </summary>
        [JsonProperty("address")]
        public string? Address { get; set; }
        /// <summary>
        /// The Names associated with the email address
        /// </summary>
        [JsonProperty("name")]
        public string? Name { get; set; }
        /// <summary>
        /// Creates a EmailAddress containing the Name and address to send to
        /// </summary>
        /// <param name="name">Name for example James Dean</param>
        /// <param name="address">JamesD@email.com</param>
        public EmailAddress(string name , string address)
        {
            Name = name;
            Address = address;
        }
    }
}
