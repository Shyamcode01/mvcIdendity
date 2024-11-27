
using System.Net;
using System.Net.Mail;

namespace dummyIdentity.Repository.service
{
    public class EmailService
    {

        private readonly IConfiguration _configuration;

        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task SendEmailAsync(string email, string subject, string body)
        {
            var smtSetting = _configuration.GetSection("smtSetting");
            var fromEmail = smtSetting["From"];
            var smtServer = smtSetting["SmtServer"];
            var port = int.Parse(smtSetting["port"]);
            var userName = smtSetting["UserName"];
            var password = smtSetting["Password"];
            var enablessl =bool.Parse( smtSetting["EnableSsl"]);


            var smtClient = new SmtpClient(smtServer)
            {
                Port = port,
                Credentials = new NetworkCredential(userName, password),
                EnableSsl = enablessl
            };

            var mailMessage=new MailMessage
            {
                From=new MailAddress(fromEmail),
                Subject=subject,
                Body=body,
                IsBodyHtml=true
            };
            mailMessage.To.Add(email);

            await smtClient.SendMailAsync(mailMessage);
           
        }
    }
}
