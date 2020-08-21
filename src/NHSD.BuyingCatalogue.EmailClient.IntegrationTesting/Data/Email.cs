namespace NHSD.BuyingCatalogue.EmailClient.IntegrationTesting.Data
{
    /// <summary>
    /// Represents the contents of an <see cref="Email"/> message.
    /// </summary>
    public sealed class Email
    {
        /// <summary>
        /// Gets or sets the from (sender's) address.
        /// </summary>
        public string? From { get; set; }

        /// <summary>
        /// Gets or sets the to (recipient's) address.
        /// </summary>
        public string? To { get; set; }

        /// <summary>
        /// Gets or sets the subject of the e-mail.
        /// </summary>
        public string? Subject { get; set; }

        /// <summary>
        /// Gets or sets the plain-text body of the e-mail.
        /// </summary>
        public string? PlainTextBody { get; set; }

        /// <summary>
        /// Gets or sets the HTML body of the e-mail.
        /// </summary>
        public string? HtmlBody { get; set; }
    }
}
