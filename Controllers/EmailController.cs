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
using API_SIDE_Dominicana.Services;

namespace API_SIDE_Dominicana.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EmailController : ControllerBase
    {
        private IEmailService _emailService;

        public EmailController(IEmailService emailService)
        {
            _emailService = emailService;
        }

        [HttpPost("SEND")]
        public async Task<ActionResult<Email>> PostEmailRequestService(Email email)
        {            
            try
            {
                await _emailService.SendEmailToSIDE(email);
            }
            catch (Exception e)
            {                
                return BadRequest(new { message = "Error al intentar enviar la solicitud...", errorException = e.ToString() });
            }
            
            return Ok(new { message = "Solicitud enviada con éxito..." });
        }
    }
}
