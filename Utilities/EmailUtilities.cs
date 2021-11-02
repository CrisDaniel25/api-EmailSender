using System;
using API_SIDE_Dominicana.Model;

namespace API_SIDE_Dominicana.Utilities
{
    public class EmailUtilities
    {
        public string GetHmlBodyMessageToSIDE(Email email) 
        {
            return "<h2 style='text-align: center; color: #3084C6;'><strong>" + email.Subject + "</strong></h2><hr />"
                   + "<p style='color: #54595F;'>" + email.Message + ".</p><br />"
                   + "<span style='text-align: left;'><strong>Solicitante:</strong> <small><i>" + email.Name + ".</small></i><span><br />"
                   + "<span style='text-align: left;'><strong>Correo Solicitante:</strong> <small><i>" + email.Mail + ".</small></i><span>";                   
        }

        public string GetHtmlBodyMessageToClient(Email email) 
        {
            return "";
        }
    }
}