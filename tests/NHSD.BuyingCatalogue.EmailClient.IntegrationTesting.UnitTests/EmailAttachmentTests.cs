using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Net.Mime;
using System.Text;
using FluentAssertions;
using Moq;
using NHSD.BuyingCatalogue.EmailClient.IntegrationTesting.Utils;
using NUnit.Framework;

namespace NHSD.BuyingCatalogue.EmailClient.IntegrationTesting.UnitTests
{
    [TestFixture]
    [Parallelizable(ParallelScope.All)]
    internal static class EmailAttachmentTests
    {
        [Test]
        [SuppressMessage("Reliability", "CA2000:Dispose objects before losing scope", Justification = "<Pending>")]
        public static void Constructor_InitializesFileName()
        {
            const string fileName = "file.pdf";
            byte[] byteArray = Encoding.ASCII.GetBytes("Test Stream");
            MemoryStream stream = new MemoryStream(byteArray);
            var contentType = new ContentType("application/json");
            var attachment = new EmailAttachment(stream, fileName, contentType);
            attachment.Name.Should().Be(fileName);
        }

        [Test]
        [SuppressMessage("Reliability", "CA2000:Dispose objects before losing scope", Justification = "<Pending>")]
        public static void Constructor_InitializesContent()
        {
            const string fileName = "file.pdf";
            byte[] byteArray = Encoding.ASCII.GetBytes("Test Stream");
            MemoryStream stream = new MemoryStream(byteArray);
            var contentType = new ContentType("application/json");
            var attachment = new EmailAttachment(stream, fileName, contentType);
            attachment.ContentAsString.Should().Be("Test Stream");
        }
    }
}
