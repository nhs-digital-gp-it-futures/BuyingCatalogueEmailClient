using System.Collections.Generic;
using NHSD.BuyingCatalogue.EmailClient.IntegrationTesting.Data;

namespace NHSD.BuyingCatalogue.EmailClient.IntegrationTesting.Builders
{
    internal sealed class EmailServerDriverResponseBuilder
    {
        private static int IdNumber = 0;
        private readonly List<EmailAddress> _to = new List<EmailAddress>();
        private readonly List<EmailAddress> _from = new List<EmailAddress>();
        private string _subject;
        private string? _html;
        private string _text;
        private readonly List<EmailResponseAttachment> _attachmentContent =new List<EmailResponseAttachment>();

        private EmailServerDriverResponseBuilder()
        {
            _to.Add(new EmailAddress("recpient","recpient@email.com"));
            _from.Add(new EmailAddress("sender","sender@email.com"));
            _subject = "important email.";
            _text = "Dear Sir or Madam";
            _html = "<p/>";
            _attachmentContent.Add(EmailResponseAttachmentBuilder.Create().Build());
        }

        public static EmailServerDriverResponseBuilder Create()
        {
            return new EmailServerDriverResponseBuilder();
        }

        public EmailServerDriverResponseBuilder WithTo(string address, string name="anonymous")
        {
            if (address == null )
            {
                _to.Clear();
            }
            else
            {
                _to.Add(new EmailAddress(name, address));
            }

            return this;
        }

        public EmailServerDriverResponseBuilder WithFrom(string address, string name="anonymous")
        {
            if (address == null)
            {
                _from.Clear();
            }
            else
            {
                _from.Add(new EmailAddress(name, address));
            }
            return this;
        }

        public EmailServerDriverResponseBuilder WithSubject(string subject)
        {
            _subject = subject;
            return this;
        }

        public EmailServerDriverResponseBuilder WithText(string text)
        {
            _text = text;
            return this;
        }

        public EmailServerDriverResponseBuilder WithHtml(string html)
        {
            _html = html;
            return this;
        }

        public EmailServerDriverResponseBuilder WithAttachmentContent(EmailResponseAttachment attachmentMetadata)
        {
            if (attachmentMetadata is null)
            {
                _attachmentContent.Clear();
            }
            else
            {
                _attachmentContent.Add(attachmentMetadata);
            }
            return this;
        }

        public EmailResponse Build()
        {
            var response= new EmailResponse
            {
                Id = @"ID"+ (++IdNumber),
                Html = _html,
                Subject = _subject,
                Text = _text
            };

            response.To.AddRange(_to);
            response.From.AddRange(_from);
            response.Attachments.AddRange(_attachmentContent);
            return response;
        }
    }
}
