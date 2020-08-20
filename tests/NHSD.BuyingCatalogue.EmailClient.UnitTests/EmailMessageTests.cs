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
        [SuppressMessage("ReSharper", "CoVariantArrayConversion", Justification = "Type will match")]
        public static void Constructor_EmailAddressArray_AddsExpectedRecipients()
        {
            var expectedRecipients = new[] { new EmailAddress(), new EmailAddress() };

            var message = new EmailMessage(expectedRecipients);

            message.Recipients.Should().BeEquivalentTo(expectedRecipients);
        }

        [Test]
        [SuppressMessage("ReSharper", "ObjectCreationAsStatement", Justification = "Exception testing")]
        public static void Sender_Set_NullAddress_ThrowsException()
        {
            Assert.Throws<ArgumentNullException>(() => new EmailMessage { Sender = null });
        }

        [Test]
        public static void HasAttachments_NoAttachments_ReturnsFalse()
        {
            var message = new EmailMessage();

            message.HasAttachments.Should().BeFalse();
        }

        [Test]
        public static void HasAttachments_WithAttachment_ReturnsTrue()
        {
            var message = new EmailMessage();
            message.Attachments.Add(new EmailAttachment("fileName", Mock.Of<Stream>()));

            message.HasAttachments.Should().BeTrue();
        }

        [Test]
        public static void AddFormatItems_NullFormatItems_ThrowsArgumentNullException()
        {
            var body = new EmailMessage();

            Assert.Throws<ArgumentNullException>(() => body.AddFormatItems(null!));
        }

        [Test]
        public static void AddFormatItems_AddsExpectedFormatItemsToHtmlBody()
        {
            const int one = 1;
            const string two = "2";

            var expectedFormatItems = new object[] { one, two };

            var message = new EmailMessage { HtmlBody = new EmailMessageBody() };
            message.AddFormatItems(one, two);

            message.HtmlBody!.FormatItems.Should().BeEquivalentTo(expectedFormatItems);
        }

        [Test]
        public static void AddFormatItems_AddsExpectedFormatItemsToTextBody()
        {
            const int one = 1;
            const string two = "2";

            var expectedFormatItems = new object[] { one, two };

            var message = new EmailMessage { TextBody = new EmailMessageBody() };
            message.AddFormatItems(one, two);

            message.TextBody!.FormatItems.Should().BeEquivalentTo(expectedFormatItems);
        }

        [Test]
        public static void AddRecipient_String_String_NullAddress_ThrowsArgumentNullException()
        {
            var message = new EmailMessage();

            Assert.Throws<ArgumentNullException>(() => message.AddRecipient(((string)null)!));
        }

        [TestCase("")]
        [TestCase("\t")]
        public static void AddRecipient_String_String_EmptyOrWhiteSpaceAddress_ThrowsArgumentException(string address)
        {
            var message = new EmailMessage();

            Assert.Throws<ArgumentException>(() => message.AddRecipient(address));
        }

        [Test]
        public static void AddRecipient_String_String_AddsExpectedRecipient()
        {
            const string address = "a@b.test";
            const string displayName = "Miss Address";

            var expectedEmailAddress = new EmailAddress(address, displayName);

            var message = new EmailMessage();
            message.AddRecipient(address, displayName);

            message.Recipients.Should().HaveCount(1);

            var actualEmailAddress = message.Recipients[0];
            actualEmailAddress.Should().BeEquivalentTo(expectedEmailAddress);
        }

        [Test]
        public static void AddRecipient_EmailAddress_NullAddress_ThrowsArgumentNullException()
        {
            var message = new EmailMessage();

            Assert.Throws<ArgumentNullException>(() => message.AddRecipient(null!));
        }

        [Test]
        public static void AddRecipient_EmailAddress_AddsExpectedRecipient()
        {
            var expectedEmailAddress = new EmailAddress("a@b.test", "Miss Address");

            var message = new EmailMessage();
            message.AddRecipient(expectedEmailAddress);

            message.Recipients.Should().HaveCount(1);

            var actualEmailAddress = message.Recipients[0];
            actualEmailAddress.Should().BeSameAs(expectedEmailAddress);
        }
    }
}
