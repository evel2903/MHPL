using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace ManageEmployeeContacts.Services
{
    public class EmailServices
    {   
        public void SendEmail(string recipient, string subject, string content)
        {
            SmtpClient smtp = new SmtpClient();
            try
            {
                smtp.Host = "smtp.gmail.com";
                smtp.Port = 587;
                smtp.EnableSsl = true;
                smtp.Credentials = new NetworkCredential("evel.lxs.ad@gmail.com", "12345678Aa");
                smtp.Send("evel.lxs.ad@gmail.com", recipient, subject, content);
            }
            catch
            {

            }
        }
    }
}
