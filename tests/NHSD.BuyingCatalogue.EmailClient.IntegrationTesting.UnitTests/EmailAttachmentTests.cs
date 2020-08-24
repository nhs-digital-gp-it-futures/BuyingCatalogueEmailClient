using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Net.Mime;
using System.Text;
using FluentAssertions;
using Moq;
using NHSD.BuyingCatalogue.EmailClient.IntegrationTesting.Data;
using NUnit.Framework;

namespace NHSD.BuyingCatalogue.EmailClient.IntegrationTesting.UnitTests
{
    [TestFixture]
    [Parallelizable(ParallelScope.All)]
    internal static class EmailAttachmentTests
    {
        const string fileName = "file.pdf";
        private static readonly ContentType contentType = new ContentType(MediaTypeNames.Application.Json);

        [Test]
        public static void Constructor_InitializesFileName()
        {
            var attachment = new EmailAttachmentData("id1", fileName, contentType);
            attachment.FileName.Should().Be(fileName);
        }

        [Test]
        public static void Constructor_InitializesContent()
        {
            var attachment = new EmailAttachmentData("Id", fileName, contentType);
            attachment.ContentType.Should().BeEquivalentTo(contentType);
        }

        [Test]
        public static void Constructor_InitializesId()
        {
            var emailId = "Id";
            var attachment = new EmailAttachmentData(emailId, fileName, contentType);
            attachment.Id.Should().BeEquivalentTo(emailId);
        }
    }
}
