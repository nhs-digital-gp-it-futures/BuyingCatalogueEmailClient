using FluentAssertions;
using NHSD.BuyingCatalogue.EmailClient.Configuration;
using NUnit.Framework;

namespace NHSD.BuyingCatalogue.EmailClient.UnitTests.Configuration
{
    [TestFixture]
    [Parallelizable(ParallelScope.All)]
    internal static class SmtpSettingsTests
    {
        [Test]
        public static void SmtpSettings_AuthenticationSettings_IsInitialized()
        {
            var settings = new SmtpSettings();

            settings.Authentication.Should().NotBeNull();
        }
    }
}
