using System.Net.Mail;
using System.Net;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Threading;
using System;

namespace covCake
{
    public class Mailer
    {
        private static readonly Regex _emailExpression = new Regex(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$", RegexOptions.Compiled | RegexOptions.Singleline);
        private SmtpClient _smtpclient;
        private MailAddressCollection _To = new MailAddressCollection();
     
        public MailAddress From { get; set; }
        public MailAddressCollection To { get { return _To; } set { _To = value; } }

        public static Regex EmailExpression
        {
            get { return Mailer._emailExpression; }
        }

        /// <summary>
        /// Initialise à partir du web.config
        /// </summary>
        /// <param name="useSSL"></param>
        public Mailer(bool useSSL)
        {
            To = new MailAddressCollection();
            _smtpclient = new SmtpClient();
            _smtpclient.EnableSsl = useSSL;
        }


        public Mailer(string hostAddress, string username, string password) 
            : this(hostAddress,username,password,false)
        {
 
        }

        public Mailer(string hostAddress, string username, string password, bool useSSL)
        {
            To = new MailAddressCollection();
            _smtpclient = new SmtpClient(hostAddress);
            _smtpclient.EnableSsl = useSSL;
            _smtpclient.Credentials = new NetworkCredential(username, password);
        }

        public static bool IsValidEmail(string email)
        {
            return _emailExpression.IsMatch(email);
        }

        public void SendMailAsync(MailMessage email)
        {
            ThreadPool.QueueUserWorkItem(state => SendMail(email));
        }

        public void SendMailAsync(string fromAddress, string toAddress, string subject, string body, bool html)
        {
            ThreadPool.QueueUserWorkItem(state => SendMail(fromAddress, toAddress, subject, body,html));
        }

        public void SendMailAsync(string from, List<string> tos, string subject, string body, bool html)
        {
            ThreadPool.QueueUserWorkItem(state => SendMail(from, tos, subject, body, html));
        }

        public void SendMailAsync(MailAddress from, string to, string subject, string body, bool html)
        {
            ThreadPool.QueueUserWorkItem(state => SendMail(from, to, subject, body, html));           
        }

        public void SendMail(string from, IEnumerable<string> tos, string subject, string body, bool html)
        {
            SendMail(new MailAddress(from), tos, subject, body, html);
        }
      
        public void SendMail(string from, string to, string subject, string body, bool html)
        {
            this.SendMail(from, new string[] { to }, subject, body, html);
        }

        public void SendMail(MailAddress from, string to, string subject, string body, bool html)
        {
            this.SendMail(from, new string[] { to }, subject, body, html);
        }

        public void SendMail(MailAddress from, MailAddress replyTo, string to, string subject, string body, bool html)
        {
            this.SendMail(from, replyTo, new string[] { to }, subject, body, html);
        }

        public void SendMail(MailMessage message)
        {
            try
            {
                _smtpclient.Send(message);
            }
            catch (Exception ex)
            {

                CovCake.Log.Exception.cError(ex.Message, ex);
                throw ex;
            }
          
        }

        public void SendMail(MailAddress from, IEnumerable<string> tos, string subject, string body, bool html)
        {
            this.SendMail(from, null, tos, subject, body, html);
        }

        public void SendMail(MailAddress from, MailAddress replyTo, IEnumerable<string> tos, string subject, string body)
        {
            this.SendMail(from, null, tos, subject, body, false);
        }

        public void SendMail(MailAddress from, MailAddress replyTo, IEnumerable<string> tos, string subject, string body, bool html)
        {
            
            using (MailMessage message = new MailMessage())
            {
                message.From = from;
                foreach (string item in tos)
                    message.To.Add(item);
                message.ReplyTo = replyTo;
                message.Subject = subject;
                message.Body = body;
                message.IsBodyHtml = html;
                this.SendMail(message);

                foreach (string item in tos)
                    CovCake.Log.Info("SENDMAIL", "To=" + item + " From=" + from.Address + " Subject="+subject);

            }
            
        }
    }
}
