using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;

namespace giri_webdev_livedemo.Utilities
{
    /// <summary>
    /// JUNE-22-2015
    /// </summary>
    public static class ExceptionLogger
    {
        public static void LogException(Exception Exc, string Source = null, bool Email = false)
        {
            try
            {
                string filePath = Path.Combine(@"~/App_Data/", "ErrorLog_" + DateTime.Now.ToString("dd-MMM-yyyy") + ".txt");
                filePath = HttpContext.Current.Server.MapPath(filePath);
                using (StreamWriter writer = new StreamWriter(filePath))
                {
                    writer.WriteLine("*Error occurred on {0} :-", DateTime.Now);
                    writer.WriteLine();

                    //Inner Exception
                    if (Exc.InnerException != null)
                    {
                        writer.Write("Inner Exception Type:- ");
                        writer.WriteLine(Exc.InnerException.GetType().ToString());
                        writer.WriteLine("Inner Exception:- ");
                        writer.WriteLine(Exc.InnerException.Message);
                        writer.WriteLine("Inner Source:- ");
                        writer.WriteLine(Exc.InnerException.Source);
                        if (Exc.InnerException.StackTrace != null)
                        {
                            writer.WriteLine("Inner Stack Trace:- ");
                            writer.WriteLine(Exc.InnerException.StackTrace);
                        }
                    }

                    //outer Exception
                    writer.WriteLine("Exception Type:- ");
                    writer.WriteLine(Exc.GetType().ToString());
                    writer.WriteLine("Exception:- ");
                    writer.WriteLine(Exc.Message);
                    writer.WriteLine("Source:- ");
                    if (Source != null)
                        writer.WriteLine(Source);
                    else
                        writer.WriteLine(Exc.Source);
                    writer.WriteLine("Stack Trace:- ");
                    if (Exc.StackTrace != null)
                    {
                        writer.WriteLine(Exc.StackTrace);
                        writer.WriteLine();
                    }

                    if (Email)
                        SendEmail(Exc, Source);

                }
            }
            catch (Exception)
            { }
        }

        public static void SendEmail(Exception Exc, string Source)
        {
            try
            {
                string subject = "Error occurred in giri-webdev.net";
                string message = "";
                message += "<b>Exception Type:- </b>" + Exc.GetType().ToString() + "<br/>";
                message += "<b>Exception:- </b>" + Exc.Message + "<br/>";
                if (Source != null)
                    message += "<b>Source:- </b>" + Source + "<br/><br/>";
                else
                    message += "<b>Source:-  </b>" + Exc.Source + "<br/><br/>";

                if (Exc.StackTrace != null)
                    message += "<b>StackTrace:- </b><br/>" + Exc.StackTrace + "<br/>";

                MailMessage errorMessage = new MailMessage("user@giri-webdev.net", "giriprasad008@gmail.com", subject, message);
                errorMessage.IsBodyHtml = true;
                SmtpClient smtp = new SmtpClient();
                smtp.Host = "smtp.gmail.com";
                smtp.Credentials = new NetworkCredential("giriprasad008@gmail.com", "apple_10");
                smtp.EnableSsl = true;
                smtp.Send(errorMessage);
            }
            catch (Exception)
            { }
        }
    }
}