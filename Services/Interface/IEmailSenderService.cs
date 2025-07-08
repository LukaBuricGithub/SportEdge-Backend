namespace SportEdge.API.Services.Interface
{
    /// <summary>
    /// Defines service operations for sending email.
    /// </summary>
    public interface IEmailSenderService
    {
        /// <summary>
        /// Sends message to a user email address.
        /// </summary>
        /// <param name="to">User email address.</param>
        /// <param name="subject">Email subject.</param>
        /// <param name="body">Email content.</param>
        Task SendEmailAsync(string to, string subject, string body);


        /// <summary>
        /// Sends an order receipt to the user's email address.
        /// </summary>
        /// <param name="to">User email address.</param>
        /// <param name="subject">Email subject.</param>
        /// <param name="body">Email content.</param>
        /// <param name="attachment">Receipt data (if any).</param>
        /// <param name="attachmentFileName">Name of receipt (if any).</param>
        Task SendReceiptEmailAsync(string to, string subject, string body, byte[]? attachment = null, string? attachmentFileName = null);

    }
}
