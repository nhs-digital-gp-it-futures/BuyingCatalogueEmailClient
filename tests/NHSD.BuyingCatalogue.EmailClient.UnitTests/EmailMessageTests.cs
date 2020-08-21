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
        public static void Constructor_MessageTemplate_NullTemplate_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => new EmailMessage(((MessageTemplate)null)!));
        }

        [Test]
        [SuppressMessage("ReSharper", "ObjectCreationAsStatement", Justification = "Exception testing")]
        public static void Constructor_MessageTemplate_NullSender_ThrowsArgumentException()
        {
            var template = new MessageTemplate();

            Assert.Throws<ArgumentException>(() => new EmailMessage(template));
        }

        [Test]
        public static void Constructor_MessageTemplate_InitializesSender()
        {
            var sender = new EmailAddress();
            var template = new MessageTemplate { Sender = sender };

            var message = new EmailMessage(template);

            message.Sender.Should().BeSameAs(sender);
        }

        [Test]
        [SuppressMessage("ReSharper", "CoVariantArrayConversion", Justification = "Type will match")]
        public static void Constructor_MessageTemplate_InitializesRecipients()
        {
            var recipient1 = new EmailAddress();
            var recipient2 = new EmailAddress();
            var recipients = new[] { recipient1, recipient2 };

            var template = new MessageTemplate { Sender = new EmailAddress() };
            template.Recipients.Add(recipient1);
            template.Recipients.Add(recipient2);

            var message = new EmailMessage(template);

            message.Recipients.Should().HaveCount(2);
            message.Recipients.Should().BeEquivalentTo(recipients);
        }

        [Test]
        public static void Constructor_MessageTemplate_InitializesSubject()
        {
            const string subject = "Ant Morphology";
            var template = new MessageTemplate
            {
                Sender = new EmailAddress(),
                Subject = subject,
            };

            var message = new EmailMessage(template);

            message.Subject.Should().BeSameAs(subject);
        }

        [Test]
        public static void Constructor_MessageTemplate_InitializesHtmlBody()
        {
            var htmlBody = new EmailMessageBody();
            var template = new MessageTemplate
            {
                Sender = new EmailAddress(),
                HtmlBody = htmlBody,
            };

            var message = new EmailMessage(template);

            message.HtmlBody.Should().BeSameAs(htmlBody);
        }

        [Test]
        public static void Constructor_MessageTemplate_InitializesTextBody()
        {
            var textBody = new EmailMessageBody();
            var template = new MessageTemplate
            {
                Sender = new EmailAddress(),
                TextBody = textBody,
            };

            var message = new EmailMessage(template);

            message.TextBody.Should().BeSameAs(textBody);
        }

        [Test]
        [SuppressMessage("ReSharper", "CoVariantArrayConversion", Justification = "Type will match")]
        public static void Constructor_EmailAddress_EmailAddressArray_AddsExpectedRecipients()
        {
            var expectedRecipients = new[] { new EmailAddress(), new EmailAddress() };

            var message = new EmailMessage(new EmailAddress(), expectedRecipients);

            message.Recipients.Should().BeEquivalentTo(expectedRecipients);
        }

        [Test]
        public static void HasAttachments_NoAttachments_ReturnsFalse()
        {
            var message = new EmailMessage(new EmailAddress());

            message.HasAttachments.Should().BeFalse();
        }

        [Test]
        public static void HasAttachments_WithAttachment_ReturnsTrue()
        {
            var message = new EmailMessage(new EmailAddress());
            message.Attachments.Add(new EmailAttachment("fileName", Mock.Of<Stream>()));

            message.HasAttachments.Should().BeTrue();
        }

        [Test]
        public static void AddFormatItems_NullFormatItems_ThrowsArgumentNullException()
        {
            var body = new EmailMessage(new EmailAddress());

            Assert.Throws<ArgumentNullException>(() => body.AddFormatItems(null!));
        }

        [Test]
        public static void AddFormatItems_AddsExpectedFormatItemsToHtmlBody()
        {
            const int one = 1;
            const string two = "2";

            var expectedFormatItems = new object[] { one, two };

            var message = new EmailMessage(new EmailAddress()) { HtmlBody = new EmailMessageBody() };
            message.AddFormatItems(one, two);

            message.HtmlBody!.FormatItems.Should().BeEquivalentTo(expectedFormatItems);
        }

        [Test]
        public static void AddFormatItems_AddsExpectedFormatItemsToTextBody()
        {
            const int one = 1;
            const string two = "2";

            var expectedFormatItems = new object[] { one, two };

            var message = new EmailMessage(new EmailAddress()) { TextBody = new EmailMessageBody() };
            message.AddFormatItems(one, two);

            message.TextBody!.FormatItems.Should().BeEquivalentTo(expectedFormatItems);
        }

        [Test]
        public static void AddRecipient_String_String_NullAddress_ThrowsArgumentNullException()
        {
            var message = new EmailMessage(new EmailAddress());

            Assert.Throws<ArgumentNullException>(() => message.AddRecipient(((string)null)!));
        }

        [TestCase("")]
        [TestCase("\t")]
        public static void AddRecipient_String_String_EmptyOrWhiteSpaceAddress_ThrowsArgumentException(string address)
        {
            var message = new EmailMessage(new EmailAddress());

            Assert.Throws<ArgumentException>(() => message.AddRecipient(address));
        }

        [Test]
        public static void AddRecipient_String_String_AddsExpectedRecipient()
        {
            const string address = "a@b.test";
            const string displayName = "Miss Address";

            var expectedEmailAddress = new EmailAddress(address, displayName);

            var message = new EmailMessage(new EmailAddress());
            message.AddRecipient(address, displayName);

            message.Recipients.Should().HaveCount(1);

            var actualEmailAddress = message.Recipients[0];
            actualEmailAddress.Should().BeEquivalentTo(expectedEmailAddress);
        }

        [Test]
        public static void AddRecipient_EmailAddress_NullAddress_ThrowsArgumentNullException()
        {
            var message = new EmailMessage(new EmailAddress());

            Assert.Throws<ArgumentNullException>(() => message.AddRecipient(null!));
        }

        [Test]
        public static void AddRecipient_EmailAddress_AddsExpectedRecipient()
        {
            var expectedEmailAddress = new EmailAddress("a@b.test", "Miss Address");

            var message = new EmailMessage(new EmailAddress());
            message.AddRecipient(expectedEmailAddress);

            message.Recipients.Should().HaveCount(1);

            var actualEmailAddress = message.Recipients[0];
            actualEmailAddress.Should().BeSameAs(expectedEmailAddress);
        }
    }
}
