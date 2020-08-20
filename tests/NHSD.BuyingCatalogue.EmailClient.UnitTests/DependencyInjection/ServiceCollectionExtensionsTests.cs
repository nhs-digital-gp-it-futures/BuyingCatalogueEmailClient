using System;
using System.Collections.Generic;
using FluentAssertions;
using MailKit;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Moq;
using NHSD.BuyingCatalogue.EmailClient.Configuration;
using NHSD.BuyingCatalogue.EmailClient.DependencyInjection;
using NUnit.Framework;

namespace NHSD.BuyingCatalogue.EmailClient.UnitTests.DependencyInjection
{
    [TestFixture]
    internal static class ServiceCollectionExtensionsTests
    {
        [Test]
        public static void AddEmailClient_IConfiguration_NullSmtpSettings_ThrowsArgumentNullException()
        {
            var services = new ServiceCollection();

            Assert.Throws<ArgumentNullException>(() => services.AddEmailClient(((IConfiguration)null)!));
        }

        [Test]
        public static void AddEmailClient_IConfiguration_AddsMailKitEmailService()
        {
            var inMemorySettings = new Dictionary<string, string> { { "Host", "Foo" } };
            IConfiguration config = new ConfigurationBuilder()
                .AddInMemoryCollection(inMemorySettings)
                .Build();

            var services = new ServiceCollection();
            services.AddSingleton(Mock.Of<ILogger<MailKitEmailService>>());
            services.AddEmailClient(config);

            using var provider = services.BuildServiceProvider();
            var mailTransport = provider.GetRequiredService<IEmailService>();

            mailTransport.Should().NotBeNull();
            mailTransport.Should().BeOfType<MailKitEmailService>();
        }

        [Test]
        public static void AddEmailClient_IConfiguration_AddsSmtpClient()
        {
            var inMemorySettings = new Dictionary<string, string> { { "Host", "Foo" } };
            IConfiguration config = new ConfigurationBuilder()
                .AddInMemoryCollection(inMemorySettings)
                .Build();

            var services = new ServiceCollection();
            services.AddEmailClient(config);

            using var provider = services.BuildServiceProvider();
            var mailTransport = provider.GetRequiredService<IMailTransport>();

            mailTransport.Should().NotBeNull();
            mailTransport.Should().BeOfType<SmtpClient>();
        }

        [Test]
        public static void AddEmailClient_SmtpSettings_NullServices_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => (((IServiceCollection)null)!).AddEmailClient(new SmtpSettings()));
        }

        [Test]
        public static void AddEmailClient_SmtpSettings_NullSmtpSettings_ThrowsArgumentNullException()
        {
            var services = new ServiceCollection();

            Assert.Throws<ArgumentNullException>(() => services.AddEmailClient(((SmtpSettings)null)!));
        }

        [Test]
        public static void AddEmailClient_SmtpSettings_AddsMailKitEmailService()
        {
            var services = new ServiceCollection();

            services.AddSingleton(Mock.Of<ILogger<MailKitEmailService>>());
            services.AddEmailClient(new SmtpSettings());

            using var provider = services.BuildServiceProvider();
            var mailTransport = provider.GetRequiredService<IEmailService>();

            mailTransport.Should().NotBeNull();
            mailTransport.Should().BeOfType<MailKitEmailService>();
        }

        [Test]
        public static void AddEmailClient_SmtpSettings_AddsSmtpClient()
        {
            var services = new ServiceCollection();

            services.AddEmailClient(new SmtpSettings());

            using var provider = services.BuildServiceProvider();
            var mailTransport = provider.GetRequiredService<IMailTransport>();

            mailTransport.Should().NotBeNull();
            mailTransport.Should().BeOfType<SmtpClient>();
        }
    }
}
