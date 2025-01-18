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
using EPYSLSAILORAPP.Application.DTO;

namespace EPYSLSAILORAPP.Application.Interface
{
    public interface IGenSegmentDetlService
    {
       Task<bool> SaveAsync(gen_segment_detl_entity entity);

		Task<bool> UpdateAsync(gen_segment_detl_entity entity);

		Task<List<gen_segment_detl_entity>> GetAllAsync();

        Task<List<gen_segment_detl_entity>> GetPagedData(DtParameters filter);

        Task<List<gen_segment_detl_entity>> GetAsync(Int64 Id);

        Task<List<gen_segment_detl_entity>> GetAllBySegmentID(Int64 Id);
    }
}

