using EPYSLSAILORAPP.Application.DTO.BusinessPlanning;
using EPYSLSAILORAPP.Domain.Entity.BusinessPlanning;
using EPYSLSAILORAPP.Domain.Statics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EPYSLSAILORAPP.Domain.Entity;

namespace EPYSLSAILORAPP.Application.Interface
{
    public interface IStylefabricdetlService
    {
       Task<bool> SaveAsync(style_fabric_detl_entity entity);

		Task<bool> UpdateAsync(style_fabric_detl_entity entity);

		Task<List<style_fabric_detl_entity>> GetAllAsync();

		Task<List<style_fabric_detl_entity>> GetAsync(Int64 Id);
    }
}

