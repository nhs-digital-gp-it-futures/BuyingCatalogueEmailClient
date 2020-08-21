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
        private readonly EmailServerDriverSettings EmailServerDriverSettings;

        /// <summary>
        /// EmailServerDriver allows access to the contents of a SMTP mail server.
        /// </summary>
        /// <param name="emailServerDriverSettings">The EmailDriverSettings object containing the URL of the SMTP server.</param>
        public EmailServerDriver(EmailServerDriverSettings emailServerDriverSettings)
        {
            EmailServerDriverSettings = emailServerDriverSettings ?? throw new ArgumentNullException(nameof(emailServerDriverSettings));
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
            var responseBody = await EmailServerDriverSettings
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
            await EmailServerDriverSettings
                .SmtpServerApiBaseUrl
                .AbsoluteUri
                .AppendPathSegments("email", "all")
                .DeleteAsync();
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Reliability", "CA2000:Dispose objects before losing scope", Justification = "<Pending>")]
        private static EmailAttachment? ExtractAttachment(dynamic x)
        {
            if (x.attachment == null)
            {
                return null;
            }

            byte[] byteArray = Encoding.ASCII.GetBytes(x.attachment.stream.ToString().Trim());
            var stream = new MemoryStream(byteArray);
            var fileName = x.attachment.fileName.Trim();
            var contentType = x.attachment.contentType.Trim();
            return new EmailAttachment(stream, fileName, new ContentType(contentType));
        }
    }
}
