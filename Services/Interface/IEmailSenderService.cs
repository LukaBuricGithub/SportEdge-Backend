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
    }
}
