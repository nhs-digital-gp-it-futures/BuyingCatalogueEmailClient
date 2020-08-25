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
        private const string FileName = "file.pdf";
        private static readonly ContentType _contentType = new ContentType(MediaTypeNames.Application.Json);
        private static readonly byte[] dataArray =  {23,34,54};

        [Test]
        public static void Constructor_InitializesFileName()
        {
            var attachment = new EmailAttachmentData(dataArray, FileName, _contentType);
            attachment.FileName.Should().Be(FileName);
        }

        [Test]
        public static void Constructor_InitializesContent()
        {
            var attachment = new EmailAttachmentData(dataArray, FileName, _contentType);
            attachment.ContentType.Should().BeEquivalentTo(_contentType);
        }

        [Test]
        public static void Constructor_InitializesId()
        {
            var attachment = new EmailAttachmentData(dataArray, FileName, _contentType);
            attachment.AttachmentData.Should().BeEquivalentTo(dataArray);
        }
    }
}
