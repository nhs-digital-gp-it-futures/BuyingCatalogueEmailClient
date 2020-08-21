using System;
using System.Collections.Generic;
using System.Text;

namespace NHSD.BuyingCatalogue.EmailClient.IntegrationTesting.UnitTests.Builders
{
    internal sealed class EmailServerDriverResponseBuilder
    {
        private List<EmailAddress> _to = new List<EmailAddress>();
        private List<EmailAddress> _from = new List<EmailAddress>();
        private string _subject;
        private string _html;
        private string _text;
        private AttachmentStream _attachmentContent;

        private EmailServerDriverResponseBuilder()
        {
            _to.Add(new EmailAddress("recpient@email.com"));
            _from.Add(new EmailAddress("sender@email.com"));
            _subject = "important email.";
            _text = "Dear Sir or Madam";
            _html = "<p/>";
            _attachmentContent = AttachmentStreamBuilder.Create().Build();
        }

        static public EmailServerDriverResponseBuilder Create()
        {
            return new EmailServerDriverResponseBuilder();
        }

        public EmailServerDriverResponseBuilder WithTo(string address)
        {
            if (address == null)
            {
                _to = new List<EmailAddress>();
            }
            else
            {
                _to.Add(new EmailAddress(address));
            }

            return this;
        }

        public EmailServerDriverResponseBuilder WithFrom(string address)
        {
            if (address == null)
            {
                _from = new List<EmailAddress>();
            }
            else
            {
                _from.Add(new EmailAddress(address));
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

        public EmailServerDriverResponseBuilder WithAttachmentContent(AttachmentStream attachmentStream)
        {
            _attachmentContent = attachmentStream;
            return this;
        }

        public EmailServerDriverResponse Build()
        {
            return  new EmailServerDriverResponse
            {
                To=_to,
                From = _from,
                Attachment = _attachmentContent,
                Html = _html,
                Subject = _subject,
                Text = _text
            };
        }
    }
}
