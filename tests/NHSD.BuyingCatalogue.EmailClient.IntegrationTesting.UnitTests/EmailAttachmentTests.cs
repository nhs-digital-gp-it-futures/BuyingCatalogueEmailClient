using System.Net.Mime;
using FluentAssertions;
using NHSD.BuyingCatalogue.EmailClient.IntegrationTesting.Data;
using NUnit.Framework;

namespace NHSD.BuyingCatalogue.EmailClient.IntegrationTesting.UnitTests
{
    [TestFixture]
    [Parallelizable(ParallelScope.All)]
    internal static class EmailAttachmentTests
    {
        private const string FileName = "file.pdf";
        private static readonly ContentType ContentType = new(MediaTypeNames.Application.Json);
        private static readonly byte[] DataArray = { 23, 34, 54 };

        [Test]
        public static void Constructor_InitializesFileName()
        {
            var attachment = new EmailAttachmentData(DataArray, FileName, ContentType);
            attachment.FileName.Should().Be(FileName);
        }

        [Test]
        public static void Constructor_InitializesContent()
        {
            var attachment = new EmailAttachmentData(DataArray, FileName, ContentType);
            attachment.ContentType.Should().BeEquivalentTo(ContentType);
        }

        [Test]
        public static void Constructor_InitializesId()
        {
            var attachment = new EmailAttachmentData(DataArray, FileName, ContentType);
            attachment.AttachmentData.Should().BeEquivalentTo(DataArray);
        }
    }
}
