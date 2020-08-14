using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using FluentAssertions;
using MimeKit;
using NUnit.Framework;

namespace NHSD.BuyingCatalogue.EmailClient.UnitTests
{
    [TestFixture]
    [Parallelizable(ParallelScope.All)]
    internal static class MimeKitExtensionsTests
    {
        [Test]
        public static void AsMailboxAddress_ReturnsExpectedType()
        {
            const string name = "Some Body";

            var emailAddress = new EmailAddress(name, "a@b.test");
            var mailboxAddress = emailAddress.AsMailboxAddress();

            mailboxAddress.Should().BeOfType<MailboxAddress>();
        }

        [Test]
        public static void AsMailboxAddress_InitializesName()
        {
            const string name = "Some Body";

            var emailAddress = new EmailAddress(name, "a@b.test");
            var mailboxAddress = emailAddress.AsMailboxAddress();

            mailboxAddress.Name.Should().Be(name);
        }

        [Test]
        public static void AsMailboxAddress_InitializesAddress()
        {
            const string address = "somebody@notarealaddress.test";

            var emailAddress = new EmailAddress("Name", address);
            var mailboxAddress = emailAddress.AsMailboxAddress();

            mailboxAddress.Should().BeOfType<MailboxAddress>();
            mailboxAddress.Address.Should().Be(address);
        }

        [Test]
        public static void AsMimeMessage_NullSubject_SetsSubjectToEmptyString()
        {
            var emailMessage = new EmailMessage
            {
                Sender = new EmailAddress { Address = "a@b.uk" },
                Recipient = new EmailAddress { Address = "a@b.uk" },
            };

            var mimeMessage = emailMessage.AsMimeMessage(string.Empty);

            mimeMessage.Subject.Should().Be(string.Empty);
        }

        [Test]
        public static void AsMimeMessage_ReturnsExpectedType()
        {
            var emailMessage = new EmailMessage
            {
                Sender = new EmailAddress { Address = "sender@somedomain.nhs.test" },
                Recipient = new EmailAddress { Address = "recipient@somedomain.nhs.test" },
            };

            var mimeMessage = emailMessage.AsMimeMessage();

            mimeMessage.Should().BeOfType<MimeMessage>();
        }

        [Test]
        public static void AsMimeMessage_InitializesSender()
        {
            const string sender = "sender@somedomain.test";

            var emailMessage = new EmailMessage
            {
                Sender = new EmailAddress { Address = sender },
                Recipient = new EmailAddress { Address = "recipient@somedomain.test" },
            };

            var mimeMessage = emailMessage.AsMimeMessage();

            IEnumerable<InternetAddress> from = mimeMessage.From;
            from.Should().HaveCount(1);
            from.First().As<MailboxAddress>().Address.Should().Be(sender);
        }

        [Test]
        public static void AsMimeMessage_InitializesRecipient()
        {
            const string recipient = "recipient@somedomain.test";

            var emailMessage = new EmailMessage
            {
                Sender = new EmailAddress { Address = "sender@somedomain.test" },
                Recipient = new EmailAddress { Address = recipient },
            };

            var mimeMessage = emailMessage.AsMimeMessage();

            IEnumerable<InternetAddress> to = mimeMessage.To;
            to.Should().HaveCount(1);
            to.First().As<MailboxAddress>().Address.Should().Be(recipient);
        }

        [Test]
        public static void AsMimeMessage_InitializesSubject()
        {
            const string subject = "Subject";

            var emailMessage = new EmailMessage
            {
                Sender = new EmailAddress { Address = "sender@somedomain.nhs.test" },
                Recipient = new EmailAddress { Address = "recipient@somedomain.nhs.test" },
                Subject = subject,
            };

            var mimeMessage = emailMessage.AsMimeMessage();

            mimeMessage.Subject.Should().Be(subject);
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase("\t")]
        public static void AsMimeMessage_NullOrWhiteSpaceSubjectPrefix_InitializesSubject(string emailSubjectPrefix)
        {
            const string subject = "Subject";

            var emailMessage = new EmailMessage
            {
                Sender = new EmailAddress { Address = "sender@somedomain.nhs.test" },
                Recipient = new EmailAddress { Address = "recipient@somedomain.nhs.test" },
                Subject = subject,
            };

            var mimeMessage = emailMessage.AsMimeMessage(emailSubjectPrefix);

            mimeMessage.Subject.Should().Be(subject);
        }

        [Test]
        public static void AsMimeMessage_WithSubjectPrefix_InitializesSubject()
        {
            const string emailSubjectPrefix = "Prefix";
            const string subject = "Subject";

            var emailMessage = new EmailMessage
            {
                Sender = new EmailAddress { Address = "sender@somedomain.nhs.test" },
                Recipient = new EmailAddress { Address = "recipient@somedomain.nhs.test" },
                Subject = subject,
            };

            var mimeMessage = emailMessage.AsMimeMessage(emailSubjectPrefix);

            mimeMessage.Subject.Should().Be($"{emailSubjectPrefix} {subject}");
        }

        [Test]
        public static void AsMimeMessage_InitializesHtmlBody()
        {
            const string htmlBody = "HTML";

            var emailMessage = new EmailMessage
            {
                Sender = new EmailAddress { Address = "sender@somedomain.nhs.test" },
                Recipient = new EmailAddress { Address = "recipient@somedomain.nhs.test" },
                HtmlBody = htmlBody,
            };

            var mimeMessage = emailMessage.AsMimeMessage();

            mimeMessage.HtmlBody.Should().Be(htmlBody);
        }

        [Test]
        public static void AsMimeMessage_InitializesTextBody()
        {
            const string textBody = "Text";

            var emailMessage = new EmailMessage
            {
                Sender = new EmailAddress { Address = "sender@somedomain.nhs.test" },
                Recipient = new EmailAddress { Address = "recipient@somedomain.nhs.test" },
                TextBody = textBody,
            };

            var mimeMessage = emailMessage.AsMimeMessage();

            mimeMessage.TextBody.Should().Be(textBody);
        }

        [Test]
        public static void AsMimeMessage_WithAttachment_HasExpectedAttachment()
        {
            const string fileName = "test.csv";
            const string content = "Hello World";
            using var contentStream = new MemoryStream(Encoding.ASCII.GetBytes(content));

            var emailMessage = new EmailMessage
            {
                Sender = new EmailAddress { Address = "sender@somedomain.nhs.test" },
                Recipient = new EmailAddress { Address = "recipient@somedomain.test" },
                Attachment = new EmailAttachment(fileName, contentStream),
            };

            var mimeMessage = emailMessage.AsMimeMessage();
            var attachments = mimeMessage.Attachments.ToList();

            attachments.Should().HaveCount(1);

            var attachment = (TextPart)attachments.First();

            attachment.Should().NotBeNull();
            attachment.ContentType.MimeType.Should().Be("text/csv");
            attachment.FileName.Should().Be(fileName);
            attachment.Text.Should().Be(content);
        }
    }
}
