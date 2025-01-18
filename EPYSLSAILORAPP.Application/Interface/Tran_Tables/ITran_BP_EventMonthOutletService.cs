using EPYSLSAILORAPP.Application.DTO;
using EPYSLSAILORAPP.Application.DTO.BusinessPlanning;
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
    public interface ITran_BP_EventMonthOutletService
    {
        Task<List<tran_bp_event_month_outlet_dto>> get_tran_bp_event_month_outletService_GetAll(Int64? tran_bp_event_monthid);

        Task<List<rpc_tran_bp_event_month_outlet>> get_tran_bp_event_month_outletService_GetByTran_event_MonthIDs(Int64 fiscal_year_id);
    }
}
