using System.Net.Mime;
using NHSD.BuyingCatalogue.EmailClient.IntegrationTesting.Data;

namespace NHSD.BuyingCatalogue.EmailClient.IntegrationTesting.Builders
{
    internal sealed class EmailResponseAttachmentBuilder
    {
        public string ContentType { get; set; }
        public string FileName { get; set; }

        private EmailResponseAttachmentBuilder()
        {
            ContentType = "application/json";
            FileName = "attachment1.txt";
        }

        public static EmailResponseAttachmentBuilder Create()
        {
            return new EmailResponseAttachmentBuilder();
        }

        public EmailResponseAttachment Build()
        {
            return new EmailResponseAttachment(ContentType,FileName);
        }
    }
}
