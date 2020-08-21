using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;

namespace NHSD.BuyingCatalogue.EmailClient.IntegrationTesting.UnitTests.Builders
{
    internal sealed class AttachmentStreamBuilder
    {
        public string ContentType { get; set; }
        public string Stream { get; set; }
        public string FileName { get; set; }

        private AttachmentStreamBuilder()
        {
            ContentType = "application/json";
            Stream = "this is stream content";
            FileName = "attachment1.txt";
        }

        public static AttachmentStreamBuilder Create()
        {
            return new AttachmentStreamBuilder();
        }

        public AttachmentStream Build()
        {
            return new AttachmentStream
            {
                Stream =Stream,
                FileName =FileName,
                ContentType = ContentType
            };
        }
    }
}
