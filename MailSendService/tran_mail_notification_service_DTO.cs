using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


    public class tran_mail_notification_service_DTO
    {
        public long tran_mail_notification_service_id { get; set; } // Corresponds to tran_mail_notification_service_id
        public string service_name { get; set; } // Corresponds to service_name
        public string status { get; set; } // Corresponds to status
        public DateTime? start_time { get; set; } // Corresponds to start_time
        public DateTime? end_time { get; set; } // Corresponds to end_time
    }

