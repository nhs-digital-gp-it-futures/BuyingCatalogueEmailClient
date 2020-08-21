using System;
using MailKit;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NHSD.BuyingCatalogue.EmailClient.Configuration;

namespace NHSD.BuyingCatalogue.EmailClient.DependencyInjection
{
    /// <summary>
    /// Contains extension methods to <see cref="IServiceCollection"/> for configuring the e-mail client.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Adds the e-mail client.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/> to add the services to.</param>
        /// <param name="smtpSettings">The SMTP settings to use.</param>
        /// <exception cref="ArgumentNullException"><paramref name="services"/> is <see langref="null"/>.</exception>
        /// <exception cref="ArgumentNullException"><paramref name="smtpSettings"/> is <see langref="null"/>.</exception>
        /// <returns>A reference to this instance after the operation has completed.</returns>
        public static IServiceCollection AddEmailClient(this IServiceCollection services, SmtpSettings smtpSettings)
        {
            if (services is null)
                throw new ArgumentNullException(nameof(services));

            if (smtpSettings is null)
                throw new ArgumentNullException(nameof(smtpSettings));

            return services
                .AddSingleton(smtpSettings)
                .AddScoped<IMailTransport, SmtpClient>()
                .AddTransient<IEmailService, MailKitEmailService>();
        }

        /// <summary>
        /// Adds the e-mail client.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/> to add the services to.</param>
        /// <param name="smtpConfiguration">The SMTP configuration to use.</param>
        /// <exception cref="ArgumentNullException"><paramref name="services"/> is <see langref="null"/>.</exception>
        /// <exception cref="ArgumentNullException"><paramref name="smtpConfiguration"/> is <see langref="null"/>.</exception>
        /// <returns>A reference to this instance after the operation has completed.</returns>
        public static IServiceCollection AddEmailClient(this IServiceCollection services, IConfiguration smtpConfiguration)
        {
            if (smtpConfiguration is null)
                throw new ArgumentNullException(nameof(smtpConfiguration));

            var smtpSettings = smtpConfiguration.Get<SmtpSettings>();

            return AddEmailClient(services, smtpSettings);
        }
    }
}
