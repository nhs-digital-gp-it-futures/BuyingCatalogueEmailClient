﻿using System;
using System.Diagnostics.CodeAnalysis;
using FluentAssertions;
using NUnit.Framework;

namespace NHSD.BuyingCatalogue.EmailClient.UnitTests
{
    [TestFixture]
    [Parallelizable(ParallelScope.All)]
    internal static class EmailAddressTests
    {
        [Test]
        public static void Constructor_String_InitializesAddress()
        {
            const string address = "somebody@notarealaddress.test";

            var emailAddress = new EmailAddress(address);

            emailAddress.Address.Should().Be(address);
        }

        [Test]
        public static void Constructor_String_DoesNotInitializeDisplayName()
        {
            var emailAddress = new EmailAddress("somebody@notarealaddress.test");

            emailAddress.DisplayName.Should().BeNull();
        }

        [Test]
        public static void Constructor_String_String_InitializesAddress()
        {
            const string address = "somebody@notarealaddress.test";

            var emailAddress = new EmailAddress(address, "Name");

            emailAddress.Address.Should().Be(address);
        }

        [Test]
        public static void Constructor_String_String_InitializesDisplayName()
        {
            const string name = "Some Body";

            var emailAddress = new EmailAddress("a@b.test", name);

            emailAddress.DisplayName.Should().Be(name);
        }

        [Test]
        [TestCase("")]
        [TestCase("\t")]
        [SuppressMessage("ReSharper", "ObjectCreationAsStatement", Justification = "Exception testing")]
        public static void Address_Set_EmptyOrWhiteSpaceAddress_ThrowsException(string address)
        {
            Assert.Throws<ArgumentException>(() => new EmailAddress { Address = address });
        }

        [Test]
        [SuppressMessage("ReSharper", "ObjectCreationAsStatement", Justification = "Exception testing")]
        public static void Address_Set_NullAddress_ThrowsException()
        {
            Assert.Throws<ArgumentNullException>(() => new EmailAddress { Address = null });
        }
    }
}
