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
using EPYSLSAILORAPP.Application.DTO.Tran_DesignMgt;

namespace EPYSLSAILORAPP.Application.Interface
{
    public interface IEmbellishmentDeliveryChallanService
    {
        Task<List<tran_service_work_order_DTO>> GetDeliveryChalan_PendingListAsync(Int64 row_index, Int64 page_size, Int64 workOrderId, string actionType, Int64 fiscal_year_id, Int64 event_id, long delivery_unit_id);
        Task<List<tran_cutting_batch_wise_DTO>> GetAll_batch_workOrder_wiseAsync(Int64? p_techpack_id);
        Task<List<tran_bundle_DTO>> GetAll_bundle_batch_wiseAsync(Int64? p_batch_id);
        Task<bool> tran_embellish_delivery_challan_insert_sp(tran_embellish_delivery_challan_DTO objtran_embellish_delivery_challan);
        Task<List<tran_embellish_delivery_challan_DTO>> GetTranDeliveryChallanListAsync(Int64 row_index, Int64 page_size, Int64 actiontype, Int64 fiscal_year_id, Int64 event_id, Int64 supplier_id);
        Task<tran_embellish_delivery_challan_DTO> Get_master_detail_tran_emb_delivery_challan_Async(Int64 embellish_delivery_challan_id);
        Task<bool> ApprovalProposedAsync(tran_embellish_delivery_challan_DTO entity);
        Task<bool> ApprovedAsync(tran_embellish_delivery_challan_DTO entity);
        Task<List<tran_service_work_order_DTO>> GetnDeliveryChallanReceivePendingAsync(Int64 row_index, Int64 page_size, Int64 workOrderId, string actionType, Int64 fiscal_year_id, Int64 event_id, long delivery_unit_id);
        Task<tran_service_work_order_DTO> GetnDeliveryChallanReceiveAddAsync(Int64 row_index, Int64 page_size, Int64 workOrderId, string actionType, Int64 fiscal_year_id, Int64 event_id, long delivery_unit_id);
        Task<List<tran_emb_part_dropdown>> GetAllproc_sp_get_color_wise_part_async(Int64? p_service_work_order_id, String p_color_code);
        Task<List<tran_emb_part_dropdown>> GetAllproc_sp_get_part_wise_batch_async(Int64? work_order_id, String color_code, Int64? gen_garment_part_id);
        Task<List<tran_bundle_DTO>> GetAll_bundle_batch_wiseAsync(Int64? p_batch_id, String color_code, Int64? gen_garment_part_id);

    }
}

