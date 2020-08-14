using FluentAssertions;
using NUnit.Framework;

namespace NHSD.BuyingCatalogue.EmailClient.UnitTests
{
    [TestFixture]
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
