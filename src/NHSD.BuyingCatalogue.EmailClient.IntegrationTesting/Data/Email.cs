namespace NHSD.BuyingCatalogue.EmailClient.IntegrationTesting.Data
{
    /// <summary>
    /// Represents the contents of an Email message.
    /// </summary>
    public sealed class Email
    {
        /// <summary>
        /// Gets or sets From.
        /// </summary>
        public string? From { get; set; }

        /// <summary>
        /// Gets or sets To.
        /// </summary>
        public string? To { get; set; }

        /// <summary>
        /// Gets or sets Subject.
        /// </summary>
        public string? Subject { get; set; }

        /// <summary>
        /// Gets or sets PlainTextBody.
        /// </summary>
        public string? PlainTextBody { get; set; }

        /// <summary>
        /// Gets or sets HtmlBody.
        /// </summary>
        public string? HtmlBody { get; set; }
    }
}
