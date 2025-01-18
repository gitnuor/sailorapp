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
    public interface IStylelabelService
    {
       Task<bool> SaveAsync(style_label_entity entity);

		Task<bool> UpdateAsync(style_label_entity entity);

		Task<List<style_label_entity>> GetAllAsync();

		Task<List<style_label_entity>> GetAsync(Int64 Id);
    }
}

