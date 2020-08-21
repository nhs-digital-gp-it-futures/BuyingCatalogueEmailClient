using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using FluentAssertions;
using NHSD.BuyingCatalogue.EmailClient.IntegrationTesting.Drivers;
using NUnit.Framework;

namespace NHSD.BuyingCatalogue.EmailClient.IntegrationTesting.UnitTests
{
    [TestFixture]
    [Parallelizable(ParallelScope.All)]
    internal static class EmailServiceDriverSettingsTests
    {
        [Test]
        [SuppressMessage("ReSharper", "ObjectCreationAsStatement", Justification = "Exception testing")]
        public static void Constructor_NullMessage_ThrowsException()
        {
            Assert.Throws<ArgumentNullException>(() => new EmailServerDriverSettings(null));
        }

        [Test]
        [SuppressMessage("ReSharper", "ObjectCreationAsStatement", Justification = "Exception testing")]
        public static void Constructor_Uri_IsReturnedCorrectly()
        {
            var serviceUri = new Uri("Http://bjss.com/index.Html");
            var emailServiceDriverSettings = new EmailServerDriverSettings(serviceUri);
            emailServiceDriverSettings.SmtpServerApiBaseUrl.Should().Be(serviceUri);
        }
    }
}
