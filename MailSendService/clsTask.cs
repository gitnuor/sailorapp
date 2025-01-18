using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using Dapper;
using System.Net.Mail;
using System.Net;
using System.Configuration;

namespace MailSendService
{
    public static class clsTask
    {

        static string con = ConfigurationManager.AppSettings["DBCon"];
        public static void ExecuteTask()
        {
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(con))
                {
                    connection.Open();

                    string query = @"select p.*,
                                   d.email_attchement1,
                                   d.email_attchement2,
                                   d.email_attchement3,
                                   d.email_attchement4,
                                   d.email_attchement5,
                                   d.email_attchement6,
                                   d.email_attchement7,
                                   d.email_attchement8,
                                   d.email_attchement9,
                                   d.email_attchement10

                            from tran_email_notification p
                            inner join tran_email_notification_master d on p.tran_email_notification_master_id = d.tran_email_notification_master_id
                            where coalesce( p.is_sent,0)=0";

                    var dataList = connection.Query<tran_email_notification_DTO>(query).ToList();

                    foreach (tran_email_notification_DTO notification in dataList)
                    {
                        try
                        {
                            // Task.Run(async () => await SendEmail(notification));
                            SendEmail(notification);
                        }
                        catch (Exception ex)
                        {

                            // connection.Execute(update, new { is_sent = Id });

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //throw (ex);
            }
        }

        private static Attachment GetAttachment(string base64string)
        {
            try
            {
                if (!string.IsNullOrEmpty(base64string) && base64string.Contains('|')) // Ensure you have this property
                {
                    var parts = base64string.Split('|');

                    string extractedFileName = parts[0];
                    string base64Content = parts[1];
                    // Decode the Base64 string to a byte array
                    byte[] attachmentBytes = Convert.FromBase64String(base64Content);

                    // Create a MemoryStream from the byte array
                    var attachmentStream = new MemoryStream(attachmentBytes);

                    // Create the attachment from the stream
                    var attachment = new Attachment(attachmentStream, extractedFileName);

                    return attachment;
                }

                return null;
            }
            catch (Exception es)
            {
                return null;
            }
        }

        private static List<Attachment> GetAllAttachments(tran_email_notification_DTO notification)
        {
            List<Attachment> list=new List<Attachment>();
             
            try
            {
                if (!string.IsNullOrEmpty(notification.email_attchement1)) // Ensure you have this property
                {
                    if (GetAttachment(notification.email_attchement1)!=null)
                    {
                        list.Add(GetAttachment(notification.email_attchement1));
                    }
                }
            }
            catch (Exception es)
            {
               
            }

            try
            {
                if (!string.IsNullOrEmpty(notification.email_attchement2)) // Ensure you have this property
                {
                    if (GetAttachment(notification.email_attchement2) != null)
                    {
                        list.Add(GetAttachment(notification.email_attchement2));
                    }
                }
            }
            catch (Exception es)
            {

            }
            try
            {
                if (!string.IsNullOrEmpty(notification.email_attchement3)) // Ensure you have this property
                {
                    if (GetAttachment(notification.email_attchement3) != null)
                    {
                        list.Add(GetAttachment(notification.email_attchement3));
                    }
                }
            }
            catch (Exception es)
            {

            }
            try
            {
                if (!string.IsNullOrEmpty(notification.email_attchement4)) // Ensure you have this property
                {
                    if (GetAttachment(notification.email_attchement4) != null)
                    {
                        list.Add(GetAttachment(notification.email_attchement4));
                    }
                }
            }
            catch (Exception es)
            {

            }
            try
            {
                if (!string.IsNullOrEmpty(notification.email_attchement5)) // Ensure you have this property
                {
                    if (GetAttachment(notification.email_attchement5) != null)
                    {
                        list.Add(GetAttachment(notification.email_attchement5));
                    }
                }
            }
            catch (Exception es)
            {

            }
            try
            {
                if (!string.IsNullOrEmpty(notification.email_attchement6)) // Ensure you have this property
                {
                    if (GetAttachment(notification.email_attchement6) != null)
                    {
                        list.Add(GetAttachment(notification.email_attchement6));
                    }
                }
            }
            catch (Exception es)
            {

            }
            try
            {
                if (!string.IsNullOrEmpty(notification.email_attchement7)) // Ensure you have this property
                {
                    if (GetAttachment(notification.email_attchement7) != null)
                    {
                        list.Add(GetAttachment(notification.email_attchement7));
                    }
                }
            }
            catch (Exception es)
            {

            }
            try
            {
                if (!string.IsNullOrEmpty(notification.email_attchement8)) // Ensure you have this property
                {
                    if (GetAttachment(notification.email_attchement8) != null)
                    {
                        list.Add(GetAttachment(notification.email_attchement8));
                    }
                }
            }
            catch (Exception es)
            {

            }

            try
            {
                if (!string.IsNullOrEmpty(notification.email_attchement9)) // Ensure you have this property
                {
                    if (GetAttachment(notification.email_attchement9) != null)
                    {
                        list.Add(GetAttachment(notification.email_attchement9));
                    }
                }
            }
            catch (Exception es)
            {

            }

            try
            {
                if (!string.IsNullOrEmpty(notification.email_attchement10)) // Ensure you have this property
                {
                    if (GetAttachment(notification.email_attchement10) != null)
                    {
                        list.Add(GetAttachment(notification.email_attchement10));
                    }
                }
            }
            catch (Exception es)
            {

            }

            return list;
        }

        private static string GetEmailBody(tran_email_notification_DTO notification)
        {
            return notification.email_body
                .Replace("{{Name}}", notification.to_name)
                .Replace("{{SystemDateTime}}", DateTime.Now.ToString("dd-MMM-yyyy HH:mm:ss"));
        }
        public static async Task<bool> SendEmail(tran_email_notification_DTO notification)
        {
            string fromAddress = ConfigurationManager.AppSettings["SmtpUser"];
            string smtpHost = ConfigurationManager.AppSettings["SmtpHost"];
            int smtpPort =Convert.ToInt32( ConfigurationManager.AppSettings["SmtpPort"]);
            string smtpUser = ConfigurationManager.AppSettings["SmtpUser"];
            string smtpPassword = ConfigurationManager.AppSettings["SmtpPassword"];

            try
            {
                // Create a new mail message
                var mailMessage = new MailMessage
                {
                    From = new MailAddress(fromAddress),
                    Subject = notification.subject,
                    Body = GetEmailBody(notification),
                    IsBodyHtml = true // Set to true if you are sending HTML content
                };

                // Add recipient
                mailMessage.To.Add(notification.to_email);

                // Add CC
                if (!string.IsNullOrEmpty(notification.cc_email))
                {
                    if (notification.cc_email.Contains(","))
                    {
                        foreach (var cc in notification.cc_email.Split(','))
                        {
                            mailMessage.CC.Add(cc.Trim());
                        }
                    }
                    else
                    {
                        mailMessage.CC.Add(notification.cc_email);
                    }
                }

                var Attachements = GetAllAttachments(notification);

                foreach (var attachment in Attachements)
                {
                    mailMessage.Attachments.Add(attachment);
                }

                using (var smtpClient = new SmtpClient(smtpHost, smtpPort)
                {
                    Credentials = new NetworkCredential(smtpUser, smtpPassword),
                    EnableSsl = false // Set to true if the SMTP server requires SSL
                })
                {
                    smtpClient.Send(mailMessage);
                }

                notification.is_sent = 1;
                notification.sent_status = "SENT SUCCESSFULLY";

                UpdateNotificationStatus(notification);

                return true;
            }
            catch (Exception ex)
            {
                // Log or handle the exception
                notification.is_sent = 0;
                notification.sent_status = ex.Message;
                UpdateNotificationStatus(notification);

                return false;
            }
        }

        private static void UpdateNotificationStatus(tran_email_notification_DTO notification)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(con))
            {
                connection.Open();

                string update = @"update tran_email_notification
                                  set attempt_count=coalesce(attempt_count,0)+1,is_sent=@is_sent,sent_status=@sent_status,sent_time=now()
                                  where tran_email_notification_id=@tran_email_notification_id";

                connection.Execute(update, new
                {
                    is_sent = notification.is_sent,
                    sent_status = notification.sent_status,
                    tran_email_notification_id = notification.tran_email_notification_id,
                });
            }
        }

        public static void InsertServiceLog(tran_mail_notification_service_DTO notification)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(con))
            {
                connection.Open();

                string update = @"insert into tran_mail_notification_service(service_name, start_time,end_time) 
                                values('Sailor Email Service',@start_time,@end_time)";

                connection.Execute(update,new {
                    start_time=notification.start_time,
                    end_time=notification.end_time
                });
            }
        }
    

    }
}
