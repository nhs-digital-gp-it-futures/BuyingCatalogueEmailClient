using System;
using System.Collections.Generic;
using FluentAssertions;
using HealthChecks.Network;
using HealthChecks.Network.Core;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Moq;
using NHSD.BuyingCatalogue.EmailClient.Configuration;
using NHSD.BuyingCatalogue.EmailClient.DependencyInjection;
using NUnit.Framework;

namespace NHSD.BuyingCatalogue.EmailClient.UnitTests.DependencyInjection
{
    [TestFixture]
    [Parallelizable(ParallelScope.All)]
    internal static class HealthChecksBuilderExtensionsTests
    {
        [Test]
        public static void AddSmtpHealthCheck_NullHealthChecksBuilder_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => HealthChecksBuilderExtensions.AddSmtpHealthCheck(null!, new SmtpSettings()));
        }

        [Test]
        public static void AddSmtpHealthCheck_NullSmtpSettings_ThrowsArgumentNullException()
        {
            var builder = Mock.Of<IHealthChecksBuilder>();

            Assert.Throws<ArgumentNullException>(() => HealthChecksBuilderExtensions.AddSmtpHealthCheck(builder, null!));
        }

        [Test]
        public static void AddSmtpHealthCheck_SetsExpectedFailureStatus()
        {
            AddSmtpHealthCheckArgs healthCheckArgs = null;

            var mockBuilder = new Mock<IHealthChecksBuilder>();
            mockBuilder.Setup(b => b.Add(It.IsNotNull<HealthCheckRegistration>()))
                .Callback<HealthCheckRegistration>(r => AddSmtpHealthCheckCallback(r, out healthCheckArgs));

            var builder = mockBuilder.Object;
            builder.AddSmtpHealthCheck(new SmtpSettings());

            healthCheckArgs.FailureStatus.Should().Be(HealthStatus.Degraded);
        }

        [Test]
        public static void AddSmtpHealthCheck_SetsExpectedName()
        {
            AddSmtpHealthCheckArgs healthCheckArgs = null;

            var mockBuilder = new Mock<IHealthChecksBuilder>();
            mockBuilder.Setup(b => b.Add(It.IsNotNull<HealthCheckRegistration>()))
                .Callback<HealthCheckRegistration>(r => AddSmtpHealthCheckCallback(r, out healthCheckArgs));

            var builder = mockBuilder.Object;
            builder.AddSmtpHealthCheck(new SmtpSettings());

            healthCheckArgs.Name.Should().Be(HealthCheck.Name);
        }

        [Test]
        public static void AddSmtpHealthCheck_NullTags_SetsDefaultTags()
        {
            AddSmtpHealthCheckArgs healthCheckArgs = null;

            var mockBuilder = new Mock<IHealthChecksBuilder>();
            mockBuilder.Setup(b => b.Add(It.IsNotNull<HealthCheckRegistration>()))
                .Callback<HealthCheckRegistration>(r => AddSmtpHealthCheckCallback(r, out healthCheckArgs));

            var builder = mockBuilder.Object;
            builder.AddSmtpHealthCheck(new SmtpSettings());

            healthCheckArgs.Tags.Should().BeEquivalentTo(HealthCheck.DefaultTags);
        }

        [Test]
        public static void AddSmtpHealthCheck_SetsAllowInvalidRemoteCertificatesOption()
        {
            AddSmtpHealthCheckArgs healthCheckArgs = null;

            var mockBuilder = new Mock<IHealthChecksBuilder>();
            mockBuilder.Setup(b => b.Add(It.IsNotNull<HealthCheckRegistration>()))
                .Callback<HealthCheckRegistration>(r => AddSmtpHealthCheckCallback(r, out healthCheckArgs));

            var settings = new SmtpSettings { AllowInvalidCertificate = true };

            var builder = mockBuilder.Object;
            builder.AddSmtpHealthCheck(settings);

            healthCheckArgs.Options.AllowInvalidRemoteCertificates.Should().BeTrue();
        }

        [Test]
        public static void AddSmtpHealthCheck_SetsConnectionTypeOption()
        {
            AddSmtpHealthCheckArgs healthCheckArgs = null;

            var mockBuilder = new Mock<IHealthChecksBuilder>();
            mockBuilder.Setup(b => b.Add(It.IsNotNull<HealthCheckRegistration>()))
                .Callback<HealthCheckRegistration>(r => AddSmtpHealthCheckCallback(r, out healthCheckArgs));

            var builder = mockBuilder.Object;
            builder.AddSmtpHealthCheck(new SmtpSettings());

            healthCheckArgs.Options.ConnectionType.Should().Be(SmtpConnectionType.TLS);
        }

        [Test]
        public static void AddSmtpHealthCheck_SetsHostOption()
        {
            const string expectedHost = "myHost";

            AddSmtpHealthCheckArgs healthCheckArgs = null;

            var mockBuilder = new Mock<IHealthChecksBuilder>();
            mockBuilder.Setup(b => b.Add(It.IsNotNull<HealthCheckRegistration>()))
                .Callback<HealthCheckRegistration>(r => AddSmtpHealthCheckCallback(r, out healthCheckArgs));

            var settings = new SmtpSettings { Host = expectedHost };

            var builder = mockBuilder.Object;
            builder.AddSmtpHealthCheck(settings);

            healthCheckArgs.Options.Host.Should().Be(expectedHost);
        }

        [Test]
        public static void AddSmtpHealthCheck_SetsPortOption()
        {
            const int expectedPort = 1234;

            AddSmtpHealthCheckArgs healthCheckArgs = null;

            var mockBuilder = new Mock<IHealthChecksBuilder>();
            mockBuilder.Setup(b => b.Add(It.IsNotNull<HealthCheckRegistration>()))
                .Callback<HealthCheckRegistration>(r => AddSmtpHealthCheckCallback(r, out healthCheckArgs));

            var settings = new SmtpSettings { Port = expectedPort };

            var builder = mockBuilder.Object;
            builder.AddSmtpHealthCheck(settings);

            healthCheckArgs.Options.Port.Should().Be(expectedPort);
        }

        [Test]
        public static void AddSmtpHealthCheck_SetsExpectedTags()
        {
            AddSmtpHealthCheckArgs healthCheckArgs = null;

            var mockBuilder = new Mock<IHealthChecksBuilder>();
            mockBuilder.Setup(b => b.Add(It.IsNotNull<HealthCheckRegistration>()))
                .Callback<HealthCheckRegistration>(r => AddSmtpHealthCheckCallback(r, out healthCheckArgs));

            var tags = new[] { "myTag" };

            var builder = mockBuilder.Object;
            builder.AddSmtpHealthCheck(new SmtpSettings(), tags);

            healthCheckArgs.Tags.Should().BeEquivalentTo(tags);
        }

        [Test]
        public static void AddSmtpHealthCheck_NullTimeout_SetsDefaultTimeout()
        {
            AddSmtpHealthCheckArgs healthCheckArgs = null;

            var mockBuilder = new Mock<IHealthChecksBuilder>();
            mockBuilder.Setup(b => b.Add(It.IsNotNull<HealthCheckRegistration>()))
                .Callback<HealthCheckRegistration>(r => AddSmtpHealthCheckCallback(r, out healthCheckArgs));

            var builder = mockBuilder.Object;
            builder.AddSmtpHealthCheck(new SmtpSettings());

            healthCheckArgs.Timeout.Should().Be(HealthCheck.DefaultTimeout);
        }

        [Test]
        public static void AddSmtpHealthCheck_SetsExpectedTimeout()
        {
            AddSmtpHealthCheckArgs healthCheckArgs = null;

            var mockBuilder = new Mock<IHealthChecksBuilder>();
            mockBuilder.Setup(b => b.Add(It.IsNotNull<HealthCheckRegistration>()))
                .Callback<HealthCheckRegistration>(r => AddSmtpHealthCheckCallback(r, out healthCheckArgs));

            var timeout = TimeSpan.FromSeconds(90);

            var builder = mockBuilder.Object;
            builder.AddSmtpHealthCheck(new SmtpSettings(), timeout: timeout);

            healthCheckArgs.Timeout.Should().Be(timeout);
        }

        private static void AddSmtpHealthCheckCallback(
            HealthCheckRegistration registration,
            out AddSmtpHealthCheckArgs args)
        {
            var factoryTarget = registration.Factory.Target;
            var options = factoryTarget
                ?.GetType()
                .GetField("options")
                ?.GetValue(factoryTarget) as SmtpHealthCheckOptions;

            args = new AddSmtpHealthCheckArgs
            {
                FailureStatus = registration.FailureStatus,
                Name = registration.Name,
                Options = options,
                Tags = registration.Tags,
                Timeout = registration.Timeout,
            };
        }

        private sealed class AddSmtpHealthCheckArgs
        {
            internal HealthStatus? FailureStatus { get; set; }

            internal string Name { get; set; }

            internal SmtpHealthCheckOptions Options { get; set; }

            internal ICollection<string> Tags { get; set; }

            internal TimeSpan? Timeout { get; set; }
        }
    }
}
