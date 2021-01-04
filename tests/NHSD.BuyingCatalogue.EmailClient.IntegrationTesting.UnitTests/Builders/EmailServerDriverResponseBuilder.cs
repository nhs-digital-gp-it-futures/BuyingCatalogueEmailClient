using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
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

            SetCollectionProperty(response, nameof(EmailResponse.To), to);
            SetCollectionProperty(response, nameof(EmailResponse.From), from);
            SetCollectionProperty(response, nameof(EmailResponse.Attachments), attachmentContent);

            return response;
        }

        [SuppressMessage("Globalization", "CA1308:Normalize strings to uppercase", Justification = "Needs to be lower")]
        private static void SetCollectionProperty<T>(
            EmailResponse response,
            string propertyName,
            IEnumerable<T> values)
        {
            var fieldInfo = response.GetType().GetField(
                propertyName.ToLowerInvariant(),
                BindingFlags.Instance | BindingFlags.NonPublic);

            fieldInfo?.SetValue(response, values);
        }
    }
}
