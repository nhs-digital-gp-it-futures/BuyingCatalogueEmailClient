using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Flurl;
using Flurl.Http;
using NHSD.BuyingCatalogue.EmailClient.IntegrationTests.Common.Data;
using NHSD.BuyingCatalogue.EmailClient.IntegrationTests.Common.Support;
using TechTalk.SpecFlow;

namespace NHSD.BuyingCatalogue.EmailClient.IntegrationTests.Common.Drivers
{
    /// <summary>
    /// EmailServerDriver
    /// </summary>
    [Binding]
    public sealed class EmailServerDriver
    {
        private readonly ScenarioContext _context;
        private readonly EmailServiceDriverSettings _emailServiceDriverSettings;

        /// <summary>
        /// EmailServerDriver
        /// </summary>
        /// <param name="context">ScenarioContext</param>
        /// <param name="emailServiceDriverSettings">EmailDriverSettings</param>
        public EmailServerDriver(ScenarioContext context, EmailServiceDriverSettings emailServiceDriverSettings)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _emailServiceDriverSettings = emailServiceDriverSettings ?? throw new ArgumentNullException(nameof(emailServiceDriverSettings));
        }

        /// <summary>
        /// GetEmailCountAsync
        /// </summary>
        /// <returns>int number of emails</returns>
        public async Task<int> GetEmailCountAsync()
        {
            var emailList = await FindAllEmailsAsync();
            return emailList.Count();
        }

        /// <summary>
        /// FindAllEmailsAsync
        /// </summary>
        /// <returns>IEnumerable of Email</returns>
        public async Task<IEnumerable<Email>> FindAllEmailsAsync()
        {
            var responseBody = await _emailServiceDriverSettings
                .SmtpServerApiBaseUrl
                .AppendPathSegment("email")
                .GetJsonListAsync();

            return responseBody.Select(x => new Email
            {
                PlainTextBody = x.text,
                HtmlBody = x.html,
                Subject = x.subject,
                From = x.from[0].address,
                To = x.to[0].address
            });
        }

        /// <summary>
        /// ClearAllEmailsAsync
        /// </summary>
        /// <returns>Task</returns>
        public async Task ClearAllEmailsAsync()
        {
            if (_context.TryGetValue(ScenarioContextKeys.EmailSent, out bool _))
            {
                await _emailServiceDriverSettings
                    .SmtpServerApiBaseUrl
                    .AppendPathSegments("email", "all")
                    .DeleteAsync();
            }
        }
    }
}
