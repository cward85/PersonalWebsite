using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using System.Web.Hosting;

namespace PersonalWebsite.Helpers
{
    public static class EmailHandler
    {
        public static readonly Regex EmailRegex = new Regex(
        @"^[^@\s]+@[^@\s]+\.[^@\s]+$",
        RegexOptions.Compiled | RegexOptions.IgnoreCase
    );

        public static async Task<bool> SendEmailAsync(string recipient, string subject, string body)
        {
            try
            {
                using (var smtp = new System.Net.Mail.SmtpClient())
                {
                    string fromEmail, fromDisplayName = string.Empty;

                    fromEmail = ConfigurationManager.AppSettings["FromEmail"].ToString();
                    fromDisplayName = ConfigurationManager.AppSettings["FromDisplayName"].ToString();

                    using (var mail = new MailMessage
                    {
                        From = new MailAddress(fromEmail, fromDisplayName),
                        Subject = subject,
                        Body = body.Replace("\\n", "<br/>"),
                        IsBodyHtml = true
                    })
                    {
                        if (IsValidEmail(recipient))
                        {
                            mail.To.Add(recipient);
                            await smtp.SendMailAsync(mail);
                        }
                        else
                        {
                            return false;
                        }    
                    }
                }

                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool SendEmail(string recipient, string subject, string body)
        {
            try
            {
                using (var smtp = new System.Net.Mail.SmtpClient())
                {
                    string fromEmail, fromDisplayName = string.Empty;

                    fromEmail = ConfigurationManager.AppSettings["FromEmail"].ToString();
                    fromDisplayName = ConfigurationManager.AppSettings["FromDisplayName"].ToString();
                    using (var mail = new MailMessage
                    {
                        From = new MailAddress(fromEmail, fromDisplayName),
                        Subject = subject,
                        Body = body.Replace("\\n", "<br/>"),
                        IsBodyHtml = true
                    })
                    {
                        if (IsValidEmail(recipient))
                        {
                            mail.To.Add(recipient);
                            smtp.Send(mail);
                        }
                        else
                        {
                            return false;
                        }
                    }
                }

                return true;
            }
            catch
            {
                return false;
            }
        }

        public static async Task<bool> SendMultipleEmailsAsync(List<string> recipients, string subject, string body)
        {
            try
            {
                using (var smtp = new System.Net.Mail.SmtpClient())
                {
                    string fromEmail, fromDisplayName = string.Empty;

                    fromEmail = ConfigurationManager.AppSettings["FromEmail"].ToString();
                    fromDisplayName = ConfigurationManager.AppSettings["FromDisplayName"].ToString();
                    using (var mail = new MailMessage
                    {
                        From = new MailAddress(fromEmail, fromDisplayName),
                        Subject = subject,
                        Body = body.Replace("\\n", "<br/>"),
                        IsBodyHtml = true
                    })
                    {
                        foreach (var recipient in recipients)
                        {
                            if (IsValidEmail(recipient))
                            {
                                mail.Bcc.Add(recipient);
                            }
                        }
                        await smtp.SendMailAsync(mail);
                    }
                }

                return true;
            }
            catch
            {
                return false;
            }
        }

        private static bool IsValidEmail(string email)
        {
            try
            {
                var addr = new MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }
    }
}