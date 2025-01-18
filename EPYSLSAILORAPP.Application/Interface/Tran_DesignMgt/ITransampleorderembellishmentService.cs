using EPYSLSAILORAPP.Application.DTO.BusinessPlanning;
using EPYSLSAILORAPP.Domain.Entity.BusinessPlanning;
using EPYSLSAILORAPP.Domain.Statics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EPYSLSAILORAPP.Domain.Entity;

namespace EPYSLSAILORAPP.Application.Interface.Tran_DesignMgt
{
    public interface ITransampleorderembellishmentService
    {
        Task<bool> SaveAsync(tran_sample_order_embellishment_entity entity);

        Task<bool> UpdateAsync(tran_sample_order_embellishment_entity entity);

        Task<List<tran_sample_order_embellishment_entity>> GetAllAsync();

        Task<List<tran_sample_order_embellishment_entity>> GetAsync(long Id);
    }
}

