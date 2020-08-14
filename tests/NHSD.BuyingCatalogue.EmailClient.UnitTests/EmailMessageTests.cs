using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using FluentAssertions;
using Moq;
using NUnit.Framework;

namespace NHSD.BuyingCatalogue.EmailClient.UnitTests
{
    [TestFixture]
    [Parallelizable(ParallelScope.All)]
    internal static class EmailMessageTests
    {
        [Test]
        [SuppressMessage("ReSharper", "ObjectCreationAsStatement", Justification = "Exception testing")]
        public static void Constructor_NullMessage_ThrowsException()
        {
            Assert.Throws<ArgumentNullException>(() => new EmailMessage(null!, new Uri("https://www.nhs.uk/")));
        }

        [Test]
        [SuppressMessage("ReSharper", "ObjectCreationAsStatement", Justification = "Exception testing")]
        public static void Constructor_NullPasswordResetUrl_ThrowsException()
        {
            Assert.Throws<ArgumentNullException>(() => new EmailMessage(new EmailMessage(), null!));
        }

        [Test]
        public static void Constructor_EmailMessage_Uri_DoesNotInitializeRecipient()
        {
            var inputMessage = new EmailMessage
            {
                Sender = new EmailAddress(),
                Recipient = new EmailAddress(),
            };

            var outputMessage = new EmailMessage(inputMessage, new Uri("https://test.uk"));

            outputMessage.Recipient.Should().BeNull();
        }

        [Test]
        public static void Constructor_EmailMessage_Uri_InitializesSender()
        {
            var sender = new EmailAddress();
            var inputMessage = new EmailMessage
            {
                Sender = sender,
            };

            var outputMessage = new EmailMessage(inputMessage, new Uri("https://test.uk"));

            outputMessage.Sender.Should().Be(sender);
        }

        [Test]
        public static void Constructor_EmailMessage_Uri_InitializesSubject()
        {
            const string subject = "Subject";

            var inputMessage = new EmailMessage
            {
                Sender = new EmailAddress(),
                Subject = subject,
            };

            var outputMessage = new EmailMessage(inputMessage, new Uri("https://test.uk"));

            outputMessage.Subject.Should().Be(subject);
        }

        [Test]
        public static void Constructor_EmailMessage_Uri_InitializesHtmlBody()
        {
            const string htmlBody = "HTML " + EmailMessage.ResetPasswordLinkPlaceholder;
            const string url = "https://www.foobar.co.uk/";

            var inputMessage = new EmailMessage
            {
                Sender = new EmailAddress(),
                HtmlBody = htmlBody,
            };

            var outputMessage = new EmailMessage(inputMessage, new Uri(url));

            outputMessage.HtmlBody.Should().Be("HTML " + url);
        }

        [Test]
        public static void Constructor_EmailMessage_Uri_InitializesTextBody()
        {
            const string textBody = "Text " + EmailMessage.ResetPasswordLinkPlaceholder;
            const string url = "https://www.foobar.co.uk/";

            var inputMessage = new EmailMessage
            {
                Sender = new EmailAddress(),
                TextBody = textBody,
            };

            var outputMessage = new EmailMessage(inputMessage, new Uri(url));

            outputMessage.TextBody.Should().Be("Text " + url);
        }

        [Test]
        public static void Constructor_EmailMessage_Uri_UrlPlaceholderCaseMismatchInHtmlBody_DoesNotSetUrl()
        {
            const string url = "https://www.foobar.co.uk/";

            var htmlBody = "HTML " + EmailMessage.ResetPasswordLinkPlaceholder.ToUpperInvariant();

            var inputMessage = new EmailMessage
            {
                Sender = new EmailAddress(),
                HtmlBody = htmlBody,
            };

            var outputMessage = new EmailMessage(inputMessage, new Uri(url));

            outputMessage.HtmlBody.Should().Be(htmlBody);
        }

        [Test]
        [SuppressMessage("Globalization", "CA1308:Normalize strings to uppercase", Justification = "Testing mixed case")]
        public static void Constructor_EmailMessage_Uri_UrlPlaceholderCaseMismatchInTextBody_DoesNotSetUrl()
        {
            const string url = "https://www.foobar.co.uk/";

            var textBody = "Text " + EmailMessage.ResetPasswordLinkPlaceholder.ToLowerInvariant();

            var inputMessage = new EmailMessage
            {
                Sender = new EmailAddress(),
                TextBody = textBody,
            };

            var outputMessage = new EmailMessage(inputMessage, new Uri(url));

            outputMessage.TextBody.Should().Be(textBody);
        }

        [Test]
        [SuppressMessage("ReSharper", "ObjectCreationAsStatement", Justification = "Exception testing")]
        public static void Recipient_Set_NullAddress_ThrowsException()
        {
            Assert.Throws<ArgumentNullException>(() => new EmailMessage { Recipient = null });
        }

        [Test]
        [SuppressMessage("ReSharper", "ObjectCreationAsStatement", Justification = "Exception testing")]
        public static void Sender_Set_NullAddress_ThrowsException()
        {
            Assert.Throws<ArgumentNullException>(() => new EmailMessage { Sender = null });
        }

        [Test]
        public static void HasAttachment_NullAttachment_ReturnsFalse()
        {
            var message = new EmailMessage();

            message.HasAttachment.Should().BeFalse();
        }

        [Test]
        public static void HasAttachment_WithAttachment_ReturnsTrue()
        {
            var message = new EmailMessage
            {
                Attachment = new EmailAttachment("fileName", Mock.Of<Stream>()),
            };

            message.HasAttachment.Should().BeTrue();
        }
    }
}
