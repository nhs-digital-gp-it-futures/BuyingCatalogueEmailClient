using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Flurl;
using Flurl.Http;
using NHSD.BuyingCatalogue.EmailClient.IntegrationTesting.Data;
using NHSD.BuyingCatalogue.EmailClient.IntegrationTesting.Drivers;

namespace NHSD.BuyingCatalogue.EmailClient.IntegrationTesting.Extensions
{
    /// <summary>
    /// Extension that allows the binary data associated with an Attachment to be downloaded.
    /// </summary>
    public static class EmailAttachmentDataExtensions
    {
        /// <summary>
        /// Downloads the binary data associated with attachment 
        /// </summary>
        /// <param name="attachment">Downloads the binary data associated with the metadata found in the <see cref="EmailAttachmentData"/> object</param>
        /// <param name="settings">settings <see cref="EmailAttachmentData"/> object that contains information need to connect to the SMTP service </param>
        /// <returns></returns>
        public static async Task<byte[]> DownloadDataAsync(this EmailAttachmentData  attachment, EmailServerDriverSettings settings)
        {
            if (attachment is null)
            {
                throw new ArgumentNullException(nameof(attachment));
            }

            if (settings is null)
            {
                throw new ArgumentNullException(nameof(settings));
            }

            var attachmentData = await settings
                .SmtpServerApiBaseUrl
                .AbsoluteUri
                .AppendPathSegment("email")
                .AppendPathSegment(attachment.Id)
                .AppendPathSegment("attachment")
                .AppendPathSegment(attachment.FileName)
                .GetStreamAsync();

            using (var memoryStream = new MemoryStream())
            {
                attachmentData.CopyTo(memoryStream);
                attachment.AttachmentData.AddRange( memoryStream.ToArray());
            }
            return attachment.AttachmentData.ToArray();
        }
    }
}
