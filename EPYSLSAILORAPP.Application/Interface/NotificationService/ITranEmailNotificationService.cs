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
    public interface ITranEmailNotificationService
    {
        Task<bool> SaveAsync(tran_email_notification_entity entity);

        Task<bool> UpdateAsync(tran_email_notification_entity entity);

        Task<List<tran_email_notification_entity>> GetAllAsync();

        Task<List<tran_email_notification_entity>> GetAllPagedDataAsync(DtParameters request);

        Task<tran_email_notification_entity> GetSingleAsync(Int64 Id);

        Task<bool> DeleteAsync(Int64? Id);

        Task<bool> tran_email_notification_insert_sp(tran_email_notification_DTO objtran_email_notification);
        Task<bool> tran_email_notification_update_sp(tran_email_notification_DTO objtran_email_notification);


    }
}

