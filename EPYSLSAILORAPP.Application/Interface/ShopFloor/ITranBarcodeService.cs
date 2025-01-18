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
using EPYSLSAILORAPP.Domain.RPC;

namespace EPYSLSAILORAPP.Application.Interface
{
    public interface ITranBarcodeService
    {
        Task<bool> SaveAsync(tran_barcode_DTO entity);

        Task<bool> UpdateAsync(tran_barcode_entity entity);

        Task<List<tran_barcode_entity>> GetAllAsync();

        Task<List<tran_barcode_entity>> GetAllPagedDataAsync(DtParameters request);

        Task<tran_barcode_entity> GetSingleAsync(Int64 Id);

        Task<bool> DeleteAsync(Int64 Id);


        Task<bool> tran_barcode_update_sp(tran_barcode_DTO objtran_barcode);
        Task<List<rpc_tran_barcode_DTO>> GetAllJoined_TranBarcodeAsync(Int64 fiscal_year_id, Int64 event_id, Int64 row_index, Int64 page_size);


    }
}

