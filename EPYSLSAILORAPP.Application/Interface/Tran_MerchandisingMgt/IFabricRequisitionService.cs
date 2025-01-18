using EPYSLSAILORAPP.Application.DTO;
using EPYSLSAILORAPP.Application.DTO.Tran_MerchandisingMgt;
using EPYSLSAILORAPP.Domain.DTO;
using EPYSLSAILORAPP.Domain.Entity.SupplyChain;

namespace EPYSLSAILORAPP.Application.Interface.Tran_MerchandisingMgt
{
    public interface IFabricRequisitionService
    {
        Task<bool> SaveAsync(tran_purchase_requisition_entity entity, List<file_upload> file_upload, List<tran_purchase_requisition_dtl_entity> detail);

        Task<bool> UpdateAsync(tran_purchase_requisition_entity entity, List<file_upload> file_upload,
            List<tran_purchase_requisition_dtl_entity> detail, List<string> DeleteFiles);

        Task<List<tran_purchase_requisition_DTO>> GetAllAsync();
        Task<List<gen_item_structure_group_sub_DTO>> GetAllFabricSubGroups(long group_id);

        Task<List<tran_purchase_requisition_DTO>> GetAllPagedDataAsync(DtParameters request);

        Task<tran_purchase_requisition_DTO> GetSingleAsync(long Id);
        Task<long> SaveItemMasterAsync(gen_item_master_DTO dto, long? userid);

        Task<bool> DeleteAsync(long Id);
    }
}

