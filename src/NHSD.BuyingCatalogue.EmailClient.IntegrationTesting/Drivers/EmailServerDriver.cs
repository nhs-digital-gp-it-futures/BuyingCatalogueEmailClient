using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using Flurl;
using Flurl.Http;
using Newtonsoft.Json.Linq;
using NHSD.BuyingCatalogue.EmailClient.IntegrationTesting.Data;
using NHSD.BuyingCatalogue.EmailClient.IntegrationTesting.Utils;
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
                To = x.to[0].address,
                Attachment = ExtractAttachment(x)
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

        private static EmailAttachment? ExtractAttachment(JToken x)
        {
            if (!x.Contains("attachment"))
            {
                return null;
            }

            byte[] byteArray = Encoding.ASCII.GetBytes(x.SelectToken("attachment").First().SelectToken("stream").ToString().Trim());
            var stream = new MemoryStream(byteArray);
            var fileName = x.SelectToken("attachment").First().SelectToken("fileName").ToString().Trim();
            var contentType = x.SelectToken("attachment").First().SelectToken("contentType").ToString().Trim();

            return new EmailAttachment(stream, fileName, new ContentType(contentType));
        }
    }
}
