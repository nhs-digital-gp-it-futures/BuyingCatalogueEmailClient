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
    public static class EmailAttachmentDataExtensions
    {
        public static async Task<byte[]> DownloadDataAsync(this EmailAttachmentData  attachment, EmailServerDriverSettings settings)
        {
            if (attachment == null)
            {
                throw new ArgumentNullException(nameof(attachment));
            }

            if (settings==null)
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
                attachment.AttachmentData = memoryStream.ToArray();
            }
            return attachment.AttachmentData;
        }
    }
}
