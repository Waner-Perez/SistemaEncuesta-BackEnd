using WebApiForm.Interfaces;
using MimeKit;
using MailKit.Security;

namespace WebApiForm.Capa_de_Servicio
{
    public class SmtpEmailSender : IEmailSender
    {
        private readonly IConfiguration _config;

        public SmtpEmailSender(IConfiguration configuration)
        {
            _config = configuration;
        }

        public async Task SendEmail(string toEmail, string subject, string plainTextContent, string htmlContent)
        {
            // Cargar configuración SMTP desde appsettings.json
            var smtpConfig = _config.GetSection("Smtp");
            var host = smtpConfig["Host"];
            var port = int.Parse(smtpConfig["Port"]);
            var username = smtpConfig["Username"];
            var password = smtpConfig["Password"];
            var fromEmail = smtpConfig["FromEmail"];
            var fromName = smtpConfig["FromName"];
            //var enableSsl = bool.Parse(smtpConfig["EnableSsl"]);

            // Crear el mensaje de correo
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(fromName, fromEmail));
            message.To.Add(new MailboxAddress("Cliente Usuario", toEmail));
            message.Subject = subject;

            var bodyBuilder = new BodyBuilder
            {
                TextBody = plainTextContent,
                HtmlBody = htmlContent
            };
            message.Body = bodyBuilder.ToMessageBody();

            // Enviar el correo
            using (var smtpClient = new MailKit.Net.Smtp.SmtpClient())
            {
                await smtpClient.ConnectAsync(host, port, SecureSocketOptions.SslOnConnect); //en caso de utilizar TLS debe de aplicar (SecureSocketOptions.StartTls)
                await smtpClient.AuthenticateAsync(username, password);
                await smtpClient.SendAsync(message);
                await smtpClient.DisconnectAsync(true);
            }
        }
    }
}