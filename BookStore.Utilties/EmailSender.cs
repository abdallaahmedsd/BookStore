using Microsoft.AspNetCore.Identity.UI.Services;

namespace BookStore.Utilties
{
    public class EmailSender : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            // implementation
            return Task.CompletedTask;
        }
    }
}
