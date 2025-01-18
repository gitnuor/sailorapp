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
    public interface IGenSegmentService
    {
       Task<bool> SaveAsync(gen_segment_DTO entity);

		Task<bool> UpdateAsync(gen_segment_DTO entity);

		Task<List<gen_segment_entity>> GetAllAsync();

        Task<List<gen_segment_entity>> GetAllPagedDataAsync(DtParameters param);

        Task<gen_segment_DTO> GetAsync(Int64 Id);

        Task<List<gen_segment_entity>> GetPagedData(DtParameters filter);
    }
}

