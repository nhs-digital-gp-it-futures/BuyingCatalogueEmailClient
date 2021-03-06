﻿using System;
using System.Collections.Generic;
using HealthChecks.Network.Core;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using NHSD.BuyingCatalogue.EmailClient.Configuration;

namespace NHSD.BuyingCatalogue.EmailClient.DependencyInjection
{
    /// <summary>
    /// Contains extension methods to <see cref="IHealthChecksBuilder"/> for configuring SMTP health checks.
    /// </summary>
    public static class HealthChecksBuilderExtensions
    {
        /// <summary>
        /// Add a health check for the SMTP connection.
        /// </summary>
        /// <param name="healthChecksBuilder">The <see cref="IHealthChecksBuilder"/> instance.</param>
        /// <param name="smtpSettings">The SMTP connection settings to use for the health check.</param>
        /// <param name="tags">An optional list of tags used to filter sets of health checks. The default is <see cref="HealthCheck.DefaultTags"/>.</param>
        /// <param name="timeout">An optional timeout value. The default is <see cref="HealthCheck.DefaultTimeout"/>.</param>
        /// <exception cref="ArgumentNullException"><paramref name="healthChecksBuilder"/> is <see langref="null"/>.</exception>
        /// <exception cref="ArgumentNullException"><paramref name="smtpSettings"/> is <see langref="null"/>.</exception>
        /// <returns>A reference to this instance after the operation has completed.</returns>
        public static IHealthChecksBuilder AddSmtpHealthCheck(
            this IHealthChecksBuilder healthChecksBuilder,
            SmtpSettings smtpSettings,
            IEnumerable<string>? tags = null,
            TimeSpan? timeout = null)
        {
            if (healthChecksBuilder is null)
                throw new ArgumentNullException(nameof(healthChecksBuilder));

            if (smtpSettings is null)
                throw new ArgumentNullException(nameof(smtpSettings));

            healthChecksBuilder.AddSmtpHealthCheck(
                smtp =>
                {
                    smtp.AllowInvalidRemoteCertificates = smtpSettings.AllowInvalidCertificate.GetValueOrDefault();
                    smtp.ConnectionType = SmtpConnectionType.TLS;
                    smtp.Host = smtpSettings.Host;
                    smtp.Port = smtpSettings.Port;
                },
                HealthCheck.Name,
                HealthStatus.Degraded,
                tags ?? HealthCheck.DefaultTags,
                timeout ?? HealthCheck.DefaultTimeout);

            return healthChecksBuilder;
        }
    }
}
