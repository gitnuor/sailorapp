using EPYSLSAILORAPP.Application.DTO;
using EPYSLSAILORAPP.Application.DTO.GenTables;
using EPYSLSAILORAPP.Domain.DTO;
using EPYSLSAILORAPP.Domain.Entity;
using EPYSLSAILORAPP.Domain.Entity.BusinessPlanning;
using EPYSLSAILORAPP.Domain.Entity.GenTables;
using EPYSLSAILORAPP.Domain.Statics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPYSLSAILORAPP.Application.Interface
{
    public interface IGenFiscalYearService
    {

        Task<List<gen_fiscal_year_dto>> get_fiscal_year_GetAll();

        Task<List<gen_fiscal_year_dto>> GetAllAsync();

        Task<List<gen_fiscal_year>> GetAllPagedDataAsync(DtParameters param);

        Task<bool> SaveAsync(gen_fiscal_year_dto entity);
        Task<gen_fiscal_year> GetSingleAsync(Int64 Id);
        Task<bool> UpdateAsync(gen_fiscal_year entity);
        Task<bool> DeleteAsync(Int64 Id);

    }
}
