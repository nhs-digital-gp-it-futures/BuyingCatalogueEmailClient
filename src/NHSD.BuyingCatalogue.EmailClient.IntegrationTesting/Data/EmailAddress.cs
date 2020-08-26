using Newtonsoft.Json;

namespace NHSD.BuyingCatalogue.EmailClient.IntegrationTesting.Data
{
    /// <summary>
    /// Represents an e-mail address.
    /// </summary>
    public sealed class EmailAddress
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EmailAddress"/> class
        /// with the specified <paramref name="name"/> and <paramref name="address"/>.
        /// </summary>
        /// <param name="name">An options display name, for example James Dean.</param>
        /// <param name="address">The actual e-mail address.</param>
        public EmailAddress(string name, string address)
        {
            Name = name;
            Address = address;
        }

        /// <summary>
        /// Gets or sets the address.
        /// </summary>
        [JsonProperty("address")]
        public string? Address { get; set; }

        /// <summary>
        /// Gets or sets the display names associated with the e-mail address.
        /// </summary>
        [JsonProperty("name")]
        public string? Name { get; set; }
    }
}
