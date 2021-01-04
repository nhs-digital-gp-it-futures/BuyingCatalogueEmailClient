using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Mime;
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
        private readonly EmailServerDriverSettings emailServerDriverSettings;

        /// <summary>
        /// Initializes a new instance of the <see cref="EmailServerDriver" /> class
        /// with the supplied settings.
        /// </summary>
        /// <param name="emailServerDriverSettings">The EmailDriverSettings object containing the URL of the SMTP server.</param>
        public EmailServerDriver(EmailServerDriverSettings emailServerDriverSettings)
        {
            this.emailServerDriverSettings = emailServerDriverSettings ?? throw new ArgumentNullException(nameof(emailServerDriverSettings));
        }

        /// <summary>
        /// Returns the number of e-mails in the mailbox.
        /// </summary>
        /// <returns>the number of e-mails in the mailbox.</returns>
        public async Task<int> GetEmailCountAsync()
        {
            var emailList = await FindAllEmailsAsync().ConfigureAwait(false);
            return emailList.Count;
        }

        /// <summary>
        /// FindAllEmailsAsync gets email objects from the SMTP service.
        /// </summary>
        /// <returns>a list containing all e-mails in the mailbox.</returns>
        public async Task<IReadOnlyList<Email>> FindAllEmailsAsync()
        {
            var responseBody = await emailServerDriverSettings
                .SmtpServerApiBaseUrl
                .AbsoluteUri
                .AppendPathSegment("email")
                .GetJsonAsync<EmailResponse[]>()
                .ConfigureAwait(false);

            var emails = new List<Email>();
            foreach (var emailResponse in responseBody)
            {
                emails.Add(await ProcessEmailResponseAsync(emailResponse).ConfigureAwait(false));
            }

            return emails;
        }

        /// <summary>
        /// Deletes all e-mails from the mailbox.
        /// </summary>
        /// <returns>An asynchronous <see cref="Task" /> context.</returns>
        public async Task ClearAllEmailsAsync()
        {
            await emailServerDriverSettings
                .SmtpServerApiBaseUrl
                .AbsoluteUri
                .AppendPathSegments("email", "all")
                .DeleteAsync()
                .ConfigureAwait(false);
        }

        /// <summary>
        /// Returns the binary data associated with the attachment.
        /// </summary>
        /// <param name="emailId">Id of the email containing the attachment.</param>
        /// <param name="fileName">file name of the attachment.</param>
        /// <returns>the binary data associated with the attachment.</returns>
        private async Task<byte[]> DownloadAttachmentAsync(string? emailId, string? fileName)
        {
            if (emailId is null)
            {
                throw new ArgumentNullException(nameof(emailId));
            }

            if (fileName is null)
            {
                throw new ArgumentNullException(nameof(fileName));
            }

            var attachmentData = await emailServerDriverSettings
                .SmtpServerApiBaseUrl
                .AbsoluteUri
                .AppendPathSegment("email")
                .AppendPathSegment(emailId)
                .AppendPathSegment("attachment")
                .AppendPathSegment(fileName)
                .GetStreamAsync()
                .ConfigureAwait(false);

            await using var memoryStream = new MemoryStream();
            await attachmentData.CopyToAsync(memoryStream).ConfigureAwait(false);
            return memoryStream.ToArray();
        }

        private async Task<Email> ProcessEmailResponseAsync(EmailResponse response)
        {
            if (response is null)
            {
                throw new ArgumentNullException(nameof(response));
            }

            var email = new Email
            {
                Text = response.Text,
                Html = response.Html,
                Subject = response.Subject,
            };

            email.AddSenders(response.From);
            email.AddRecipients(response.To);
            email.AddAttachments(await ExtractAttachmentsMetadataAsync(response).ConfigureAwait(false));

            return email;
        }

        private async Task<List<EmailAttachmentData>> ExtractAttachmentsMetadataAsync(EmailResponse emailResponse)
        {
            if (emailResponse is null)
            {
                throw new ArgumentNullException(nameof(emailResponse));
            }

            var attachmentResults = new List<EmailAttachmentData>();

            foreach (var attachment in emailResponse.Attachments)
            {
                string fileName = attachment.FileName;
                var contentType = attachment.ContentType;
                var data = await DownloadAttachmentAsync(emailResponse.Id, fileName).ConfigureAwait(false);
                var result = new EmailAttachmentData(data, fileName, new ContentType(contentType));
                attachmentResults.Add(result);
            }

            return attachmentResults;
        }
    }
}
