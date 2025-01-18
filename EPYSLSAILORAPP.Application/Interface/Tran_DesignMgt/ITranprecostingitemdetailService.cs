using EPYSLSAILORAPP.Application.DTO.BusinessPlanning;
using EPYSLSAILORAPP.Domain.Entity.BusinessPlanning;
using EPYSLSAILORAPP.Domain.Statics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EPYSLSAILORAPP.Domain.Entity.Tran_Tables;

namespace EPYSLSAILORAPP.Application.Interface.Tran_DesignMgt
{
    public interface ITranprecostingitemdetailService
    {
        Task<bool> SaveAsync(tran_pre_costing_item_detail_entity entity);

        Task<bool> UpdateAsync(tran_pre_costing_item_detail_entity entity);

        Task<List<tran_pre_costing_item_detail_entity>> GetAllAsync();

        Task<List<tran_pre_costing_item_detail_entity>> GetAsync(long Id);
    }
}

