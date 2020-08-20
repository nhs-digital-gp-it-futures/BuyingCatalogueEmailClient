﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace NHSD.BuyingCatalogue.EmailClient
{
    /// <summary>
    /// Represents body of an e-mail message.
    /// </summary>
    public sealed class EmailMessageBody
    {
        private readonly List<object> _formatItems = new List<object>();

        /// <summary>
        /// Initializes a new instance of the <see cref="EmailMessageBody"/> class.
        /// </summary>
        public EmailMessageBody()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EmailMessageBody"/> class
        /// with the specified <paramref name="content"/> and optional format items.
        /// </summary>
        /// <param name="content">The content of the message body.</param>
        /// <param name="formatItems">Any format items to format the content with.</param>
        public EmailMessageBody(string content, params object[] formatItems)
        {
            Content = content;
            _formatItems.AddRange(formatItems);
        }

        /// <summary>
        /// Gets the list of format items.
        /// </summary>
        public IList<object> FormatItems => _formatItems;

        /// <summary>
        /// Gets or sets the content of the message body.
        /// </summary>
        /// <remarks>Accepts format items; see <see cref="string.Format(string, object[])"/> for formatting options.</remarks>
        public string Content { get; set; } = string.Empty;

        /// <summary>
        /// Returns a string that represents the content of the message.
        /// </summary>
        /// <returns>A string that represents the content of the message.</returns>
        public override string ToString()
        {
            return string.IsNullOrWhiteSpace(Content)
                ? string.Empty
                : string.Format(CultureInfo.CurrentCulture, Content, FormatItems.ToArray());
        }

        /// <summary>
        /// Adds all provided format items to
        /// the <see cref="FormatItems"/> collection.
        /// </summary>
        /// <param name="formatItems">The list of format items.</param>
        /// <exception cref="ArgumentNullException"><paramref name="formatItems"/> is <see langref="null"/>.</exception>
        public void AddFormatItems(params object[] formatItems)
        {
            if (formatItems is null)
                throw new ArgumentNullException(nameof(formatItems));

            _formatItems.AddRange(formatItems);
        }
    }
}
