using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using Flurl;
using Flurl.Http;
using Newtonsoft.Json.Linq;
using NHSD.BuyingCatalogue.EmailClient.IntegrationTesting.Data;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist.ValueRetrievers;
using NHSD.BuyingCatalogue.EmailClient.IntegrationTesting.Extensions;

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
        private readonly EmailServerDriverSettings _emailServerDriverSettings;

        /// <summary>
        /// Initializes a new instance of the <see cref="EmailServerDriver" /> class
        /// with the supplied settings.
        /// </summary>
        /// <param name="emailServerDriverSettings">The EmailDriverSettings object containing the URL of the SMTP server.</param>
        public EmailServerDriver(EmailServerDriverSettings emailServerDriverSettings)
        {
            _emailServerDriverSettings = emailServerDriverSettings ?? throw new ArgumentNullException(nameof(emailServerDriverSettings));
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
        /// Gets the binary data associated with the attachment
        /// </summary>
        /// <param name="attachment">Attachment data <see cref="EmailAttachmentData"/></param>
        /// <returns></returns>
        public async Task<byte[]> DownloadAttachment(EmailAttachmentData attachment)
        {
            return await attachment.DownloadDataAsync(_emailServerDriverSettings);
        }

        /// <summary>
        /// FindAllEmailsAsync gets email objects from the SMTP service.
        /// </summary>
        /// <returns>a list containing all e-mails in the mailbox.</returns>
        public async Task<IReadOnlyList<Email>> FindAllEmailsAsync()
        {
            var responseBody = await _emailServerDriverSettings
                .SmtpServerApiBaseUrl
                .AbsoluteUri
                .AppendPathSegment("email")
                .GetJsonAsync<EmailResponse[]>();

            var emails=  responseBody.Select(ProcessEmailResponse).ToList();
            return emails;
        }

        private Email ProcessEmailResponse(EmailResponse response)
        {
            if (response is null)
            {
                throw new ArgumentNullException(nameof(response));
            }

            var email= new Email
                {
                    PlainTextBody = response.Text,
                    HtmlBody = response.Html,
                    Subject = response.Subject
                };
            email.From.AddRange(response.From.Where(x=> x!=null));
            email.To.AddRange(response.To.Where(x=> x!=null));
            email.Attachments.AddRange(ExtractAttachmentsMetadata(response));
            return email;
        }

        /// <summary>
        /// Deletes all e-mails from the mailbox.
        /// </summary>
        /// <returns>An asynchronous <see cref="Task" /> context.</returns>
        public async Task ClearAllEmailsAsync()
        {
            await _emailServerDriverSettings
                .SmtpServerApiBaseUrl
                .AbsoluteUri
                .AppendPathSegments("email", "all")
                .DeleteAsync();
        }

        private static List<EmailAttachmentData> ExtractAttachmentsMetadata(EmailResponse emailResponse)
        {
            if (emailResponse is null)
            {
                throw new ArgumentNullException(nameof(emailResponse));
            }

            var attachmentResults = new List<EmailAttachmentData>();

            if (emailResponse.Attachments != null)
            {
                var emailId = emailResponse.Id ?? throw new NullReferenceException();
                var attachments = emailResponse.Attachments;

                foreach (var attachment in attachments)
                {
                    string fileName = attachment.FileName ?? throw new NullReferenceException();
                    var contentType = attachment.ContentType;
                    var result = new EmailAttachmentData(emailId, fileName, new ContentType(contentType));
                    attachmentResults.Add(result);
                }
            }
            return attachmentResults;
        }
    }
}
