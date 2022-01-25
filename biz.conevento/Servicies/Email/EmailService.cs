using biz.conevento.Models.Email;
using System;
using System.IO;
using System.Net;
using System.Net.Mail;

namespace biz.conevento.Servicies
{
    public class EmailService : IEmailService
    {
        private const string V = "C:/Users/erick/Documents/conevento API/back-3.1/back/api.conevento/api.conevento/Files/";

        public string SendEmail(EmailModel email)
        {
            var response = "";
            try
            {
                //SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587);
                //smtpClient.EnableSsl = true;
                //smtpClient.Timeout = 10000;
                //smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                //smtpClient.UseDefaultCredentials = false;
                //smtpClient.Credentials = new NetworkCredential("rodrigo@minimalist.mx", "$dvs1188");

                SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587);
                smtpClient.EnableSsl = true;
                smtpClient.Timeout = 10000;
                smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = new NetworkCredential("avisodeprivacidadconevento@gmail.com", "C0n3v3nt0");

                //  SmtpClient smtpClient = new SmtpClient("smtpout.secureserver.net", 465); //correo de godady 
                //SmtpClient smtpClient = new SmtpClient("smtpout.secureserver.net", 465); //correo de spanel
                //smtpClient.EnableSsl = true;
                //smtpClient.Timeout = 30000;
                //smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                //smtpClient.UseDefaultCredentials = false;
                //smtpClient.Credentials = new NetworkCredential("contacto@conevento.com", "Conevento2022");



                MailMessage mailMessage = new MailMessage();
                mailMessage.From = new MailAddress("NoReply.VIOLET@premierdestinationservices.com", "Conevento");
                mailMessage.To.Add(email.To);
                mailMessage.Subject = email.Subject;
                mailMessage.SubjectEncoding = System.Text.Encoding.UTF8;
                mailMessage.IsBodyHtml = email.IsBodyHtml;
                mailMessage.Body = email.Body;
                mailMessage.BodyEncoding = System.Text.Encoding.UTF8;
                smtpClient.Send(mailMessage);

                response = "Correo enviado";
            }
            catch (Exception ex)
            {
                response = ex.ToString();
            }

            return response;
        }

        public string SendEmailAttach(EmailModelAttach email)
        {
            var response = "";
            try
            {
                SmtpClient smtpClient = new SmtpClient();
                smtpClient.Host = "smtp.office365.com";
                smtpClient.Port = 587;
                smtpClient.EnableSsl = true;
                smtpClient.Timeout = 10000;
                smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = new NetworkCredential("NoReply.VIOLET@conevento.com", "Laurel22");
                
                MailMessage mailMessage = new MailMessage();
                mailMessage.From = new MailAddress("NoReply.VIOLET@conevento.com", "Conevento");
                mailMessage.To.Add(email.To);
                mailMessage.Subject = email.Subject;
                mailMessage.IsBodyHtml = email.IsBodyHtml;
                mailMessage.Body = email.Body;

                foreach(var data in email.File)
                {
                    //string path = HttpContext.Current.Server.MapPath();
                    string random = Path.GetFullPath(data.attach);
                    Attachment attachment;
                    attachment = new Attachment(random);
                    mailMessage.Attachments.Add(attachment);
                }

                smtpClient.Send(mailMessage);
                //new SmtpClient
                //{
                //    Host = "Smtp.Gmail.com",
                //    Port = 587,
                //    EnableSsl = true,
                //    Timeout = 10000,
                //    DeliveryMethod = SmtpDeliveryMethod.Network,
                //    UseDefaultCredentials = false,
                //    Credentials = new NetworkCredential("rodrigo.stps@gmail.com", "$dvs1188")
                //}.Send(new MailMessage
                //{
                    
                //});

                response = "Correo enviado";
            }
            catch (Exception ex)
            {
                response = ex.ToString();
            }

            return response;
        }
    }
}
