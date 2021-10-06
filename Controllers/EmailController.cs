using System;
using System.Linq;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using API_SIDE_Dominicana.Model;
using API_SIDE_Dominicana.Utilities;

namespace API_SIDE_Dominicana.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EmailController : ControllerBase
    {
        public EmailUtilities emailUtilities = new EmailUtilities(); 
        public readonly string name = "Servicios Integrales de Desarrollo Empresarial"; 
        public readonly string mail = "side.rdominicana@gmail.com"; 

        [HttpPost("SEND")]
        public async Task<ActionResult<Email>> PostEmailRequestService(Email email)
        {
            MailAddress from = new MailAddress(email.Mail, email.Name);
            MailAddress to = new MailAddress(mail, name);
            
            try
            {
                using (MailMessage message = new MailMessage(from, to))
                {
                    message.Subject = email.Subject;
                    message.Body = emailUtilities.GetHmlBodyMessageToSIDE(email);
                    message.IsBodyHtml = true;
                    SmtpClient smtp = new SmtpClient();
                    smtp.Host = "smtp.gmail.com";
                    smtp.EnableSsl = true;
                    NetworkCredential NetworkCred = new NetworkCredential("sidedominicana@gmail.com", "Migloria25");
                    smtp.UseDefaultCredentials = true;
                    smtp.Credentials = NetworkCred;
                    smtp.Port = 587;
                    smtp.Send(message);
                }
            }
            catch (Exception e)
            {
                
                return BadRequest(new { message = "Error al intentar enviar la solicitud...", errorException = e.ToString() });
            }
            
            return Ok(new { message = "Solicitud enviada con éxito..." });
        }
    }
}
