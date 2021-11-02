using System;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Threading.Tasks;
using API_SIDE_Dominicana.Model;
using API_SIDE_Dominicana.Utilities;
using Microsoft.Extensions.Configuration;

namespace API_SIDE_Dominicana.Services
{
    public interface IEmailService
    {
        Task SendEmailToSIDE(Email email);
        Task SendEmailToClient(Email email);
    }

    public class EmailServices : IEmailService
    {
        private readonly string name_info = "Servicios Integrales de Desarrollo Empresarial"; 
        private readonly string email_info = "info@sidedominicana.com";
        private readonly string _subject_tecnologia = "Formulario de Solicitudes - Sitio Web <<https://sidedominicana.com>>"; 
        private readonly string _email_tecnologia = "tecnologia@sidedominicana.com";
        private IConfiguration _configuration;
        private EmailUtilities _emailUtilities = new EmailUtilities();
        
        public EmailServices(IConfiguration configuration) 
        {
            _configuration = configuration;
        }
                
        #region Método que envia un Correo del Solicitante a Side
        public async Task SendEmailToSIDE(Email email) 
        {
            var apiKey = _configuration["SendGridAPIKey"];
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress(_email_tecnologia, _subject_tecnologia);
            var subject = email.Subject;
            var to = new EmailAddress("side.rdominicana@gmail.com", "Servicios Integrales de Desarrollo Empresarial");
            var plainTextContent = email.Message;
            var htmlContent = _emailUtilities.GetHmlBodyMessageToSIDE(email);
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            var response = await client.SendEmailAsync(msg).ConfigureAwait(false);

            if (response.IsSuccessStatusCode)
                await SendEmailToClient(email);
        }
        #endregion 

        #region Método que envia un correo al Solicitante de parte de Side
        public async Task SendEmailToClient(Email email)
        {
            var apiKey = _configuration["SendGridAPIKey"];
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress(email_info, name_info);
            var subject = email.Subject + " - Respuesta";
            var to = new EmailAddress(email.Mail, email.Name);
            var plainTextContent = "and easy to do anywhere, even with C#";
            var htmlContent = "<strong>and easy to do anywhere, even with C#</strong>";
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            var response = await client.SendEmailAsync(msg).ConfigureAwait(false);
        }
        #endregion 
    }
}