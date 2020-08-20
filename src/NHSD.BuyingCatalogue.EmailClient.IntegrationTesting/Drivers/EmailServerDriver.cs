using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Flurl;
using Flurl.Http;
using NHSD.BuyingCatalogue.EmailClient.IntegrationTesting.Data;
using TechTalk.SpecFlow;

namespace NHSD.BuyingCatalogue.EmailClient.IntegrationTesting.Drivers
{
    /// <summary>
    ///  Manages interaction with a SMTP server.
    /// </summary>
    [Binding]
    public sealed class EmailServerDriver
    {
        private readonly EmailServiceDriverSettings _emailServiceDriverSettings;

        /// <summary>
        /// EmailServerDriver allows access to the contents of a SMTP mail server.
        /// </summary>
        /// <param name="emailServiceDriverSettings">The EmailDriverSettings object containing the URL of the SMTP server.</param>
        public EmailServerDriver(EmailServiceDriverSettings emailServiceDriverSettings)
        {
            _emailServiceDriverSettings = emailServiceDriverSettings ?? throw new ArgumentNullException(nameof(emailServiceDriverSettings));
        }

        /// <summary>
        /// Gets the number of emails in the mailbox.
        /// </summary>
        /// <returns>the number of <see cref="Email"/> sent</returns>
        public async Task<int> GetEmailCountAsync()
        {
            var emailList = await FindAllEmailsAsync();
            return emailList.Count;
        }

        /// <summary>
        /// FindAllEmailsAsync gets email objects from the SMTP service.
        /// </summary>
        /// <returns>A IReadOnlyList of <see cref="Email"/></returns>
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
                To = x.to[0].address
            }).ToList();
        }

        /// <summary>
        /// ClearAllEmailsAsync deletes all emails from the SMTP service.
        /// </summary>
        /// <returns><see cref="Task" /></returns>
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
