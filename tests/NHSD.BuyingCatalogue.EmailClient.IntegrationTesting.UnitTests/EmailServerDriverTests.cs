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
using Flurl.Http.Testing;
using Newtonsoft.Json;
using NHSD.BuyingCatalogue.EmailClient.IntegrationTesting.Drivers;
using NHSD.BuyingCatalogue.EmailClient.IntegrationTesting.UnitTests.Builders;
using NUnit.Framework;

namespace NHSD.BuyingCatalogue.EmailClient.IntegrationTesting.UnitTests
{
    [TestFixture]
    [Parallelizable(ParallelScope.All)]
    internal static class EmailServerDriverTests
    {
        [Test]
        [SuppressMessage("ReSharper", "ObjectCreationAsStatement", Justification = "Exception testing")]
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
                var responseList = new List<EmailServerDriverResponse>()
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

                var responseList = new List<EmailServerDriverResponse>{email, secondEmail };

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

                var responseList = new List<EmailServerDriverResponse> { email};

                httpTest.RespondWithJson(responseList);
                var resultEmail = (await driver.FindAllEmailsAsync()).Single();

                httpTest.ShouldHaveCalled("http://email.com/email")
                    .WithVerb(HttpMethod.Get);

                resultEmail.From.Should().Be(email.From[0].Address);
                resultEmail.To.Should().Be(email.To[0].Address);
                resultEmail.Subject.Should().Be(email.Subject);
                resultEmail.PlainTextBody.Should().Be(email.Text);
                resultEmail.HtmlBody.Should().Be(email.Html);
                resultEmail.Attachment.ContentAsString.Should().Be(email.Attachment.Stream);
                resultEmail.Attachment.Name.Should().Be(email.Attachment.FileName);
                resultEmail.Attachment.ContentType.MediaType.Should().BeEquivalentTo(email.Attachment.ContentType);
            }
        }
    }
}
