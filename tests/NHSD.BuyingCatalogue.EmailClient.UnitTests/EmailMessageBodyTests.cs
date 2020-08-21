using System;
using FluentAssertions;
using NUnit.Framework;

namespace NHSD.BuyingCatalogue.EmailClient.UnitTests
{
    [TestFixture]
    [Parallelizable(ParallelScope.All)]
    internal static class EmailMessageBodyTests
    {
        [Test]
        public static void Constructor_Parameterless_ContentIsInitializedAsEmptyString()
        {
            var body = new EmailMessageBody();

            body.Content.Should().BeEmpty();
        }

        [Test]
        public static void Constructor_String_ObjectArray_InitializesContent()
        {
            const string expectedContent = "Message content";

            var body = new EmailMessageBody(expectedContent);

            body.Content.Should().Be(expectedContent);
        }

        [Test]
        public static void Constructor_String_ObjectArray_InitializesFormatItems()
        {
            const int one = 1;
            const string two = "2";

            var expectedFormatItems = new object[] { one, two };

            var body = new EmailMessageBody("content", one, two);

            body.FormatItems.Should().BeEquivalentTo(expectedFormatItems);
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase("\t")]
        public static void ToString_NullOrWhiteSpaceContent_ReturnsEmptyString(string content)
        {
            var body = new EmailMessageBody(content);

            var formattedContent = body.ToString();

            formattedContent.Should().BeEmpty();
        }

        [Test]
        public static void ToString_ReturnsFormattedString()
        {
            var body = new EmailMessageBody("{0} {1:dd/MM/yyyy}");
            body.FormatItems.Add("Hello");
            body.FormatItems.Add(new DateTime(2020, 8, 20));

            var formattedContent = body.ToString();

            formattedContent.Should().Be("Hello 20/08/2020");
        }

        [Test]
        public static void AddFormatItems_NullFormatItems_ThrowsArgumentNullException()
        {
            var body = new EmailMessageBody();

            Assert.Throws<ArgumentNullException>(() => body.AddFormatItems(null!));
        }

        [Test]
        public static void AddFormatItems_AddsExpectedFormatItems()
        {
            const int one = 1;
            const string two = "2";

            var expectedFormatItems = new object[] { one, two };

            var body = new EmailMessageBody();
            body.AddFormatItems(one, two);

            body.FormatItems.Should().BeEquivalentTo(expectedFormatItems);
        }
    }
}
