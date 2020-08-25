using System;

namespace NHSD.BuyingCatalogue.EmailClient
{
    /// <summary>
    /// Defines a template that can be used to initialize the <see cref="EmailAddress"/>
    /// sender of a new <see cref="EmailMessage"/>.
    /// </summary>
    public sealed class EmailAddressTemplate
    {
        private string? _address;

        /// <summary>
        /// Initializes a new instance of the <see cref="EmailAddressTemplate"/> class.
        /// </summary>
        /// <remarks>Required for deserialization.</remarks>
        public EmailAddressTemplate()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EmailAddressTemplate"/> class
        /// with the specified <paramref name="address"/> and <paramref name="displayName"/>.
        /// </summary>
        /// <param name="address">The actual e-mail address.</param>
        /// <param name="displayName">An optional display name.</param>
        public EmailAddressTemplate(string address, string? displayName = null)
        {
            Address = address;
            DisplayName = displayName;
        }

        /// <summary>
        /// Gets or sets the actual e-mail address.
        /// </summary>
        /// <exception cref="ArgumentNullException"><paramref name="value"/> is <see langref="null"/>.</exception>
        /// <exception cref="ArgumentException"><paramref name="value"/> is empty or white space.</exception>
        public string? Address
        {
            get => _address;
            set
            {
                if (value == null)
                    throw new ArgumentNullException(nameof(value));

                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException($"{nameof(value)} cannot be empty or white space.", nameof(value));

                _address = value;
            }
        }

        /// <summary>
        /// Gets or sets the display name of the address.
        /// </summary>
        /// <remarks>An optional display name, for example Buying Catalogue Team.</remarks>
        public string? DisplayName { get; set; }

        /// <summary>
        /// Returns the <see cref="EmailAddress"/> representation of the current instance.
        /// </summary>
        /// <returns>The <see cref="EmailAddress"/> representation of the current instance.</returns>
        public EmailAddress? AsEmailAddress() => Address is null
            ? null
            : new EmailAddress(Address, DisplayName);
    }
}
