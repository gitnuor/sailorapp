using EPYSLSAILORAPP.Application.DTO.BusinessPlanning;
using EPYSLSAILORAPP.Domain.Entity.BusinessPlanning;
using EPYSLSAILORAPP.Domain.Statics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EPYSLSAILORAPP.Domain.Entity;
using EPYSLSAILORAPP.Application.DTO.Tran_DesignMgt;
using EPYSLSAILORAPP.Domain.Entity.Tran_DesignMgt;
using EPYSLSAILORAPP.Domain.RPC;

namespace EPYSLSAILORAPP.Application.Interface.Tran_DesignMgt
{
    public interface ITransampleorderService
    {
        Task<bool> SaveAsync(tran_sample_order_DTO entity);

        Task<bool> UpdateAsync(tran_sample_order_DTO entity);

        Task<List<tran_sample_order_entity>> GetAllAsync();

        Task<List<tran_sample_order_entity>> GetAsync(long Id);

        Task<tran_sample_order_DTO> GetSingleWithImageByIdAsync(long Id, long? image_position = 0);

        Task<bool> tran_sample_order_insert_sp(tran_sample_order_DTO objtran_sample_order);
        Task<bool> tran_sample_order_update_sp(tran_sample_order_DTO objtran_sample_order);

        
    }
}

