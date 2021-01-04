using System;
using System.Collections.Generic;
using NHSD.BuyingCatalogue.EmailClient.IntegrationTesting.Data;

namespace NHSD.BuyingCatalogue.EmailClient.IntegrationTesting.UnitTests.Builders
{
    internal sealed class EmailServerDriverResponseBuilder
    {
        private const string Text = "Dear Sir or Madam";
        private readonly List<EmailAddress> to = new();
        private readonly List<EmailAddress> from = new();
        private readonly List<EmailResponseAttachment> attachmentContent = new();
        private string subject;
        private string html;

        private EmailServerDriverResponseBuilder()
        {
            to.Add(new EmailAddress("recipient", "recpient@email.com"));
            from.Add(new EmailAddress("sender", "sender@email.com"));
            subject = "important email.";
            html = "<p/>";
            attachmentContent.Add(EmailResponseAttachmentBuilder.Create().Build());
        }

        public static EmailServerDriverResponseBuilder Create()
        {
            return new();
        }

        public EmailServerDriverResponseBuilder WithSubject(string value)
        {
            subject = value;
            return this;
        }

        public EmailServerDriverResponseBuilder WithHtml(string content)
        {
            html = content;
            return this;
        }

        public EmailResponse Build()
        {
            var response = new EmailResponse
            {
                Id = Guid.NewGuid().ToString(),
                Html = html,
                Subject = subject,
                Text = Text,
            };

            response.To.AddRange(to);
            response.From.AddRange(from);
            response.Attachments.AddRange(attachmentContent);
            return response;
        }
    }
}
