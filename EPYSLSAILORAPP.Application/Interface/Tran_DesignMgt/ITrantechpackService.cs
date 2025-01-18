using EPYSLSAILORAPP.Application.DTO;
using EPYSLSAILORAPP.Application.DTO.Tran_DesignMgt;
using EPYSLSAILORAPP.Domain.DTO;
using EPYSLSAILORAPP.Domain.Entity.Tran_DesignMgt;
using EPYSLSAILORAPP.Domain.RPC;

namespace EPYSLSAILORAPP.Application.Interface.Tran_DesignMgt
{
    public interface ITrantechpackService
    {
        Task<bool> SaveAsync(tran_tech_pack_DTO entity);

        Task<bool> UpdateAsync_Extended(tran_tech_pack_DTO entity);
		Task<List<tran_tech_pack_entity>> GetPagedData(DtParameters filter);

        Task<List<tran_tech_pack_entity>> GetPagedDataForSelect2(DtParameters param);
        Task<List<tran_tech_pack_entity>> GetPagedDataForSelectFinishProductionProcess(DtParameters param);
        Task<List<tran_tech_pack_entity>> TechPackDetailListForPPMeeting(DtParameters param);

        Task<bool> UpdateAck(long Id, long userid);

        Task<bool> UpdateAsync(tran_tech_pack_entity entity);
        Task<List<tran_tech_pack_entity>> GetAllAsync();

        Task<List<tran_tech_pack_entity>> GetTechPackListWithAvlFabric(DtParameters dt);
        Task<tran_tech_pack_entity> GetAsync(long Id);

        Task<tran_tech_pack_DTO> GetTechpackByID(long Id);

		Task<bool> tran_tech_pack_approve_reject(tran_tech_pack_DTO objtran_tech_pack);
        Task<rpc_proc_sp_get_techapack_details_for_sewing_DTO> GetAllproc_sp_get_techapack_details_for_sewingAsync(Int64? p_techpack_id);
        Task<rpc_proc_sp_get_techapack_details_for_final_inspection_DTO> GetAllproc_sp_get_techapack_details_for_final_inspection(Int64? p_techpack_id);
        List<rpc_proc_sp_get_colors_by_techpack_DTO> GetAllproc_sp_get_colors_by_techpackAsync(Int64? p_techpack_id);
        List<sizes> GetTechpackColorWiseSizeList(long p_techpack_id,string color_code);
       

        Task<tran_tech_pack_DTO> GetEmbellishmentByTechpackAsync(Int64 row_index, Int64 page_size, string query_type, Int64 receivedID, Int64 fiscal_year_id, Int64 event_id, Int64 supplier_id);

        Task<tran_tech_pack_DTO> GetAllproc_sp_get_techpack_data_for_packing_list(Int64 techpack_id);
        Task<List<rpc_proc_sp_get_data_techpack_for_distribution_DTO>> GetAllproc_sp_get_data_techpack_for_distributionAsync(Int64? p_fiscal_year_id, Int64? p_event_id);
    }
}

