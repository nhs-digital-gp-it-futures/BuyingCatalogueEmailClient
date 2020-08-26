using System.Net.Mime;
using NHSD.BuyingCatalogue.EmailClient.IntegrationTesting.Data;

namespace NHSD.BuyingCatalogue.EmailClient.IntegrationTesting.UnitTests.Builders
{
    internal sealed class EmailResponseAttachmentBuilder
    {
        private EmailResponseAttachmentBuilder()
        {
            ContentType = MediaTypeNames.Application.Json;
            FileName = "attachment1.txt";
        }

        public string ContentType { get; set; }

        public string FileName { get; set; }

        public static EmailResponseAttachmentBuilder Create()
        {
            return new EmailResponseAttachmentBuilder();
        }

        public EmailResponseAttachment Build()
        {
            return new EmailResponseAttachment(ContentType, FileName);
        }
    }
}
