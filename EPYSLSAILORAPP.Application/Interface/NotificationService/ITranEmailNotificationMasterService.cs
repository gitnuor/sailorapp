using EPYSLSAILORAPP.Application.DTO;
using EPYSLSAILORAPP.Application.DTO.BusinessPlanning;
using EPYSLSAILORAPP.Domain.Entity.BusinessPlanning;
using EPYSLSAILORAPP.Domain.Statics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EPYSLSAILORAPP.Domain.Entity;
using EPYSLSAILORAPP.Domain.DTO;
using EPYSLSAILORAPP.Domain.RPC;

namespace EPYSLSAILORAPP.Application.Interface
{
    public interface ITranEmailNotificationMasterService
    {
        Task<bool> SaveAsync(tran_email_notification_master_entity entity);

        Task<bool> UpdateAsync(tran_email_notification_master_entity entity);

        Task<List<tran_email_notification_master_entity>> GetAllAsync();

        Task<List<tran_email_notification_master_entity>> GetAllPagedDataAsync(DtParameters request);

        Task<tran_email_notification_master_entity> GetSingleAsync(Int64 Id);

        Task<bool> DeleteAsync(Int64? Id);

        Task<bool> tran_email_notification_master_insert_sp(tran_email_notification_master_DTO objtran_email_notification_master);
        Task<bool> tran_email_notification_master_update_sp(tran_email_notification_master_DTO objtran_email_notification_master);

        Task<bool> SendNotificationEmail(string to_email, string to_name, string subject, string attachment,Int64? initiated_by);

        Task<bool> SendNotificationEmail(List<tran_email_notification_DTO> to_email_list, string subject, string attachment, Int64? initiated_by, Int64? email_template_id);
    }
}

