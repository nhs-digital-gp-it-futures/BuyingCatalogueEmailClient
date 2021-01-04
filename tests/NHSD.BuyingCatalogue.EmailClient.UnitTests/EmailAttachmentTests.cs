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
    internal static class EmailAttachmentTests
    {
        [TestCase(null)]
        [TestCase("")]
        [TestCase("\t")]
        [SuppressMessage("ReSharper", "ObjectCreationAsStatement", Justification = "Exception testing")]
        public static void Constructor_NullEmptyOrWhiteSpaceFileName_ThrowsArgumentException(string fileName)
        {
            Assert.Throws<ArgumentException>(() => new EmailAttachment(fileName, Mock.Of<Stream>()));
        }

        [Test]
        [SuppressMessage("ReSharper", "ObjectCreationAsStatement", Justification = "Exception testing")]
        public static void Constructor_NullContent_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => new EmailAttachment("fileName", null!));
        }

        [Test]
        public static void Constructor_InitializesFileName()
        {
            const string fileName = "file.pdf";

            var attachment = new EmailAttachment(fileName, Mock.Of<Stream>());

            attachment.FileName.Should().Be(fileName);
        }

        [Test]
        public static void Constructor_InitializesContent()
        {
            var content = Mock.Of<Stream>();

            var attachment = new EmailAttachment("fileName", content);

            attachment.Content.Should().Be(content);
        }
    }
}
