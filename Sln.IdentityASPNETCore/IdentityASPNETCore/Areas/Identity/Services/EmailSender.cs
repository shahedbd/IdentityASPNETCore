using IdentityASPNETCore.Resources;
using Microsoft.AspNetCore.Identity.UI.Services;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Threading.Tasks;

namespace IdentityASPNETCore.Areas.Identity.Services
{
    public class EmailSender : IEmailSender
    {
        //public EmailSender(IOptions<AuthMessageSenderOptions> optionsAccessor)
        //{
        //    Options = optionsAccessor.Value;
        //}

        //public AuthMessageSenderOptions Options { get; } //set only via Secret Manager


        public Task SendEmailAsync(string email, string subject, string message)
        {

            //string SendGridKey = "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";

            return Execute(subject, message, email);
        }

        public Task Execute(string subject, string message, string email)
        {
            var client = new SendGridClient(Resources.EmailComponents.apiKey);
            var msg = new SendGridMessage()
            {
                From = new EmailAddress(EmailComponents.fromEmail, EmailComponents.fromEmaliUserName),
                Subject = subject,
                PlainTextContent = message,
                HtmlContent = message
            };
            msg.AddTo(new EmailAddress(email));

            // Disable click tracking.
            // See https://sendgrid.com/docs/User_Guide/Settings/tracking.html
            msg.TrackingSettings = new TrackingSettings
            {
                ClickTracking = new ClickTracking { Enable = false }
            };

            return client.SendEmailAsync(msg);
        }
    }
}
