using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using Flurl;
using Flurl.Http;
using NHSD.BuyingCatalogue.EmailClient.IntegrationTesting.Data;
using TechTalk.SpecFlow;

namespace NHSD.BuyingCatalogue.EmailClient.IntegrationTesting.Drivers
{
    /// <summary>
    /// Provides functionality for executing integration tests
    /// that test e-mail functionality.
    /// </summary>
    [Binding]
    [SuppressMessage("ReSharper", "UnusedMember.Global", Justification = "Public API")]
    public sealed class EmailServerDriver
    {
        private readonly EmailServiceDriverSettings _emailServiceDriverSettings;

        /// <summary>
        /// Initializes a new instance of the <see cref="EmailServerDriver" /> class
        /// with the supplied settings.
        /// </summary>
        /// <param name="emailServiceDriverSettings">The driver settings.</param>
        public EmailServerDriver(EmailServiceDriverSettings emailServiceDriverSettings)
        {
            _emailServiceDriverSettings = emailServiceDriverSettings ?? throw new ArgumentNullException(nameof(emailServiceDriverSettings));
        }

        /// <summary>
        /// Returns the number of e-mails in the mailbox.
        /// </summary>
        /// <returns>the number of e-mails in the mailbox.</returns>
        public async Task<int> GetEmailCountAsync()
        {
            var emailList = await FindAllEmailsAsync();
            return emailList.Count;
        }

        /// <summary>
        /// Returns a list containing all e-mails in the mailbox.
        /// </summary>
        /// <returns>a list containing all e-mails in the mailbox.</returns>
        public async Task<IReadOnlyList<Email>> FindAllEmailsAsync()
        {
            var responseBody = await _emailServiceDriverSettings
                .SmtpServerApiBaseUrl
                .AbsoluteUri
                .AppendPathSegment("email")
                .GetJsonListAsync();

            return responseBody.Select(x => new Email
            {
                PlainTextBody = x.text,
                HtmlBody = x.html,
                Subject = x.subject,
                From = x.from[0].address,
                To = x.to[0].address,
            }).ToList();
        }

        /// <summary>
        /// Deletes all e-mails from the mailbox.
        /// </summary>
        /// <returns>An asynchronous <see cref="Task" /> context.</returns>
        public async Task ClearAllEmailsAsync()
        {
            await _emailServiceDriverSettings
                .SmtpServerApiBaseUrl
                .AbsoluteUri
                .AppendPathSegments("email", "all")
                .DeleteAsync();
        }
    }
}
