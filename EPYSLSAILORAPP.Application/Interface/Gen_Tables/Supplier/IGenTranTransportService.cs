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
using EPYSLSAILORAPP.Application.DTO.RPC;

namespace EPYSLSAILORAPP.Application.Interface
{
    public interface IGenTranTransportService
    {
        Task<bool> SaveAsync(gen_tran_transport_entity entity);

        Task<bool> UpdateAsync(gen_tran_transport_entity entity);

        Task<List<gen_tran_transport_entity>> GetAllAsync();

        Task<List<gen_tran_transport_entity>> GetAllPagedDataAsync(DtParameters request);

        Task<gen_tran_transport_entity> GetSingleAsync(Int64 Id);

        Task<bool> DeleteAsync(Int64? Id);

        Task<bool> gen_tran_transport_insert_sp(gen_tran_transport_DTO objgen_tran_transport);
        Task<bool> gen_tran_transport_update_sp(gen_tran_transport_DTO objgen_tran_transport);

        Task<List<rpc_tran_distribution_plan_DTO>> GetAllTechAsync();



    }
}

