
using Newtonsoft.Json.Linq;
using Postgrest.Attributes;
using Postgrest.Models;using Dapper.Contrib.Extensions;

namespace EPYSLSAILORAPP.Domain.Entity
{ [System.ComponentModel.DataAnnotations.Schema.Table("tran_email_notification")]

		public class tran_email_notification_entity : DapperExt
		{

 			[Key]
			public Int64 tran_email_notification_id  { get; set;}

 			[Column("sender_email")]
			public string? sender_email  { get; set;}

 			[Column("to_email")]
			public string? to_email  { get; set;}

 			[Column("cc_email")]
			public string? cc_email  { get; set;}

 			[Column("subject")]
			public string? subject  { get; set;}

 			[Column("email_body")]
			public string? email_body  { get; set;}

 			[Column("initiated_by")]
			public Int64 initiated_by  { get; set;}

 			[Column("initiated_date")]
			public DateTime? initiated_date  { get; set;}

 			[Column("is_sent")]
			public Int32? is_sent  { get; set;}

 			[Column("sent_status")]
			public string? sent_status  { get; set;}

 			[Column("sent_time")]
			public DateTime? sent_time  { get; set;}

 			[Column("attempt_count")]
			public Int64? attempt_count  { get; set;}


		}
}
