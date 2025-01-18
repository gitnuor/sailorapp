using System;

public class tran_email_notification_DTO
{
    public long tran_email_notification_id { get; set; }
    public string sender_email { get; set; }
    public string to_email { get; set; }

    public string to_name { get; set; }
    public string cc_email { get; set; }
    public string subject { get; set; }
    public string email_body { get; set; }
    public long initiated_by { get; set; }
    public DateTime initiated_date { get; set; }
    public int is_sent { get; set; }
    public string sent_status { get; set; }
    public DateTime sent_time { get; set; }
    public Int64 attempt_count { get; set; }

    
    public string email_attchement1 { get; set; }

   
    public string email_attchement2 { get; set; }

    
    public string email_attchement3 { get; set; }

  
    public string email_attchement4 { get; set; }

   
    public string email_attchement5 { get; set; }

   
    public string email_attchement6 { get; set; }

   
    public string email_attchement7 { get; set; }

    
    public string email_attchement8 { get; set; }

  
    public string email_attchement9 { get; set; }


    public string email_attchement10 { get; set; }
}

