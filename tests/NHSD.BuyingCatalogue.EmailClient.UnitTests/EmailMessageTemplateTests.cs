using System;
using FluentAssertions;
using NUnit.Framework;

namespace NHSD.BuyingCatalogue.EmailClient.UnitTests
{
    [TestFixture]
    internal static class EmailMessageTemplateTests
    {
        [Test]
        public static void Constructor_EmailAddressTemplate_InitializesSender()
        {
            var addressTemplate = new EmailAddressTemplate();

            var template = new EmailMessageTemplate(addressTemplate);

            template.Sender.Should().BeSameAs(addressTemplate);
        }

        [Test]
        public static void GetSenderAsEmailAddress_NullAddress_ReturnsNull()
        {
            var template = new EmailMessageTemplate();

            var address = template.GetSenderAsEmailAddress();

            address.Should().BeNull();
        }

        [Test]
        public static void GetSenderAsEmailAddress_ReturnsExpectedAddress()
        {
            var addressTemplate = new EmailAddressTemplate { Address = "someone@somewhere.test" };
            var messageTemplate = new EmailMessageTemplate { Sender = addressTemplate };

            var expectedAddress = addressTemplate.AsEmailAddress();
            var actualAddress = messageTemplate.GetSenderAsEmailAddress();

            actualAddress.Should().BeEquivalentTo(expectedAddress);
        }

        [Test]
        public static void Sender_Set_NullSender_ThrowsArgumentNullException()
        {
            var template = new EmailMessageTemplate();

            Assert.Throws<ArgumentNullException>(() => template.Sender = null);
        }
    }
}
