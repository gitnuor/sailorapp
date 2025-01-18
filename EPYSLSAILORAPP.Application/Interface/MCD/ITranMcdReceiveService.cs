using EPYSLSAILORAPP.Application.DTO;
using EPYSLSAILORAPP.Domain.DTO;
using EPYSLSAILORAPP.Domain.Entity;
using EPYSLSAILORAPP.Domain.RPC;

namespace EPYSLSAILORAPP.Application.Interface
{
    public interface ITranMcdReceiveService
    {
        Task<bool> SaveAsync(tran_mcd_receive_entity entity, List<tran_mcd_receive_detail_entity> details,List<file_upload> files);

        //Task<bool> UpdateAsync(tran_mcd_receive_entity entity);
        Task<bool> UpdateAsync(tran_mcd_receive_entity entity, List<tran_mcd_receive_detail_entity> details, List<file_upload> files);
        

		Task<List<tran_mcd_receive_DTO>> GetAllAsync();

      //  Task<List<tran_scm_po_entity_test>> GetAllPagedDataAsync(DtParameters request);

		Task<tran_mcd_receive_DTO> GetSingleAsync(Int64 Id);

        //Task<tran_scm_po_DTO> GetPO(long po_id);

        Task<bool> DeleteAsync(Int64 Id);
        Task<List<tran_scm_po_DTO>> GetDropDownData(DtParameters filter, long group);
      //  Task<List<tran_scm_po_DTO>> GetAllPagedDataForSelect2(DtParameters filter, long group);
       // Task<List<gen_warehouse_floor_DTO>> GetWarehouseFloor();
        //Task<List<gen_warehouse_floor_rack_DTO>> GetWarehouseRackName(long selectedValue);

        Task<bool> AcKnowledgeAsync(tran_mcd_receive_entity request);
        Task<List<tran_mcd_receive_transport_DTO>> GetAllTransportType();
        Task<List<gen_transaction_mode_DTO>> GetAllTransactionMode();
        Task<List<gen_delivery_status_DTO>> GetAllDeliveryStatus();

        Task<List<rpc_tran_mcd_requisition_slip_DTO>> GetAllsp_get_techPack_by_gen_item_structure_group_sub_idAsync(Int64 gen_item_master_id, Int64 gen_item_structure_group_sub_id_filter);

    }
}

