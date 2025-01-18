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

namespace EPYSLSAILORAPP.Application.Interface
{
    public interface IGenBankBranchService
    {
        Task<bool> SaveAsync(gen_bank_branch_entity entity);

        Task<bool> UpdateAsync(gen_bank_branch_entity entity);

        Task<List<gen_bank_branch_DTO>> GetAllAsync();

        Task<List<gen_bank_branch_entity>> GetAllPagedDataAsync(DtParameters request);

        Task<gen_bank_branch_DTO> GetSingleAsync(Int64 Id);

        Task<bool> DeleteAsync(Int64 Id);
    }
}

