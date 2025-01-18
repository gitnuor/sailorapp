
using Newtonsoft.Json.Linq;
using Postgrest.Attributes;
using Postgrest.Models;using Dapper.Contrib.Extensions;

namespace EPYSLSAILORAPP.Domain.Entity
{ [System.ComponentModel.DataAnnotations.Schema.Table("tran_email_notification_master")]

		public class tran_email_notification_master_entity : DapperExt
		{

 			[Key]
			public Int64 tran_email_notification_master_id  { get; set;}

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

 			[Column("email_attchement1")]
			public string? email_attchement1  { get; set;}

 			[Column("email_attchement2")]
			public string? email_attchement2  { get; set;}

 			[Column("email_attchement3")]
			public string? email_attchement3  { get; set;}

 			[Column("email_attchement4")]
			public string? email_attchement4  { get; set;}

 			[Column("email_attchement5")]
			public string? email_attchement5  { get; set;}

 			[Column("email_attchement6")]
			public string? email_attchement6  { get; set;}

 			[Column("email_attchement7")]
			public string? email_attchement7  { get; set;}

 			[Column("email_attchement8")]
			public string? email_attchement8  { get; set;}

 			[Column("email_attchement9")]
			public string? email_attchement9  { get; set;}

 			[Column("email_attchement10")]
			public string? email_attchement10  { get; set;}

 			[Column("initiated_by")]
			public Int64 initiated_by  { get; set;}

 			[Column("initiated_date")]
			public DateTime? initiated_date  { get; set;}

 			[Column("detl_list")]
			public String? detl_list  { get; set;}


		}
}
