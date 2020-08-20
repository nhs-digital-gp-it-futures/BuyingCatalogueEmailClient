using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace NHSD.BuyingCatalogue.EmailClient
{
    /// <summary>
    /// Defines operations for sending e-mails.
    /// </summary>
    [SuppressMessage("ReSharper", "UnusedMemberInSuper.Global", Justification = "Public API")]
    public interface IEmailService
    {
        /// <summary>
        /// Sends an e-mail message asynchronously.
        /// </summary>
        /// <param name="emailMessage">The e-mail message to send asynchronously.</param>
        /// <returns>An asynchronous task context.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="emailMessage" /> is <see langref="null" />.</exception>
        Task SendEmailAsync(EmailMessage emailMessage);
    }
}
