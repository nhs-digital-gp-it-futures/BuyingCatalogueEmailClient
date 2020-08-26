﻿using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Net.Http;
using System.Net.Mime;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using FluentAssertions.Equivalency;
using Flurl.Http.Testing;
using Newtonsoft.Json;
using NHSD.BuyingCatalogue.EmailClient.IntegrationTesting.Builders;
using NHSD.BuyingCatalogue.EmailClient.IntegrationTesting.Data;
using NHSD.BuyingCatalogue.EmailClient.IntegrationTesting.Drivers;
using NUnit.Framework;

namespace NHSD.BuyingCatalogue.EmailClient.IntegrationTesting.UnitTests
{
    [TestFixture]
    [Parallelizable(ParallelScope.All)]
    internal static class EmailServerDriverTests
    {
        [Test]
        public static void Constructor_NullMessage_ThrowsException()
        {
            Assert.Throws<ArgumentNullException>(() => new EmailServerDriver(null));
        }

        [Test]
        public static async Task EmailServerDriver_ClearAllEmails_CallsDeleteAllEmails()
        {
            var settings = new EmailServerDriverSettings(new Uri("http://bjss.com/"));
            var driver = new EmailServerDriver(settings);
            using (var httpTest = new HttpTest())
            {
                var values = new {};
                httpTest.RespondWithJson(values);
                await driver.ClearAllEmailsAsync();
                httpTest.ShouldHaveCalled("http://bjss.com/email/all")
                    .WithVerb(HttpMethod.Delete);
            }
        }

        [Test]
        public static async Task EmailServerDriver_GetEmailCountAsync_CallsGetAllEmails()
        {
            var settings = new EmailServerDriverSettings(new Uri("http://bjss.com/"));
            var driver = new EmailServerDriver(settings);
            using (var httpTest = new HttpTest())
            {
                var responseList = new List<EmailResponse>()
                {
                    EmailServerDriverResponseBuilder.Create().Build(),
                    EmailServerDriverResponseBuilder.Create().Build()
                };
                httpTest.RespondWithJson(responseList);
                var resultCount =await driver.GetEmailCountAsync();
                resultCount.Should().Be(2);
                httpTest.ShouldHaveCalled("http://bjss.com/email")
                    .WithVerb(HttpMethod.Get);
            }
        }

        [Test]
        public static async Task EmailServerDriver_FindAllEmailsAsync_CallsFindsAllEmails()
        {
            var settings = new EmailServerDriverSettings(new Uri("http://email.com/"));
            var driver = new EmailServerDriver(settings);
            using (var httpTest = new HttpTest())
            {
                var email = EmailServerDriverResponseBuilder.Create()
                    .WithSubject("Subject1")
                    .Build();

                var secondEmail = EmailServerDriverResponseBuilder.Create()
                    .WithSubject("Subject2")
                    .Build();

                var responseList = new List<EmailResponse> {email, secondEmail };

                httpTest.RespondWithJson(responseList);
                var resultEmails = await driver.FindAllEmailsAsync();
                
                httpTest.ShouldHaveCalled("http://email.com/email")
                    .WithVerb(HttpMethod.Get);
                resultEmails.Should().ContainSingle(e => e.Subject == email.Subject);
                resultEmails.Should().ContainSingle(e => e.Subject == secondEmail.Subject);
            }
        }

        [Test]
        public static async Task EmailServerDriver_FindAllEmailsAsync_FieldsAreTheSame()
        {
            var settings = new EmailServerDriverSettings(new Uri("http://email.com/"));
            var driver = new EmailServerDriver(settings);
            using (var httpTest = new HttpTest())
            {
                var email = EmailServerDriverResponseBuilder.Create()
                    .WithSubject("Subject1")
                    .Build();

                var responseList = new List<EmailResponse> { email};

                httpTest.RespondWithJson(responseList);
                var resultEmail = (await driver.FindAllEmailsAsync()).Single();

                httpTest.ShouldHaveCalled("http://email.com/email")
                    .WithVerb(HttpMethod.Get);

                EquivalencyAssertionOptions<EmailResponse> ExcludeProperties(EquivalencyAssertionOptions<EmailResponse> options)
                {
                    options.Excluding(r => r.Id);
                    options.Excluding(r => r.Attachments);
                    return options;
                }

                resultEmail.Should().BeEquivalentTo(email, ExcludeProperties);

                resultEmail.Attachments[0].FileName.Should().Be(email.Attachments[0].FileName);
                resultEmail.Attachments[0].ContentType.MediaType.Should().BeEquivalentTo(email.Attachments[0].ContentType);
            }
        }

        [Test]
        public static async Task EmailServerDriver_DownloadAttachmentAsync_DownloadaData()
        {
            var settings = new EmailServerDriverSettings(new Uri("http://email.com/"));
            var driver = new EmailServerDriver(settings);
            using (var httpTest = new HttpTest())
            {
                var email = EmailServerDriverResponseBuilder.Create()
                    .WithSubject("Subject1")
                    .Build();

                var responseList = new List<EmailResponse> { email };

                httpTest.RespondWithJson(responseList);
                httpTest.RespondWith("This is the attachment");

                var resultEmail = (await driver.FindAllEmailsAsync()).Single();

                httpTest.ShouldHaveCalled("http://email.com/email")
                    .WithVerb(HttpMethod.Get);
                
                httpTest.ShouldHaveCalled("http://email.com/email/*/attachment/attachment1.txt")
                    .WithVerb(HttpMethod.Get);

                var data = resultEmail.Attachments.First().AttachmentData;

                Encoding.UTF8.GetString(data.ToArray()).Should().Be("This is the attachment");
            }
        }

        [Test]
        public static async Task EmailServerDriver_FindAllEmailsWithNullHtml_DoesNotThrowException()
        {
            var settings = new EmailServerDriverSettings(new Uri("http://email.com/"));
            var driver = new EmailServerDriver(settings);
            using (var httpTest = new HttpTest())
            {
                var email = EmailServerDriverResponseBuilder.Create()
                    .WithSubject("Subject1")
                    .WithHtml(null)
                    .Build();

                var responseList = new List<EmailResponse> { email };

                httpTest.RespondWithJson(responseList);
                var resultEmail = (await driver.FindAllEmailsAsync()).Single();

                httpTest.ShouldHaveCalled("http://email.com/email")
                    .WithVerb(HttpMethod.Get);

                resultEmail.Html.Should().Be(email.Html);
            }
        }
    }
}
