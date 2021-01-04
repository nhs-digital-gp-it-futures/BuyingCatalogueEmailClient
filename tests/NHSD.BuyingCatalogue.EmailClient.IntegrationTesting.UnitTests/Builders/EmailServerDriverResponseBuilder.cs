using System;
using System.Collections.Generic;
using NHSD.BuyingCatalogue.EmailClient.IntegrationTesting.Data;

namespace NHSD.BuyingCatalogue.EmailClient.IntegrationTesting.UnitTests.Builders
{
    internal sealed class EmailServerDriverResponseBuilder
    {
        private const string Text = "Dear Sir or Madam";
        private readonly List<EmailAddress> _to = new();
        private readonly List<EmailAddress> _from = new();
        private readonly List<EmailResponseAttachment> _attachmentContent = new();
        private string _subject;
        private string _html;

        private EmailServerDriverResponseBuilder()
        {
            _to.Add(new EmailAddress("recipient", "recpient@email.com"));
            _from.Add(new EmailAddress("sender", "sender@email.com"));
            _subject = "important email.";
            _html = "<p/>";
            _attachmentContent.Add(EmailResponseAttachmentBuilder.Create().Build());
        }

        public static EmailServerDriverResponseBuilder Create()
        {
            return new();
        }

        public EmailServerDriverResponseBuilder WithSubject(string subject)
        {
            _subject = subject;
            return this;
        }

        public EmailServerDriverResponseBuilder WithHtml(string html)
        {
            _html = html;
            return this;
        }

        public EmailResponse Build()
        {
            var response = new EmailResponse
            {
                Id = Guid.NewGuid().ToString(),
                Html = _html,
                Subject = _subject,
                Text = Text,
            };

            response.To.AddRange(_to);
            response.From.AddRange(_from);
            response.Attachments.AddRange(_attachmentContent);
            return response;
        }
    }
}
