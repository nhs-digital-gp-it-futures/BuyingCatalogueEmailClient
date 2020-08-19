namespace NHSD.BuyingCatalogue.EmailClient.IntegrationTests.Common.Data
{
    /// <summary>
    /// Email Data Class
    /// </summary>
    public sealed class Email
    {
        /// <summary>
        /// From Address
        /// </summary>
        public string? From { get; set; }

        /// <summary>
        ///  To Address
        /// </summary>
        public string? To { get; set; }

        /// <summary>
        /// Subject
        /// </summary>
        public string? Subject { get; set; }

        /// <summary>
        /// PlainTextBody
        /// </summary>
        public string? PlainTextBody { get; set; }

        /// <summary>
        /// HtmlBody
        /// </summary>
        public string? HtmlBody { get; set; }
    }
}
