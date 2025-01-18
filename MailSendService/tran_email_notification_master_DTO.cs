


using System;
using System.Collections.Generic;

public class tran_email_notification_master_DTO
{

    public Int64 tran_email_notification_master_id { get; set; }


    public string sender_email { get; set; }


    public string to_email { get; set; }


    public string cc_email { get; set; }


    public string subject { get; set; }


    public string email_body { get; set; }


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

    public Int64 initiated_by { get; set; }


    public DateTime initiated_date { get; set; }

    public string detl_list { get; set; }

    public List<tran_email_notification_DTO> TranEmailNotification_List { get; set; }



}
