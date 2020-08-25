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
using NHSD.BuyingCatalogue.EmailClient.IntegrationTesting.Data;
using TechTalk.SpecFlow;

namespace NHSD.BuyingCatalogue.EmailClient.IntegrationTesting.Drivers
{
    /// <summary>
    /// Provides functionality for executing integration tests
    /// that test e-mail functionality.
    /// </summary>
    [Binding]
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
        /// Gets the binary data associated with the attachment.
        /// </summary>
        /// <param name="emailId">Id of the email containg the attachment.</param>
        /// <param name="fileName">file name of the attachment.</param>
        /// <returns>byte array of attachment data</returns>
        public async Task<byte[]> DownloadAttachmentAsync(string? emailId,string? fileName)
        {
            if (emailId is null)
            {
                throw new ArgumentNullException(nameof(emailId));
            }

            if (fileName is null)
            {
                throw new ArgumentNullException(nameof(fileName));
            }

            var attachmentData = await _emailServerDriverSettings
                .SmtpServerApiBaseUrl
                .AbsoluteUri
                .AppendPathSegment("email")
                .AppendPathSegment(emailId)
                .AppendPathSegment("attachment")
                .AppendPathSegment(fileName)
                .GetStreamAsync();

            using (var memoryStream = new MemoryStream())
            {
                attachmentData.CopyTo(memoryStream);
                return memoryStream.ToArray();
            }
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

            var emails = new List<Email>();
            foreach (var emailResponse in responseBody)
            {
                emails.Add(await ProcessEmailResponseAsync(emailResponse));
            }
            return emails;
        }

        private async Task<Email> ProcessEmailResponseAsync(EmailResponse response)
        {
            if (response is null)
            {
                throw new ArgumentNullException(nameof(response));
            }

            var email= new Email
                {
                    Text = response.Text,
                    Html = response.Html,
                    Subject = response.Subject
                };
            email.From.AddRange(response.From.Where(x=> x!=null));
            email.To.AddRange(response.To.Where(x=> x!=null));
            email.Attachments.AddRange( await ExtractAttachmentsMetadataAsync(response));
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

        private async Task<List<EmailAttachmentData>> ExtractAttachmentsMetadataAsync(EmailResponse emailResponse)
        {
            if (emailResponse is null)
            {
                throw new ArgumentNullException(nameof(emailResponse));
            }

            var attachmentResults = new List<EmailAttachmentData>();

            if (emailResponse.Attachments != null)
            {
                foreach (var attachment in emailResponse.Attachments)
                {
                    string? fileName = attachment.FileName;
                    var contentType = attachment.ContentType;
                    var data = await DownloadAttachmentAsync(emailResponse.Id, fileName);
                    var result = new EmailAttachmentData(data, fileName, new ContentType(contentType));
                    attachmentResults.Add(result);
                }
            }
            return attachmentResults;
        }
    }
}
