using EPYSLSAILORAPP.Application.DTO.BusinessPlanning;
using EPYSLSAILORAPP.Domain.Entity.BusinessPlanning;
using EPYSLSAILORAPP.Domain.Statics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EPYSLSAILORAPP.Domain.Entity;
using EPYSLSAILORAPP.Application.DTO;
using EPYSLSAILORAPP.Domain.DTO;
using EPYSLSAILORAPP.Domain.RPC;

namespace EPYSLSAILORAPP.Application.Interface
{
    public interface IStyleitemproductService
    {
        Task<bool> SaveAsync(style_item_product_DTO entity);

        Task<bool> UpdateAsync(style_item_product_DTO entity);

		Task<List<style_item_product_entity>> GetAllAsync(Int64? product_id);

        Task<List<style_item_product_entity>> GetAllByPagingAsync(Filter_RangePlan_DataTable dtParameters);

        Task<style_item_product_DTO> GetAsync(Int64 Id);
    }
}

