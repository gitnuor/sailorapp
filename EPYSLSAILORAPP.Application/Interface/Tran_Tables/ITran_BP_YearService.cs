using EPYSLSAILORAPP.Application.DTO;
using EPYSLSAILORAPP.Application.DTO.GenTables;
using EPYSLSAILORAPP.Application.DTO.TranTables;
using EPYSLSAILORAPP.Domain.Entity.BusinessPlanning;
using EPYSLSAILORAPP.Domain.Statics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPYSLSAILORAPP.Application.Interface
{
    public interface ITran_BP_YearService
    {
      
        Task<bool> Update(tran_bp_year_dto obj);

    }
}
