using KashmirPoultrySoftware.Application.Abstraction.IEmail;
using KashmirPoultrySoftware.Application.EmailSetting;
using KashmirPoultrySoftware.Infrastructure.MailSetting;
using Mailjet.Client;
using Mailjet.Client.TransactionalEmails;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KashmirPoultrySoftware.Infrastructure.Services
{
    public class EmailService : IEmailService
    {
        private readonly MailOption options;

        public EmailService(IOptions<MailOption> options)
        {
            this.options = options.Value;
        }

   


        public async Task<bool> SendEmailAsync(Application.EmailSetting.MailSetting emailSetting)
        {
            try
            {
                MailjetClient mailjetClient = new MailjetClient(options.ApiKey, options.SecretKey);
                var email = new TransactionalEmailBuilder().
                WithFrom(new SendContact(options.FromEmail)).
                    WithTo(new SendContact(emailSetting.To.FirstOrDefault())).
                WithHtmlPart(emailSetting.Body).
                    WithSubject(emailSetting.Subject).
                    Build();
                await mailjetClient.SendTransactionalEmailAsync(email);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return true;
        }
    }
}
