using Dapper.Contrib.Extensions;
using EPYSLSAILORAPP.Domain.General;

namespace EPYSLSAILORAPP.Domain.Security
{
    [Table("MessageQueue")]
    public class MessageQueue : DapperBaseEntity
    {
        #region Table properties
        [Key]
        public int MessageID { get; set; }

        public int MenuId { get; set; }

        public int ForMenuId { get; set; }

        public int EventId { get; set; }

        public int? ParameterColumn0 { get; set; }

        public int? ParameterColumn1 { get; set; }

        public int ParentColumnId { get; set; }

        public string ReferenceValue { get; set; }

        public int SenderId { get; set; }

        public DateTime SendDate { get; set; }

        public bool Received { get; set; }

        public int? ReceiverId { get; set; }

        public DateTime? ReceivedDate { get; set; }

        public bool AutoReceived { get; set; }

        public string Subject { get; set; }

        public string Message { get; set; }

        public string SenderIpAddress { get; set; }

        public string ReceiverIpAddress { get; set; }

        public string RefNo { get; set; }

        public DateTime? ExpiryDate { get; set; }

        public int Readable { get; set; }

        public int NotifyFor { get; set; }

        public int? CompanyId { get; set; }

        public string EntryFrom { get; set; }

        #endregion

        #region Additional properties
        [Write(false)]
        public override bool IsModified => MessageID > 0 || EntityState == EntityState.Modified;

        public virtual Menu ForMenu { get; set; }


        #endregion

        public MessageQueue()
        {
            Received = false;
            AutoReceived = false;
            RefNo = "";
            Readable = 0;
            NotifyFor = 0;
        }
    }
}
