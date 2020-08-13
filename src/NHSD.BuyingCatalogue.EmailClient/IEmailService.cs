using System.Threading.Tasks;
using JetBrains.Annotations;

namespace NHSD.BuyingCatalogue.EmailClient
{
    /// <summary>
    /// Defines operations for sending e-mails.
    /// </summary>
    [PublicAPI]
    public interface IEmailService
    {
        /// <summary>
        /// Sends an e-mail message asynchronously.
        /// </summary>
        /// <param name="emailMessage">The e-mail message to send asynchronously.</param>
        /// <returns>An asynchronous task context.</returns>
        Task SendEmailAsync(EmailMessage emailMessage);
    }
}
